using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;
using System;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class AAddRandomCantrip : CardAction
{
    public int execCount = 1;
    public required Upgrade upgr;

    public override void Begin(G g, State s, Combat c)
    {
        for(int i = 0; i < execCount; ++i)
        {
            List<Card> offeringList = new List<Card>()
		    {
		    	new CardTremor(){ upgrade = upgr },
		    	new CardGust(){ upgrade = upgr },
		    	new CardFlare(){ upgrade = upgr },
		    	new CardWhirlpool(){ upgrade = upgr }
		    };

            c.QueueImmediate(new AAddCard()
            {
                card = offeringList[s.rngCardOfferingsMidcombat.NextInt() % offeringList.Count],
                destination = CardDestination.Hand,
                amount = 1
            });
        }
    }

    public override Icon? GetIcon(State s)
    {
        if(upgr == Upgrade.B)
        {
            return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandomB.Sprite, number: execCount, color: Colors.textMain, flipY: false);
        }
        else
        {
            return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: execCount, color: Colors.textMain, flipY: false);
        }
        
    }

	public override List<Tooltip> GetTooltips(State s)
    {
        string execCountString = "<c=boldPink>" + execCount + "</c>";
        Card selCard;
        long currTimeInSecondsMod8 = DateTimeOffset.UtcNow.ToUnixTimeSeconds() % 8;

        switch (currTimeInSecondsMod8)
        {
            case 0:
            case 1:
                selCard = new CardTremor() { upgrade = upgr };
                break;
            case 2:
            case 3:
                selCard = new CardGust() { upgrade = upgr };
                break;
            case 4:
            case 5:
                selCard = new CardFlare() { upgrade = upgr };
                break;
            case 6:
            case 7:
            default:
                selCard = new CardWhirlpool() { upgrade = upgr };
                break;
        }

        if(upgr == Upgrade.B)
        {
            return [
                new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandomB.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random B", "name"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random B", "description"], new { execCountString } )
                },
                new TTCard
                {
                    card = selCard,
                    showCardTraitTooltips = true
                }
            ];
        }

        else
        {
            return [
                new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "name"]),
                    Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "description"], new { execCountString } )
                },
                new TTCard
                {
                    card = selCard,
                    showCardTraitTooltips = true
                }
            ];
        }        
    }
}