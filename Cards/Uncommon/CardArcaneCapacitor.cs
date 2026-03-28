using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardArcaneCapacitor : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Arcane Capacitor", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Arcane Capacitor", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Arcane Capacitor", "description", upgrade.ToString()]),
            cost = 0,
            retain = true,
            exhaust = true
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
                    new AEnergy()
                    {
                        changeAmount = 2
                    },
                    new AAddCard()
                    {
                        card = new CardDischargedCapacitor() { upgrade = Upgrade.None },
                        destination = CardDestination.Discard,
                        amount = 1
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 2
                    },
                    new AAddCard()
                    {
                        card = new CardDischargedCapacitor() { upgrade = Upgrade.A },
                        destination = CardDestination.Discard,
                        amount = 1
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 2
                    },
                    new AAddCard()
                    {
                        card = new CardDischargedCapacitor() { upgrade = Upgrade.B },
                        destination = CardDestination.Discard,
                        amount = 1
                    }
                };
                break;
        }
        return actions;
    }
}
