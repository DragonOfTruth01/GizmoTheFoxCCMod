using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class ACustomAddCantrip : CardAction
{
    public enum AddCantripType
    {
        addCantrip2,
        addCantrip4,
        addCantripA,
        addCantripB
    };

    public AddCantripType cantripType;

    public override void Begin(G g, State s, Combat c)
    {
		switch(cantripType){
            case AddCantripType.addCantrip2:
				List<Card> offeringList = new List<Card>()
				{
					new CardTremor(),
					new CardGust(),
					new CardFlare(),
					new CardWhirlpool()
				};

				int rand1 = s.rngCardOfferingsMidcombat.NextInt() % offeringList.Count;
				Card card1 = offeringList[rand1];
				offeringList.RemoveAt(rand1);

				int rand2 = s.rngCardOfferingsMidcombat.NextInt() % offeringList.Count;
				Card card2 = offeringList[rand2];
				offeringList.RemoveAt(rand2);

                c.QueueImmediate(new ASpecificCardOffering()
						{
							Destination = CardDestination.Hand,
							Cards = [
								card1,
								card2
							]
						});
                break;
            case AddCantripType.addCantrip4:
                c.QueueImmediate(new ASpecificCardOffering()
						{
							Destination = CardDestination.Hand,
							Cards = [
								new CardTremor(),
								new CardGust(),
								new CardFlare(),
								new CardWhirlpool()
							]
						});
                break;
            case AddCantripType.addCantripA:
                c.QueueImmediate(new ASpecificCardOffering()
						{
							Destination = CardDestination.Hand,
							Cards = [
								new CardTremor() { upgrade = Upgrade.A },
								new CardGust() { upgrade = Upgrade.A },
								new CardFlare() { upgrade = Upgrade.A },
								new CardWhirlpool() { upgrade = Upgrade.A }
							]
						});
                break;
            case AddCantripType.addCantripB:
                c.QueueImmediate(new ASpecificCardOffering()
						{
							Destination = CardDestination.Hand,
							Cards = [
								new CardTremor() { upgrade = Upgrade.B },
								new CardGust() { upgrade = Upgrade.B },
								new CardFlare() { upgrade = Upgrade.B },
								new CardWhirlpool() { upgrade = Upgrade.B }
							]
						});
                break;
            default:
                break;
        }

        base.Begin(g, s, c);
    }

    public override Icon? GetIcon(State s)
    {
        switch(cantripType){
            case AddCantripType.addCantrip2:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip2.Sprite, number: null, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantrip4:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip4.Sprite, number: null, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantripA:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripA.Sprite, number: null, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantripB:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripB.Sprite, number: null, color: Colors.textMain, flipY: false);
            default:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: null, color: Colors.textMain, flipY: false);
        }
    }

	public override List<Tooltip> GetTooltips(State s)
	{
		switch(cantripType){
            case AddCantripType.addCantrip2:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantrip2")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip2.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip 2", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip 2", "description"])
                    }];
            case AddCantripType.addCantrip4:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantrip4")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip4.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip 4", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip 4", "description"])
                    }];
            case AddCantripType.addCantripA:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripA")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripA.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip A", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip A", "description"])
                    }];
            case AddCantripType.addCantripB:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripB")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripB.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip B", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip B", "description"])
                    }];
            default:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Add Cantrip Random", "description"])
                    }];
        }
    }
}