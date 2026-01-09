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
using static BetterTeleportPlugin.ContentInfo;

namespace BetterTeleportPlugin;

public partial class ContentManager
{
    public ContentManager contentManager = new ContentManager();

    public enum AlliedSocietyID : byte 
    {Amaljaa = 1, Sylph = 2, Kobold = 3, Sahagin = 4, Ixal = 5, VanuVanu = 6, Vath = 7, Moogle = 8,
     Kojin = 9, Ananta = 10, Namazu = 11, Pixie = 12, Qitari = 13, Dwarf = 14, Arkasodara = 15, Omicron = 16, Loporrit = 17,
     PeluPelu = 18, MamoolJa = 19, YokHuy = 20}
    public enum QuestID : uint
    {
        MateriaTransmutation = 66999,
        IslandSanctuary = 68622,
        //IslandSanctuary = 70179
    }

    public static unsafe bool CheckUnlocked(ContentInfo.Categories category, Tooltip tooltip)
    {
        return tooltip switch
        {
            Tooltip.AlliedSocietiesAmaljaa => CheckAlliedSociety((byte)AlliedSocietyID.Amaljaa),
            Tooltip.AlliedSocietiesSylph => CheckAlliedSociety((byte)AlliedSocietyID.Sylph),
            Tooltip.AlliedSocietiesKobold => CheckAlliedSociety((byte)AlliedSocietyID.Kobold),
            Tooltip.AlliedSocietiesSahagin => CheckAlliedSociety((byte)AlliedSocietyID.Sahagin),
            Tooltip.AlliedSocietiesIxal => CheckAlliedSociety((byte)AlliedSocietyID.Ixal),
            Tooltip.AlliedSocietiesVanuVanu => CheckAlliedSociety((byte)AlliedSocietyID.VanuVanu),
            Tooltip.AlliedSocietiesVath => CheckAlliedSociety((byte)AlliedSocietyID.Vath),
            Tooltip.AlliedSocietiesMoogle => CheckAlliedSociety((byte)AlliedSocietyID.Moogle),
            Tooltip.AlliedSocietiesKojin => CheckAlliedSociety((byte)AlliedSocietyID.Kojin),
            Tooltip.AlliedSocietiesAnanta => CheckAlliedSociety((byte)AlliedSocietyID.Ananta),
            Tooltip.AlliedSocietiesNamazu => CheckAlliedSociety((byte)AlliedSocietyID.Namazu),
            Tooltip.AlliedSocietiesPixie => CheckAlliedSociety((byte)AlliedSocietyID.Pixie),
            Tooltip.AlliedSocietiesQitari => CheckAlliedSociety((byte)AlliedSocietyID.Qitari),
            Tooltip.AlliedSocietiesDwarf => CheckAlliedSociety((byte)AlliedSocietyID.Dwarf),
            Tooltip.AlliedSocietiesArkasodara => CheckAlliedSociety((byte)AlliedSocietyID.Arkasodara),
            Tooltip.AlliedSocietiesOmicron => CheckAlliedSociety((byte)AlliedSocietyID.Omicron),
            Tooltip.AlliedSocietiesLoporrit => CheckAlliedSociety((byte)AlliedSocietyID.Loporrit),
            Tooltip.AlliedSocietiesPeluPelu => CheckAlliedSociety((byte)AlliedSocietyID.PeluPelu),
            Tooltip.AlliedSocietiesMamoolJa => CheckAlliedSociety((byte)AlliedSocietyID.MamoolJa),
            Tooltip.AlliedSocietiesYokHuy => CheckAlliedSociety((byte)AlliedSocietyID.YokHuy),

            Tooltip.MateriaTransmutation => CheckQuestComplete((uint)QuestID.MateriaTransmutation),
            Tooltip.IslandSanctuary => CheckQuestComplete((uint)QuestID.IslandSanctuary),
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

    public static bool HasAetheryteUnlockedAlliedSocietyContent(uint aetheryteId)
    {
        if (!ContentManager.Content.TryGetValue(aetheryteId, out var entries))
            return false;

        return entries.Any(entry =>
            entry.ContentCategory == Categories.AlliedSocieties &&
            CheckUnlocked(entry.ContentCategory, entry.TooltipText)
        );
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
