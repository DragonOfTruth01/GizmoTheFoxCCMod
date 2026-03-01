using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardFlutter : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Flutter", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Flutter", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 0
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
                    new AMove()
                    {
                        isRandom = true,
                        dir = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        isRandom = true,
                        dir = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    },
                    new AStatus()
                    {
                        status = Status.hermes,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        isRandom = true,
                        dir = 1,
                        targetPlayer = true
                    },
                    new AStatus()
                    {
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    },
                    new AStatus()
                    {
                        status = Status.engineStall,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}
