using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardSteamEngine : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Steam Engine", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Steam Engine", "name"]).Localize
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

        discount = upgrade == Upgrade.B ? -1 : 0;

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
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.FireBitMask,
                        disabled = !flipped
                    },
                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.FireBitMask,
                        disabled = !flipped
                    },
                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        statusAmount = 2,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = ModEntry.Instance.WindCharge.Status,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAttuneMulti()
                    {
                        elementBitfieldModifier1 = AttunementManager.WindBitMask,
                        elementBitfieldModifier2 = AttunementManager.FireBitMask,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.overdrive,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAttuneMulti()
                    {
                        elementBitfieldModifier1 = AttunementManager.FireBitMask,
                        elementBitfieldModifier2 = AttunementManager.WindBitMask,
                        disabled = !flipped
                    },
                    new AStatus()
                    {
                        status = Status.energyNextTurn,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}
