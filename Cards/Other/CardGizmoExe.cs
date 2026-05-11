using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardGizmoEXE : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Gizmo.EXE", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.colorless,
                rarity = Rarity.uncommon,
                dontOffer = false,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Gizmo.EXE", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Gizmo.EXE", "description", upgrade.ToString()]),
            cost = upgrade == Upgrade.A ? 0 : 1,
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
                    new AAttuneRandomRepeater()
                    {
                        execCount = 2
                    },
                    new ACardOffering
                        {
                            amount = 3,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1
                        }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttuneRandomRepeater()
                    {
                        execCount = 2
                    },
                    new ACardOffering
                        {
                            amount = 3,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1
                        }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttuneRandomRepeater()
                    {
                        execCount = 3
                    },
                    new ACardOffering
                        {
                            amount = 5,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                            makeAllCardsTemporary = true,
                            overrideUpgradeChances = false,
                            canSkip = false,
                            inCombat = true,
                            discount = -1
                        }
                };
                break;
        }
        return actions;
    }
}
