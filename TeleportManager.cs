using Dalamud.Game;
using Dalamud.Game.Text;
using Dalamud.Game.Text.SeStringHandling;
using Dalamud.Plugin;
using Dalamud.Plugin.Services;
using FFXIVClientStructs.FFXIV.Client.Game;
using FFXIVClientStructs.FFXIV.Client.Game.Control;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using FFXIVClientStructs.FFXIV.Client.System.Framework;
using FFXIVClientStructs.FFXIV.Client.UI.Agent;
using FFXIVClientStructs.FFXIV.Client.UI.Misc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static FFXIVClientStructs.FFXIV.Client.Game.UI.Telepo.Delegates;
using HousingAethernet = Lumina.Excel.Sheets.HousingAethernet;

namespace BetterTeleportPlugin;

public static unsafe class TeleportManager
{
    // Cache of teleports the player can actually use
    public static readonly List<TeleportInfo> AvailableTeleports = new();

    /// <summary>
    /// Updates the list of teleportable locations, including apartments the player owns.
    /// </summary>
    public static void UpdateAvailableTeleports()
    {
        var tp = Telepo.Instance();
        if (tp == null) return;

        try
        {
            tp->UpdateAetheryteList(); // Refresh the in-memory list
            AvailableTeleports.Clear();

            for (long i = 0; i < tp->TeleportList.LongCount; i++)
            {
                AvailableTeleports.Add(tp->TeleportList[i]);
            }
        }
        catch (Exception ex)
        {
            AvailableTeleports.Clear();
            BetterTeleport.Log.Error(ex, "Error updating teleport list");
        }
    }

    public static TeleportInfo? GetApartmentLocation()
    {
        UpdateAvailableTeleports();

        var apartment = AvailableTeleports.FirstOrDefault(tp => tp.IsApartment);
        if (apartment.AetheryteId == 0)
        {
            BetterTeleport.Log.Warning("No owned apartment found.");
            return apartment;
        }

        return apartment;
    }

    public static TeleportInfo? GetPersonalEstate()
    {
        UpdateAvailableTeleports();
        var estate = AvailableTeleports.FirstOrDefault(tp => tp.EstateType == EstateType.PersonalEstate);
        if (estate.AetheryteId == 0)
        {
            BetterTeleport.Log.Warning("No owned personal estate found.");
            return estate;
        }
        return estate;
    }

    public static TeleportInfo? GetFreeCompanyEstate()
    {
        UpdateAvailableTeleports();
        var fcEstate = AvailableTeleports.FirstOrDefault(tp => tp.EstateType == EstateType.FreeCompanyEstate);
        if (fcEstate.AetheryteId == 0)
        {
            BetterTeleport.Log.Warning("No Free Company estate found.");
            return null;
        }
        return fcEstate;
    }

    public static bool IsAttuned(uint aetheryte)
    {
        var teleport = Telepo.Instance();
        var localPlayer = BetterTeleport.ClientState.LocalPlayer;
        if (teleport == null)
        {
            return false;
        }
        if(localPlayer == null)

        {
            return true;
        }

        var endPtr = teleport->TeleportList.Last;
        for (var it = teleport->TeleportList.First; it != endPtr; ++it)
        {
            if (it->AetheryteId == aetheryte)
                return true;
        }

        return false;
    }

    public static void Teleport(uint aetheryte)
    {
        if (IsAttuned(aetheryte))
        {
            Telepo.Instance()->Teleport(aetheryte, 0);
        }
    }

    public static bool TeleportEstate(TeleportInfo info)
    {
        var localPlayer = Control.GetLocalPlayer();
        if (localPlayer == null)
            return false;

        var status = ActionManager.Instance()->GetActionStatus(ActionType.Action, 5);
        if (status != 0)
        {
            BetterTeleport.Log.Information($"Cannot teleport: action status {status}");
            return false;
        }

        if (info.AetheryteId != 0)
        {
            return Telepo.Instance()->Teleport(info.AetheryteId, info.SubIndex);
        }
        return false;
    }

    public static uint GetPlayerGil()
    {
        var playerGil = InventoryManager.Instance()->GetGil();
        return playerGil;
    }

    public static int GetInventoryItem(uint itemID)
    {
        int total = 0;

        var inv = InventoryManager.Instance();

        if (inv == null) return -999;

        for (int bagIdx = 0; bagIdx < 4; bagIdx++)
        {
            var bag = inv->GetInventoryContainer((InventoryType)bagIdx);
            if (bag == null) continue;

            var count = bag->Size;
            BetterTeleport.Log.Information("Count total: " + count);

            for (int i = 0; i < count; i++)
            {
                var slot = bag->Items[i];
                BetterTeleport.Log.Information("Slot: " + slot);
                if (slot.ItemId == itemID)
                    total += slot.Quantity;
                    BetterTeleport.Log.Information("Aetheryte Ticket total: " + total);
            }
        }
        return total;
    }
}

