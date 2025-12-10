using Dalamud.Bindings.ImGui;
using Dalamud.Interface.Textures.TextureWraps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow
{
    public enum Tab
    {
        All, Residential, LaNoscea, BlackShroud, Thanalan, Ishgard, GyrAbania, FarEast, IndependentNations, Ilsabard, Tural,
        Norvrandt, BeyondTheSource, Favourites, MarketBoards, SummoningBells, AlliedSocieties, CustomDeliveries, DeepDungeons,
        RestorationContent, FieldOperations
    };

    private static readonly Dictionary<string, TabData> DropdownTabs = new()
    {
        ["Market Boards"] = new TabData {Tab = Tab.MarketBoards, LocationIDs = LocationManager.locationIDs.MarketBoardIDs},
        ["Summoning Bells"] = new TabData {Tab = Tab.SummoningBells, LocationIDs = LocationManager.locationIDs.SummoningBellIDs},
        ["Allied Societies"] = new TabData {Tab = Tab.AlliedSocieties, LocationIDs = LocationManager.locationIDs.AlliedSocietyIDs},
        ["Custom Deliveries"] = new TabData {Tab = Tab.CustomDeliveries, LocationIDs = LocationManager.locationIDs.CustomDeliveriesIDs},
        ["Deep Dungeons"] = new TabData { Tab = Tab.DeepDungeons, LocationIDs = LocationManager.locationIDs.DeepDungeonIDs },
        ["Restoration Content"] = new TabData { Tab = Tab.RestorationContent, LocationIDs = LocationManager.locationIDs.RestorationContentIDs },
        ["Field Operations"] = new TabData { Tab = Tab.FieldOperations, LocationIDs = LocationManager.locationIDs.FieldOperationIDs }
    };
    public static Tab currentTab = Tab.All;

    private List<SubCategories> currentTabData;

    private static readonly Dictionary<Tab, LocationManager.TabLocation> TabKeys = new()
    {
        { Tab.All, LocationManager.TabLocation.All },
        { Tab.LaNoscea, LocationManager.TabLocation.LaNoscea },
        { Tab.BlackShroud, LocationManager.TabLocation.BlackShroud },
        { Tab.Thanalan, LocationManager.TabLocation.Thanalan },
        { Tab.Ishgard, LocationManager.TabLocation.Ishgard },
        { Tab.GyrAbania, LocationManager.TabLocation.GyrAbania },
        { Tab.FarEast, LocationManager.TabLocation.FarEast },
        { Tab.IndependentNations, LocationManager.TabLocation.IndependentNations },
        { Tab.Ilsabard, LocationManager.TabLocation.Ilsabard },
        { Tab.Tural, LocationManager.TabLocation.Tural },
        { Tab.Norvrandt, LocationManager.TabLocation.Norvrandt },
        { Tab.BeyondTheSource, LocationManager.TabLocation.BeyondTheSource },
        { Tab.Favourites, LocationManager.TabLocation.Favourites }
    };

    private void GetTabData()
    {
        if (TabKeys.TryGetValue(currentTab, out var key) &&
        LocationManager.SubCategories.TryGetValue(key, out var tabData))
        {
            currentTabData = tabData;
        }
    }

    private void SetDropdownEnum(string item)
    {
        if (DropdownTabs.TryGetValue(item, out var tabData))
        {
            currentTab = tabData.Tab;
        }
    }
    private void DropdownSelection(string item)
    {
        if (DropdownTabs.TryGetValue(item, out var tabData))
        {
            //locationIDs = tabData.LocationIDs;
        }
    }

    private void CreateTabButton(Tab selectedTab, IDalamudTextureWrap textureSheet, ULDLibraryData iconData, string tooltip)
    {
        if (TabKeys.TryGetValue(selectedTab, out var key) &&
        LocationManager.SubCategories.TryGetValue(key, out var tabData))
        {
            var validTab = false;
            foreach (var subCategory in tabData)
            {
                foreach (var id in subCategory.ids)
                {
                    if (TeleportManager.IsAttuned(id))
                    {
                        {
                            validTab = true;
                        }
                    }
                }
            }
            if (validTab)
            {
                var iconProperties = GetIconProperties(textureSheet, iconData);
                if (textureSheet != null)
                {
                    ImGui.PushID($"##{selectedTab}");
                    if (ImGui.ImageButton(textureSheet.Handle, new Vector2(iconProperties.Width, iconProperties.Height), new Vector2(iconProperties.U0, iconProperties.V0), new Vector2(iconProperties.U1, iconProperties.V1)))
                    {
                        currentTab = selectedTab;
                        currentContentDropdownItem = "";
                        resetScrollbar = true;
                    }
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text(tooltip);
                        ImGui.EndTooltip();
                    }
                }
            }
        }
    }

    private bool FilterContentByCategory(ContentInfo entry)
    {
        return currentTab switch
        {
            Tab.MarketBoards => entry.ContentCategory == ContentInfo.Categories.MarketBoard,
            Tab.SummoningBells => entry.ContentCategory == ContentInfo.Categories.SummoningBell,
            Tab.AlliedSocieties => entry.ContentCategory == ContentInfo.Categories.AlliedSocieties,
            Tab.CustomDeliveries => entry.ContentCategory == ContentInfo.Categories.CustomDeliveries,
            Tab.DeepDungeons => entry.ContentCategory == ContentInfo.Categories.DeepDungeons,
            Tab.RestorationContent => entry.ContentCategory == ContentInfo.Categories.RestorationContent,
            Tab.FieldOperations => entry.ContentCategory == ContentInfo.Categories.FieldOperations,
            _ => true
        };
    }
}
public class TabData()
{
    private static readonly IconLibrary Icons = IconData.IconLibrary;
    public MainWindow.Tab Tab { get; set; }
    public uint[] LocationIDs { get; set; }
}
