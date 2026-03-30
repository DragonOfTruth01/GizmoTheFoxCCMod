using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;
using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardHomunculus : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Homunculus", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Homunculus", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = upgrade == Upgrade.B ? 2 : 1,
            flippable = upgrade == Upgrade.B
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
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    },
                    new ASpawn()
                    {
                        thing = new MidrowStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    },
                    new ASpawn()
                    {
                        thing = new MidrowImbuedStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    },
                    new ASpawn()
                    {
                        thing = new MidrowStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    },
                    new ASpawn()
                    {
                        thing = new MidrowStoneConstruct()
                        {
                            yAnimation = 0.0
                        },
                        offset = 1
                    },
                };
                break;
        }
        return actions;
    }
}
