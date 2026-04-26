using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardMaceOfSeasonsWinter : Card, IGizmoTheFoxCCModCard, IHasCustomCardTraits
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Mace of Seasons (Winter)", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.rare,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Mace of Seasons (Winter)", "name"]).Localize
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
                    new AStatus()
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsSpring(),
                        destination = CardDestination.Hand
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 3)
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsSpring() { upgrade = Upgrade.A },
                        destination = CardDestination.Hand
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AStatus()
                    {
                        status = Status.libra,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new AAttack()
                    {
                        damage = GetDmg(s, 2)
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WaterBitMask
                    },
                    new AAddCard()
                    {
                        card = new CardMaceOfSeasonsSpring() { upgrade = Upgrade.B },
                        destination = CardDestination.Hand
                    }
                };
                break;
        }
        return actions;
    }
}
