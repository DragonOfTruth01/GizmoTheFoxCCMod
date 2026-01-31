using FSPRO;
using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using Microsoft.Extensions.Logging;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class AAddRandomCantrip : AAddCard
{
    public override void Begin(G g, State s, Combat c)
    {
        base.Begin(g, s, c);
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite, number: amount, color: Colors.textMain, flipY: false);
    }

	public override List<Tooltip> GetTooltips(State s)
        => [new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AddCantripRandom")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_AddCantripRandom.Sprite,
                TitleColor = Colors.action,
                Title = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["character", "action", "Add Cantrip Random", "description"])
            }];
}