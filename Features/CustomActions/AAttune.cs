using Nickel;
using FSPRO;
using System.Collections.Generic;
using HarmonyLib;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

[HarmonyPatch]
public sealed class AAttune : CardAction // NOTE: This should only be used to attune one status at a time, until solution is found to do multiple
{
    public int elementBitfieldModifier;

    public override void Begin(G g, State s, Combat c)
    {
        timer = 0.0f;

        int currAttunement = s.ship.Get(ModEntry.Instance.Attunement.Status);
        int newAttunement = currAttunement | elementBitfieldModifier;

        if(newAttunement != currAttunement)
        {
            c.QueueImmediate(new AStatus{
                mode = AStatusMode.Set,
                status = ModEntry.Instance.Attunement.Status,
                statusAmount = newAttunement,
                targetPlayer = true
            });
        }
    }

    public override Icon? GetIcon(State s)
    {
        return new Icon(GetStatusIcon(), number: null, color: Colors.textMain, flipY: false);
    }

    public override List<Tooltip> GetTooltips(State s)
    {
        switch (elementBitfieldModifier)
        {
            case AttunementManager.EarthBitMask:
                return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneEarth")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneEarth.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Earth", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Earth", "description"])
                    }
                ];
            case AttunementManager.WindBitMask:
                return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneWind")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneWind.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Wind", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Wind", "description"])
                    }
                ];
            case AttunementManager.FireBitMask:
                return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneFire")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneFire.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Fire", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Fire", "description"])
                    }
                ];
            case AttunementManager.WaterBitMask:
                return [
                    new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::AttuneWater")
                    {
                        Icon = ModEntry.Instance.GizmoTheFoxCCMod_AttuneWater.Sprite,
                        TitleColor = Colors.action,
                        Title = ModEntry.Instance.Localizations.Localize(["action", "Attune Water", "name"]),
                        Description = ModEntry.Instance.Localizations.Localize(["action", "Attune Water", "description"])
                    }
                ];
            default:
                return new List<Tooltip>();
        }
    }

    private Spr GetStatusIcon()
    {
        switch (elementBitfieldModifier)
        {
            case AttunementManager.EarthBitMask:
                return ModEntry.Instance.GizmoTheFoxCCMod_AttuneEarth.Sprite;
            case AttunementManager.WindBitMask:
                return ModEntry.Instance.GizmoTheFoxCCMod_AttuneWind.Sprite;
            case AttunementManager.FireBitMask:
                return ModEntry.Instance.GizmoTheFoxCCMod_AttuneFire.Sprite;
            case AttunementManager.WaterBitMask:
                return ModEntry.Instance.GizmoTheFoxCCMod_AttuneWater.Sprite;
            default:
                return new Spr();
        }
    }
}