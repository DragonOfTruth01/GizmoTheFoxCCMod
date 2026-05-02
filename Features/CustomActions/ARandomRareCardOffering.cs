using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class ARandomRareCardOffering : CardAction
{
	public int amount = 3;

	public override Route? BeginWithRoute(G g, State s, Combat c)
	{
        List<Deck> charDecks = s.storyVars.GetUnlockedChars().ToList();

        List<Deck> chosenDecks = new List<Deck>();
		List<Card> chosenCards = new List<Card>();

        for(int i = 0; i < amount; ++i)
        {
            chosenDecks.Add(charDecks[s.rngCardOfferings.NextInt() % charDecks.Count()]);
        }

		for(int i = 0; i < amount; ++i)
		{
			chosenCards.Add(
				CardReward.GetOffering(
                    s: s,
                    count: 1,
                    limitDeck: chosenDecks[i],
                    rarityOverride: Rarity.rare, // Any rare card from any character
                    inCombat: true,
                    isEvent: false,
					makeAllCardsTemporary: true,
					discount: -99
				)[0]
			);
		}

		timer = 0.0;
		return new CardReward
		{
			cards = chosenCards,
			canSkip = false
		};
	}

	public override List<Tooltip> GetTooltips(State s)
    => [new TTGlossary("cardtrait.discount", Math.Abs(99)), new TTGlossary("cardtrait.temporary")];

	public override Icon? GetIcon(State s)
	{
		return new Icon(Spr.icons_cardOffering, amount, Colors.textMain);
	}
}
