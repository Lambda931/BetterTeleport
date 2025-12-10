using Dalamud.Bindings.ImGui;
using Dalamud.Game.Text;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow : Window, IDisposable
{
    private readonly BetterTeleport plugin;
    private uint[] locationIDs = LocationManager.locationIDs.AllIDs;
    public static Icons? Icons;
    public enum Tab { 
        All, Residential, LaNoscea, BlackShroud, Thanalan, Ishgard, GyrAbania, FarEast, IndependentNations, Ilsabard, Tural, 
        Norvrandt, BeyondTheSource, Favourites, MarketBoards, SummoningBells, AlliedSocieties, CustomDeliveries, DeepDungeons, 
        RestorationContent, FieldOperations 
    };
    public static Tab currentTab = Tab.All;

    string currentContentDropdownItem = "";
    string[] contentDropdownitems = { "Market Boards", "Summoning Bells", "Allied Societies", "Custom Deliveries",
                                        "Deep Dungeons", "Restoration Content", "Field Operations" };
    
    private bool resetScrollbar;

    public MainWindow(BetterTeleport plugin) : base("Teleport Menu", ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse)
    {
        SizeConstraints = new WindowSizeConstraints
        {

            MinimumSize = new Vector2(700, 600),
            MaximumSize = new Vector2(700, 600)
        };

        this.plugin = plugin;
    }

    public void Dispose() { }


    public override void Draw()
    {
        try
        {
            var TextureSheet = Icons.ConvertToTextureWrap(BetterTeleport.TeleportTexture);

            //Draw Tabs
            if (ImGui.Button("All", new Vector2(62, 31)))
            {
                currentTab = Tab.All;
                locationIDs = LocationManager.locationIDs.AllIDs;
                currentContentDropdownItem = "";
                resetScrollbar = true;
            }
            ImGui.SameLine();

            if (TextureSheet != null)
            {
                var ResidentialTexture = Icons.GetTextureFromIconID(IconData.IconLibrary.HousingTabIcon);
                if (ResidentialTexture.TryGetWrap(out var ResidentialIcon, out Exception? ResidentialException))
                {
                    if (ImGui.ImageButton(ResidentialIcon.Handle, new Vector2(28, 26)))
                    {
                        currentTab = Tab.Residential;
                    }
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text("Residential Areas");
                        ImGui.EndTooltip();
                    }
                }

                ImGui.SameLine();
                CreateTabButton(Tab.LaNoscea, TextureSheet, IconData.ULDLibrary.LaNosceaTabIcon, LocationManager.locationIDs.LaNosceaIDs, "La Noscea"); ImGui.SameLine();
                CreateTabButton(Tab.BlackShroud, TextureSheet, IconData.ULDLibrary.BlackShroudTabIcon, LocationManager.locationIDs.BlackShroudIDs, "The Black Shroud"); ImGui.SameLine();
                CreateTabButton(Tab.Thanalan, TextureSheet, IconData.ULDLibrary.ThanalanTabIcon, LocationManager.locationIDs.ThanalanIDs, "Thanalan"); ImGui.SameLine();
                CreateTabButton(Tab.Ishgard, TextureSheet, IconData.ULDLibrary.IshgardTabIcon, LocationManager.locationIDs.IshgardIDs, "Ishgard and Surrounding Areas"); ImGui.SameLine();
                CreateTabButton(Tab.GyrAbania, TextureSheet, IconData.ULDLibrary.GyrAbaniaTabIcon, LocationManager.locationIDs.GyrAbaniaIDs, "Gyr Abania"); ImGui.SameLine();
                CreateTabButton(Tab.FarEast, TextureSheet, IconData.ULDLibrary.FarEastTabIcon, LocationManager.locationIDs.FarEastIDs, "The Far East"); ImGui.SameLine();
                CreateTabButton(Tab.IndependentNations, TextureSheet, IconData.ULDLibrary.IndependentNationsTabIcon, LocationManager.locationIDs.IndependentNationsIDs, "Independent Nations"); ImGui.SameLine();
                CreateTabButton(Tab.Ilsabard, TextureSheet, IconData.ULDLibrary.IlsabardTabIcon, LocationManager.locationIDs.IlsabardIDs, "Ilsabard"); ImGui.SameLine();
                CreateTabButton(Tab.Tural, TextureSheet, IconData.ULDLibrary.TuralTabIcon, LocationManager.locationIDs.TuralIDs, "Tural"); ImGui.SameLine();
                CreateTabButton(Tab.Norvrandt, TextureSheet, IconData.ULDLibrary.NorvrandtTabIcon, LocationManager.locationIDs.NorvrandtIDs, "Norvrandt"); ImGui.SameLine();
                CreateTabButton(Tab.BeyondTheSource, TextureSheet, IconData.ULDLibrary.BeyondTheSourceTabIcon, LocationManager.locationIDs.BeyondTheSourceIDs, "Beyond the Source"); ImGui.SameLine();

                var FavouritesIconData = GetIconProperties(TextureSheet, IconData.ULDLibrary.FavouritesTabIcon);
                ImGui.PushID($"{Tab.Favourites}");
                if (ImGui.ImageButton(TextureSheet.Handle, new Vector2(FavouritesIconData.Width, FavouritesIconData.Height), new Vector2(FavouritesIconData.U0, FavouritesIconData.V0), new Vector2(FavouritesIconData.U1, FavouritesIconData.V1)))
                {
                    currentTab = Tab.Favourites;
                    locationIDs = GetFavouriteLocations();
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.Text("Favorites");
                    ImGui.EndTooltip();
                }
            }

            ImGui.SameLine();

            //Draw Gil
            string playerGil = TeleportManager.GetPlayerGil().ToString("N0");
            float gilTextWidth = ImGui.CalcTextSize(playerGil).X;
            ImGui.SameLine(ImGui.GetWindowContentRegionMax().X - (gilTextWidth + 42));
            ImGui.Text(playerGil);
            ImGui.SameLine();
            var GilIcon = Icons.GetTextureFromIconID(IconData.IconLibrary.GilIcon);
            if (GilIcon.TryGetWrap(out var wrap, out Exception? exception))
            {
                ApplyOffset(-5, 0);
                ImGui.Image(wrap.Handle, new Vector2(32, 32));
            }
            ImGui.Spacing();

            //Draw Content Dropdown
            ImGui.SetNextItemWidth(180);
            if (ImGui.BeginCombo("Filter by Content", currentContentDropdownItem))
            {
                foreach (var item in contentDropdownitems)
                {
                    bool isSelected = (currentContentDropdownItem == item);
                    if (ImGui.Selectable(item, isSelected))
                    {
                        currentContentDropdownItem = item;
                        DropdownSelection(item);
                        SetDropdownEnum(item);
                    }
                    if (isSelected)
                    {
                        ImGui.SetItemDefaultFocus();
                        
                    }
                }
                ImGui.EndCombo();
            }

            ImGui.SameLine(ImGui.GetWindowContentRegionMax().X - 75);
            if (ImGui.Button("Settings"))
            {
                plugin.ToggleConfigUi();
            }

            ImGui.Spacing();

            //Draw Table
            using (var child = ImRaii.Child("SomeChildWithAScrollbar", new Vector2(0, 600), true))
            {
                if (child.Success)
                {
                    if (ImGui.BeginTable("Teleport", 7, ImGuiTableFlags.BordersOuter))
                    {
                        ImGui.TableSetupColumn("", 0 , 0.0001f);
                        ImGui.TableSetupColumn("", 0, 0.11f);
                        ImGui.TableSetupColumn("", 0, 0.111f);
                        ImGui.TableSetupColumn("Area", 0, 1.5f);
                        ImGui.TableSetupColumn("Aetheryte", 0, 1.2f);
                        ImGui.TableSetupColumn("Content");
                        ImGui.TableSetupColumn("Fee", 0, 0.5f);

                        ImGui.PushStyleColor(ImGuiCol.HeaderHovered, new Vector4(0, 0, 0, 0));
                        ImGui.PushStyleColor(ImGuiCol.HeaderActive, new Vector4(0, 0, 0, 0));
                        ImGui.TableHeadersRow();
                        ImGui.PopStyleColor(2);

                        DrawTopHeader();
                        if (currentTab == Tab.All || currentTab == Tab.Residential)
                        {
                            var estates = new (TeleportInfo? info, string label)[]
                            {
                                (TeleportManager.GetApartmentLocation(), "Apartment"),
                                (TeleportManager.GetPersonalEstate(), "Estate (Personal)"),
                                (TeleportManager.GetFreeCompanyEstate(), "Estate (Free Company)")
                            };
                            foreach (var (info, label) in estates)
                            {
                                if (info != null)
                                    PopulateEstateTable(info.Value, label);
                                else
                                    BetterTeleport.Log.Error($"{label} is null");
                            }
                            if(currentTab != Tab.Residential)
                            {
                                DrawHeader("La Noscea");
                            }
                        }
                        if (currentTab != Tab.Residential)
                        {
                            foreach (uint i in locationIDs)
                                PopulateTable(i);
                        }
                        if(resetScrollbar)
                        {
                            ImGui.SetScrollY(0f);
                            resetScrollbar = false;
                        }
                        ImGui.EndTable();
                    }
                }
                else
                {

                    BetterTeleport.Log.Information("child failed");
                }
            }
            ImGui.Spacing();

            //Draw Aetheryte Tickets
            /*string playerAetheryteTickets = TeleportManager.GetInventoryItem(21072).ToString();
            float aetheryeTicketTextWidth = ImGui.CalcTextSize(playerAetheryteTickets).X;
            ImGui.SameLine(ImGui.GetWindowContentRegionMax().X - (aetheryeTicketTextWidth + 150));
            ImGui.Text("Aetheryte Tickets: " + playerAetheryteTickets);*/
        }
        catch(Exception ex)
        {
            if(BetterTeleport.TeleportTexture == null)
            {
                BetterTeleport.Log.Information("TeleportTexture is" + BetterTeleport.TeleportTexture);
            }
            else
            {
                BetterTeleport.Log.Information("Unknown Error during Draw.");
            }
        }
    }
    private void PopulateTable(uint i)
    {
        if (TeleportManager.IsAttuned(i))
        {
            var teleportTexture = Icons.ConvertToTextureWrap(BetterTeleport.TeleportTexture);
            if (teleportTexture != null)
            {
                ImGui.TableNextRow(ImGuiTableRowFlags.None, 32f);
                bool selected = false;

                ImGui.TableSetColumnIndex(0);

                selected = ImGui.Selectable($"##row{i}", false, ImGuiSelectableFlags.SpanAllColumns, new Vector2(825, 25f));

                ImGui.TableSetColumnIndex(1);
                var IsFavourite = Aetheryte.AetheryteFavourite(i);
                if (IsFavourite != 0)
                {
                    ULDLibraryData starIconRef = IconData.ULDLibrary.Default;
                    if (IsFavourite == 1)
                    {
                        starIconRef = IconData.ULDLibrary.FavouritesStarIcon;
                    }
                    if (IsFavourite == 2)
                    {
                        starIconRef = IconData.ULDLibrary.FreeDestinationStarIcon;
                    }
                    if (starIconRef != null)
                    {
                        var starIconData = Icons.ULDSprite(teleportTexture, starIconRef.X, starIconRef.Y, starIconRef.Width, starIconRef.Height);
                        ApplyOffset(0, 2);
                        ImGui.PushID($"##{i}");
                        ImGui.Image(teleportTexture.Handle, new Vector2(starIconData.Width, starIconData.Height), new Vector2(starIconData.U0, starIconData.V0), new Vector2(starIconData.U1, starIconData.V1));
                    }
                }

                ImGui.TableSetColumnIndex(2);
                var locationData = Icons.GetLocationIconData(i);
                if (locationData != null)
                {
                    ApplyOffset(0, 2);
                    ImGui.PushID($"##{i}");
                    var locationUVs = Icons.ULDSprite(Icons.ConvertToTextureWrap(BetterTeleport.TeleportTexture), locationData.X, locationData.Y, locationData.Width, locationData.Height);
                    ImGui.Image(teleportTexture.Handle, new Vector2(locationUVs.Width, locationUVs.Height), new Vector2(locationUVs.U0, locationUVs.V0), new Vector2(locationUVs.U1, locationUVs.V1));

                }

                ImGui.TableSetColumnIndex(3);
                ImGui.Text(Aetheryte.AetheryteRegion(i).ToString());

                ImGui.TableSetColumnIndex(4);
                ImGui.Text(Aetheryte.AetheryteName(i).ToString());

                ImGui.TableSetColumnIndex(5);
                if (ContentManager.Content.TryGetValue(i, out var entries))
                {
                    Vector2 nextIconSpacing = new Vector2(0, 0);
                    var start = ImGui.GetCursorPos();
                    foreach (var entry in entries)
                    {
                        if (FilterContentByCategory(entry) == true)
                        {
                            var texture = Icons.GetTextureFromIconID(entry.IconID);
                            if (texture.TryGetWrap(out var wrap, out Exception? exception))
                            {
                                var offset = entry.IconOffset;
                                ImGui.SetCursorPos(start + offset + nextIconSpacing);
                                ImGui.Image(wrap.Handle, entry.IconSize);
                                ImGui.SetCursorPos(start);
                            }

                            if (ImGui.IsItemHovered())
                            {
                                ImGui.BeginTooltip();
                                ImGui.Text(entry.TooltipText);
                                ImGui.EndTooltip();
                            }
                            ImGui.PushStyleVar(ImGuiStyleVar.ItemSpacing, new Vector2(0, 0));
                            ImGui.SameLine(0, 0);
                            ImGui.PopStyleVar();
                            nextIconSpacing.X += 30;
                        }
                    }
                }
                ImGui.TableSetColumnIndex(6);
                ImGui.Text(Aetheryte.AetheryteCost(i).ToString("N0") + $"{(char)SeIconChar.Gil}");

                if (selected)
                {
                    SetupTeleport(i);
                }
                DrawHeaderInNextRow(i);
            }
        }
    }

    private void PopulateEstateTable(TeleportInfo info, string estateType)
    {
        var teleportTexture = Icons.ConvertToTextureWrap(BetterTeleport.TeleportTexture);
        if (teleportTexture == null)
            return;

        ImGui.TableNextRow(ImGuiTableRowFlags.None, 32f);

        // Selectable row
        ImGui.TableSetColumnIndex(0);
        bool selected = ImGui.Selectable($"##estate_{info.AetheryteId}_{info.SubIndex}", false, ImGuiSelectableFlags.SpanAllColumns, new Vector2(825, 25f));

        // Icon column
        ImGui.TableSetColumnIndex(2);
        var locationData = IconData.ULDLibrary.MiscLocationTableIcon;
        if (locationData != null)
        {
            ApplyOffset(0, 2);
            ImGui.PushID($"icon_{info.AetheryteId}_{info.SubIndex}");
            var locationUVs = Icons.ULDSprite(teleportTexture, locationData.X, locationData.Y, locationData.Width, locationData.Height);
            ImGui.Image(teleportTexture.Handle, new Vector2(locationUVs.Width, locationUVs.Height), new Vector2(locationUVs.U0, locationUVs.V0), new Vector2(locationUVs.U1, locationUVs.V1));
            ImGui.PopID();
        }

        // Region column
        ImGui.TableSetColumnIndex(3);
        ImGui.Text(Aetheryte.AetheryteRegion((uint)info.AetheryteId).ToString());

        // Estate type column
        ImGui.TableSetColumnIndex(4);
        ImGui.Text(estateType);

        // Gil cost column
        ImGui.TableSetColumnIndex(6);
        ImGui.Text(info.GilCost.ToString("N0") + $"{(char)SeIconChar.Gil}");

        // Teleport on click
        if (selected)
        {
            TeleportManager.TeleportEstate(info);
            plugin.ToggleMainUi();
        }
    }
    private IconProperties GetIconProperties(IDalamudTextureWrap textureSheet, ULDLibraryData iconData)
    {
        var iconProperties = Icons.ULDSprite(textureSheet, iconData.X, iconData.Y, iconData.Width, iconData.Height);
        return iconProperties;
    }
    private void CreateTabButton(Tab selectedTab, IDalamudTextureWrap textureSheet, ULDLibraryData iconData, uint[] idLocation, string tooltip)
    {
        var iconProperties = GetIconProperties(textureSheet, iconData);
        if (textureSheet != null)
        {
            ImGui.PushID($"##{selectedTab}");
            if (ImGui.ImageButton(textureSheet.Handle, new Vector2(iconProperties.Width, iconProperties.Height), new Vector2(iconProperties.U0, iconProperties.V0), new Vector2(iconProperties.U1, iconProperties.V1)))
            {
                currentTab = selectedTab;
                locationIDs = idLocation;
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
    private void SetDropdownEnum(string item)
    {
        if(item == "Market Boards")
        {
            currentTab = Tab.MarketBoards;
        }
        if (item == "Summoning Bells")
        {
            currentTab = Tab.SummoningBells;
        }
        if (item == "Allied Societies")
        {
            currentTab = Tab.AlliedSocieties;
        }
        if (item == "Custom Deliveries")
        {
            currentTab = Tab.CustomDeliveries;
        }
        if (item == "Deep Dungeons")
        {
            currentTab = Tab.DeepDungeons;
        }
        if (item == "Restoration Content")
        {
            currentTab = Tab.RestorationContent;
        }
        if (item == "Field Operations")
        {
            currentTab = Tab.FieldOperations;
        }
    }
    private void DropdownSelection(string selection)
    {
        string[] contentDropdownitems = { "Market Boards", "Allied Societies", "Custom Deliveries", "Deep Dungeons" };
        switch (selection)
        {
            case "Market Boards":
                locationIDs = LocationManager.locationIDs.MarketBoardIDs;
                break;
            case "Summoning Bells":
                locationIDs = LocationManager.locationIDs.SummoningBellIDs;
                break;
            case "Allied Societies":
                locationIDs = LocationManager.locationIDs.AlliedSocietyIDs;
                break;
            case "Custom Deliveries":
                locationIDs = LocationManager.locationIDs.CustomDeliveriesIDs;
                break;
            case "Deep Dungeons":
                locationIDs = LocationManager.locationIDs.DeepDungeonIDs;
                break;
            case "Restoration Content":
                locationIDs = LocationManager.locationIDs.RestorationContentIDs;
                break;
            case "Field Operations":
                locationIDs = LocationManager.locationIDs.FieldOperationIDs;
                break;
        }
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

    private uint[] GetFavouriteLocations()
    {
        List<uint> favouriteIDList = new List<uint>();
        foreach (uint i in LocationManager.locationIDs.AllIDs)
        {
            var IsFavourite = Aetheryte.AetheryteFavourite(i);
            if (IsFavourite != 0)
            {
                favouriteIDList.Add(i);
            }
        }
        uint[] favouriteIDs = favouriteIDList.ToArray();
        return favouriteIDs;
    }

    private void SetupTeleport(uint id)
    {
        TeleportManager.Teleport(id);
        Aetheryte.AetheryteData(id, out var destination, out var destinationRegion, out var aethernet, out int teleportCost);
        BetterTeleport.Log.Information("Teleporting to " + destination + " in " + destinationRegion + " costing " + teleportCost);
        plugin.ToggleMainUi();
    }

    private void ApplyOffset(float xOffset, float yOffset)
    {
        float x = ImGui.GetCursorPosX();
        float y = ImGui.GetCursorPosY();
        ImGui.SetCursorPosX(x + xOffset);
        ImGui.SetCursorPosY(y + yOffset);
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

    public override void OnClose()
    {
        base.OnClose();
        BetterTeleport.teleportWindowOpen = false;
    }

    public override void OnOpen()
    {
        base.OnOpen();
        currentTab = Tab.All;
        locationIDs = LocationManager.locationIDs.AllIDs;
        resetScrollbar = true;
    }
}
