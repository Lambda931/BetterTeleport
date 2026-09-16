using System.Collections.Generic;

namespace BetterTeleportPlugin;

public class IconData
{
    public static ULDLibrary ULDLibrary = new ULDLibrary
    {
        FavouritesStarIcon = new ULDLibraryData { X = 153, Y = 0, Width = 17, Height = 17 },
        FreeDestinationStarIcon = new ULDLibraryData { X = 153, Y = 20, Width = 17, Height = 17 },

        LaNosceaTabIcon = new ULDLibraryData { X = 3, Y = 45, Width = 28, Height = 26 },
        BlackShroudTabIcon = new ULDLibraryData { X = 37, Y = 45, Width = 28, Height = 26 },
        ThanalanTabIcon = new ULDLibraryData { X = 71, Y = 45, Width = 28, Height = 26 },
        IshgardTabIcon = new ULDLibraryData { X = 173, Y = 45, Width = 28, Height = 26 },
        GyrAbaniaTabIcon = new ULDLibraryData { X = 3, Y = 79, Width = 28, Height = 26 },
        FarEastTabIcon = new ULDLibraryData { X = 37, Y = 79, Width = 28, Height = 26 },
        IlsabardTabIcon = new ULDLibraryData { X = 105, Y = 79, Width = 28, Height = 26 },
        TuralTabIcon = new ULDLibraryData { X = 241, Y = 45, Width = 28, Height = 26 },
        IndependentNationsTabIcon = new ULDLibraryData { X = 173, Y = 79, Width = 28, Height = 26 },
        NorvrandtTabIcon = new ULDLibraryData { X = 71, Y = 79, Width = 28, Height = 26 },
        BeyondTheSourceTabIcon = new ULDLibraryData { X = 241, Y = 79, Width = 28, Height = 26 },
        OtherTabIcon = new ULDLibraryData { X = 105, Y = 45, Width = 28, Height = 26 },
        FavouritesTabIcon = new ULDLibraryData { X = 139, Y = 45, Width = 28, Height = 26 },

        LaNosceaTableIcon = new ULDLibraryData { X = 93, Y = 0, Width = 17, Height = 20 },
        BlackShroudTableIcon = new ULDLibraryData { X = 113, Y = 0, Width = 17, Height = 20 },
        ThanalanTableIcon = new ULDLibraryData { X = 93, Y = 20, Width = 17, Height = 20 },
        IshgardTableIcon = new ULDLibraryData { X = 133, Y = 0, Width = 17, Height = 20 },
        GyrAbaniaTableIcon = new ULDLibraryData { X = 173, Y = 0, Width = 17, Height = 20 },
        KuganeTableIcon = new ULDLibraryData { X = 193, Y = 0, Width = 17, Height = 20 },
        SharlayanTableIcon = new ULDLibraryData { X = 233, Y = 0, Width = 17, Height = 20 },
        IlsabardTableIcon = new ULDLibraryData { X = 213, Y = 20, Width = 17, Height = 20 },
        GarlemaldTableIcon = new ULDLibraryData { X = 233, Y = 20, Width = 17, Height = 20 },
        TuralTableIcon = new ULDLibraryData { X = 253, Y = 0, Width = 17, Height = 20 },
        AlexandriaTableIcon = new ULDLibraryData { X = 253, Y = 20, Width = 17, Height = 20 },
        CrystariumTableIcon = new ULDLibraryData { X = 193, Y = 20, Width = 17, Height = 20 },
        EulmoreTableIcon = new ULDLibraryData { X = 213, Y = 0, Width = 17, Height = 20 },

        MiscLocationTableIcon = new ULDLibraryData { X = 113, Y = 20, Width = 17, Height = 20 },

        Default = new ULDLibraryData { X = 0, Y = 0, Width = 0, Height = 0 },
    };

    public static IconLibrary IconLibrary = new IconLibrary()
    {
        GilIcon = 65002,
        MarketBoardIcon = 60570, 
        SummoningBellIcon = 60425,
        HousingTabIcon = 52,

        AmaljaaAlliedSocietyIcon = 65016, SylphAlliedSocietyIcon = 65017, KoboldAlliedSocietyIcon = 65019,
        SahaginAlliedSocietyIcon = 65020, IxalAlliedSocietyIcon = 65018, VanuVanuAlliedSocietyIcon = 65036,
        VathAlliedSocietyIcon = 65037, MoogleAlliedSocietyIcon = 65039, KojinAlliedSocietyIcon = 65048,
        AnantaAlliedSocietyIcon = 65047, NamazuAlliedSocietyIcon = 65063, PixieAlliedSocietyIcon = 65072,
        QitariAlliedSocietyIcon = 65076, DwarfAlliedSocietyIcon = 65079, ArkasodaraAlliedSocietyIcon = 65092,
        OmicronAlliedSocietyIcon = 65093, LoporritAlliedSocietyIcon = 65101, PelupeluAlliedSocietyIcon = 65113,
        MamoolJaAlliedSocietyIcon = 65115, YokHuyAlliedSocietyIcon = 65131,

        DomanReconstructionIcon = 60969,
        IshgardianRestorationIcon = 60993,
        IslandSanctuaryIcon = 63970,
        CosmicExplorationIcon = 63895,

        DeepDungeonIcon = 63971,
        FieldOperationsIcon = 63972,
        CustomDeliveriesIcon = 60927,

        GoldSaucerIcon = 60848,
        WonderousTailsIcon = 60926,
        PvpZoneIcon = 60459,
        MateriaTransmutationIcon = 60910,
        MaskedCarnivalIcon = 60983,
        CrucibleOfTheUnbrokenIcon = 63997
    };

