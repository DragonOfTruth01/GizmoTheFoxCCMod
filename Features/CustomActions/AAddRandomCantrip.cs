using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class AAddRandomCantrip : CardAction
{
    public int execCount = 1;

    public override void Begin(G g, State s, Combat c)
    {
        for(int i = 0; i < execCount; ++i)
        {
            List<Card> offeringList = new List<Card>()
		    {
		    	new CardTremor(),
		    	new CardGust(),
		    	new CardFlare(),
		    	new CardWhirlpool()
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
        return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: execCount, color: Colors.textMain, flipY: false);
    }

	public override List<Tooltip> GetTooltips(State s)
    {
        string execCountString = "<c=boldPink>" + execCount + "</c>";

        return [
            new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                TitleColor = Colors.action,
                Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "description"], new { execCountString } )
            }
        ];
    }
}