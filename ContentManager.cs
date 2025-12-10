using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.Numerics;
using static BetterTeleportPlugin.ContentInfo;

namespace BetterTeleportPlugin;

public class ContentInfo
{
    public enum Categories { Misc, MarketBoard, SummoningBell, AlliedSocieties, CustomDeliveries, DeepDungeons, RestorationContent, FieldOperations}

    public Categories ContentCategory { get; set; }
    public required string TooltipText { get; set; }
    public required int IconID { get; set; }
    public Vector2 IconSize { get; set; }
    public Vector2 IconOffset { get; set; }
}

public class ContentManager
{
    private static readonly IconLibrary Icons = IconData.IconLibrary;

    private static ContentInfo ContentInfo(
        ContentInfo.Categories category,
        string tooltipText,
        int icon,
        int size = 26,
        float offsetX = 0,
        float offsetY = 0
    ) => new()
    {
        ContentCategory = category,
        TooltipText = tooltipText,
        IconID = icon,
        IconSize = new Vector2(size, size),
        IconOffset = new Vector2(offsetX, offsetY)
    };

    private static ContentInfo MarketBoard() =>
        ContentInfo(Categories.MarketBoard,"Market Board", Icons.MarketBoardIcon, 30, -2, -3);

    private static ContentInfo SummoningBell() =>
        ContentInfo(Categories.SummoningBell, "Summoning Bell", Icons.SummoningBellIcon, 34, -5, -5);

    private static ContentInfo DeepDungeon(string text) =>
        ContentInfo(Categories.DeepDungeons, text, Icons.DeepDungeonIcon, 38, -6, -6.5f);

    private static ContentInfo CustomDelivery(string text) =>
        ContentInfo(Categories.CustomDeliveries, text, Icons.CustomDeliveriesIcon, 33, -4, -4);

    private static ContentInfo FieldOperation(string text) =>
        ContentInfo(Categories.FieldOperations, text, Icons.FieldOperationsIcon, 38, -6, -6.5f);

