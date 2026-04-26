using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardConjureMaceOfSeasons : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Conjure Mace of Seasons", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Conjure Mace of Seasons", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellRare.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Conjure Mace of Seasons", "description", upgrade.ToString()]),
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
                    new AAddRandomMaceOfSeasonsVariant()
                    {
                        upgr = Upgrade.None
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAddRandomMaceOfSeasonsVariant()
                    {
                        upgr = Upgrade.None
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAddRandomMaceOfSeasonsVariant()
                    {
                        upgr = Upgrade.B
                    }
                };
                break;
        }
        return actions;
    }
}
