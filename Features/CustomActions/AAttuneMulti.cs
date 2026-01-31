using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class AAttuneMulti : DynamicWidthCardAction
{
    public int elementBitfieldModifier1; // Represented as 0bXXXX, where the Xs are earth, wind, fire, and water respectively
    public int elementBitfieldModifier2;

    public override void Begin(G g, State s, Combat c)
    {
        c.QueueImmediate(new AAttune(){
            elementBitfieldModifier = elementBitfieldModifier2
        });
        c.QueueImmediate(new AAttune(){
            elementBitfieldModifier = elementBitfieldModifier1
        });
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(GetStatusIcon(), number: null, color: Colors.textMain, flipY: false);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        if(elementBitfieldModifier1 == 0b1000 && elementBitfieldModifier2 == 0b0001)
        {
            return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneEarthAndWater")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneEarthAndWater.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Earth and Water", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Earth and Water", "description"])
                    }
                ];
        }
        else if(elementBitfieldModifier1 == 0b0001 && elementBitfieldModifier2 == 0b1000)
        {
            return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneWaterAndEarth")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneWaterAndEarth.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Water and Earth", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Water and Earth", "description"])
                    }
                ];
        }
        else
        {
            return new List<Tooltip>();
        }
    }

    private Spr GetStatusIcon()
    {
        if(elementBitfieldModifier1 == 0b1000 && elementBitfieldModifier2 == 0b0001)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneEarthAndWater.Sprite;
        }
        else if(elementBitfieldModifier1 == 0b0001 && elementBitfieldModifier2 == 0b1000)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneWaterAndEarth.Sprite; 
        }
        else
        {
            return new Spr();
        }
    }
}