    public static readonly Dictionary<uint, List<ContentInfo>> Content = new() {
    
    // --------------- ARR ---------------
    /* New Gridania                     */ {2,   [MarketBoard(), SummoningBell()]},
    /* East Shroud: The Hawthorne Hut   */ {4,   [ContentInfo(Categories.AlliedSocieties, "Sylph Allied Society Quests", Icons.SylphAlliedSocietyIcon)]},
    /* South Shroud: Quarrymill         */ {5,   [DeepDungeon("Deep Dungeon: Palace of the Dead") ]},
    /* North Shroud: Fallgourd Float    */ {7,   [ContentInfo(Categories.AlliedSocieties, "Ixal Allied Society Quests", Icons.IxalAlliedSocietyIcon)]},
    /* Limsa Lominsa Lower Decks        */ {8,   [MarketBoard(), SummoningBell()]},
    /* Ul'Dah - Steps of Nald           */ {9,   [MarketBoard(), SummoningBell()]},
    /* Lower La Noscea: Moraby Drydocks */ {10,  [ContentInfo(Categories.RestorationContent, "Island Sanctuary", Icons.IslandSanctuaryIcon, 30, -2, -3)]},
    /* Western La Noscea: Aleport       */ {14,  [ContentInfo(Categories.AlliedSocieties, "Sahagin Allied Society Quests", Icons.SahaginAlliedSocietyIcon)]},
    /* Outer La Noscea: Camp Overlook   */ {16,  [ContentInfo(Categories.AlliedSocieties, "Kobolds Allied Society Quests", Icons.KoboldAlliedSocietyIcon)]},
    /* Southern Thanalan: Lil Ala Mhigo */ {19,  [ContentInfo(Categories.AlliedSocieties, "Amalj'aa Allied Society Quests", Icons.AmaljaaAlliedSocietyIcon)]},
    /* Mor Dhona: Revenant's Toll       */ {24,  [DeepDungeon("Deep Dungeon: Eureka Orthos"), SummoningBell()]},
    /* Central Than: Blk Brush Station  */ {53,  [ContentInfo(Categories.Misc, "Materia Transmutation", Icons.MateriaTransmutationIcon, 30, -2, -3)]},
    /* Wolves' Den Pier                 */ {55,  [ContentInfo(Categories.Misc, "PVP Zone", Icons.PvpZoneIcon, 30, -2, -3)]},
    /* The Gold Saucer                  */ {62,  [ContentInfo(Categories.Misc, "The Gold Saucer", Icons.GoldSaucerIcon), SummoningBell()]},

    // ----------- Heavensward -----------
    /* Foundation                       */ {70,  [MarketBoard(), SummoningBell(),
                                                  ContentInfo(Categories.RestorationContent, "Ishgardian Restoration", Icons.IshgardianRestorationIcon, 30, -2, -3),
                                                  CustomDelivery("Custom Deliveries: Ehll Tou"),
                                                  CustomDelivery("Custom Deliveries: Charlemend")]},
    /* The Sea of Clouds: Ok'Zundu      */ {73,  [ContentInfo(Categories.AlliedSocieties, "Vanu Vanu Allied Society Quests", Icons.VanuVanuAlliedSocietyIcon)]},
    /* Idyllshire                       */ {75,  [SummoningBell(),
                                                  CustomDelivery("Custom Deliveries: Zhloe Aliapoe"),
                                                  CustomDelivery("Custom Deliveries: Adkiragh")]},
    /* Dravanian Forelands: Anyx Trine  */ {77,  [ContentInfo(Categories.AlliedSocieties, "Vath Allied Society Quests", Icons.VathAlliedSocietyIcon)]},
    /* Churning Mists: Zenith           */ {79,  [ContentInfo(Categories.AlliedSocieties, "Moogle Allied Society Quests", Icons.MoogleAlliedSocietyIcon)]},

    // ----------- Stormblood ------------
    /* The Fringes: The Peering Stones  */ {99,  [ContentInfo(Categories.AlliedSocieties, "Ananta Allied Society Quests", Icons.AnantaAlliedSocietyIcon)]},
    /* Rhalgr's Reach                   */ {104, [SummoningBell(), 
                                                  CustomDelivery("Custom Deliveries: M'naago")]},
    /* The Ruby Sea: Tamamizu           */ {105, [ContentInfo(Categories.AlliedSocieties, "Kojin Allied Society Quests", Icons.KojinAlliedSocietyIcon),
                                                  CustomDelivery("Custom Deliveries: Kurenai")]},
    /* The Ruby Sea: Onokoro            */ {106, [DeepDungeon("Deep Dungeon: Heaven on High")]},
    /* Kugane                           */ {111, [MarketBoard(), SummoningBell(), FieldOperation("The Forbidden Land, Eureka"),]},
    /* The Doman Enclave                */ {127, [SummoningBell(), 
                                                  ContentInfo(Categories.RestorationContent, "Doman Enclave Reconstruction", Icons.DomanReconstructionIcon, 30, -2, -3), 
                                                  FieldOperation("Save the Queen (Bozja)")]},
    /* The Azim Steppe: Dhoro Iloh      */ {128, [ContentInfo(Categories.AlliedSocieties, "Namazu Allied Society Quests", Icons.NamazuAlliedSocietyIcon)]},

    // --------- Shadowbringers ----------
    /* The Crystarium                   */ {133, [MarketBoard(), SummoningBell()]},
    /* Eulmore                          */ {134, [CustomDelivery("Custom Deliveries: Kai-Shirr"), SummoningBell()]},   
    /* Lakeland: The Ostall Imperative  */ {136, [ContentInfo(Categories.AlliedSocieties, "Dwarf Allied Society Quests", Icons.DwarfAlliedSocietyIcon)]},
    /* The Rak'tika Greatwood: Fanow    */ {143, [ContentInfo(Categories.AlliedSocieties, "Qitari Allied Society Quests", Icons.QitariAlliedSocietyIcon)]},
    /* Il Mheg: Lydha Lran              */ {144, [ContentInfo(Categories.AlliedSocieties, "Pixie Allied Society Quests", Icons.PixieAlliedSocietyIcon), 
                                                  CustomDelivery("Custom Deliveries: Anden")]},
    /* Il Mheg: Wolekdorf               */ {146, [DeepDungeon("Deep Dungeon: Pilgrim's Traverse")]},

    // ----------- Endwalker -------------
    /* Labyrinthos: Sharlayan Hamlet    */ {167, [CustomDelivery("Custom Deliveries: Margrat")]},
    /* Thavnair: Yedlihmad              */ {169, [ContentInfo(Categories.AlliedSocieties, "Arkasodara Allied Society Quests", Icons.ArkasodaraAlliedSocietyIcon)]},
    /* Mare Lamentorum: Bestway Burrow  */ {175, [ContentInfo(Categories.AlliedSocieties, "Loporrit Allied Society Quests", Icons.LoporritAlliedSocietyIcon),
                                                  ContentInfo(Categories.RestorationContent, "Cosmic Exploration", Icons.CosmicExplorationIcon, 30, -2, -3)]},
    /* Ultima Thule: Base Omicron       */ {181, [ContentInfo(Categories.AlliedSocieties, "Omicron Allied Society Quests", Icons.OmicronAlliedSocietyIcon)]},
    /* Old Sharlayan                    */ {182, [MarketBoard(), SummoningBell(),
                                                  CustomDelivery("Custom Deliveries: Ameliance") ]},
    /* Radz-at-Han                      */ {183, [SummoningBell()]},

    // ----------- Dawntrail -------------
    /* Urqopacha: Worlar's Echo         */ {201, [ContentInfo(Categories.AlliedSocieties, "Yok Huy Allied Society Quests", Icons.YokHuyAlliedSocietyIcon)]},
    /* Yak T'el: Mamook                 */ {206, [ContentInfo(Categories.AlliedSocieties, "Mamool Ja Allied Society Quests", Icons.MamoolJaAlliedSocietyIcon)]},
    /* Shaaloani: Sheshenewezi Springs  */ {208, [CustomDelivery("Custom Deliveries: Nitowikwe") ]},
    /* Tulitollal                       */ {216, [MarketBoard(), SummoningBell(), FieldOperation("The Occult Cresent")]},
    /* Solution Nine                    */ {217, [SummoningBell()]},
    /* Kozama'uka: Dock Poga            */ {238, [ContentInfo(Categories.AlliedSocieties, "Pelupelu Allied Society Quests", Icons.PelupeluAlliedSocietyIcon)]},
    };
}

