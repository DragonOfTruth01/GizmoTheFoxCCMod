using Nickel;
using System.Collections.Generic;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class AttunedCondition : IKokoroApi.IV2.IConditionalApi.IBoolExpression
{
    private int displayedElementBitfield;
    private bool conditionalFade;

    public AttunedCondition(bool cf, int bf)
    {
        conditionalFade = cf;
        displayedElementBitfield = bf;
    }

    public bool GetValue(State state, Combat combat)
    {
        return (state.ship.Get(ModEntry.Instance.Attunement.Status) & displayedElementBitfield) != 0;
    }

    public string GetTooltipDescription(State state, Combat? combat)
    {
        if(displayedElementBitfield == AttunementManager.EarthBitMask)
        {
            return ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Earth", "description"]);
        }

        else if(displayedElementBitfield == AttunementManager.WindBitMask)
        {
            return ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Wind", "description"]);
        }

        else if(displayedElementBitfield == AttunementManager.FireBitMask)
        {
            return ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Fire", "description"]);
        }

        else
        {
            return ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Water", "description"]);
        }
    }

    public void Render(G g, ref Vec position, bool isDisabled, bool dontRender)
    {
        if (!dontRender)
        {
            Spr elementSprite;

            if(displayedElementBitfield == AttunementManager.EarthBitMask)
            {
                elementSprite = ModEntry.Instance.GizmoTheFoxCCMod_Earth.Sprite;
            }
            else if(displayedElementBitfield == AttunementManager.WindBitMask)
            {
                elementSprite = ModEntry.Instance.GizmoTheFoxCCMod_Wind.Sprite;
            }
            else if(displayedElementBitfield == AttunementManager.FireBitMask)
            {
                elementSprite = ModEntry.Instance.GizmoTheFoxCCMod_Fire.Sprite;
            }
            else
            {
                elementSprite = ModEntry.Instance.GizmoTheFoxCCMod_Water.Sprite;
            }

            Draw.Sprite(
                elementSprite,
                position.x,
                position.y,
                color: conditionalFade && isDisabled ? Colors.disabledIconTint : Colors.white
            );
        }

        position.x += 8;
    }

    public IEnumerable<Tooltip> OverrideConditionalTooltip(State state, Combat? combat, Tooltip defaultTooltip, string defaultTooltipDescription)
    {
        if(displayedElementBitfield == AttunementManager.EarthBitMask)
        {
            return [
                new GlossaryTooltip($"AConditional::{ModEntry.Instance.Package.Manifest.UniqueName}::isAttunedEarth")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_Earth.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Earth", "name"]),
                    Description = defaultTooltipDescription,
                }
            ];
        }

        else if(displayedElementBitfield == AttunementManager.WindBitMask)
        {
            return [
                new GlossaryTooltip($"AConditional::{ModEntry.Instance.Package.Manifest.UniqueName}::isAttunedWind")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_Wind.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Wind", "name"]),
                    Description = defaultTooltipDescription,
                }
            ];
        }

        else if(displayedElementBitfield == AttunementManager.EarthBitMask)
        {
            return [
                new GlossaryTooltip($"AConditional::{ModEntry.Instance.Package.Manifest.UniqueName}::isAttunedFire")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_Fire.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Fire", "name"]),
                    Description = defaultTooltipDescription,
                }
            ];
        }

        else
        {
            return [
                new GlossaryTooltip($"AConditional::{ModEntry.Instance.Package.Manifest.UniqueName}::EnisAttunedWater")
                {
                    Icon = ModEntry.Instance.GizmoTheFoxCCMod_Water.Sprite,
                    TitleColor = Colors.action,
                    Title = ModEntry.Instance.Localizations.Localize(["action", "Is Attuned Water", "name"]),
                    Description = defaultTooltipDescription,
                }
            ];
        }
        
    }
}