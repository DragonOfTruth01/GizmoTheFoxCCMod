using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDimensionalStorage : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Dimensional Storage", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Dimensional Storage", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Dimensional Storage", "description", upgrade.ToString()]),
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellUncommon.Sprite,
            cost = upgrade == Upgrade.A ? 0 : upgrade == Upgrade.B ? 2 : 1,
            exhaust = upgrade != Upgrade.B
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
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    },
                    new ACardSelect()
                    {
                        browseAction = new PutDiscardedCardInYourHand(),
                        browseSource = CardBrowse.Source.DiscardPile
                    }
                };
                break;
        }
        return actions;
    }
}
