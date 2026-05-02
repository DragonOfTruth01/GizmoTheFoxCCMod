using System;
using System.Collections.Generic;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class ChooseCardToMakeTempExhaustFreeCopyOfAndPutInHand : CardAction
{
	public override void Begin(G g, State s, Combat c)
	{
		Card? card = selectedCard;
		if (card != null)
		{
			Card card2 = card.CopyWithNewId();
			card2.temporaryOverride = true;
            card2.discount = -99;
			card2.exhaustOverride = true;
			c.QueueImmediate(new AAddCard
			{
				card = card2,
				destination = CardDestination.Hand,
				amount = 1
			});
		}
	}

	public override string? GetCardSelectText(State s)
	{
		return Loc.T("action.ChooseCardToMakeTempCopyOfAndPutInHand.GetCardSelectText", "Pick a card in your hand to make a temporary copy of.");
	}

	public override List<Tooltip> GetTooltips(State s)
    => [new TTGlossary("cardtrait.discount", Math.Abs(99)), new TTGlossary("cardtrait.temporary"), new TTGlossary("cardtrait.exhaust")];
}
