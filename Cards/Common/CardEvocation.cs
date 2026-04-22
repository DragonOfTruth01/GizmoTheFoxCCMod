using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardEvocation : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Evocation", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Evocation", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_CardEvocationBG.Sprite,
            cost = 1,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellCommon.Sprite
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
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip4,
                        dest = CardDestination.Hand
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantripA,
                        dest = CardDestination.Hand
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantripB,
                        dest = CardDestination.Hand
                    }
                };
                break;
        }
        return actions;
    }
}
