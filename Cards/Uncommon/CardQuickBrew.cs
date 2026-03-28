using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardQuickBrew : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Quick Brew", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Quick Brew", "name"]).Localize
        });
    }

    public int currCost = 0;
    public int numPlaysUntilIncrease = 2;

    public override void OnExitCombat(State s, Combat c)
    {
        currCost = 0;
        numPlaysUntilIncrease = 2;
    }

    public override CardData GetData(State state)
    {
        string numPlaysString = "<c=boldPink>" + numPlaysUntilIncrease + "</c> play" + (numPlaysUntilIncrease == 1 ? "" : "s");
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Quick Brew", "description", upgrade.ToString()], new {numPlaysString}),
            cost = upgrade == Upgrade.B ? 0 : currCost,
            exhaust = upgrade == Upgrade.B
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
                            rarityOverride = Rarity.uncommon, // Non-shimmering potions
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Hand,
                            amount = 1
                        }
                    ).AsCardAction,
                    new AIncrementQuickBrew() { uuid = uuid }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new ACardOffering()
                    {
                        amount = 2,
                        limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                        canSkip = false,
                        rarityOverride = Rarity.uncommon, // Non-shimmering potions
                        inCombat = true
                    },
                    new AIncrementQuickBrew() { uuid = uuid }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        new ACardOffering()
                        {
                            amount = 1,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                            canSkip = false,
                            rarityOverride = Rarity.uncommon, // Non-shimmering potions
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Deck,
                            amount = 1
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        new ACardOffering()
                        {
                            amount = 1,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                            canSkip = false,
                            rarityOverride = Rarity.uncommon, // Non-shimmering potions
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Deck,
                            amount = 1
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        new ACardOffering()
                        {
                            amount = 1,
                            limitDeck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                            canSkip = false,
                            rarityOverride = Rarity.uncommon, // Non-shimmering potions
                            inCombat = true
                        },
                        new AAddCard()
                        {
                            card = CardReward.GetOffering(
                                s: s,
                                count: 1,
                                limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                inCombat: true,
                                isEvent: false)
                            [0], // We only have one card so index the first one
                            destination = CardDestination.Deck,
                            amount = 1
                        }
                    ).AsCardAction,
                };
                break;
        }
        return actions;
    }
}
