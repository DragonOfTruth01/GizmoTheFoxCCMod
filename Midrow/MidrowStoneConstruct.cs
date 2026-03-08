using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Midrow;

internal sealed class MidrowStoneConstruct : StuffBase
{
    public override Spr? GetIcon()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_stoneConstructSmall.Sprite;
    }

    public override double GetWiggleAmount() => 1.0;
    public override double GetWiggleRate() => 1.0;
    public override bool IsHostile() => targetPlayer;

    public override List<Tooltip> GetTooltips()
    {
        List<Tooltip> tooltips = [
            new GlossaryTooltip($"{ModEntry.Instance.Package.Manifest.UniqueName}::{GetType()}")
            {
                Icon = GetIcon()!,
                flipIconY = targetPlayer,
                Title = ModEntry.Instance.Localizations.Localize(["midrow", "Stone Construct", "name"]),
                TitleColor = Colors.midrow,
                Description = ModEntry.Instance.Localizations.Localize(["midrow", "Stone Construct", "description"])
            }
        ];
        if (bubbleShield)
            tooltips.Add(new TTGlossary("midrow.bubbleShield"));
        return tooltips;
    }

    public override bool IsFriendly()
    {
        return targetPlayer;
    }

    public override List<CardAction>? GetActions(State s, Combat c)
    {
        // No end-of-turn actions to perform
        return null;
    }

    public override List<CardAction>? GetActionsOnDestroyed(State s, Combat c, bool wasPlayer, int worldX)
    {
        return new List<CardAction>
        {
            new AAttuneRandomRepeater()
            {
                execCount = 2
            }
        };
    }

    public override void Render(G g, Vec v)
    {
        Spr Sprite;
        Sprite = ModEntry.Instance.GizmoTheFoxCCMod_stoneConstruct.Sprite;
        DrawWithHilight(g, Sprite, v + GetOffset(g), flipX: false, targetPlayer);
    }
    
}