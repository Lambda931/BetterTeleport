using Dalamud.Bindings.ImGui;
using Dalamud.Game.Text;
using Dalamud.Interface.Textures.TextureWraps;
using Dalamud.Interface.Utility.Raii;
using Dalamud.Interface.Windowing;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Data.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace BetterTeleportPlugin.Windows;

public partial class MainWindow : Window, IDisposable
{
    private readonly BetterTeleport plugin;
    public static Icons? Icons;

    string currentContentDropdownItem = "";
    string[] contentDropdownitems = { "Market Boards", "Summoning Bells", "Allied Societies", "Custom Deliveries",
                                        "Deep Dungeons", "Restoration Content", "Field Operations" };
    
    private bool resetScrollbar;

    private int colourChannels;

    private List<uint> unlockedIds;

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

    public override void PreDraw()
    {
        WindowColourManager.ColourData.TryGetValue(WindowColourManager.Colours.ClearBlue, out var colourData);
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
    }

    public override void Draw()
    {
        try
        {
            var TextureSheet = Icons.ConvertToTextureWrap(BetterTeleport.TeleportTexture);

            //Draw Tabs
            if (ImGui.Button("All", new Vector2(62, 31)))
            {
                currentTab = Tab.All;
                currentContentDropdownItem = "";
                resetScrollbar = true;
            }
            ImGui.SameLine();

            if (TextureSheet != null)
            {
                var estates = new (TeleportInfo? info, string label)[]
                {
                    (TeleportManager.GetApartmentLocation(), "Apartment"),
                    (TeleportManager.GetPersonalEstate(), "Estate (Personal)"),
                    (TeleportManager.GetFreeCompanyEstate(), "Estate (Free Company)")
                };
                var estateValid = false;
                foreach(var estate in estates)
                {
                    if(estate.info.Value.AetheryteId != 0)
                    {
                        estateValid = true;
                        BetterTeleport.Log.Debug(estate.info.Value.AetheryteId.ToString());
                    }
                }
                if(estateValid)
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
                }

                ImGui.SameLine();
                CreateTabButton(Tab.LaNoscea, TextureSheet, IconData.ULDLibrary.LaNosceaTabIcon, "La Noscea"); ImGui.SameLine();
                CreateTabButton(Tab.BlackShroud, TextureSheet, IconData.ULDLibrary.BlackShroudTabIcon, "The Black Shroud"); ImGui.SameLine();
                CreateTabButton(Tab.Thanalan, TextureSheet, IconData.ULDLibrary.ThanalanTabIcon, "Thanalan"); ImGui.SameLine();
                CreateTabButton(Tab.Ishgard, TextureSheet, IconData.ULDLibrary.IshgardTabIcon, "Ishgard and Surrounding Areas"); ImGui.SameLine();
                CreateTabButton(Tab.GyrAbania, TextureSheet, IconData.ULDLibrary.GyrAbaniaTabIcon, "Gyr Abania"); ImGui.SameLine();
                CreateTabButton(Tab.FarEast, TextureSheet, IconData.ULDLibrary.FarEastTabIcon, "The Far East"); ImGui.SameLine();
                CreateTabButton(Tab.IndependentNations, TextureSheet, IconData.ULDLibrary.IndependentNationsTabIcon, "Independent Nations"); ImGui.SameLine();
                CreateTabButton(Tab.Ilsabard, TextureSheet, IconData.ULDLibrary.IlsabardTabIcon, "Ilsabard"); ImGui.SameLine();
                CreateTabButton(Tab.Tural, TextureSheet, IconData.ULDLibrary.TuralTabIcon, "Tural"); ImGui.SameLine();
                CreateTabButton(Tab.Norvrandt, TextureSheet, IconData.ULDLibrary.NorvrandtTabIcon, "Norvrandt"); ImGui.SameLine();
                CreateTabButton(Tab.BeyondTheSource, TextureSheet, IconData.ULDLibrary.BeyondTheSourceTabIcon, "Beyond the Source"); ImGui.SameLine();

                //Favourites Button
                var FavouritesIconData = GetIconProperties(TextureSheet, IconData.ULDLibrary.FavouritesTabIcon);
                ImGui.PushID($"{Tab.Favourites}");
                if (ImGui.ImageButton(TextureSheet.Handle, new Vector2(FavouritesIconData.Width, FavouritesIconData.Height), new Vector2(FavouritesIconData.U0, FavouritesIconData.V0), new Vector2(FavouritesIconData.U1, FavouritesIconData.V1)))
                {
                    LocationManager.GetFavouriteLocations();
                    currentTab = Tab.Favourites;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.Text("Favorites");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();

                //DebugAll
                var DebugIconData = GetIconProperties(TextureSheet, IconData.ULDLibrary.OtherTabIcon);
                ImGui.PushID($"{Tab.Debug}");
                if (BetterTeleport.Debug)
                {
                    if (ImGui.ImageButton(TextureSheet.Handle, new Vector2(DebugIconData.Width, DebugIconData.Height), new Vector2(DebugIconData.U0, DebugIconData.V0), new Vector2(DebugIconData.U1, DebugIconData.V1)))
                    {
                        currentTab = Tab.Debug;
                    }
                    if (ImGui.IsItemHovered())
                    {
                        ImGui.BeginTooltip();
                        ImGui.Text("Debug");
                        ImGui.EndTooltip();
                    }
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
                    if (!DoesTabHaveValidRows(item))
                        continue;

                    bool isSelected = (currentContentDropdownItem == item);
                    if (ImGui.Selectable(item, isSelected))
                    {
                        currentContentDropdownItem = item;
                        SetDropdownTab(item);
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
                if (!child.Success)
                {
                    BetterTeleport.Log.Information("child failed");
                    return;
                }
                if (ImGui.BeginTable("Teleport", 7, ImGuiTableFlags.BordersOuter))
                {
                    ImGui.TableSetupColumn("", 0 , 0.0001f);
                    ImGui.TableSetupColumn("", 0, 0.11f);
                    ImGui.TableSetupColumn("", 0, 0.111f);
                    ImGui.TableSetupColumn("Area", 0, 1.5f);
                    ImGui.TableSetupColumn("Aetheryte", 0, 1.2f);
                    ImGui.TableSetupColumn("Content");
                    ImGui.TableSetupColumn("Fee", 0, 0.5f);
                    ImGui.TableHeadersRow();

                    if (currentTab == Tab.All || currentTab == Tab.Residential)
                    {
                        var estates = new (TeleportInfo? info, string label)[]
                        {
                            (TeleportManager.GetApartmentLocation(), "Apartment"),
                            (TeleportManager.GetPersonalEstate(), "Estate (Personal)"),
                            (TeleportManager.GetFreeCompanyEstate(), "Estate (Free Company)")
                        };
                        var estateValid = false;
                        foreach (var estate in estates)
                        {
                            if (estate.info.Value.AetheryteId != 0)
                            {
                                estateValid = true;
                                BetterTeleport.Log.Debug(estate.info.Value.AetheryteId.ToString());
                            }
                        }
                        if (estateValid)
                        {
                            DrawHeader("Residential Areas");
                            foreach (var (info, label) in estates)
                            {
                                if (info != null)
                                    PopulateEstateTable(info.Value, label);
                                else
                                    BetterTeleport.Log.Error($"{label} is null");
                            }
                        }
                    }
                    if (currentTab != Tab.Residential)
                    {
                        GetTabData(currentTab);

                        if(currentTab != Tab.Debug)
                        {
                            foreach (var category in currentTabData)
                            {
                                var attunedIds = category.ids.Where(TeleportManager.IsAttuned).Where(id => IsDropdownEntryUnlocked(currentTab, id)).ToList();
                                if (attunedIds.Count == 0)
                                    continue;
                                
                                DrawHeader(category.Header);
                                foreach (var id in attunedIds)
                                {
                                    PopulateTable(id);
                                }
                            }
                        }
                        else
                        {
                            foreach (var category in currentTabData)
                            {
                                DrawHeader(category.Header);
                                foreach (var id in category.ids)
                                {
                                    PopulateTable(id);
                                }
                            }
                        }
                        
                    }
                    if (resetScrollbar)
                    {
                        ImGui.SetScrollY(0f);
                        resetScrollbar = false;
                    }
                    ImGui.EndTable();
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
    public override void PostDraw()
    {
        ImGui.PopStyleColor(colourChannels);
    }

    private void PopulateTable(uint i)
    {
        if (TeleportManager.IsAttuned(i) || currentTab == Tab.Debug)
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
                        if(ContentManager.CheckUnlocked(entry.ContentCategory, entry.TooltipText))
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
                }
                ImGui.TableSetColumnIndex(6);
                ImGui.Text(Aetheryte.AetheryteCost(i).ToString("N0") + $"{(char)SeIconChar.Gil}");

                if (selected)
                {
                    SetupTeleport(i);
                }
            }
        }
    }

    private void PopulateEstateTable(TeleportInfo info, string estateType)
    {
        if (TeleportManager.IsAttuned(info.AetheryteId))
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

    public override void OnClose()
    {
        base.OnClose();
        BetterTeleport.teleportWindowOpen = false;
    }

    public override void OnOpen()
    {
        base.OnOpen();
        currentTab = Tab.All;
        resetScrollbar = true;
    }
}
