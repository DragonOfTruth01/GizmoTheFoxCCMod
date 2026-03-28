using Nickel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardFermentedTincture : Card, IGizmoTheFoxCCModCard, IHasCustomCardTraits
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Fermented Tincture", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Fermented Tincture", "name"]).Localize
        });
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
		=> new HashSet<ICardTraitEntry> { ModEntry.Instance.KokoroApi.Fleeting.Trait };

    public int currentCostBase = 8;
    public int currentCostA = 6;
    public int currentCostB = 11;

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Fermented Tincture", "description", upgrade.ToString()]),
            cost = upgrade == Upgrade.A ? currentCostA : (upgrade == Upgrade.B ? currentCostB : currentCostBase),
            singleUse = true
        };
        return data;
    }

    public override void OnDraw(State s, Combat c)
    {
        if(currentCostBase != 0)
        {
            currentCostBase--;
        }
        if(currentCostA != 0)
        {
            currentCostA--;
        }
        if(currentCostBase != 0)
        {
            currentCostB--;
        }
    }

    public override List<CardAction> GetActions(State s, Combat c)
    {
        List<CardAction> actions = new();

        switch (upgrade)
        {
            case Upgrade.None:
                actions = new()
                {
                    new AHullMax()
                    {
                        amount = 3,
                        targetPlayer = true
                    },
                    new AHeal()
                    {
                        healAmount = 3,
                        targetPlayer = true
                    },
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AHullMax()
                    {
                        amount = 3,
                        targetPlayer = true
                    },
                    new AHeal()
                    {
                        healAmount = 3,
                        targetPlayer = true
                    },
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AHullMax()
                    {
                        amount = 5,
                        targetPlayer = true
                    },
                    new AHeal()
                    {
                        healAmount = 5,
                        targetPlayer = true
                    },
                };
                break;
        }
        return actions;
    }
}
