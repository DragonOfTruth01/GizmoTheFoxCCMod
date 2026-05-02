using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDisplacerPotion : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Displacer Potion", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                rarity = Rarity.rare
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Displacer Potion", "name"]).Localize
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
            flippable = true,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_ShimmeringPotion_CardOverlay.Sprite
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        actions = new()
        {
            new AMove()
            {
                dir = 7,
                targetPlayer = true
            }
        };
        
        return actions;
    }
}
