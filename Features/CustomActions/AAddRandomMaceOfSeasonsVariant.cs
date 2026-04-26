using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;
using System;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class AAddRandomMaceOfSeasonsVariant : CardAction
{
    public required Upgrade upgr;

    public override void Begin(G g, State s, Combat c)
    {
        List<Card> offeringList = new List<Card>()
		{
			new CardMaceOfSeasonsWinter(){ upgrade = upgr },
			new CardMaceOfSeasonsSpring(){ upgrade = upgr },
			new CardMaceOfSeasonsSummer(){ upgrade = upgr },
			new CardMaceOfSeasonsAutumn(){ upgrade = upgr }
		};

        c.QueueImmediate(new AAddCard()
        {
            card = offeringList[s.rngCardOfferingsMidcombat.NextInt() % offeringList.Count],
            destination = CardDestination.Hand,
            amount = 1
        });
    }

    public override Icon? GetIcon(State s) =>
        new Icon(Spr.icons_addCard, null, color: Colors.textMain, flipY: false);

	public override List<Tooltip> GetTooltips(State s)
    {
        Card selectedCard;
        long currTimeInSecondsMod8 = DateTimeOffset.UtcNow.ToUnixTimeSeconds() % 8;

        switch (currTimeInSecondsMod8)
        {
            case 0:
            case 1:
                selectedCard = new CardMaceOfSeasonsWinter() { upgrade = upgr };
                break;
            case 2:
            case 3:
                selectedCard = new CardMaceOfSeasonsSpring() { upgrade = upgr };
                break;
            case 4:
            case 5:
                selectedCard = new CardMaceOfSeasonsSummer() { upgrade = upgr };
                break;
            case 6:
            case 7:
            default:
                selectedCard = new CardMaceOfSeasonsAutumn() { upgrade = upgr };
                break;
        }


        if(upgr != Upgrade.B)
        {
            return [
                new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddRandomMaceOfSeasonsVariant")
                {
                    Icon = Spr.icons_addCard,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Add Random Mace of Seasons Variant", "name"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "Add Random Mace of Seasons Variant", "description"] )
                },
                new TTCard
                {
                    card = selectedCard,
                    showCardTraitTooltips = true
                }
            ];
        }

        else
        {
            return [
                new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddRandomMaceOfSeasonsVariantB")
                {
                    Icon = Spr.icons_addCard,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Add Random Mace of Seasons Variant B", "name"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "Add Random Mace of Seasons Variant B", "description"] )
                },
                new TTCard
                {
                    card = selectedCard,
                    showCardTraitTooltips = true
                }
            ];
        }        
    }
}