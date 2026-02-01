using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardPotionOfHaste : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Potion of Haste", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                rarity = Rarity.common
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Potion of Haste", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 0,
            exhaust = true,
            temporary = true,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Potion_CardFrame.Sprite
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        actions = new()
        {
            new ADrawCard()
            {
                count = 1
            },
            new AEnergy()
            {
                changeAmount = 1
            }
        };
        
        return actions;
    }
}
