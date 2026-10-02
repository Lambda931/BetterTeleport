using Dalamud.Bindings.ImGui;
using Dalamud.Interface.ManagedFontAtlas;
using Dalamud.Interface.Windowing;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow
{
    private static readonly Vector2 ReferenceWindowSize = new(790f, 730f);
    public IFontHandle? TableFont { get; private set; }
    public IFontHandle? GilFont { get; private set; }
    private float tableFontScale = 20f;
    private float gilFontScale = 25f;

    private float GetBaseScale()
    {
        return ImGui.GetIO().DisplaySize.Y / 1440f;
    }
    private float MinScale(float baseValue)
    {
        float minScaleValue = baseValue * 0.8f;
        return minScaleValue;
    }

    private float ResolutionScaling(float value)
    {
        float scale = GetBaseScale();
        float scaledValue = value * scale;
        if (scaledValue < MinScale(value))
            scaledValue = MinScale(value);
        return scaledValue;
    }

    private Vector2 GetScaledWindowSize()
    {
        float scale = GetBaseScale();
        Vector2 windowSize = ReferenceWindowSize * scale;
        if (windowSize.X < MinScale(ReferenceWindowSize.X))
            windowSize.X = MinScale(ReferenceWindowSize.X);
        if (windowSize.Y < MinScale(ReferenceWindowSize.Y))
            windowSize.Y = MinScale(ReferenceWindowSize.Y);
        return windowSize;
    }

    private void SetScaledWindowSize()
    {
        Vector2 scaledSize = GetScaledWindowSize();
        SizeConstraints = new WindowSizeConstraints
        {
            MinimumSize = scaledSize,
            MaximumSize = scaledSize
        };
    }

    private void CreateTableFont()
    {
        var fontAtlas = BetterTeleport.PluginInterface.UiBuilder
            .CreateFontAtlas(FontAtlasAutoRebuildMode.OnNewFrame);

        TableFont = fontAtlas.NewDelegateFontHandle(
            e => e.OnPreBuild(
                tk => tk.AddDalamudDefaultFont(tableFontScale)));
    }
    private void CreateGilFont()
    {
        var fontAtlas = BetterTeleport.PluginInterface.UiBuilder
            .CreateFontAtlas(FontAtlasAutoRebuildMode.OnNewFrame);

        GilFont = fontAtlas.NewDelegateFontHandle(
            e => e.OnPreBuild(
                tk => tk.AddDalamudDefaultFont(gilFontScale)));
    }

    private float GetFontScale()
    {
        float fontScale = GetBaseScale();
        if (fontScale < 0.8f)
            fontScale = 0.8f;
        return fontScale;
    }

    private void DrawRowAlignedText(string text, float rowHeight)
    {
        if (TableFont == null)
            return;

        using (TableFont.Push())
        {
            float textHeight = ImGui.GetTextLineHeight();
            float paddingY = ImGui.GetStyle().CellPadding.Y;

            float availableHeight = (rowHeight - (paddingY * 2)) / 1.4f;
            float offsetY = (availableHeight - textHeight) * 0.5f;

            ImGui.SetCursorPosY(ImGui.GetCursorPosY() + offsetY);
            ImGui.Text(text);
        }
    }

    private void DrawGilText()
    {
        if (GilFont == null)
            return;
        string playerGil = TeleportManager.GetPlayerGil().ToString("N0");
        float gilTextWidth = ImGui.CalcTextSize(playerGil).X;
        ImGui.SameLine(ImGui.GetWindowContentRegionMax().X - (gilTextWidth + ResolutionScaling(75)));
        using (GilFont.Push())
        {
            ApplyOffset(ResolutionScaling(-7.5f), ResolutionScaling(-3f));
            ImGui.Text(playerGil);
        }
    }
}

