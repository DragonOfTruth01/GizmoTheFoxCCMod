using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardTremor : Card, IGizmoTheFoxCCModCard, IHasCustomCardTraits
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Tremor", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Tremor", "name"]).Localize
        });

        // Set all upgrades to limited 3
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, 3);
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
		=> new HashSet<ICardTraitEntry> { ModEntry.Instance.KokoroApi.Limited.Trait };

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            retain = upgrade == Upgrade.B,
            temporary = true
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
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b1000
                    },
                    new ADrawCard()
                    {
                        count = 1
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b1000
                    },
                    new ADrawCard()
                    {
                        count = 2
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = 0b1000
                    },
                    new ADrawCard()
                    {
                        count = 1
                    }
                };
                break;
        }
        return actions;
    }
}
