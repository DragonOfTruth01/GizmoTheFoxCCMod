using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Cards;

internal sealed class CardBloodstoneBattleaxe : Card, IGizmoTheFoxCCModCard
{
    public int damageModifier = 0;

    public static void Register(IModHelper helper)
    {
        helper.Content.Cards.RegisterCard("Bloodstone Battleaxe", new()
        {
            CardType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                deck = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                rarity = Rarity.common,
                upgradesTo = [Upgrade.A, Upgrade.B]
            },
            Name = ModEntry.Instance.AnyLocalizations.Bind(["card", "Bloodstone Battleaxe", "name"]).Localize
        });
    }
    public override CardData GetData(State state)
    {
        int baseDamage = upgrade == Upgrade.None ? 1 : 2;
        int damageCalc = baseDamage + damageModifier;

        CardData data = new CardData()
        {
            art = ModEntry.Instance.GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            description = ModEntry.Instance.Localizations.Localize(["card", "Bloodstone Battleaxe", "description", upgrade.ToString()], new { damageCalc }),
            cost = upgrade == Upgrade.B ? 2 : 1,
            buoyant = upgrade == Upgrade.A
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
                        damage = GetDmg(s, 1 + damageModifier),
                        piercing = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    }
                };
                break;

            case Upgrade.A:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 2 + damageModifier),
                        piercing = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    }
                };
                break;

            case Upgrade.B:
                actions = new()
                {
                    new AAttack()
                    {
                        damage = GetDmg(s, 2 + damageModifier),
                        piercing = true
                    },
                    new AAttune()
                    {
                        elementBitfieldModifier = AttunementManager.EarthBitMask
                    }
                };
                break;
        }
        return actions;
    }

    public override void OnExitCombat(State s, Combat c)
    {
        damageModifier = 0;
    }

    public override void AfterWasPlayed(State state, Combat c)
    {
        damageModifier++;
        
        if(upgrade == Upgrade.B)
        {
            damageModifier++;
        }
    }
}
