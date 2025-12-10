using System;
using System.Collections.Generic;
using System.Linq;
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
            locationIDs = tabData.LocationIDs;
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
