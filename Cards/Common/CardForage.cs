using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardForage : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Forage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Forage", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 1
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
                    new AAttuneRandomRepeater
                    {
                        execCount = 1
                    },
                    new ADrawCard
                    {
                        count = 2
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttuneRandomRepeater
                    {
                        execCount = 1
                    },
                    new ADrawCard
                    {
                        count = 3
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttuneRandomRepeater
                    {
                        execCount = 2
                    },
                    new ADrawCard
                    {
                        count = 2
                    }
                };
                break;
        }
        return actions;
    }
}
