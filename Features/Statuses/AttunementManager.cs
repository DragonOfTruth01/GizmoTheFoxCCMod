namespace DragonOfTruth01.GizmoTheFoxCCMod;

using HarmonyLib;
using FSPRO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using OneOf.Types;
using System.Runtime.CompilerServices;
using System.Reflection.Metadata;

[HarmonyPatch]
internal sealed class AttunementManager : IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

    // Elements are represented as 0bXXXX in a bitfield,
    // where the Xs are earth, wind, fire, and water respectively
    public const int EarthBitMask = 0b1000;
    public const int WindBitMask  = 0b0100;
    public const int FireBitMask  = 0b0010;
    public const int WaterBitMask = 0b0001;

    public static readonly int AllElementBitMask = 0b1111;

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

        bool earth = (args.Amount & EarthBitMask) != 0;
        bool wind  = (args.Amount & WindBitMask) != 0;
        bool fire  = (args.Amount & FireBitMask) != 0;
        bool water = (args.Amount & WaterBitMask) != 0;

        colors[0] = earth ? new Color(0xFF796775) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[1] = wind  ? new Color(0xFF14A02E) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[2] = fire  ? new Color(0xFFE74C31) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;
        colors[3] = water ? new Color(0xFF3E71D6) : ModEntry.Instance.KokoroApi.StatusRendering.DefaultInactiveStatusBarColor;

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
        if(ship.Get(ModEntry.Instance.Attunement.Status) >= AllElementBitMask)
        {
            c.QueueImmediate(new ACardOffering()
            {
                amount = 3,
                limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                canSkip = false,
                inCombat = true
            });

            c.QueueImmediate(new AStatus
            {
                status = ModEntry.Instance.Attunement.Status,
                mode = AStatusMode.Set,
                statusAmount = 0,
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