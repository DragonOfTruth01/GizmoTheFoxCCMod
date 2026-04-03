using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardPrismaticWall : Card, IGizmoTheFoxCCModCard
{
    private static IKokoroApi.IV2.IConditionalApi Conditional => ModEntry.Instance.KokoroApi.Conditional;

    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Prismatic Wall", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Prismatic Wall", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellRare.Sprite,
            cost = upgrade == Upgrade.B ? 3 : 2
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
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.EarthBitMask),
                        new AStatus(){
                            status = Status.shield,
                            statusAmount = 3,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WindBitMask),
                        new AStatus(){
                            status = Status.evade,
                            statusAmount = 1,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.FireBitMask),
                        new AStatus(){
                            status = Status.tempPayback,
                            statusAmount = 2,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WaterBitMask),
                        new AStatus(){
                            status = Status.tempShield,
                            statusAmount = 4,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    new AStatus()
                    {
                        mode = AStatusMode.Set,
                        status = ModEntry.Instance.Attunement.Status,
                        statusAmount = 0,
                        targetPlayer = true
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.EarthBitMask),
                        new AStatus(){
                            status = Status.shield,
                            statusAmount = 3,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WindBitMask),
                        new AStatus(){
                            status = Status.evade,
                            statusAmount = 1,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.FireBitMask),
                        new AStatus(){
                            status = Status.tempPayback,
                            statusAmount = 2,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WaterBitMask),
                        new AStatus(){
                            status = Status.tempShield,
                            statusAmount = 4,
                            targetPlayer = true
                        }
                    ).AsCardAction
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.EarthBitMask),
                        new AStatus(){
                            status = Status.shield,
                            statusAmount = 4,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WindBitMask),
                        new AStatus(){
                            status = Status.evade,
                            statusAmount = 2,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.FireBitMask),
                        new AStatus(){
                            status = Status.tempPayback,
                            statusAmount = 3,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WaterBitMask),
                        new AStatus(){
                            status = Status.tempShield,
                            statusAmount = 5,
                            targetPlayer = true
                        }
                    ).AsCardAction,
                    new AStatus()
                    {
                        mode = AStatusMode.Set,
                        status = ModEntry.Instance.Attunement.Status,
                        statusAmount = 0,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}
