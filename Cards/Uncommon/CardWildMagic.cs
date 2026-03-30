using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardWildMagic : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Wild Magic", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Wild Magic", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = upgrade == Upgrade.A ? 0 : 1,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellUncommon.Sprite,
            floppable = true,
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
                    new AMove()
                    {
                        dir = 2,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.None,
                        execCount = 2,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AMove()
                    {
                        dir = 4,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.None,
                        execCount = 2,
                        disabled = !flipped
                    },
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.None,
                        execCount = 2,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AMove()
                    {
                        dir = 4,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.None,
                        execCount = 2,
                        disabled = !flipped
                    },
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AMove()
                    {
                        dir = 2,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.B,
                        execCount = 2,
                        disabled = flipped
                    },
                    new ADummyAction(),
                    new AMove()
                    {
                        dir = 4,
                        isRandom = true,
                        targetPlayer = true,
                        disabled = !flipped
                    },
                    new AAddRandomCantrip()
                    {
                        upgr = Upgrade.B,
                        execCount = 2,
                        disabled = !flipped
                    },
                };
                break;
        }
        return actions;
    }
}
