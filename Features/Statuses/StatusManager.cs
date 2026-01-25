namespace DragonOfTruth01.GizmoTheFoxCCMod;

using HarmonyLib;
using FSPRO;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using OneOf.Types;

[HarmonyPatch]
internal sealed class StatusManager : IKokoroApi.IV2.IStatusLogicApi.IHook, IKokoroApi.IV2.IStatusRenderingApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

    public StatusManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
        Instance.KokoroApi.StatusRendering.RegisterHook(this, 0);
    }

    private class HarmonyRef
    {
        
    }

    public IKokoroApi.IV2.IStatusRenderingApi.IStatusInfoRenderer? OverrideStatusInfoRenderer(IKokoroApi.IV2.IStatusRenderingApi.IHook.IOverrideStatusInfoRendererArgs args)
	{
        // Hijack this for the attunement status

        return ModEntry.Instance.KokoroApi.StatusRendering.MakeBarStatusInfoRenderer().SetSegments(Array.Empty<Color>()).SetRows(1);
	}

    [HarmonyPrefix]
    [HarmonyPatch(typeof(AStatus), "Begin")]
    private static void AStatus_Begin_Prefix(AStatus __instance, State s, out HarmonyRef __state)
    {
        __state = new HarmonyRef();

    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(AStatus), "Begin")]
    private static void AStatus_Begin_Postfix(AStatus __instance, State s, Combat c, HarmonyRef __state)
    {
        
    }

    public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        return false;
    }

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Ship), "ResetAfterCombat")]
    private static void Ship_ResetAfterCombat_Prefix(Ship __instance)
    {
        
    }
}