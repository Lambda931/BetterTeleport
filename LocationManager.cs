using System;
using System.Collections.Generic;

namespace BetterTeleportPlugin;

public class LocationIDs
{
    //Categories
    public required uint[] LaNosceaIDs { get; set; }
    public required uint[] BlackShroudIDs { get; set; }
    public required uint[] ThanalanIDs { get; set; }
    public required uint[] CoerthasIDs { get; set; }
    public required uint[] AbalathiasSpineIDs { get; set; }
    public required uint[] DravaniaIDs { get; set; }
    public required uint[] GyrAbaniaIDs { get; set; }
    public required uint[] HingashiIDs { get; set; }
    public required uint[] OthardIDs { get; set; }
    public required uint[] MorDhonaIDs { get; set; }
    public required uint[] NorthernEmptyIDs { get; set; }
    public required uint[] IlsabardIDs { get; set; }
    public required uint[] YokTuralIDs { get; set; }
    public required uint[] XakTuralIDs { get; set; }
    public required uint[] NorvrandtIDs { get; set; }
    public required uint[] SeaOfStarsIDs { get; set; }
    public required uint[] WorldUnsunderedIDs { get; set; }
    public required uint[] UnlostWorldIDs { get; set; }
    public required uint[] FavouriteIDs { get; set; }

    //Dropdown
    public required uint[] MarketBoardIDs { get; set; }
    public required uint[] SummoningBellIDs { get; set; }
    public required uint[] AlliedSocietyIDs { get; set; }
    public required uint[] CustomDeliveriesIDs { get; set; }
    public required uint[] DeepDungeonIDs { get; set; }
    public required uint[] RestorationContentIDs { get; set; }
    public required uint[] FieldOperationIDs { get; set; }
}

public class LocationManager
{
    public static uint[] currentResidentialIDs;
    public enum TabLocation {
        All, Residential, LaNoscea, BlackShroud, Thanalan, Ishgard, GyrAbania, FarEast, IndependentNations, Ilsabard, Tural,
        Norvrandt, BeyondTheSource, Favourites, MarketBoards, SummoningBells, AlliedSocieties, CustomDeliveries, DeepDungeons,
        RestorationContent, FieldOperations
    }



    public static LocationIDs locationIDs = new LocationIDs
    {
        LaNosceaIDs = new uint[] { 8, 52, 10, 11, 12, 13, 14, 15, 16, 55 },
        BlackShroudIDs = new uint[] { 2, 3, 4, 5, 6, 7 },
        ThanalanIDs = new uint[] { 9, 17, 53, 18, 19, 20, 21, 22, 62 },
        CoerthasIDs = new uint[] { 70, 23, 71 },
        AbalathiasSpineIDs = new uint[] { 72, 73, 74 },
        DravaniaIDs = new uint[] { 75, 76, 77, 78, 79 },
        GyrAbaniaIDs = new uint[] { 104, 98, 99, 100, 101, 102, 103 },
        HingashiIDs = new uint[] { 111 },
        OthardIDs = new uint[] { 105, 106, 107, 108, 109, 110, 128, 127 },
        MorDhonaIDs = new uint[] { 24 },
        NorthernEmptyIDs = new uint[] { 182, 166, 167, 168 },
        IlsabardIDs = new uint[] { 183, 169, 170, 171, 172, 173 },
        YokTuralIDs = new uint[] { 216, 200, 201, 202, 203, 204, 238, 205, 206,},
        XakTuralIDs = new uint[] { 217, 207, 208, 209, 210, 211, 212 },
        NorvrandtIDs = new uint[] { 133, 134, 132, 136, 137, 138, 139, 140, 161, 141, 144, 145, 146, 142, 143, 147, 148 },
        SeaOfStarsIDs = new uint[] { 174, 175, 179, 180, 181},
        WorldUnsunderedIDs = new uint[] {176, 177, 178 },
        UnlostWorldIDs = new uint[] {213, 214, 215 },
        FavouriteIDs = new uint[] { },

        MarketBoardIDs = new uint[] { 8, 2, 9, 70, 111, 133, 182, 216 },
        SummoningBellIDs = new uint[] { 8, 2, 24, 9, 62, 70, 75, 104, 111, 127, 133, 134, 182, 183, 216, 217 },
        AlliedSocietyIDs = new uint[] { 19, 4, 16, 14, 7, 73, 77, 79, 105, 99, 128, 144, 143, 136, 169, 175, 181, 238, 206, 201 },
        CustomDeliveriesIDs = new uint[] { 75, 104, 105, 134, 70, 182, 144, 167, 208 },
        DeepDungeonIDs = new uint[] { 5, 106, 24, 146 },
        RestorationContentIDs = new uint[] { 127, 70, 10, 175 },
        FieldOperationIDs = new uint[] { 111, 127, 216 }
    };

