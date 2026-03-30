using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardDischargedCapacitor : Card, IGizmoTheFoxCCModCard
{
    private static IKokoroApi.IV2.IConditionalApi Conditional => ModEntry.Instance.KokoroApi.Conditional;

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Discharged Capacitor", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = Deck.trash,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Discharged Capacitor", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 1,
            temporary = true,
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
                CardAction act1CardU = GenerateAttuneConditionalAddCard(s, 3, Upgrade.None);

                actions = new()
                {
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardU,
                        new AAddCard()
                        {
                            card = new CardArcaneCapacitor() { upgrade = Upgrade.None, temporaryOverride = true },
                            destination = CardDestination.Hand,
                            amount = 1,
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
                CardAction act1CardA = GenerateAttuneConditionalAddCard(s, 2, Upgrade.A);

                actions = new()
                {
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardA,
                        new AAddCard()
                        {
                            card = new CardArcaneCapacitor() { upgrade = Upgrade.A, temporaryOverride = true },
                            destination = CardDestination.Hand,
                            amount = 1,
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
                CardAction act1CardB = GenerateAttuneConditionalAddCard(s, 3, Upgrade.B);

                actions = new()
                {
                    ModEntry.Instance.KokoroApi.SpoofedActions.MakeAction(
                        act1CardB,
                        new AAddCard()
                        {
                            card = new CardArcaneCapacitor() { upgrade = Upgrade.B, temporaryOverride = true },
                            destination = CardDestination.Hand,
                            amount = 1,
                            disabled = GetNumAttunedElements(s) < 3
                        }
                    ).AsCardAction,
                    new ADrawCard()
                    {
                        count = 2
                    },
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

    private CardAction GenerateAttuneConditionalAddCard(State s, int attuneAmount, Upgrade u)
    {
        IKokoroApi.IV2.IConditionalApi.IConditionalAction condAct = Conditional.MakeAction(
            Conditional.Equation(
                Conditional.Status(ModEntry.Instance.AttunementCount.Status),
                IKokoroApi.IV2.IConditionalApi.EquationOperator.GreaterThanOrEqual,
                Conditional.Constant(attuneAmount),
                IKokoroApi.IV2.IConditionalApi.EquationStyle.Possession
            ).SetShowOperator(false),
            new AAddCard(){
                card = new CardDischargedCapacitor() { upgrade = u, temporaryOverride = true },
                destination = CardDestination.Hand,
                amount = 1
            }
        );

        condAct.SetFadeUnsatisfied(false);
        CardAction cardAct = condAct.AsCardAction;
        cardAct.disabled = GetNumAttunedElements(s) < attuneAmount;
        
        return cardAct;
    }

    private int GetNumAttunedElements(State s)
    {
        // If not looking at this card in combat,
        // consider three elements attuned for rendering purposes
        if (s.route == null)
        {
            return 3;
        }
        
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
