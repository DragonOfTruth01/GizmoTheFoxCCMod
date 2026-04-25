using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
internal sealed class AccumulateManager : IKokoroApi.IV2.IStatusLogicApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

	public AccumulateManager()
	{
		/* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
	}
	
	public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        if( args.Timing == IKokoroApi.IV2.IStatusLogicApi.StatusTurnTriggerTiming.TurnStart
            && args.Status == Instance.Accumulate.Status
            && args.Amount > 0)
        {
            args.Combat.QueueImmediate(
                new AAttuneRandomRepeater()
                {
                    execCount = args.Amount
                }
            );
        }

        return false;
    }
}