    public static readonly Dictionary<TabLocation, List<SubCategories>> SubCategories = new()
    {
        {TabLocation.All,                [new SubCategories {Header = "La Noscea", ids = locationIDs.LaNosceaIDs},
                                          new SubCategories {Header = "The Black Shroud", ids = locationIDs.BlackShroudIDs},
                                          new SubCategories {Header = "Thanalan", ids = locationIDs.ThanalanIDs},
                                          new SubCategories {Header = "Coerthas", ids = locationIDs.CoerthasIDs},
                                          new SubCategories {Header = "Abalathia's Spine", ids = locationIDs.AbalathiasSpineIDs},
                                          new SubCategories {Header = "Dravania", ids = locationIDs.DravaniaIDs},
                                          new SubCategories {Header = "Gyr Abania", ids = locationIDs.GyrAbaniaIDs},
                                          new SubCategories {Header = "Hingashi", ids = locationIDs.HingashiIDs},
                                          new SubCategories {Header = "Othard", ids = locationIDs.OthardIDs},
                                          new SubCategories {Header = "Mor Dhona", ids = locationIDs.MorDhonaIDs},
                                          new SubCategories {Header = "The Northern Empty", ids = locationIDs.NorthernEmptyIDs},
                                          new SubCategories {Header = "Ilsabard", ids = locationIDs.IlsabardIDs},
                                          new SubCategories {Header = "Yok Tural", ids = locationIDs.YokTuralIDs},
                                          new SubCategories {Header = "Xak Tural", ids = locationIDs.XakTuralIDs},
                                          new SubCategories {Header = "Norvrandt", ids = locationIDs.NorvrandtIDs},
                                          new SubCategories {Header = "The Sea of Stars", ids = locationIDs.SeaOfStarsIDs},
                                          new SubCategories {Header = "The World Unsundered", ids = locationIDs.WorldUnsunderedIDs},
                                          new SubCategories {Header = "Unlost World", ids = locationIDs.UnlostWorldIDs},] },

        {TabLocation.LaNoscea,           [new SubCategories {Header = "La Noscea", ids = locationIDs.LaNosceaIDs}] },
        {TabLocation.BlackShroud,        [new SubCategories {Header = "The Black Shroud", ids = locationIDs.BlackShroudIDs}] },
        {TabLocation.Thanalan,           [new SubCategories {Header = "Thanalan", ids = locationIDs.ThanalanIDs}] },
        {TabLocation.Ishgard,            [new SubCategories {Header = "Coerthas", ids = locationIDs.CoerthasIDs},
                                          new SubCategories {Header = "Abalathia's Spine", ids = locationIDs.AbalathiasSpineIDs},
                                          new SubCategories {Header = "Dravania", ids = locationIDs.DravaniaIDs},] },
        {TabLocation.GyrAbania,          [new SubCategories {Header = "Gyr Abania", ids = locationIDs.GyrAbaniaIDs}] },
        {TabLocation.FarEast,            [new SubCategories {Header = "Hingashi", ids = locationIDs.HingashiIDs},
                                          new SubCategories {Header = "Othard", ids = locationIDs.OthardIDs},] },
        {TabLocation.IndependentNations, [new SubCategories {Header = "Mor Dhona", ids = locationIDs.MorDhonaIDs},
                                          new SubCategories {Header = "The Northern Empty", ids = locationIDs.NorthernEmptyIDs},] },
        {TabLocation.Ilsabard,           [new SubCategories {Header = "Ilsabard", ids = locationIDs.IlsabardIDs},] },
        {TabLocation.Tural,              [new SubCategories {Header = "Yok Tural", ids = locationIDs.YokTuralIDs},
                                          new SubCategories {Header = "Xak Tural", ids = locationIDs.XakTuralIDs},] },
        {TabLocation.Norvrandt,          [new SubCategories {Header = "Norvrandt", ids = locationIDs.NorvrandtIDs}] },
        {TabLocation.BeyondTheSource,    [new SubCategories {Header = "The Sea of Stars", ids = locationIDs.SeaOfStarsIDs},
                                          new SubCategories {Header = "The World Unsundered", ids = locationIDs.WorldUnsunderedIDs},
                                          new SubCategories {Header = "Unlost World", ids = locationIDs.UnlostWorldIDs}] },

        {TabLocation.Favourites,        [new SubCategories {Header = "Favourites", ids = locationIDs.FavouriteIDs}] },
    };

    public static void GetCurrentResidentialIDs()
    {
        uint[] ResidentialIDList = new uint[]
        {
        56, // Mist
        57, // Lavender Beds
        58, // Goblet
        59, // Shirogane
        60, // Empyreum
        };

        List<uint> rIDs = new();

        foreach (uint id in ResidentialIDList)
        {
            if (TeleportManager.IsAttuned(id))
            {
                // Add the main residential aetheryte
                rIDs.Add(id);

                // Add "virtual apartment ID"
                uint apartmentId = id + 1000;
                rIDs.Add(apartmentId);
            }
        }

        currentResidentialIDs = rIDs.ToArray();
    }

    public static void GetFavouriteLocations()
    {
        List<uint> favouriteIDList = new List<uint>();
        for (uint i = 0; i < 250; i++)
        {
            var IsFavourite = Aetheryte.AetheryteFavourite(i);
            if (IsFavourite != 0)
            {
                favouriteIDList.Add(i);
            }
        }
        uint[] favouriteIDs = favouriteIDList.ToArray();
        SubCategories[TabLocation.Favourites][0].ids = favouriteIDs;
    }
}

public class SubCategories
{
    public required string Header { get; set; }
    public required uint[] ids { get; set; }
}
        
