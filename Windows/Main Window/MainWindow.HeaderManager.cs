using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Textures.TextureWraps;
using System.Linq;
using System.Numerics;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow
{
    private IDalamudTextureWrap headerBackground;// = Icons.ConvertToTextureWrap(BetterTeleport.JournalSeparatorTexture);
    private Vector4 headerTextColour = new Vector4(1f, 0.85f, 0f, 1f);


    private void DrawTopHeader()
    {
        switch (currentTab)
        {
            case Tab.All:
                DrawTableSectionHeader("Residential Areas", headerBackground, headerTextColour);
                break;
            case Tab.Residential:
                DrawTableSectionHeader("Residential Areas", headerBackground, headerTextColour);
                break;
            case Tab.LaNoscea:
                DrawTableSectionHeader("La Noscea", headerBackground, headerTextColour);
                break;
            case Tab.BlackShroud:
                DrawTableSectionHeader("The Black Shroud", headerBackground, headerTextColour);
                break;
            case Tab.Thanalan:
                DrawTableSectionHeader("Thanalan", headerBackground, headerTextColour);
                break;
            case Tab.Ishgard:
                DrawTableSectionHeader("Coerthas", headerBackground, headerTextColour);
                break;
            case Tab.GyrAbania:
                DrawTableSectionHeader("Gyr Abania", headerBackground, headerTextColour);
                break;
            case Tab.FarEast:
                DrawTableSectionHeader("Hingashi", headerBackground, headerTextColour);
                break;
            case Tab.IndependentNations:
                DrawTableSectionHeader("Mor Dhona", headerBackground, headerTextColour);
                break;
            case Tab.Ilsabard:
                DrawTableSectionHeader("Ilsabard", headerBackground, headerTextColour);
                break;
            case Tab.Tural:
                DrawTableSectionHeader("Yok Tural", headerBackground, headerTextColour);
                break;
            case Tab.Norvrandt:
                DrawTableSectionHeader("Norvrandt", headerBackground, headerTextColour);
                break;
            case Tab.BeyondTheSource:
                DrawTableSectionHeader("The Sea of Stars", headerBackground, headerTextColour);
                break;
            case Tab.MarketBoards:
                DrawTableSectionHeader("Market Boards", headerBackground, headerTextColour);
                break;
            case Tab.SummoningBells:
                DrawTableSectionHeader("Summoning Bells", headerBackground, headerTextColour);
                break;
            case Tab.AlliedSocieties:
                DrawTableSectionHeader("A Realm Reborn", headerBackground, headerTextColour);
                break;
            case Tab.CustomDeliveries:
                DrawTableSectionHeader("Custom Deliveries", headerBackground, headerTextColour);
                break;
            case Tab.DeepDungeons:
                DrawTableSectionHeader("Deep Dungeons", headerBackground, headerTextColour);
                break;
            case Tab.RestorationContent:
                DrawTableSectionHeader("Restoration Content", headerBackground, headerTextColour);
                break;
            case Tab.FieldOperations:
                DrawTableSectionHeader("Field Operations", headerBackground, headerTextColour);
                break;
        }
    }

    private void DrawHeaderInNextRow(uint id)
    {
        LocationManager.GetCurrentResidentialIDs();
        var lastResidentialID = LocationManager.currentResidentialIDs.Last();
        if(currentTab == Tab.All && id == lastResidentialID)
        {
            DrawTableSectionHeader("La Noscea", headerBackground, headerTextColour);
        }

        switch (currentTab)
        {
            case Tab.All:
                switch (id)
                {
                    case 55:
                        DrawTableSectionHeader("The Black Shroud", headerBackground, headerTextColour);
                        break;
                    case 7:
                        DrawTableSectionHeader("Thanalan", headerBackground, headerTextColour);
                        break;
                    case 62:
                        DrawTableSectionHeader("Coerthas", headerBackground, headerTextColour);
                        break;
                    case 71:
                        DrawTableSectionHeader("Abalathia's Spine", headerBackground, headerTextColour);
                        break;
                    case 74:
                        DrawTableSectionHeader("Dravania", headerBackground, headerTextColour);
                        break;
                    case 79:
                        DrawTableSectionHeader("Gyr Abania", headerBackground, headerTextColour);
                        break;
                    case 103:
                        DrawTableSectionHeader("Hingashi", headerBackground, headerTextColour);
                        break;
                    case 111:
                        DrawTableSectionHeader("Othard", headerBackground, headerTextColour);
                        break;
                    case 212:
                        DrawTableSectionHeader("Mor Dhona", headerBackground, headerTextColour);
                        break;
                    case 24:
                        DrawTableSectionHeader("The Northern Empty", headerBackground, headerTextColour);
                        break;
                    case 127:
                        DrawTableSectionHeader("Ilsabard", headerBackground, headerTextColour);
                        break;
                    case 173:
                        DrawTableSectionHeader("Yok Tural", headerBackground, headerTextColour);
                        break;
                    case 206:
                        DrawTableSectionHeader("Xak Tural", headerBackground, headerTextColour);
                        break;
                    case 168:
                        DrawTableSectionHeader("Norvrandt", headerBackground, headerTextColour);
                        break;
                    case 148:
                        DrawTableSectionHeader("The Sea of Stars", headerBackground, headerTextColour);
                        break;
                    case 181:
                        DrawTableSectionHeader("The World Unsundered", headerBackground, headerTextColour);
                        break;
                    case 178:
                        DrawTableSectionHeader("Unlost World", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.Ishgard:
                switch (id)
                {
                    case 71:
                        DrawTableSectionHeader("Abalathia's Spine", headerBackground, headerTextColour);
                        break;
                    case 74:
                        DrawTableSectionHeader("Dravania", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.FarEast:
                switch (id)
                {
                    case 111:
                        DrawTableSectionHeader("Othard", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.IndependentNations:
                switch (id)
                {
                    case 24:
                        DrawTableSectionHeader("The Northern Empty", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.Tural:
                switch (id)
                {
                    case 206:
                        DrawTableSectionHeader("Xak Tural", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.BeyondTheSource:
                switch (id)
                {
                    case 181:
                        DrawTableSectionHeader("The World Unsundered", headerBackground, headerTextColour);
                        break;
                    case 178:
                        DrawTableSectionHeader("Unlost World", headerBackground, headerTextColour);
                        break;
                }
                break;
            case Tab.AlliedSocieties:
                switch (id)
                {
                    case 7:
                        DrawTableSectionHeader("Heavensward", headerBackground, headerTextColour);
                        break;
                    case 79:
                        DrawTableSectionHeader("Stormblood", headerBackground, headerTextColour);
                        break;
                    case 128:
                        DrawTableSectionHeader("Shadowbringers", headerBackground, headerTextColour);
                        break;
                    case 136:
                        DrawTableSectionHeader("Endwalker", headerBackground, headerTextColour);
                        break;
                    case 181:
                        DrawTableSectionHeader("Dawntrail", headerBackground, headerTextColour );
                        break;
                }
                break;
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
        
