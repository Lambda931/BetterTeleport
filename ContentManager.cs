using Lumina.Excel.Sheets;
using System.Collections.Generic;
using System.Numerics;
using static BetterTeleportPlugin.ContentInfo;

namespace BetterTeleportPlugin;

public class ContentInfo
{
    public enum Categories { Misc, MarketBoard, SummoningBell, AlliedSocieties, CustomDeliveries, DeepDungeons, RestorationContent, FieldOperations}
    public enum Tooltip
    {
        MarketBoard, SummoningBell,

        AlliedSocietiesAmaljaa, AlliedSocietiesSylph, AlliedSocietiesKobold, AlliedSocietiesSahagin, AlliedSocietiesIxal, AlliedSocietiesVanuVanu, AlliedSocietiesVath,
        AlliedSocietiesMoogle, AlliedSocietiesKojin, AlliedSocietiesAnanta, AlliedSocietiesNamazu, AlliedSocietiesPixie, AlliedSocietiesQitari, AlliedSocietiesDwarf,
        AlliedSocietiesArkasodara, AlliedSocietiesOmicron, AlliedSocietiesLoporrit, AlliedSocietiesPeluPelu, AlliedSocietiesMamoolJa, AlliedSocietiesYokHuy,

        CustomDeliveriesZhloe, CustomDeliveriesMnaago, CustomDeliveriesKurenai, CustomDeliveriesAdkiragh, CustomDeliveriesKaiShirr, CustomDeliveriesEhllTou, 
        CustomDeliveriesCharlemend, CustomDeliveriesAmeliance, CustomDeliveriesAnden, CustomDeliveriesMargrat, CustomDeliveriesNitowikwe,

        PalaceOfTheDead, HeavenOnHigh, EurekaOrthos, PilgrimsTraverse,
        Eureka, Bozja, OccultCresent,
        DomanRestoration, IshgardianRestoration, IslandSanctuary, CosmicExploration,

        MateriaTransmutation, PVPZone, GoldSaucer
    }
    public Categories ContentCategory { get; set; }
    public required Tooltip TooltipText { get; set; }
    public required int IconID { get; set; }
    public Vector2 IconSize { get; set; }
    public Vector2 IconOffset { get; set; }
}

public partial class ContentManager
{
    private static readonly IconLibrary Icons = IconData.IconLibrary;

