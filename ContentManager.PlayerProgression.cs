using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using System.Linq;
using static BetterTeleportPlugin.ContentInfo;

namespace BetterTeleportPlugin;

public partial class ContentManager
{
    public ContentManager contentManager = new ContentManager();

    public enum AlliedSocietyID : byte 
    {
        Amaljaa = 1, Sylph = 2, Kobold = 3, Sahagin = 4, Ixal = 5, VanuVanu = 6, Vath = 7, Moogle = 8,
        Kojin = 9, Ananta = 10, Namazu = 11, Pixie = 12, Qitari = 13, Dwarf = 14, Arkasodara = 15, Omicron = 16, Loporrit = 17,
        PeluPelu = 18, MamoolJa = 19, YokHuy = 20
    }
    public enum QuestID : uint
    {
        CustomDeliveryZhloe = 67087, CustomDeliveryMnaago = 68541, CustomDeliveryKurenai = 68675, CustomDeliveryAdkiragh = 68713, CustomDeliveryKaiShirr = 69265, CustomDeliveryEhllTou = 69425, CustomDeliveryCharlemend = 69615, 
        CustomDeliveryAmeliance = 70059, CustomDeliveryAnden = 70251, CustomDeliveryMargrat = 70351, CustomDeliveryNitowikwe = 70775, CustomDeliveryAuntTi = 70996,
        PalaceOfTheDead = 67092, HeavenOnHigh = 68667, EurekaOrthos = 70199, PilgrimsTraverse = 70941,
        DomanRestoration = 68677, IshgardianRestoration = 69208, IslandSanctuary = 68622, CosmicExploration = 70789,
        Eureka = 68614, Bozja = 69370, OccultCresent = 70847,
        MaskedCarnivale = 68734, CrucibleOfTheUnbroken = 71030,
        MateriaTransmutation = 66999
    }

    public static unsafe bool CheckUnlocked(ContentInfo.Categories category, Tooltip tooltip)
    {
        return tooltip switch
        {
            //Allied Societies
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

            //Custom Deliveries
            Tooltip.CustomDeliveriesZhloe => CheckQuestComplete((uint)QuestID.CustomDeliveryZhloe),
            Tooltip.CustomDeliveriesMnaago => CheckQuestComplete((uint)QuestID.CustomDeliveryMnaago),
            Tooltip.CustomDeliveriesKurenai => CheckQuestComplete((uint)QuestID.CustomDeliveryKurenai),
            Tooltip.CustomDeliveriesAdkiragh => CheckQuestComplete((uint)QuestID.CustomDeliveryAdkiragh),
            Tooltip.CustomDeliveriesKaiShirr => CheckQuestComplete((uint)QuestID.CustomDeliveryKaiShirr),
            Tooltip.CustomDeliveriesEhllTou => CheckQuestComplete((uint)QuestID.CustomDeliveryEhllTou),
            Tooltip.CustomDeliveriesCharlemend => CheckQuestComplete((uint)QuestID.CustomDeliveryCharlemend),
            Tooltip.CustomDeliveriesAmeliance => CheckQuestComplete((uint)QuestID.CustomDeliveryAmeliance),
            Tooltip.CustomDeliveriesAnden => CheckQuestComplete((uint)QuestID.CustomDeliveryAnden),
            Tooltip.CustomDeliveriesMargrat => CheckQuestComplete((uint)QuestID.CustomDeliveryMargrat),
            Tooltip.CustomDeliveriesNitowikwe => CheckQuestComplete((uint)QuestID.CustomDeliveryNitowikwe),
            Tooltip.CustomDeliveriesAuntTi => CheckQuestComplete((uint)QuestID.CustomDeliveryAuntTi),

            //Deep Dungeons
            Tooltip.PalaceOfTheDead => CheckQuestComplete((uint)QuestID.PalaceOfTheDead),
            Tooltip.HeavenOnHigh => CheckQuestComplete((uint)QuestID.HeavenOnHigh),
            Tooltip.EurekaOrthos => CheckQuestComplete((uint)QuestID.EurekaOrthos),
            Tooltip.PilgrimsTraverse => CheckQuestComplete((uint)QuestID.PilgrimsTraverse),

            //Restoration Content
            Tooltip.DomanRestoration => CheckQuestComplete((uint)QuestID.DomanRestoration),
            Tooltip.IshgardianRestoration => CheckQuestComplete((uint)QuestID.IshgardianRestoration),
            Tooltip.IslandSanctuary => CheckQuestComplete((uint)QuestID.IslandSanctuary),
            Tooltip.CosmicExploration => CheckQuestComplete((uint)QuestID.CosmicExploration),

            //Field Operations
            Tooltip.Eureka => CheckQuestComplete((uint)QuestID.Eureka),
            Tooltip.Bozja => CheckQuestComplete((uint)QuestID.Bozja),
            Tooltip.OccultCresent => CheckQuestComplete((uint)QuestID.OccultCresent),

            //Limited Jobs
            Tooltip.MaskedCarnivale => CheckQuestComplete((uint)QuestID.MaskedCarnivale),
            Tooltip.CrucibleOfTheUnbroken => CheckQuestComplete((uint)QuestID.CrucibleOfTheUnbroken),
            
            //Misc
            Tooltip.MateriaTransmutation => CheckQuestComplete((uint)QuestID.MateriaTransmutation),
            _ => true
        };
    }
    private static bool CheckQuestComplete(uint questID)
    {
        if (QuestManager.IsQuestComplete(questID))
        {
            return true;
        }
        BetterTeleport.Log.Information($"{questID} has not been completed");
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
}
