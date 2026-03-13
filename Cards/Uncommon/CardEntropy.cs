using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardEntropy : Card, IGizmoTheFoxCCModCard
{
    private static IKokoroApi.IV2.IConditionalApi Conditional => ModEntry.Instance.KokoroApi.Conditional;

    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Entropy", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Entropy", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 0
        };
        return data;
    }
    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                CardAction act1CardU = GenerateAttuneConditionalEnergyGain(s, 3);

                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 1
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardU,
                        new AEnergy()
                        {
                            changeAmount = 1,
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
                CardAction act1CardA = GenerateAttuneConditionalEnergyGain(s, 2);

                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 1
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardA,
                        new AEnergy()
                        {
                            changeAmount = 1,
                            disabled = GetNumAttunedElements(s) < 2
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
                CardAction act1CardB = GenerateAttuneConditionalEnergyGain(s, 3);
                CardAction act2CardB = GenerateAttuneConditionalEnergyGainNextTurn(s, 3);

                actions = new()
                {
                    new AEnergy()
                    {
                        changeAmount = 1
                    },
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardB,
                        new AEnergy()
                        {
                            changeAmount = 1,
                            disabled = GetNumAttunedElements(s) < 3
                        }
                    ).AsCardAction,
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act2CardB,
                        new AStatus()
                        {
                            status = Status.energyNextTurn,
                            statusAmount = 1,
                            targetPlayer = true,
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

    private CardAction GenerateAttuneConditionalEnergyGain(State s, int attuneAmount)
    {
        IKokoroApi.IV2.IConditionalApi.IConditionalAction condAct = Conditional.MakeAction(
            Conditional.Equation(
                Conditional.Status(ModEntry.Instance.AttunementCount.Status),
                IKokoroApi.IV2.IConditionalApi.EquationOperator.GreaterThanOrEqual,
                Conditional.Constant(attuneAmount),
                IKokoroApi.IV2.IConditionalApi.EquationStyle.Possession
            ).SetShowOperator(false),
            new AEnergy(){
                changeAmount = 1
            }
        );

        condAct.SetFadeUnsatisfied(false);
        CardAction cardAct = condAct.AsCardAction;
        cardAct.disabled = GetNumAttunedElements(s) < attuneAmount;
        
        return cardAct;
    }

    private CardAction GenerateAttuneConditionalEnergyGainNextTurn(State s, int attuneAmount)
    {
        IKokoroApi.IV2.IConditionalApi.IConditionalAction condAct = Conditional.MakeAction(
            Conditional.Equation(
                Conditional.Status(ModEntry.Instance.AttunementCount.Status),
                IKokoroApi.IV2.IConditionalApi.EquationOperator.GreaterThanOrEqual,
                Conditional.Constant(attuneAmount),
                IKokoroApi.IV2.IConditionalApi.EquationStyle.Possession
            ).SetShowOperator(false),
            new AStatus(){
                status = Status.energyNextTurn,
                statusAmount = 1,
                targetPlayer = true
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
