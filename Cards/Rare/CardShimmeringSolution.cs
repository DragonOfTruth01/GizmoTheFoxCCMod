using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardShimmeringSolution : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Shimmering Solution", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Shimmering Solution", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Shimmering Solution", "description", upgrade.ToString()]),
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
                    // Need to spoof this action so we don't try to display a card before CardReward.GetOffering
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        new ACardOffering()
                        {
                            amount = 1,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                            canSkip = false,
                            rarityOverride = Rarity.rare, // Shimmering potions only
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.rare, // Shimmering potions only
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Hand,
                            amount = 1
                        }
                    ).AsCardAction
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    // Need to spoof this action so we don't try to display a card before CardReward.GetOffering
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        new ACardOffering()
                        {
                            amount = 1,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                            canSkip = false,
                            rarityOverride = Rarity.rare, // Shimmering potions only
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.rare, // Shimmering potions only
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Hand,
                            amount = 1
                        }
                    ).AsCardAction
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new ACardOffering()
                    {
                        amount = 2,
                        limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                        canSkip = false,
                        rarityOverride = Rarity.rare, // Shimmering potions only
                        inCombat = true
                    }
                };
                break;
        }
        return actions;
    }
}
