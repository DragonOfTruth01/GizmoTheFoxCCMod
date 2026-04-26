using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardMaceOfSeasonsAutumn : Card, IGizmoTheFoxCCModCard
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Mace of Seasons (Autumn)", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Mace of Seasons (Autumn)", "name"]).Localize
        });
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
		=> new HashSet<ICardTraitEntry> { ModEntry.Instance.Immutable };

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 1,
            exhaust = true,
            temporary = true,
            retain = upgrade == Upgrade.B
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
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsWinter()
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 3)
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsWinter() { upgrade = Upgrade.A }
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },
                    new AStatus()
                    {
                        status = Status.shield,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsWinter() { upgrade = Upgrade.B }
                    }
                };
                break;
        }
        return actions;
    }
}
