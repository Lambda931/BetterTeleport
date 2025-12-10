using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Textures.TextureWraps;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow
{
    private IDalamudTextureWrap headerBackground;// = Icons.ConvertToTextureWrap(BetterTeleport.JournalSeparatorTexture);
    private Vector4 headerTextColour = new Vector4(1f, 0.85f, 0f, 1f);

    private void DrawHeader(string header)
    {
        DrawTableSectionHeader(header, headerBackground, headerTextColour);
    }

    public static void DrawTableSectionHeader(string text, IDalamudTextureWrap bgImage, Vector4 textColor)
    {
        ImGui.TableNextRow();
        ImGui.TableSetColumnIndex(0);

        var drawList = ImGui.GetWindowDrawList();

        Vector2 start = ImGui.GetCursorScreenPos();
        float fullWidth = ImGui.GetContentRegionAvail().X;
        float lineHeight = ImGui.GetTextLineHeight();
        float paddingY = ImGui.GetStyle().FramePadding.Y;
        float totalHeight = lineHeight + paddingY * 2;

        drawList.PopClipRect();

        var tableClipMin = drawList.GetClipRectMin();
        var tableClipMax = drawList.GetClipRectMax();
        drawList.PushClipRect(tableClipMin, tableClipMax, true);

        //drawList.AddImage(bgImage.Handle, new Vector2(0, 0), new Vector2(0, 0));

        /*drawList.AddRectFilled(
            start,
            new Vector2(start.X + fullWidth, start.Y + totalHeight),
            ImGui.GetColorU32(bgColor)
        );*/

        drawList.AddText(
            new Vector2(start.X + 6, start.Y + paddingY),
            ImGui.GetColorU32(textColor),
            text
        );
        drawList.PopClipRect();

        var cellMin = ImGui.GetCursorScreenPos();
        var cellMax = new Vector2(cellMin.X + fullWidth, cellMin.Y + totalHeight);
        drawList.PushClipRect(cellMin, cellMax, true);

        ImGui.Dummy(new Vector2(fullWidth, totalHeight + 2));
    }
}
