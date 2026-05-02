using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class APotionOfThePhoenixTooltip : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {

    }

    public override List<Tooltip> GetTooltips(State s)
    => [
        new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::PotionOfThePhoenix")
        {
            Icon = Spr.icons_survive,
            TitleColor = Colors.action,
            Title = ModEntry.Instance.Localizations.Localize(["action", "Potion of the Phoenix", "name"]),
            Description = ModEntry.Instance.Localizations.Localize(["action", "Potion of the Phoenix", "description"])
        }
    ];
};
