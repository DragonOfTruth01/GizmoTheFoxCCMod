using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class AAttuneRandom : CardAction
{
    public override void Begin(G g, State s, Combat c)
    {
        int currAttunement = s.ship.Get(ModEntry.Instance.Attunement.Status);
        List<int> elementSelectionlist = new List<int>();
        List<int> masks = new List<int>{AttunementManager.EarthBitMask,
                                        AttunementManager.WindBitMask,
                                        AttunementManager.FireBitMask,
                                        AttunementManager.WaterBitMask};
        foreach (int mask in masks)
        {
            if( (currAttunement & mask) == 0)
            {
                elementSelectionlist.Add(mask);
            }
        }
        if(elementSelectionlist.Count == 0)
        {
            // This should never happen...but can cause issues if it does, so return
            return;
        }
        else if(elementSelectionlist.Count == 1)
        {
            c.QueueImmediate(new AAttune
            {
                elementBitfieldModifier = elementSelectionlist[0]
            });
        }
        else
        {
            int randIndex = s.rngActions.NextInt() % elementSelectionlist.Count;
            c.QueueImmediate(new AAttune
            {
                elementBitfieldModifier = elementSelectionlist[randIndex]
            });
        }
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(GetStatusIcon(), number: null, color: Colors.textMain, flipY: false);
    }

    public override List<Tooltip> GetTooltips(State s)
        => [
            new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneRandom")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneRandom.Sprite,
                TitleColor = Colors.action,
                Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Random", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Random", "description"], new { execCountString = 1 })
            }];

    private Spr GetStatusIcon()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_AttuneRandom.Sprite;
    }
}