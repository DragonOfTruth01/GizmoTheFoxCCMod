using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardConjureManaBlades : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Conjure Mana Blades", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Conjure Mana Blades", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Conjure Mana Blades", "description", upgrade.ToString()]),
            cost = upgrade == Upgrade.B ? 0 : 1
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardManaBladeFire(),
                        destination = CardDestination.Hand,
                        amount = 1
                    },
                    new AAddCard()
                    {
                        card = new CardManaBladeIce(),
                        destination = CardDestination.Hand,
                        amount = 1
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardManaBladeFire() { upgrade = Upgrade.A },
                        destination = CardDestination.Hand,
                        amount = 1
                    },
                    new AAddCard()
                    {
                        card = new CardManaBladeIce() { upgrade = Upgrade.A },
                        destination = CardDestination.Hand,
                        amount = 1
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAddCard()
                    {
                        card = new CardManaBladeFire(),
                        destination = CardDestination.Hand,
                        amount = 1
                    },
                    new AAddCard()
                    {
                        card = new CardManaBladeIce(),
                        destination = CardDestination.Hand,
                        amount = 1
                    }
                };
                break;
        }
        return actions;
    }
}
