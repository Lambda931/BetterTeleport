using BetterTeleportPlugin;
using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Windowing;
using System;
using System.Numerics;
using static BetterTeleportPlugin.WindowColourManager;
using static BetterTeleportPlugin.Windows.MainWindow;

namespace BetterTeleportPlugin.Windows;

public class ConfigWindow : Window, IDisposable
{
    
    private readonly Configuration configuration;
    private int colourChannels;

    // We give this window a constant ID using ###.
    // This allows for labels to be dynamic, like "{FPS Counter}fps###XYZ counter window",
    // and the window ID will always be "###XYZ counter window" for ImGui
    public ConfigWindow(BetterTeleportPlugin.BetterTeleport plugin) : base("Settings")
    {
        Flags = ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar |
                ImGuiWindowFlags.NoScrollWithMouse;

        Size = new Vector2(275, 95);
        SizeCondition = ImGuiCond.Always;

        configuration = plugin.Configuration;
    }

    public void Dispose() { }

    public override void PreDraw()
    {
        WindowColourManager.ColourData.TryGetValue(configuration.ColourProfile, out var colourData);
        if (colourData != null)
        {
            ImGui.PushStyleColor(ImGuiCol.WindowBg, colourData.windowBackground);
            ImGui.PushStyleColor(ImGuiCol.TitleBg, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.TitleBgActive, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.TitleBgCollapsed, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.TableHeaderBg, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.Header, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.HeaderHovered, colourData.windowTitles);
            ImGui.PushStyleColor(ImGuiCol.HeaderActive, colourData.windowTitles);
        }
        colourChannels = 8;

        // Flags must be added or removed before Draw() is being called, or they won't apply
        if (configuration.IsConfigWindowMovable)
        {
            Flags &= ~ImGuiWindowFlags.NoMove;
        }
        else
        {
            Flags |= ImGuiWindowFlags.NoMove;
        }
    }

    public override void Draw()
    {
        ImGui.Text("Themes");
        if (ImGui.Button("Default", new Vector2(100, 31))) { configuration.ColourProfile = Colours.DalamudDefault; configuration.Save(); } ImGui.SameLine();
        if (ImGui.Button("Clear Blue", new Vector2(100, 31))) { configuration.ColourProfile = Colours.ClearBlue; configuration.Save(); } ImGui.SameLine();
        if (ImGui.Button("Clear Purple", new Vector2(100, 31))) { configuration.ColourProfile = Colours.ClearPurple; configuration.Save(); } ImGui.SameLine();
    }

    public override void PostDraw()
    {
        ImGui.PopStyleColor(colourChannels);
    }
}
