using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
internal sealed class ZephyrManager : IKokoroApi.IV2.IStatusLogicApi.IHook
{
    public static ModEntry Instance => ModEntry.Instance;

    public static readonly int AllElementBitMask = 0b1111;

    public ZephyrManager()
    {
        /* We task Kokoro with the job to register our status into the game */
        Instance.KokoroApi.StatusLogic.RegisterHook(this, 0);
    }

    [HarmonyPostfix]
    [HarmonyPatch(typeof(AAttack), "Begin")]
    private static void AAttack_Begin_Postfix(AAttack __instance, State s, Combat c)
    {
        var ship = GetShip(__instance, s);

        // If we have at least 1 zephyr, immediately lose 1 zephyr and gain one evade
        if(ship.Get(ModEntry.Instance.Zephyr.Status) > 0
           && __instance.fromDroneX == null)
        {
            c.QueueImmediate(new AStatus
            {
                status = Status.evade,
                statusAmount = 1,
                targetPlayer = !__instance.targetPlayer
            });

            c.QueueImmediate(new AStatus
            {
                status = ModEntry.Instance.Zephyr.Status,
                statusAmount = -1,
                targetPlayer = !__instance.targetPlayer
            });
        }
    }

    public bool HandleStatusTurnAutoStep(IKokoroApi.IV2.IStatusLogicApi.IHook.IHandleStatusTurnAutoStepArgs args)
    {
        if( args.Timing == IKokoroApi.IV2.IStatusLogicApi.StatusTurnTriggerTiming.TurnStart
            && args.Status == Instance.Zephyr.Status
            && args.Amount > 0)
        {
            args.Amount = 0;
            return false;
        }

        return false;
    }

    private static Ship GetShip(AAttack instance, State s)
    {
        return instance.targetPlayer ? ((Combat)s.route).otherShip : s.ship;
    }
}