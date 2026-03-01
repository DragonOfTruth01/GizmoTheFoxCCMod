using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class AAttuneMulti : DynamicWidthCardAction
{
    public int elementBitfieldModifier1;
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
        if(elementBitfieldModifier1 == AttunementManager.EarthBitMask && elementBitfieldModifier2 == AttunementManager.WaterBitMask)
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
        else if(elementBitfieldModifier1 == AttunementManager.WaterBitMask && elementBitfieldModifier2 == AttunementManager.EarthBitMask)
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
        else if(elementBitfieldModifier1 == AttunementManager.WindBitMask && elementBitfieldModifier2 == AttunementManager.FireBitMask)
        {
            return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneWindAndFire")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneWindAndFire.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Wind and Fire", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Wind and Fire", "description"])
                    }
                ];
        }
        else if(elementBitfieldModifier1 == AttunementManager.FireBitMask && elementBitfieldModifier2 == AttunementManager.WindBitMask)
        {
            return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneFireAndWind")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneFireAndWind.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Fire and Wind", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Fire and Wind", "description"])
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
        if(elementBitfieldModifier1 == AttunementManager.EarthBitMask && elementBitfieldModifier2 == AttunementManager.WaterBitMask)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneEarthAndWater.Sprite;
        }
        else if(elementBitfieldModifier1 == AttunementManager.WaterBitMask && elementBitfieldModifier2 == AttunementManager.EarthBitMask)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneWaterAndEarth.Sprite; 
        }
        else if(elementBitfieldModifier1 == AttunementManager.WindBitMask && elementBitfieldModifier2 == AttunementManager.FireBitMask)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneWindAndFire.Sprite; 
        }
        else if(elementBitfieldModifier1 == AttunementManager.FireBitMask && elementBitfieldModifier2 == AttunementManager.WindBitMask)
        {
            return ModEntry.Instance.GizmoTheFoxCCMod_AttuneFireAndWind.Sprite; 
        }
        else
        {
            return new Spr();
        }
    }
}