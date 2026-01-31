using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardSeaQuake : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Sea Quake", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Sea Quake", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 2,
            floppable = true
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
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 3,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b0001,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b1000,
                        disabled = !flipped
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 3,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttuneMulti()
                    {
                        elementBitfieldModifier1 = 0b1000,
                        elementBitfieldModifier2 = 0b0001,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 2,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttuneMulti()
                    {
                        elementBitfieldModifier1 = 0b0001,
                        elementBitfieldModifier2 = 0b1000,
                        disabled = !flipped
                    },
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 5,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b0001,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 3,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b1000,
                        disabled = !flipped
                    }
                };
                break;
        }
        return actions;
    }
}
