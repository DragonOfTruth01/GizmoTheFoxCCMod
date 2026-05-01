using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDuplicationPotion : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Duplication Potion", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                rarity = Rarity.rare
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Duplication Potion", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Duplication Potion", "description"]),
            cost = 0,
            exhaust = true,
            temporary = true,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_ShimmeringPotion_CardOverlay.Sprite
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        actions = new()
        {
            new ACardSelect()
            {
                browseAction = new ChooseCardToMakeTempExhaustFreeCopyOfAndPutInHand(),
                browseSource = CardBrowse.Source.Hand
            }
        };
        
        return actions;
    }
}
