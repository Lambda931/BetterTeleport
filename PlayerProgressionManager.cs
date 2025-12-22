using BetterTeleportPlugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.Graphics.Render;
using Lumina.Data;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterTeleportPlugin;

public class PlayerProgressionManager
{
    public ContentManager contentManager = new ContentManager();

    public enum AlliedSociety : byte 
    {Amaljaa = 1, Sylph = 2, Kobold = 3, Sahagin = 4, Ixal = 5, VanuVanu = 6, Vath = 7, Moogle = 8,
     Kojin = 9, Ananta = 10, Namazu = 11, Pixie = 12, Qitari = 13, Dwarf = 14, Arkasodara = 15, Omicron = 16, Loporrit = 17,
     PeluPelu = 18, MamoolJa = 19, YokHuy = 20}
    public enum QuestID : uint
    {
        MateriaTransmutation = 66999
    }

    public static unsafe bool CheckUnlocked(ContentInfo.Categories category, string name)
    {
        return name switch
        {
            "Amalj'aa Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Amaljaa),
            "Sylph Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Sylph),
            "Kobold Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Kobold),
            "Sahagin Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Sahagin),
            "Ixal Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Ixal),
            "Vanu Vanu Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.VanuVanu),
            "Vath Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Vath),
            "Moogle Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Moogle),
            "Kojin Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Kojin),
            "Ananta Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Ananta),
            "Namazu Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Namazu),
            "Pixie Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Pixie),
            "Qitari Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Qitari),
            "Dwarf Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Dwarf),
            "Arkasodara Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Arkasodara),
            "Omicron Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Omicron),
            "Loporrit Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.Loporrit),
            "Pelupelu Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.PeluPelu),
            "Mamool Ja Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.MamoolJa),
            "Yok Huy Allied Society Quests" => CheckAlliedSociety((byte)AlliedSociety.YokHuy),
            "Materia Transmutation" => CheckQuestComplete((uint)QuestID.MateriaTransmutation),
            _ => true
        };
    }
    private static bool CheckQuestComplete(uint questID)
    {
        if (QuestManager.IsQuestComplete(questID))
        {
            return true;
        }
        return false;
    }

    private static unsafe bool CheckAlliedSociety(byte index)
    {
        var playerState = PlayerState.Instance();
        var rank = playerState->GetBeastTribeRank(index);
        if(rank > 0)
        {
            return true;
        }
        return false;
    }

    /*private static unsafe bool CheckCustomDeliveries(uint npcID)
    {
        var satisfactionManager = SatisfactionSupplyManager.Instance();
        satisfactionManager->NpcInfos
        foreach (var npc in satisfactionManager)
        {
            if (npc.NpcId != npcId)
                continue;

            return npc.Rank > 0;
        }
        return false;
    }*/
}
