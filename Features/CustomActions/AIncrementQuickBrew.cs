using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class AIncrementQuickBrew : CardAction
{
    public int uuid;

    // This method is called by each cantrip, every time one is played
    public override void Begin(G g, State s, Combat c)
    {
        Card? card = s.FindCard(uuid);

        if(card != null && card is CardQuickBrew cardQB)
        {
            if(cardQB.numPlaysUntilIncrease == 1)
            {
                cardQB.numPlaysUntilIncrease = 2;
                ++cardQB.currCost;
            }
            else{
                cardQB.numPlaysUntilIncrease = 1;
            }
        }
    }
}

public ref struct QuickBrewRefStruct
{
    public ref int currentCost;
    public ref int playsUntilInc;
}