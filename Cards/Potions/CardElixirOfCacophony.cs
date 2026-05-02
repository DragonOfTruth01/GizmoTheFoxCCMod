using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardElixirOfCacophony : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Elixir of Cacophony", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck,
                rarity = Rarity.rare
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Elixir of Cacophony", "name"]).Localize
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
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_ShimmeringPotion_CardOverlay.Sprite
        };
        return data;
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        actions = new()
        {
            new AStatus()
            {
                status = Status.stunSource,
                statusAmount = 1,
                targetPlayer = true
            }
        };
        
        return actions;
    }
}
