using Nickel;
using System.Collections.Generic;
using System.Reflection;

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
            description = ModEntry.Instance.Localizations.Localize(["card", "Chromatic Orb", "description", upgrade.ToString()], new { damageString }),
            artOverlay = ModEntry.Instance.GizmoTheFoxCCMod_Character_CardOverlaySpellUncommon.Sprite,
            cost = 1,
            exhaust = upgrade == Upgrade.B
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
                        damage = GetDmg(s, 2),
                    },
                    new ADrawCard
                    {
                        count = GetNumAttunedElements(s)
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

            case Upgrade.A:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 2),
                    },
                    new ADrawCard
                    {
                        count = GetNumAttunedElements(s)
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

            case Upgrade.B:
                actions = new()
                {
                    new AAttack
                    {
                        damage = GetDmg(s, 2),
                    },
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.EarthBitMask),
                        new AAddCard(){
                            card = new CardTremor(){ upgrade = Upgrade.A },
                            destination = CardDestination.Hand
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WindBitMask),
                        new AAddCard(){
                            card = new CardGust(){ upgrade = Upgrade.A },
                            destination = CardDestination.Hand
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.FireBitMask),
                        new AAddCard(){
                            card = new CardFlare(){ upgrade = Upgrade.A },
                            destination = CardDestination.Hand
                        }
                    ).AsCardAction,
                    Conditional.MakeAction(
                        new AttunedCondition(true, AttunementManager.WaterBitMask),
                        new AAddCard(){
                            card = new CardWhirlpool(){ upgrade = Upgrade.A },
                            destination = CardDestination.Hand
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
