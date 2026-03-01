using Nickel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardMagicMissile : Card, IGizmoTheFoxCCModCard
{
    private static IKokoroApi.IV2.IConditionalApi Conditional => ModEntry.Instance.KokoroApi.Conditional;

    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Magic Missile", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Magic Missile", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 1
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:

            CardAction act2CardU = GenerateAttuneConditionalAttack(s, 2, false);
            CardAction act3CardU = GenerateAttuneConditionalAttack(s, 3, false);

                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 1)
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act2CardU,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            disabled = GetNumAttunedElements(s) < 2
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act3CardU,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            disabled = GetNumAttunedElements(s) < 3
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
                CardAction act1CardA = GenerateAttuneConditionalAttack(s, 1, false);
                CardAction act2CardA = GenerateAttuneConditionalAttack(s, 2, false);
                CardAction act3CardA = GenerateAttuneConditionalAttack(s, 3, false);

                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 1)
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardA,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            disabled = GetNumAttunedElements(s) < 1
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act2CardA,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            disabled = GetNumAttunedElements(s) < 2
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act3CardA,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            disabled = GetNumAttunedElements(s) < 3
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

            case Upgrade.B:
                CardAction act2CardB = GenerateAttuneConditionalAttack(s, 2, true);
                CardAction act3CardB = GenerateAttuneConditionalAttack(s, 3, true);

                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 1),
                        piercing = true
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act2CardB,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            piercing = true,
                            disabled = GetNumAttunedElements(s) < 2
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act3CardB,
                        new AAttack()
                        {
                            damage = GetDmg(s, 1),
                            piercing = true,
                            disabled = GetNumAttunedElements(s) < 3
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

    private CardAction GenerateAttuneConditionalAttack(State s, int attuneAmount, bool isPiercing)
    {
        IKokoroApi.IV2.IConditionalApi.IConditionalAction condAct = Conditional.MakeAction(
            Conditional.Equation(
                Conditional.Status(ModEntry.Instance.AttunementCount.Status),
                IKokoroApi.IV2.IConditionalApi.EquationOperator.GreaterThanOrEqual,
                Conditional.Constant(attuneAmount),
                IKokoroApi.IV2.IConditionalApi.EquationStyle.Possession
            ).SetShowOperator(false),
            new AAttack(){
                damage = GetDmg(s, 1),
                piercing = isPiercing
            }
        );

        condAct.SetFadeUnsatisfied(false);
        CardAction cardAct = condAct.AsCardAction;
        cardAct.disabled = GetNumAttunedElements(s) < attuneAmount;
        
        return cardAct;
    }

    private int GetNumAttunedElements(State s)
    {
        int retVal = 0;
        int currAttunement = s.ship.Get(ModEntry.Instance.Attunement.Status);

        List<int> masks = new List<int>{AttunementManager.EarthBitMask,
                                        AttunementManager.WindBitMask,
                                        AttunementManager.FireBitMask,
                                        AttunementManager.WaterBitMask};
        foreach (int mask in masks)
        {
            if( (currAttunement & mask) != 0)
            {
                ++retVal;
            }
        }

        return retVal;
    }
}
