using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class AAttuneRandomRepeater : CardAction
{
    // This class needs a special repeater because random attunement is state-dependent.
    // Calling new queues from this class forces AAttuneRandom to check the state independently.

    public int execCount;

    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0f;

        for(int i = 0; i < execCount; ++i)
        {
            c.QueueImmediate(new AAttuneRandom());
        }
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(GetStatusIcon(), number: execCount, color: Colors.textMain, flipY: false);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        string execCountString = "<c=boldPink>" + execCount + "</c>";

        return [
            new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneRandom")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneRandom.Sprite,
                TitleColor = Colors.action,
                Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Random", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Random", "description"], new { execCountString })
            }];
    }

    private Spr GetStatusIcon()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_AttuneRandom.Sprite;
    }
}