    public static readonly Dictionary<uint, ULDLibraryData> IconLookup = new()
    {
        // La Noscea
        { 8, ULDLibrary.LaNosceaTableIcon }, { 52, ULDLibrary.LaNosceaTableIcon }, { 10, ULDLibrary.LaNosceaTableIcon }, 
        { 11, ULDLibrary.LaNosceaTableIcon }, { 12, ULDLibrary.LaNosceaTableIcon }, { 13, ULDLibrary.LaNosceaTableIcon },
        { 14, ULDLibrary.LaNosceaTableIcon }, { 15, ULDLibrary.LaNosceaTableIcon }, { 16, ULDLibrary.LaNosceaTableIcon }, 
        { 55, ULDLibrary.LaNosceaTableIcon },

        // Black Shroud
        { 2, ULDLibrary.BlackShroudTableIcon }, { 3, ULDLibrary.BlackShroudTableIcon }, { 4, ULDLibrary.BlackShroudTableIcon }, 
        { 5, ULDLibrary.BlackShroudTableIcon }, { 6, ULDLibrary.BlackShroudTableIcon }, { 7, ULDLibrary.BlackShroudTableIcon },

        // Thanalan
        { 9, ULDLibrary.ThanalanTableIcon }, { 17, ULDLibrary.ThanalanTableIcon }, { 53, ULDLibrary.ThanalanTableIcon }, 
        { 18, ULDLibrary.ThanalanTableIcon }, { 19, ULDLibrary.ThanalanTableIcon }, { 20, ULDLibrary.ThanalanTableIcon },
        { 21, ULDLibrary.ThanalanTableIcon }, { 22, ULDLibrary.ThanalanTableIcon }, { 62, ULDLibrary.ThanalanTableIcon },

        // Ishgard
        { 70, ULDLibrary.IshgardTableIcon }, { 23, ULDLibrary.IshgardTableIcon }, { 71, ULDLibrary.IshgardTableIcon }, 

        // GyrAbania
        { 104, ULDLibrary.GyrAbaniaTableIcon }, { 98, ULDLibrary.GyrAbaniaTableIcon }, { 99, ULDLibrary.GyrAbaniaTableIcon }, 
        { 100, ULDLibrary.GyrAbaniaTableIcon }, { 101, ULDLibrary.GyrAbaniaTableIcon }, { 102, ULDLibrary.GyrAbaniaTableIcon }, 
        { 103, ULDLibrary.GyrAbaniaTableIcon }, 

        // Kugane
        { 111, ULDLibrary.KuganeTableIcon }, 

        // Sharlayan
        { 75, ULDLibrary.SharlayanTableIcon }, { 182, ULDLibrary.SharlayanTableIcon }, { 166, ULDLibrary.SharlayanTableIcon }, 
        { 167, ULDLibrary.SharlayanTableIcon }, { 168, ULDLibrary.SharlayanTableIcon }, 

        // Ilsabard
        { 183, ULDLibrary.IlsabardTableIcon }, { 169, ULDLibrary.IlsabardTableIcon }, { 170, ULDLibrary.IlsabardTableIcon }, 
        { 171, ULDLibrary.IlsabardTableIcon },

        // Garlemald
        { 172, ULDLibrary.GarlemaldTableIcon }, { 173, ULDLibrary.GarlemaldTableIcon },

        // Tural
        { 216, ULDLibrary.TuralTableIcon }, { 200, ULDLibrary.TuralTableIcon }, { 201, ULDLibrary.TuralTableIcon }, 
        { 202, ULDLibrary.TuralTableIcon }, { 203, ULDLibrary.TuralTableIcon }, { 204, ULDLibrary.TuralTableIcon }, 
        { 238, ULDLibrary.TuralTableIcon }, { 205, ULDLibrary.TuralTableIcon }, { 206, ULDLibrary.TuralTableIcon }, 
        { 207, ULDLibrary.TuralTableIcon }, { 208, ULDLibrary.TuralTableIcon }, { 209, ULDLibrary.TuralTableIcon }, 

        // Alexandria
        { 217, ULDLibrary.AlexandriaTableIcon }, { 210, ULDLibrary.AlexandriaTableIcon }, { 211, ULDLibrary.AlexandriaTableIcon }, 
        { 212, ULDLibrary.AlexandriaTableIcon }, 

        

        // Crystarium
        { 133, ULDLibrary.CrystariumTableIcon },

        // Eulmore
        { 134, ULDLibrary.EulmoreTableIcon },
    };    
}

