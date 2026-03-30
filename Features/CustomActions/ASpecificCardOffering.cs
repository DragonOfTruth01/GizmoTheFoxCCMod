using HarmonyLib;
using Microsoft.Extensions.Logging;
using Nickel;
using System.Collections.Generic;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class ASpecificCardOffering : CardAction
{
	public List<Card> Cards { get; set; } = [];
	public bool CanSkip { get; set; } = false;
	public required CardDestination Destination { get; set; }

	public override Route? BeginWithRoute(G g, State s, Combat c)
	{
		timer = 0;
		return new CustomCardReward
		{
			cards = Cards.Select(c =>
			{
				c.drawAnim = 1;
				c.flipAnim = 1;
				return c;
			}).ToList(),
			canSkip = CanSkip,
			Destination = Destination
		};
	}

	public override List<Tooltip> GetTooltips(State s)
		=> [
			new TTGlossary("action.cardOffering")
		];

	[HarmonyPostfix]
    [HarmonyPatch(typeof(CardReward), nameof(CardReward.TakeCard))]
	private static void CardReward_TakeCard_Postfix(CardReward __instance, G g, Card card)
	{
		if (__instance is not CustomCardReward custom)
			return;
		if (g.state.route is not Combat combat)
			return;

		g.state.RemoveCardFromWhereverItIs(card.uuid);
		switch (custom.Destination)
		{
			case CardDestination.Hand:
				combat.SendCardToHand(g.state, card);
				break;
			case CardDestination.Deck:
				g.state.SendCardToDeck(card);
				break;
			case CardDestination.Discard:
				combat.SendCardToDiscard(g.state, card);
				break;
			case CardDestination.Exhaust:
				combat.SendCardToExhaust(g.state, card);
				break;
		}
	}

	public sealed class CustomCardReward : CardReward
	{
		public required CardDestination Destination { get; set; }
	}
}