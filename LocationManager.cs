using System;
using System.Collections.Generic;

namespace BetterTeleportPlugin;

public class LocationIDs
{
    //Tabs
    public required uint[] AllIDs { get; set; }
    public required uint[] LaNosceaIDs { get; set; }
    public required uint[] BlackShroudIDs { get; set; }
    public required uint[] ThanalanIDs { get; set; }
    public required uint[] IshgardIDs { get; set; }
    public required uint[] GyrAbaniaIDs { get; set; }
    public required uint[] FarEastIDs { get; set; }
    public required uint[] IlsabardIDs { get; set; }
    public required uint[] TuralIDs { get; set; }
    public required uint[] IndependentNationsIDs { get; set; }
    public required uint[] NorvrandtIDs { get; set; }
    public required uint[] BeyondTheSourceIDs { get; set; }
    public required uint[] OtherIDs { get; set; }
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

    public static LocationIDs locationIDs = new LocationIDs
    {
        AllIDs = new uint[] { /*56, 57, 58, 59, 60, 61, 96, 97, 164, 165,*/ 8, 52, 10, 11, 12, 13, 14, 15, 16, 55, 2, 3, 4, 5, 6, 7, 9, 17, 53, 18, 19, 20, 21, 22, 62, 70, 23, 71, 72, 73, 74, 75, 76, 77, 78, 79, 104,
                                98, 99, 100, 101, 102, 103, 111, 105, 106, 107, 108, 109, 110, 128, 127, 183, 169, 170, 171, 172, 173, 216, 200, 201, 202, 203, 204, 238, 205,
                                206, 217, 207, 208, 209, 210, 211, 212, 24, 182, 166, 167, 168, 133, 134, 132, 136, 137, 138, 139, 140, 161, 141, 144, 145, 146, 142, 143, 147,
                                148, 174, 175, 179, 180, 181, 176, 177, 178, 213, 214, 215 },
        LaNosceaIDs = new uint[] { 8, 52, 10, 11, 12, 13, 14, 15, 16, 55 },
        BlackShroudIDs = new uint[] { 2, 3, 4, 5, 6, 7 },
        ThanalanIDs = new uint[] { 9, 17, 53, 18, 19, 20, 21, 22, 62 },
        IshgardIDs = new uint[] { 70, 23, 71, 72, 73, 74, 75, 76, 77, 78, 79 },
        GyrAbaniaIDs = new uint[] { 104, 98, 99, 100, 101, 102, 103 },
        FarEastIDs = new uint[] { 111, 105, 106, 107, 108, 109, 110, 128, 127 },
        IndependentNationsIDs = new uint[] { 24, 182, 166, 167, 168 },
        IlsabardIDs = new uint[] { 183, 169, 170, 171, 172, 173 },
        TuralIDs = new uint[] { 216, 200, 201, 202, 203, 204, 238, 205, 206, 217, 207, 208, 209, 210, 211, 212 },
        NorvrandtIDs = new uint[] { 133, 134, 132, 136, 137, 138, 139, 140, 161, 141, 144, 145, 146, 142, 143, 147, 148 },
        BeyondTheSourceIDs = new uint[] { 174, 175, 179, 180, 181, 176, 177, 178, 213, 214, 215 },
        OtherIDs = new uint[] { },
        FavouriteIDs = new uint[] { },

        MarketBoardIDs = new uint[] { 8, 2, 9, 70, 111, 133, 182, 216 },
        SummoningBellIDs = new uint[] { 8, 2, 24, 9, 62, 70, 75, 104, 111, 127, 133, 134, 182, 183, 216, 217 },
        AlliedSocietyIDs = new uint[] { 19, 4, 16, 14, 7, 73, 77, 79, 105, 99, 128, 144, 143, 136, 169, 175, 181, 238, 206, 201 },
        CustomDeliveriesIDs = new uint[] { 75, 104, 105, 134, 70, 182, 144, 167, 208 },
        DeepDungeonIDs = new uint[] { 5, 106, 24, 146 },
        RestorationContentIDs = new uint[] { 127, 70, 10, 175 },
        FieldOperationIDs = new uint[] { 111, 127, 216 }
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

    /*public static void GetCurrentResidentialIDs()
    {
        uint[] ResidentialIDList = new uint[] { 56, 57, 58, 59, 60, 61, 96, 97, 164, 165 };
        List<uint> rIDs = new List<uint>();
        foreach (uint id in ResidentialIDList)
        {
            if (TeleportManager.IsAttuned(id))
            {
                rIDs.Add(id);
            }
        }
        currentResidentialIDs = rIDs.ToArray();
    }*/
}
        
