using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardGust : Card, IGizmoTheFoxCCModCard, IHasCustomCardTraits
{
    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Gust", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B],
                dontOffer = true
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Gust", "name"]).Localize
        });

        // Set limited on cards
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, Upgrade.None, 2);
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, Upgrade.A, 3);
        ModEntry.Instance.KokoroApi.Limited.SetBaseLimitedUses(entry.UniqueName, Upgrade.B, 2);
    }

    public IReadOnlySet<ICardTraitEntry> GetInnateTraits(State state)
		=> new HashSet<ICardTraitEntry> { ModEntry.Instance.KokoroApi.Limited.Trait, ModEntry.Instance.KokoroApi.Fleeting.Trait };

    public override CardData GetData(State state)
    {
        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            cost = 0,
            temporary = true,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardFrameSpellCantrip.Sprite
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
                        elementBitfieldModifier = AttunementManager.WindBitMask
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
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    },
                    new ADrawCard()
                    {
                        count = 1
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.WindBitMask
                    },
                    new ADrawCard()
                    {
                        count = 1
                    },
                    new AStatus()
                    {
                        status = Status.tempShield,
                        statusAmount = 1,
                        targetPlayer = true
                    }
                };
                break;
        }
        return actions;
    }
}
