using Nickel;
using System.Collections.Generic;
using System.Reflection;
using DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardQuickBrew : Card, IGizmoTheFoxCCModCard, IHasCustomCardTraits
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

        // Set limited on cards
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, Upgrade.None, 3);
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, Upgrade.A, 3);
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
		=> upgrade != Upgrade.B
            ? new HashSet<ICardTraitEntry> { ModEntry.Instance.KokoroApi.Limited.Trait }
            : new HashSet<ICardTraitEntry>();

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Quick Brew", "description", upgrade.ToString()]),
            cost = 0,
            exhaust = upgrade == Upgrade.B
        };
        return data;
    }
    
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        bool retainPotions = false;
        var potionBelt = s.EnumerateAllArtifacts().OfType<ArtifactPotionBelt>().FirstOrDefault();

        // If we have a potion belt, all potions retain
        if (potionBelt != null)
        {
            retainPotions = true;
        }

        switch (upgrade)
        {
            case Upgrade.None:
                Card selectedCard = CardReward.GetOffering(
                                    s: s,
                                    count: 1,
                                    limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                    rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                    inCombat: true,
                                    isEvent: false) [0]; // We only have one card so index the first one

                selectedCard.flipAnim = 0.0f;

                if (retainPotions)
                {
                    selectedCard.retainOverride = true;
                }

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
                            card = selectedCard,
                            destination = CardDestination.Hand,
                            amount = 1
                        }
                    ).AsCardAction
                };
                break;

            case Upgrade.A:
                List<Card> cardList = CardReward.GetOffering(
                                      s: s,
                                      count: 2,
                                      limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                      rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                      inCombat: true,
                                      isEvent: false);

                Card selectedCardA1 = cardList[0];
                Card selectedCardA2 = cardList[1];

                selectedCardA1.flipAnim = 1.0f;
                selectedCardA2.flipAnim = 1.0f;

                if (retainPotions)
                {
                    selectedCardA1.retainOverride = true;
                    selectedCardA2.retainOverride = true;
                }

                actions = new()
                {
                    new ASpecificCardOffering()
                    {
                        Destination = CardDestination.Hand,
                        Cards = [
                            selectedCardA1,
                            selectedCardA2
                        ]
                    }
                };
                break;

            case Upgrade.B:
                Card selectedCardB1 = CardReward.GetOffering(
                                    s: s,
                                    count: 1,
                                    limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                    rarityOverride: Rarity.uncommon, // Non-shimmering potions
                                    inCombat: true,
                                    isEvent: false) [0]; // We only have one card so index the first one

                Card selectedCardB2 = CardReward.GetOffering(
                                    s: s,
                                    count: 1,
                                    limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                    rarityOverride: Rarity.uncommon,
                                    inCombat: true,
                                    isEvent: false) [0];

                Card selectedCardB3 = CardReward.GetOffering(
                                    s: s,
                                    count: 1,
                                    limitDeck: ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                                    rarityOverride: Rarity.uncommon,
                                    inCombat: true,
                                    isEvent: false) [0];

                selectedCardB1.flipAnim = 0.0f;
                selectedCardB2.flipAnim = 0.0f;
                selectedCardB3.flipAnim = 0.0f;

                if (retainPotions)
                {
                    selectedCardB1.retainOverride = true;
                    selectedCardB2.retainOverride = true;
                    selectedCardB3.retainOverride = true;
                }

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
                            card = selectedCardB1,
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
                            card = selectedCardB2,
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
                            card = selectedCardB3,
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
