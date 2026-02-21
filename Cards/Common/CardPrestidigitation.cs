using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardPrestidigitation : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Prestidigitation", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Prestidigitation", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = upgrade == Upgrade.B ? (flipped ? ModEntry.Instance.GizmoTheFoxCCMod_CardPrestidigitationBGBottomCondensed.Sprite : ModEntry.Instance.GizmoTheFoxCCMod_CardPrestidigitationBGTopCondensed.Sprite)
                                       : (flipped ? ModEntry.Instance.GizmoTheFoxCCMod_CardPrestidigitationBGBottom.Sprite : ModEntry.Instance.GizmoTheFoxCCMod_CardPrestidigitationBGTop.Sprite),
            cost = 1,
            floppable = true,
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        disabled = flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip2,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip2,
                        disabled = !flipped
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 1),
                        disabled = flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip4,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip4,
                        disabled = !flipped
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 2),
                        disabled = flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip2,
                        disabled = flipped
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip2,
                        disabled = !flipped
                    }
                };
                break;
        }
        return actions;
    }
}
