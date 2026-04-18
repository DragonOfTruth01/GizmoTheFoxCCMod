using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDarkLightning : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Dark Lightning", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Dark Lightning", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellRare.Sprite,
            cost = upgrade == Upgrade.A ? 2 : upgrade == Upgrade.B ? 0 : 3
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
                    new ADarkLightningVariableHint()
                    {
                        status = ModEntry.Instance.EnemyMissingHull.Status
                    },
                    new AAttack(){
                        damage = 0,
                        piercing = true,
                        xHint = 1
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AVariableHint()
                    {
                        status = ModEntry.Instance.EnemyMissingHull.Status
                    },
                    new AAttack(){
                        damage = 0,
                        piercing = true,
                        xHint = 1
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AVariableHint()
                    {
                        status = ModEntry.Instance.EnemyMissingHull.Status
                    },
                    new AAttack(){
                        damage = 0,
                        piercing = true,
                        xHint = 1
                    }
                };
                break;
        }
        return actions;
    }
}
