namespace DragonOfTruth01.GizmoTheFoxCCMod;

using HarmonyLib;
using FSPRO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using OneOf.Types;
using System.Runtime.CompilerServices;

[HarmonyPatch]
internal sealed class AttunementManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

    public bool targetPlayer;

    public int elementBitfield; // Represented as 0bXXXX, where the Xs are earth, wind, fire, and water respectively

    public AttunementManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.StatusRendering.RegisterHook(this, 0);
    }

    private class HarmonyRef
    {
        public int oldElementBitfield;
    }

    public IKokoroApi.IV2.IStatusRenderingApi.IStatusInfoRenderer? OverrideStatusInfoRenderer(IKokoroApi.IV2.IStatusRenderingApi.IHook.IOverrideStatusInfoRendererArgs args)
	{
        if (args.Status != ModEntry.Instance.Attunement.Status)
        {
            return null;
        }

        Color[] colors = new Color[4];

        bool earth = (elementBitfield & 0b1000) != 0;
        bool wind  = (elementBitfield & 0b0100) != 0;
        bool fire  = (elementBitfield & 0b0010) != 0;
        bool water = (elementBitfield & 0b0001) != 0;

        colors[0] = earth ? new Color(0x005A4E44) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[1] = wind  ? new Color(0x0014A02E) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[2] = fire  ? new Color(0x00E74C31) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[3] = water ? new Color(0x003E71D6) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;

        return ModEntry.Instance.KokoroApi.StatusRendering.MakeBarStatusInfoRenderer().SetSegments(colors).SetRows(1);
	}

    [HarmonyPrefix]
    [HarmonyPatch(typeof(AStatus), "Begin")]
    private static void AStatus_Begin_Prefix(AStatus __instance, State s, out HarmonyRef __state)
    {
        var ship = GetShip(__instance, s);
        __state = new HarmonyRef();

        __state.oldElementBitfield = ship.Get(ModEntry.Instance.Attunement.Status);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(AStatus), "Begin")]
    private static void AStatus_Begin_Postfix(AStatus __instance, State s, Combat c, HarmonyRef __state)
    {
        var ship = GetShip(__instance, s);

        // Do this logic if all element slots are attuned
        if(ship.Get(ModEntry.Instance.Attunement.Status) >= 0b1111)
        {
            // Want to add a type of potion later, but for now just heal the player
            c.QueueImmediate(new AHeal
            {
                targetPlayer = __instance.targetPlayer,
                healAmount = 1
            });

            c.QueueImmediate(new AStatus
            {
                status = ModEntry.Instance.Attunement.Status,
                statusAmount = -ship.Get(ModEntry.Instance.Attunement.Status),
                targetPlayer = __instance.targetPlayer
            });
        }
    }

    public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        return false;
    }

    private static Ship GetShip(AStatus instance, State s)
    {
        return instance.targetPlayer ? s.ship : ((Combat)s.route).otherShip;
    }
}