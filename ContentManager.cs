using Lumina.Data;
using System.Collections.Generic;
using System.Numerics;
using static BetterTeleportPlugin.ContentInfo;

namespace BetterTeleportPlugin;

public class ContentInfo
{
    public enum Categories { Misc, MarketBoard, AlliedSocieties, CustomDeliveries, DeepDungeons}

    public Categories ContentCategory { get; set; }
    public required string TooltipText { get; set; }
    public required int IconID { get; set; }
    public Vector2 IconSize { get; set; }
    public Vector2 IconOffset { get; set; }
}

public class ContentManager
{
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
        ContentInfo(Categories.MarketBoard,"Market Board", IconData.IconLibrary.MarketBoardIcon, 30, -2, -3);

    private static ContentInfo DeepDungeon(string text) =>
        ContentInfo(Categories.DeepDungeons, text, IconData.IconLibrary.DeepDungeonIcon, 38, -6, -6.5f);

    private static ContentInfo CustomDeliveries(string text) =>
        ContentInfo(Categories.CustomDeliveries, text, IconData.IconLibrary.CustomDeliveriesIcon, 33, -4, -4);

    public static readonly Dictionary<uint, List<ContentInfo>> Content = new()
    {
        // ARR
        {2,   [ MarketBoard() ]},
        {4,   [ ContentInfo(Categories.AlliedSocieties, "Sylph Allied Society Quests", IconData.IconLibrary.SylphAlliedSocietyIcon) ]},
        {5,   [ DeepDungeon("Deep Dungeon: Palace of the Dead") ]},
        {7,   [ ContentInfo(Categories.AlliedSocieties, "Ixal Allied Society Quests", IconData.IconLibrary.IxalAlliedSocietyIcon) ]},
        {8,   [ MarketBoard() ]},
        {9,   [ MarketBoard() ]},
        {10,  [ ContentInfo(Categories.Misc, "Island Sanctuary", IconData.IconLibrary.IslandSanctuaryIcon, 30, -2, -3) ]},
        {14,  [ ContentInfo(Categories.AlliedSocieties, "Sahagin Allied Society Quests", IconData.IconLibrary.SahaginAlliedSocietyIcon) ]},
        {16,  [ ContentInfo(Categories.AlliedSocieties, "Kobolds Allied Society Quests", IconData.IconLibrary.KoboldAlliedSocietyIcon) ]},
        {19,  [ ContentInfo(Categories.AlliedSocieties, "Amalj'aa Allied Society Quests", IconData.IconLibrary.AmaljaaAlliedSocietyIcon) ]},
        {24,  [ DeepDungeon("Deep Dungeon: Eureka Orthos") ]},
        {62,  [ ContentInfo(Categories.Misc, "The Gold Saucer", IconData.IconLibrary.GoldSaucerIcon) ]},

        // Heavensward
        {70,  [ MarketBoard(),
                CustomDeliveries("Custom Deliveries: Ehll Tou"),
                CustomDeliveries("Custom Deliveries: Charlemend") ]},

        {73,  [ ContentInfo(Categories.AlliedSocieties, "Vanu Vanu Allied Society Quests", IconData.IconLibrary.VanuVanuAlliedSocietyIcon) ]},

        {75,  [ CustomDeliveries("Custom Deliveries: Zhloe Aliapoe"),
                CustomDeliveries("Custom Deliveries: Adkiragh") ]},

        {77,  [ ContentInfo(Categories.AlliedSocieties, "Vath Allied Society Quests", IconData.IconLibrary.VathAlliedSocietyIcon) ]},
        {79,  [ ContentInfo(Categories.AlliedSocieties, "Moogle Allied Society Quests", IconData.IconLibrary.MoogleAlliedSocietyIcon) ]},

        // Stormblood
        {99,  [ ContentInfo(Categories.AlliedSocieties, "Ananta Allied Society Quests", IconData.IconLibrary.AnantaAlliedSocietyIcon) ]},
        {104, [ CustomDeliveries("Custom Deliveries: M'naago") ]},

        {105, [ ContentInfo(Categories.AlliedSocieties, "Kojin Allied Society Quests", IconData.IconLibrary.KojinAlliedSocietyIcon),
                CustomDeliveries("Custom Deliveries: Kurenai") ]},

        {106, [ DeepDungeon("Deep Dungeon: Heaven on High") ]},
        {111, [ MarketBoard() ]},
        {128, [ ContentInfo(Categories.AlliedSocieties, "Namazu Allied Society Quests", IconData.IconLibrary.NamazuAlliedSocietyIcon) ]},

        // Shadowbringers
        {133, [ MarketBoard() ]},
        {134, [ CustomDeliveries("Custom Deliveries: Kai-Shirr") ]},
        {136, [ ContentInfo(Categories.AlliedSocieties, "Dwarf Allied Society Quests", IconData.IconLibrary.DwarfAlliedSocietyIcon) ]},
        {143, [ ContentInfo(Categories.AlliedSocieties, "Qitari Allied Society Quests", IconData.IconLibrary.QitariAlliedSocietyIcon) ]},
        {144, [ ContentInfo(Categories.AlliedSocieties, "Pixie Allied Society Quests", IconData.IconLibrary.PixieAlliedSocietyIcon), 
                CustomDeliveries("Custom Deliveries: Anden") ]},
        {146, [ DeepDungeon("Deep Dungeon: Pilgrim's Traverse") ]},

        // Endwalker
        {167, [ CustomDeliveries("Custom Deliveries: Margrat") ]},
        {169, [ ContentInfo(Categories.AlliedSocieties, "Arkasodara Allied Society Quests", IconData.IconLibrary.ArkasodaraAlliedSocietyIcon) ]},
        {175, [ ContentInfo(Categories.AlliedSocieties, "Loporrit Allied Society Quests", IconData.IconLibrary.LoporritAlliedSocietyIcon) ]},
        {181, [ ContentInfo(Categories.AlliedSocieties, "Omicron Allied Society Quests", IconData.IconLibrary.OmicronAlliedSocietyIcon) ]},
        {182, [ MarketBoard(),
                CustomDeliveries("Custom Deliveries: Ameliance") ]},

        // Dawntrail
        {201, [ ContentInfo(Categories.AlliedSocieties, "Yok Huy Allied Society Quests", IconData.IconLibrary.YokHuyAlliedSocietyIcon) ]},
        {206, [ ContentInfo(Categories.AlliedSocieties, "Mamool Ja Allied Society Quests", IconData.IconLibrary.MamoolJaAlliedSocietyIcon) ]},
        {208, [ CustomDeliveries("Custom Deliveries: Nitowikwe") ]},
        {216, [ MarketBoard() ]},
        {238, [ ContentInfo(Categories.AlliedSocieties, "Pelupelu Allied Society Quests", IconData.IconLibrary.PelupeluAlliedSocietyIcon) ]},
    };
}

