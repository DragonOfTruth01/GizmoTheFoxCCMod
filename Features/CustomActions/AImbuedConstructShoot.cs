using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class AImbuedConstructShoot : CardAction
{
    // This method is called by each cantrip, every time one is played
    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0;

        SortedList<int, CardAction> sortedList = new SortedList<int, CardAction>();

        foreach (KeyValuePair<int, StuffBase> item in c.stuff)
        {
            // Find each Imbued Stone Construct to shoot from
            if(item.Value is MidrowImbuedStoneConstruct)
            {
                MidrowImbuedStoneConstruct imbuedConstruct = (MidrowImbuedStoneConstruct)item.Value;
                AAttack atk = new AAttack()
                {
                    damage = imbuedConstruct.AttackDamage()
                };

                // Originate the attack from this drone
                atk.fromDroneX = item.Value.x;
                atk.fast = true;

                sortedList.Add(item.Value.x, atk);
            }
        }

        c.QueueImmediate(sortedList.Values);
    }
}