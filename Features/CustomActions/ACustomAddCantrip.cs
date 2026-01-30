using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

// This class's only purpose is to override the icon for adding cards to your hand when you add cantrips.
public sealed class ACustomAddCantrip : AAddCard
{
    public enum AddCantripType
    {
        addCantrip2,
        addCantrip4,
        addCantripA,
        addCantripB,
        addCantripRandom
    };

    public AddCantripType cantripType;

    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
    }

    public override Icon? GetIcon(State s)
    {
        switch(cantripType){
            case AddCantripType.addCantrip2:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip2.Sprite, number: amount, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantrip4:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip4.Sprite, number: amount, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantripA:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripA.Sprite, number: amount, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantripB:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripB.Sprite, number: amount, color: Colors.textMain, flipY: false);
            case AddCantripType.addCantripRandom:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: amount, color: Colors.textMain, flipY: false);
            default:
                return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: amount, color: Colors.textMain, flipY: false);
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
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip 2", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip 2", "description"])
                    }];
            case AddCantripType.addCantrip4:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantrip4")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantrip4.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip 4", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip 4", "description"])
                    }];
            case AddCantripType.addCantripA:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripA")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripA.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip A", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip A", "description"])
                    }];
            case AddCantripType.addCantripB:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripB")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripB.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip B", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip B", "description"])
                    }];
            case AddCantripType.addCantripRandom:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "description"])
                    }];
            default:
                return [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "description"])
                    }];
        }
    }
}