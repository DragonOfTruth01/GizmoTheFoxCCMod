using System;
using System.Runtime.CompilerServices;
using FMOD;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
internal sealed class ImmutableManager
{
    public static ModEntry Instance => ModEntry.Instance;

	public ImmutableManager()
	{

	}

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Card), nameof(Card.GetCurrentCost))]
	public static bool Card_GetCurrentCost_Prefix(Card __instance, State s, ref int __result)
	{
        if(ModEntry.Instance.Helper.Content.Cards.IsCardTraitActive(s, __instance, ModEntry.Instance.Immutable))
        {
            CardData dataWithOverrides = __instance.GetDataWithOverrides(s);
            CardData data = __instance.GetData(s);
            __result = Math.Max(dataWithOverrides.cost, data.cost);
            // Since we're done with our logic, return out of GetCurrentCost()
            return false;
        }

        // If the card isn't immutable, continue to the real GetCurrentCost()
        return true;
	}
}