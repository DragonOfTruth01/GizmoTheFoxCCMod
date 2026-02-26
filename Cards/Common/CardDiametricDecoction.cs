using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDiametricDecoction : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Diametric Decoction", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Diametric Decoction", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = upgrade == Upgrade.A ? 0 : 1,
            exhaust = upgrade == Upgrade.A,
            recycle = upgrade == Upgrade.B,
            floppable = upgrade != Upgrade.A
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
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.FireBitMask,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask,
                        disabled = !flipped
                    },
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.FireBitMask
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask
                    },
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask,
                        disabled = flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.FireBitMask,
                        disabled = !flipped
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask,
                        disabled = !flipped
                    },
                };
                break;
        }
        return actions;
    }
}
