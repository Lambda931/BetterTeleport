using AetheryteRow = Lumina.Excel.Sheets.Aetheryte;
using FFXIVClientStructs.FFXIV.Client.Game.UI;

namespace BetterTeleportPlugin;

public class Aetheryte
{
    public AetheryteRow Data { get; set; }
    //public string Name { get; set; }

    public uint Id
        => Data.RowId;

    public static void AetheryteData(in uint id, out string aetheryteName, out string zoneName, out string aethernetName, out int teleportCost)
    {
        aetheryteName = AetheryteName(id);
        zoneName = AetheryteRegion(id);
        aethernetName = AethernetName(id);
        teleportCost = AetheryteCost(id);
    }

    public static string AetheryteName(uint id)
    {
        var aetheryte = BetterTeleport.DataManager.GetExcelSheet<AetheryteRow>()?.GetRow(id);
        if (aetheryte == null)
        {
            return $"Cannot find Aetheryte Region ({id})";
        }
        var aetheryteName = aetheryte?.PlaceName.Value.Name;
        return aetheryteName.ToString() ?? $"Can't find Aetheryte Region ({id})";
    }

    public static string AethernetName(uint id)
    {
        var aetheryte = BetterTeleport.DataManager.GetExcelSheet<AetheryteRow>()?.GetRow(id);
        if (aetheryte == null)
        {
            return $"Cannot find Aetheryte ({id})";
        }

        var aethernetName = aetheryte?.AethernetName.Value.Name;
        return aethernetName.ToString() ?? $"Can't find Aethernet Node ({id})";
    }

    public static string AetheryteRegion(uint id)
    {
        var aetheryte = BetterTeleport.DataManager.GetExcelSheet<AetheryteRow>()?.GetRow(id);
        if (aetheryte == null)
        {
            return $"Cannot find Aetheryte Region ({id})";
        }
        var aetheryteRegion = aetheryte?.Map.Value.PlaceName.Value.Name;
        return aetheryteRegion.ToString() ?? $"Can't find Aetheryte Region ({id})";
    }
    public static unsafe int AetheryteCost(uint id)
    {
        var telepo = Telepo.Instance();
        if (telepo == null)
        {
            return -1;
        }
        var list = telepo->TeleportList;
        var last = list.Last;

        for(var i = list.First; i != last; ++i)
        {
            if(i->AetheryteId == id)
            {
                return (int)i->GilCost;
            }
        }
        return -1;
    }

    public static unsafe int AetheryteFavourite(uint id)
    {
        var telepo = Telepo.Instance();
        if (telepo == null)
        {
            return 0;
        }
        var list = telepo->TeleportList;
        var last = list.Last;

        for (var i = list.First; i != last; ++i)
        {
            if (i->AetheryteId == id)
            {
                var favorite = i->IsFavourite;
                var free = i->IsFreeAetheryte;
                if(favorite)
                {
                    return 1;
                }
                else if(free)
                {
                    return 2;
                }
                else
                {
                    return 0;
                }
            }
        }
        return 0;
    }
}
        
