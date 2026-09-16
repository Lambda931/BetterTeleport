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
        //Main Tabs
        All, Residential, LaNoscea, BlackShroud, Thanalan, Ishgard, GyrAbania, FarEast, IndependentNations, Ilsabard, Tural,
        Norvrandt, BeyondTheSource, Favourites, Debug,

        //Dropdown Tabs
        MarketBoards, SummoningBells, AlliedSocieties, CustomDeliveries, DeepDungeons,
        RestorationContent, FieldOperations, LimitedJobs
    };

    private static readonly Dictionary<string, Tab> DropdownTabs = new()
    {
        ["Market Boards"] = Tab.MarketBoards,
        ["Summoning Bells"] = Tab.SummoningBells,
        ["Allied Societies"] = Tab.AlliedSocieties,
        ["Custom Deliveries"] = Tab.CustomDeliveries,
        ["Deep Dungeons"] = Tab.DeepDungeons,
        ["Restoration Content"] = Tab.RestorationContent,
        ["Field Operations"] = Tab.FieldOperations,
        ["Limited Jobs"] = Tab.LimitedJobs
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
        { Tab.Favourites, LocationManager.TabLocation.Favourites },
        { Tab.Debug, LocationManager.TabLocation.All },

        { Tab.MarketBoards, LocationManager.TabLocation.MarketBoards },
        { Tab.SummoningBells, LocationManager.TabLocation.SummoningBells},
        { Tab.AlliedSocieties, LocationManager.TabLocation.AlliedSocieties },
        { Tab.CustomDeliveries, LocationManager.TabLocation.CustomDeliveries },
        { Tab.DeepDungeons, LocationManager.TabLocation.DeepDungeons },
        { Tab.RestorationContent, LocationManager.TabLocation.RestorationContent },
        { Tab.FieldOperations, LocationManager.TabLocation.FieldOperations },
        { Tab.LimitedJobs, LocationManager.TabLocation.LimitedJobs },


    };

    private void GetTabData(Tab tab)
    {
        if (TabKeys.TryGetValue(tab, out var key) &&
        LocationManager.SubCategories.TryGetValue(key, out var tabData))
        {
            currentTabData = tabData;
        }
    }

    private void SetDropdownTab(string item)
    {
        if (DropdownTabs.TryGetValue(item, out var tabData))
        {
            currentTab = tabData;
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
            Tab.LimitedJobs => entry.ContentCategory == ContentInfo.Categories.LimitedJobs,
            _ => true
        };
    }
    private bool IsDropdownEntryUnlocked(Tab tab, uint id)
    {
        return tab switch
        {
            Tab.AlliedSocieties => ContentManager.HasAetheryteUnlockedAlliedSocietyContent(id),
            _ => true
        };
    }

    private bool DoesTabHaveValidRows(string dropdownItem)
    {
        DropdownTabs.TryGetValue(dropdownItem, out var tab);
        GetTabData(tab);
        foreach (var category in currentTabData)
        {
            foreach (var id in category.ids)
            {
                if (!TeleportManager.IsAttuned(id) || !IsDropdownEntryUnlocked(tab, id))
                    continue;
                return true;
            }
        }
        return false;
    }
    private IconProperties GetIconProperties(IDalamudTextureWrap textureSheet, ULDLibraryData iconData)
    {
        var iconProperties = Icons.ULDSprite(textureSheet, iconData.X, iconData.Y, iconData.Width, iconData.Height);
        return iconProperties;
    }
}
public class TabData()
{
    private static readonly IconLibrary Icons = IconData.IconLibrary;
    public MainWindow.Tab Tab { get; set; }
    public uint[] LocationIDs { get; set; }
}
