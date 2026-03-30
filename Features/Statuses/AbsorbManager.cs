using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
internal sealed class AbsorbManager : IKokoroApi.IV2.IStatusLogicApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

	public AbsorbManager()
	{
		/* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
	}

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Ship), nameof(Ship.NormalDamage))]
	private static void Ship_NormalDamage_Postfix(Ship __instance, State s, Combat c)
	{
		if (__instance.Get(ModEntry.Instance.Absorb.Status) > 0
            && __instance.isPlayerShip)
        {
		    c.QueueImmediate(
                new AAttuneRandomRepeater()
                {
                    execCount = __instance.Get(ModEntry.Instance.Absorb.Status)
                }
            );
        }
	}
	
	public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        if( args.Timing == IKokoroApi.IV2.IStatusLogicApi.StatusTurnTriggerTiming.TurnStart
            && args.Status == Instance.Absorb.Status
            && args.Amount > 0)
        {
            args.Amount = 0;
            return false;
        }

        return false;
    }
}