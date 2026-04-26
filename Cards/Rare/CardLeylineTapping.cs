using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardLeylineTapping : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Leyline Tapping", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Leyline Tapping", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = upgrade == Upgrade.A ? 0 : upgrade == Upgrade.B ? 2 : 1,
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
                    new AStatus
                    {
                        status = ModEntry.Instance.Accumulate.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AStatus
                    {
                        status = ModEntry.Instance.Accumulate.Status,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AStatus
                    {
                        status = ModEntry.Instance.Accumulate.Status,
                        statusAmount = 2,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}
