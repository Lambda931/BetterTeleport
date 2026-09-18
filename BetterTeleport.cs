using BetterTeleportPlugin.Windows;
using Dalamud.Game.Addon.Lifecycle;
using Dalamud.Game.Addon.Lifecycle.AddonArgTypes;
using Dalamud.Game.Command;
using Dalamud.Interface.Textures;
using Dalamud.Interface.Windowing;
using Dalamud.IoC;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Component.GUI;
using System;

namespace BetterTeleportPlugin;

public sealed class BetterTeleport : IDalamudPlugin
{
    [PluginService] internal static IDalamudPluginInterface PluginInterface { get; private set; } = null!;
    [PluginService] internal static ITextureProvider TextureProvider { get; private set; } = null!;
    [PluginService] internal static ICommandManager CommandManager { get; private set; } = null!;
    [PluginService] internal static IClientState ClientState { get; private set; } = null!;
    [PluginService] internal static IDataManager DataManager { get; private set; } = null!;
    [PluginService] internal static IPluginLog Log { get; private set; } = null!;
    [PluginService] internal static IFramework Framework { get; private set; } = null!;
    [PluginService] internal static IGameGui GameGui { get; private set; } = null!;
    [PluginService] internal static IAddonLifecycle AddonLifecycle { get; private set; } = null!;
    [PluginService] internal static IObjectTable ObjectTable { get; private set; } = null!;

    public static bool Debug = true;

    private const string TeleportMenuCommand = "/betterteleport";

    public Configuration Configuration { get; init; }

    public readonly WindowSystem WindowSystem = new("BetterTeleport");
    private ConfigWindow ConfigWindow { get; init; }
    private MainWindow MainWindow { get; init; }

    public static ISharedImmediateTexture? TeleportTexture;
    public static ISharedImmediateTexture? JournalSeparatorTexture;

    private bool texturesInitialized = false;
    public static bool teleportWindowOpen = false;

    public BetterTeleport()
    {
        CommandManager.AddHandler(TeleportMenuCommand, new CommandInfo(TeleportMenuToggle)
        {
            HelpMessage = "Opens the teleport window"
        });

        Configuration = PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();

        ConfigWindow = new ConfigWindow(this);
        MainWindow = new MainWindow(this);



        WindowSystem.AddWindow(ConfigWindow);
        WindowSystem.AddWindow(MainWindow);

        PluginInterface.UiBuilder.Draw += WindowSystem.Draw; 
        PluginInterface.UiBuilder.OpenConfigUi += ToggleConfigUi;
        PluginInterface.UiBuilder.OpenMainUi += ToggleMainUi;

        Log.Debug("Register Listener");
        AddonLifecycle.RegisterListener(AddonEvent.PreSetup, "Teleport", ToggleTeleportWindow);
        Framework.Update += Framework_Update;
        ClientState.Logout += OnLogout;
    }

    private void Framework_Update(IFramework _)
    {
        if (texturesInitialized)
            return;
        InitializeTextures();
        texturesInitialized = true;
        Framework.Update -= Framework_Update;
    }

    private void TeleportMenuToggle(string command, string args)
    {
        MainWindow.Toggle();
    }

    private unsafe void ToggleTeleportWindow(AddonEvent type, AddonArgs args)
    {
        if (args.Addon == nint.Zero)
            return;

        var atk = (AtkUnitBase*)((IntPtr)args.Addon).ToPointer();
        if (atk == null)
            return;

        atk->Close(true);
        teleportWindowOpen = !teleportWindowOpen;
        MainWindow.IsOpen = teleportWindowOpen;
    }

    public void Dispose()
    {
        PluginInterface.UiBuilder.Draw -= WindowSystem.Draw;
        PluginInterface.UiBuilder.OpenConfigUi -= ToggleConfigUi;
        PluginInterface.UiBuilder.OpenMainUi -= ToggleMainUi;
        ClientState.Logout -= OnLogout;

        WindowSystem.RemoveAllWindows();

        ConfigWindow.Dispose();
        MainWindow.Dispose();

        CommandManager.RemoveHandler(TeleportMenuCommand);
        AddonLifecycle.UnregisterListener(AddonEvent.PostDraw, "Teleport", ToggleTeleportWindow);
        
    }

    public void ToggleConfigUi() => ConfigWindow.Toggle();
    public void ToggleMainUi() => MainWindow.Toggle();

    public static void InitializeTextures()
    {
        try
        {
            Log.Debug("Attempting to load Teleport.tex...");

            var teleportWrap = TextureProvider.GetFromGame("ui/uld/teleport.tex");
            var journalSeparatorWrap = TextureProvider.GetFromGame("ui/uld/journal_separator.tex");

            if (teleportWrap == null)
            {
                Log.Error("Teleport.tex failed to load (shared texture was null).");
                return;

            }

            TeleportTexture = teleportWrap;
            JournalSeparatorTexture = journalSeparatorWrap;

            Log.Debug("Teleport texture initialized successfully.");
        }
        catch (Exception ex)
        {
            Log.Error(ex, "Exception while initializing Teleport texture.");
        }
    }

    public void AutoCloseConfigWindow()
    {
        if (ConfigWindow.IsOpen == true)
        {
            ConfigWindow.IsOpen = false;
        }
    }

    private void OnLogout(int type, int code)
    {
        teleportWindowOpen = false;
        MainWindow.IsOpen = false;
        ConfigWindow.IsOpen = false;
    }
}
