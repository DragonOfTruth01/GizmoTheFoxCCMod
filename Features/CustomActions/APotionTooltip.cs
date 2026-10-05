using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public class APotionTooltip : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {

    }

    public override List<Tooltip> GetTooltips(State s)
    => [
        new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::Potion")
        {
            Icon = ModEntry.Instance.GizmoTheFoxCCMod_Potion.Sprite,
            TitleColor = Colors.action,
            Title = ModEntry.Instance.Localizations.Localize(["action", "Potion", "name"]),
            Description = ModEntry.Instance.Localizations.Localize(["action", "Potion", "description"])
        }
    ];
};