public class ULDLibrary
{
    public required ULDLibraryData FavouritesStarIcon { get; set; }
    public required ULDLibraryData FreeDestinationStarIcon { get; set; }

    public required ULDLibraryData LaNosceaTabIcon { get; set; }
    public required ULDLibraryData BlackShroudTabIcon { get; set; }
    public required ULDLibraryData ThanalanTabIcon { get; set; }
    public required ULDLibraryData IshgardTabIcon { get; set; }
    public required ULDLibraryData GyrAbaniaTabIcon { get; set; }
    public required ULDLibraryData FarEastTabIcon { get; set; }
    public required ULDLibraryData IlsabardTabIcon { get; set; }
    public required ULDLibraryData TuralTabIcon { get; set; }
    public required ULDLibraryData IndependentNationsTabIcon { get; set; }
    public required ULDLibraryData NorvrandtTabIcon { get; set; }
    public required ULDLibraryData BeyondTheSourceTabIcon { get; set; }
    public required ULDLibraryData OtherTabIcon { get; set; }
    public required ULDLibraryData FavouritesTabIcon { get; set; }

    public required ULDLibraryData LaNosceaTableIcon { get; set; }
    public required ULDLibraryData BlackShroudTableIcon { get; set; }
    public required ULDLibraryData ThanalanTableIcon { get; set; }
    public required ULDLibraryData IshgardTableIcon { get; set; }
    public required ULDLibraryData GyrAbaniaTableIcon { get; set; }
    public required ULDLibraryData KuganeTableIcon { get; set; }
    public required ULDLibraryData IlsabardTableIcon { get; set; }
    public required ULDLibraryData GarlemaldTableIcon { get; set; }
    public required ULDLibraryData TuralTableIcon { get; set; }
    public required ULDLibraryData AlexandriaTableIcon { get; set; }
    public required ULDLibraryData SharlayanTableIcon { get; set; }
    public required ULDLibraryData CrystariumTableIcon { get; set; }
    public required ULDLibraryData EulmoreTableIcon { get; set; }

    public required ULDLibraryData MiscLocationTableIcon { get; set; }
    public required ULDLibraryData Default { get; set; }
}

public class IconLibrary
{
    #region Misc
    public required int GilIcon { get; set; }   
    public required int MarketBoardIcon { get; set; }
    public required int SummoningBellIcon { get; set; }
    public required int HousingTabIcon { get; set; }
    #endregion

    #region Allied Societies
    public required int AmaljaaAlliedSocietyIcon { get; set; }
    public required int SylphAlliedSocietyIcon { get; set; }
    public required int KoboldAlliedSocietyIcon { get; set; }
    public required int SahaginAlliedSocietyIcon { get; set; }
    public required int IxalAlliedSocietyIcon { get; set; }
    public required int VanuVanuAlliedSocietyIcon { get; set; }
    public required int VathAlliedSocietyIcon { get; set; }
    public required int MoogleAlliedSocietyIcon { get; set; }
    public required int KojinAlliedSocietyIcon { get; set; }
    public required int AnantaAlliedSocietyIcon { get; set; }
    public required int NamazuAlliedSocietyIcon { get; set; }
    public required int PixieAlliedSocietyIcon { get; set; }
    public required int QitariAlliedSocietyIcon { get; set; }
    public required int DwarfAlliedSocietyIcon { get; set; }
    public required int ArkasodaraAlliedSocietyIcon { get; set; }
    public required int OmicronAlliedSocietyIcon { get; set; }
    public required int LoporritAlliedSocietyIcon { get; set; }
    public required int PelupeluAlliedSocietyIcon { get; set; }
    public required int MamoolJaAlliedSocietyIcon { get; set; }
    public required int YokHuyAlliedSocietyIcon { get; set; }
    #endregion

    #region Restoration Content
    public required int DomanReconstructionIcon { get; set; }
    public required int IshgardianRestorationIcon { get; set; }
    public required int IslandSanctuaryIcon { get; set; }
    public required int CosmicExplorationIcon { get; set; }
    #endregion


    #region General Content
    public required int DeepDungeonIcon { get; set; }
    public required int FieldOperationsIcon { get; set; }
    public required int CustomDeliveriesIcon { get; set; }
    
    public required int GoldSaucerIcon { get; set; }
    public required int WonderousTailsIcon { get; set; }
    public required int PvpZoneIcon { get; set; }
    public required int MateriaTransmutationIcon { get; set; }
    public required int MaskedCarnivalIcon { get; set; }
    public required int CrucibleOfTheUnbrokenIcon { get; set; }
    #endregion
}

public class ULDLibraryData()
{
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

public class IconLibraryData()
{
    public int ID { get; set; }
    public int Size { get; set; }
}