    private static ContentInfo ContentInfo(
        ContentInfo.Categories category,
        Tooltip tooltipText,
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

    public static readonly Dictionary<Tooltip, string> TooltipText = new()
    {
        {Tooltip.MarketBoard, "Market Board"}, {Tooltip.SummoningBell, "Summoning Bell"},

        {Tooltip.AlliedSocietiesAmaljaa, "Amalj'aa Allied Society Quests"},  {Tooltip.AlliedSocietiesSylph, "Sylph Allied Society Quests"},        {Tooltip.AlliedSocietiesKobold, "Kobold Allied Society Quests"},         {Tooltip.AlliedSocietiesSahagin, "Sahagin Allied Society Quests"},
        {Tooltip.AlliedSocietiesIxal, "Ixal Allied Society Quests"},         {Tooltip.AlliedSocietiesVanuVanu, "Vanu Vanu Allied Society Quests"}, {Tooltip.AlliedSocietiesVath, "Vath Allied Society Quests"},             {Tooltip.AlliedSocietiesMoogle, "Moogle Allied Society Quests"},
        {Tooltip.AlliedSocietiesKojin, "Kojin Allied Society Quests"},       {Tooltip.AlliedSocietiesAnanta, "Ananta Allied Society Quests"},      {Tooltip.AlliedSocietiesNamazu, "Namazu Allied Society Quests"},         {Tooltip.AlliedSocietiesPixie, "Pixie Allied Society Quests"},
        {Tooltip.AlliedSocietiesQitari, "Qitari Allied Society Quests"},     {Tooltip.AlliedSocietiesDwarf, "Dwarf Allied Society Quests"},        {Tooltip.AlliedSocietiesArkasodara, "Arkasodara Allied Society Quests"}, {Tooltip.AlliedSocietiesOmicron, "Omicron Allied Society Quests"},
        {Tooltip.AlliedSocietiesLoporrit, "Loporrit Allied Society Quests"}, {Tooltip.AlliedSocietiesPeluPelu, "Pelupelu Allied Society Quests"},  {Tooltip.AlliedSocietiesMamoolJa, "Mamool Ja Allied Society Quests"},    {Tooltip.AlliedSocietiesYokHuy, "Yok Huy Allied Society Quests"},

        {Tooltip.CustomDeliveriesZhloe, "Custom Deliveries: Zhloe Aliapoh"}, {Tooltip.CustomDeliveriesMnaago, "Custom Deliveries: M'naago"},       {Tooltip.CustomDeliveriesKurenai, "Custom Deliveries: Kurenai"},         {Tooltip.CustomDeliveriesAdkiragh, "Custom Deliveries: Adkiragh"},     
        {Tooltip.CustomDeliveriesKaiShirr, "Custom Deliveries: Kai-Shirr"},  {Tooltip.CustomDeliveriesEhllTou, "Custom Deliveries: Ehll Tou"},     {Tooltip.CustomDeliveriesCharlemend, "Custom Deliveries: Charlemend"},   {Tooltip.CustomDeliveriesAmeliance, "Custom Deliveries: Ameliance"},  
        {Tooltip.CustomDeliveriesAnden, "Custom Deliveries: Anden"},         {Tooltip.CustomDeliveriesMargrat, "Custom Deliveries: Margrat"},      {Tooltip.CustomDeliveriesNitowikwe, "Custom Deliveries: Nitowikwe"},

        {Tooltip.PalaceOfTheDead, "Deep Dungeon: Palace of the Dead"},       {Tooltip.HeavenOnHigh, "Deep Dungeon: Heaven on High"},               {Tooltip.EurekaOrthos, "Deep Dungeon: Eureka Orthos"},                   {Tooltip.PilgrimsTraverse, "Deep Dungeon: Pilgrim's Traverse"},
        {Tooltip.DomanRestoration, "Doman Enclave Reconstruction"},          {Tooltip.IshgardianRestoration, "Ishgardian Restoration"},            {Tooltip.IslandSanctuary, "Island Sanctuary"},                           {Tooltip.CosmicExploration, "Cosmic Exploration"},
        {Tooltip.Eureka, "The Forbidden Land, Eureka"},                      {Tooltip.Bozja, "Save the Queen (Bozja)"},                            {Tooltip.OccultCresent, "The Occult Cresent"},

        {Tooltip.MateriaTransmutation, "Materia Transmutation"},             {Tooltip.PVPZone, "PVP Zone"},                                        {Tooltip.GoldSaucer, "The Gold Saucer"}
    };

    private static ContentInfo MarketBoard() =>
        ContentInfo(Categories.MarketBoard, Tooltip.MarketBoard, Icons.MarketBoardIcon, 30, -2, -3);

    private static ContentInfo SummoningBell() =>
        ContentInfo(Categories.SummoningBell, Tooltip.SummoningBell, Icons.SummoningBellIcon, 34, -5, -5);

    private static ContentInfo DeepDungeon(Tooltip tooltip) =>
        ContentInfo(Categories.DeepDungeons, tooltip, Icons.DeepDungeonIcon, 38, -6, -6.5f);

    private static ContentInfo AlliedSociety(Tooltip tooltip, int icon) =>
        ContentInfo(Categories.AlliedSocieties, tooltip, icon, 30, -3, -3);

    private static ContentInfo CustomDelivery(Tooltip tooltip) =>
        ContentInfo(Categories.CustomDeliveries, tooltip, Icons.CustomDeliveriesIcon, 33, -4, -4);

    private static ContentInfo FieldOperation(Tooltip tooltip) =>
        ContentInfo(Categories.FieldOperations, tooltip, Icons.FieldOperationsIcon, 38, -6, -6.5f);

    public static readonly Dictionary<uint, List<ContentInfo>> Content = new() {
    
    // --------------- ARR ---------------
    /* New Gridania                     */ {2,   [MarketBoard(), SummoningBell()]},
    /* East Shroud: The Hawthorne Hut   */ {4,   [AlliedSociety(Tooltip.AlliedSocietiesSylph, Icons.SylphAlliedSocietyIcon)]},
    /* South Shroud: Quarrymill         */ {5,   [DeepDungeon(Tooltip.PalaceOfTheDead)]},
    /* North Shroud: Fallgourd Float    */ {7,   [AlliedSociety(Tooltip.AlliedSocietiesIxal, Icons.IxalAlliedSocietyIcon)]},
    /* Limsa Lominsa Lower Decks        */ {8,   [MarketBoard(), SummoningBell()]},
    /* Ul'Dah - Steps of Nald           */ {9,   [MarketBoard(), SummoningBell()]},
    /* Lower La Noscea: Moraby Drydocks */ {10,  [ContentInfo(Categories.RestorationContent, Tooltip.IslandSanctuary, Icons.IslandSanctuaryIcon, 30, -2, -3)]},
    /* Western La Noscea: Aleport       */ {14,  [AlliedSociety(Tooltip.AlliedSocietiesSahagin, Icons.SahaginAlliedSocietyIcon)]},
    /* Outer La Noscea: Camp Overlook   */ {16,  [AlliedSociety(Tooltip.AlliedSocietiesKobold, Icons.KoboldAlliedSocietyIcon)]},
    /* Southern Thanalan: Lil Ala Mhigo */ {19,  [AlliedSociety(Tooltip.AlliedSocietiesAmaljaa, Icons.AmaljaaAlliedSocietyIcon)]},
    /* Mor Dhona: Revenant's Toll       */ {24,  [DeepDungeon(Tooltip.EurekaOrthos), SummoningBell()]},
    /* Central Than: Blk Brush Station  */ {53,  [ContentInfo(Categories.Misc, Tooltip.MateriaTransmutation, Icons.MateriaTransmutationIcon, 30, -2, -3)]},
    /* Wolves' Den Pier                 */ {55,  [ContentInfo(Categories.Misc, Tooltip.PVPZone, Icons.PvpZoneIcon, 30, -2, -3)]},
    /* The Gold Saucer                  */ {62,  [ContentInfo(Categories.Misc, Tooltip.GoldSaucer, Icons.GoldSaucerIcon), SummoningBell()]},

    // ----------- Heavensward -----------
    /* Foundation                       */ {70,  [MarketBoard(), SummoningBell(), ContentInfo(Categories.RestorationContent, Tooltip.IshgardianRestoration, Icons.IshgardianRestorationIcon, 30, -2, -3), 
                                                  CustomDelivery(Tooltip.CustomDeliveriesEhllTou), CustomDelivery(Tooltip.CustomDeliveriesCharlemend)]},
    /* The Sea of Clouds: Ok'Zundu      */ {73,  [AlliedSociety(Tooltip.AlliedSocietiesVanuVanu, Icons.VanuVanuAlliedSocietyIcon)]},
    /* Idyllshire                       */ {75,  [SummoningBell(), CustomDelivery(Tooltip.CustomDeliveriesZhloe), CustomDelivery(Tooltip.CustomDeliveriesAdkiragh)]},
    /* Dravanian Forelands: Anyx Trine  */ {77,  [AlliedSociety(Tooltip.AlliedSocietiesVath, Icons.VathAlliedSocietyIcon)]},
    /* Churning Mists: Zenith           */ {79,  [AlliedSociety(Tooltip.AlliedSocietiesMoogle, Icons.MoogleAlliedSocietyIcon)]},

    // ----------- Stormblood ------------
    /* The Fringes: The Peering Stones  */ {99,  [AlliedSociety(Tooltip.AlliedSocietiesAnanta, Icons.AnantaAlliedSocietyIcon)]},
    /* Rhalgr's Reach                   */ {104, [SummoningBell(), CustomDelivery(Tooltip.CustomDeliveriesMnaago)]},
    /* The Ruby Sea: Tamamizu           */ {105, [AlliedSociety(Tooltip.AlliedSocietiesKojin, Icons.KojinAlliedSocietyIcon), CustomDelivery(Tooltip.CustomDeliveriesKurenai)]},
    /* The Ruby Sea: Onokoro            */ {106, [DeepDungeon(Tooltip.HeavenOnHigh)]},
    /* Kugane                           */ {111, [MarketBoard(), SummoningBell(), FieldOperation(Tooltip.Eureka),]},
    /* The Doman Enclave                */ {127, [SummoningBell(), ContentInfo(Categories.RestorationContent, Tooltip.DomanRestoration, Icons.DomanReconstructionIcon, 30, -2, -3), FieldOperation(Tooltip.Bozja)]},
    /* The Azim Steppe: Dhoro Iloh      */ {128, [AlliedSociety(Tooltip.AlliedSocietiesNamazu, Icons.NamazuAlliedSocietyIcon)]},

    // --------- Shadowbringers ----------
    /* The Crystarium                   */ {133, [MarketBoard(), SummoningBell()]},
    /* Eulmore                          */ {134, [SummoningBell(), CustomDelivery(Tooltip.CustomDeliveriesKaiShirr)]},   
    /* Lakeland: The Ostall Imperative  */ {136, [AlliedSociety(Tooltip.AlliedSocietiesDwarf, Icons.DwarfAlliedSocietyIcon)]},
    /* The Rak'tika Greatwood: Fanow    */ {143, [AlliedSociety(Tooltip.AlliedSocietiesQitari, Icons.QitariAlliedSocietyIcon)]},
    /* Il Mheg: Lydha Lran              */ {144, [AlliedSociety(Tooltip.AlliedSocietiesPixie, Icons.PixieAlliedSocietyIcon), CustomDelivery(Tooltip.CustomDeliveriesAnden)]},
    /* Il Mheg: Wolekdorf               */ {146, [DeepDungeon(Tooltip.PilgrimsTraverse)]},

    // ----------- Endwalker -------------
    /* Labyrinthos: Sharlayan Hamlet    */ {167, [CustomDelivery(Tooltip.CustomDeliveriesMargrat)]},
    /* Thavnair: Yedlihmad              */ {169, [AlliedSociety(Tooltip.AlliedSocietiesArkasodara, Icons.ArkasodaraAlliedSocietyIcon)]},
    /* Mare Lamentorum: Bestway Burrow  */ {175, [AlliedSociety(Tooltip.AlliedSocietiesLoporrit, Icons.LoporritAlliedSocietyIcon), ContentInfo(Categories.RestorationContent, Tooltip.CosmicExploration, Icons.CosmicExplorationIcon, 30, -2, -3)]},
    /* Ultima Thule: Base Omicron       */ {181, [AlliedSociety(Tooltip.AlliedSocietiesOmicron, Icons.OmicronAlliedSocietyIcon)]},
    /* Old Sharlayan                    */ {182, [MarketBoard(), SummoningBell(), CustomDelivery(Tooltip.CustomDeliveriesAmeliance)]},
    /* Radz-at-Han                      */ {183, [SummoningBell()]},

    // ----------- Dawntrail -------------
    /* Urqopacha: Worlar's Echo         */ {201, [AlliedSociety(Tooltip.AlliedSocietiesYokHuy, Icons.YokHuyAlliedSocietyIcon)]},
    /* Yak T'el: Mamook                 */ {206, [AlliedSociety(Tooltip.AlliedSocietiesMamoolJa, Icons.MamoolJaAlliedSocietyIcon)]},
    /* Shaaloani: Sheshenewezi Springs  */ {208, [CustomDelivery(Tooltip.CustomDeliveriesNitowikwe)]},
    /* Tulitollal                       */ {216, [MarketBoard(), SummoningBell(), FieldOperation(Tooltip.OccultCresent)]},
    /* Solution Nine                    */ {217, [SummoningBell()]},
    /* Kozama'uka: Dock Poga            */ {238, [AlliedSociety(Tooltip.AlliedSocietiesPeluPelu, Icons.PelupeluAlliedSocietyIcon)]},
    };
}

