using Nickel;
using System.Collections.Generic;
using System.Reflection;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardChromaticOrb : Card, IGizmoTheFoxCCModCard
{
    private static IKokoroApi.IV2.IConditionalApi Conditional => ModEntry.Instance.KokoroApi.Conditional;

    public static void Register(IModHelper helper)
    {
        var entry = helper.Content.Cards.RegisterCard("Chromatic Orb", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.uncommon,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Chromatic Orb", "name"]).Localize
        });
    }

    public override CardData GetData(State state)
    {
        string damageString = upgrade == Upgrade.A ? GetDmg(state, 3).ToString() : GetDmg(state, 2).ToString();

        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellUncommon.Sprite,
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
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 1),
                    },
                    new AStatus()
                    {
                        status = Status.droneShift,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new MidrowStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 1),
                    },
                    new AStatus()
                    {
                        status = Status.droneShift,
                        statusAmount = 2,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new MidrowImbuedStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 2),
                        piercing = true
                    },
                    new AStatus()
                    {
                        status = Status.droneShift,
                        statusAmount = 1,
                        targetPlayer = true
                    },
                    new ASpawn()
                    {
                        thing = new MidrowImbuedStoneConstruct()
                        {
                            yAnimation = 0.0
                        }
                    }
                };
                break;
        }
        return actions;
    }
}
