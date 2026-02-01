using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardEvocation : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Evocation", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Evocation", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Evocation", "description", upgrade.ToString()]),
            cost = 1,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellCommon.Sprite
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
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantrip4
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantripA
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new ACustomAddCantrip()
                    {
                        cantripType = ACustomAddCantrip.AddCantripType.addCantripB
                    }
                };
                break;
        }
        return actions;
    }
}
