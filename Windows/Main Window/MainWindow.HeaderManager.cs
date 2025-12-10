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

    private void DrawTopHeader()
    {
        var header = currentTab switch
        {
            Tab.All => "Residential Areas",
            Tab.Residential => "Residential Areas",
            Tab.LaNoscea => "La Noscea",
            Tab.BlackShroud => "The Black Shroud",
            Tab.Thanalan => "Thanalan",
            Tab.Ishgard => "Coerthas",
            Tab.GyrAbania => "Gyr Abania",
            Tab.FarEast => "Hingashi",
            Tab.IndependentNations => "Mor Dhona",
            Tab.Ilsabard => "Ilsabard",
            Tab.Tural => "Yok Tural",
            Tab.Norvrandt => "Norvrandt",
            Tab.BeyondTheSource => "The Sea of Stars",
            Tab.MarketBoards => "Market Boards",
            Tab.SummoningBells => "Summoning Bells",
            Tab.AlliedSocieties => "A Realm Reborn",
            Tab.CustomDeliveries => "Custom Deliveries",
            Tab.DeepDungeons => "Deep Dungeons",
            Tab.RestorationContent => "Restoration Content",
            Tab.FieldOperations => "Field Operations",
            _ => "Unknown"
        };

        DrawTableSectionHeader(header, headerBackground, headerTextColour);
    }

    private static readonly Dictionary<Tab, Dictionary<uint, string>> HeaderMap = new()
    {
        [Tab.All] = new()
        {
            { 55, "The Black Shroud" }, { 7, "Thanalan" }, { 62, "Coerthas" }, { 71, "Abalathia's Spine" },
            { 74, "Dravania" }, { 79, "Gyr Abania" }, { 103, "Hingashi" }, { 111, "Othard" }, { 212, "Mor Dhona" },
            { 24, "The Northern Empty" }, { 127, "Ilsabard" }, { 173, "Yok Tural" }, { 206, "Xak Tural" },
            { 168, "Norvrandt" }, { 148, "The Sea of Stars" }, { 181, "The World Unsundered" }, { 178, "Unlost World" },
        },

        [Tab.Ishgard] = new()
        {
            { 71, "Abalathia's Spine" }, { 74, "Dravania" }
        },

        [Tab.FarEast] = new()
        {
            { 111, "Othard" }
        },

        [Tab.IndependentNations] = new()
        {
            { 24, "The Northern Empty" }
        },

        [Tab.Tural] = new()
        {
            { 206, "Xak Tural" }
        },

        [Tab.BeyondTheSource] = new()
        {
            { 181, "The World Unsundered" }, { 178, "Unlost World" }
        },

        [Tab.AlliedSocieties] = new()
        {
            { 7, "Heavensward" }, { 79, "Stormblood" }, { 128, "Shadowbringers" }, { 136, "Endwalker" }, { 181, "Dawntrail" }
        }
    };

    private void DrawHeaderInNextRow(uint id)
    {
        LocationManager.GetCurrentResidentialIDs();
        var lastResidentialID = LocationManager.currentResidentialIDs.Last();

        if (currentTab == Tab.All && id == lastResidentialID)
            DrawTableSectionHeader("La Noscea", headerBackground, headerTextColour);

        if (HeaderMap.TryGetValue(currentTab, out var idMap) &&
            idMap.TryGetValue(id, out var header))
        {
            DrawTableSectionHeader(header, headerBackground, headerTextColour);
        }
    }

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
