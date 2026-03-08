using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Midrow;

internal sealed class MidrowImbuedStoneConstruct : StuffBase
{
    public override Spr? GetIcon()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_imbuedStoneConstructSmall.Sprite;
    }

    public override double GetWiggleAmount() => 1.0;
    public override double GetWiggleRate() => 1.0;
    public override bool IsHostile() => targetPlayer;

    public override List<Tooltip> GetTooltips()
    {
        int atkDmg = AttackDamage();
        List<Tooltip> tooltips = [
            new GlossaryTooltip($"{ModEntry.Instance.Package.Manifest.UniqueName}::{GetType()}")
            {
                Icon = GetIcon()!,
                flipIconY = targetPlayer,
                Title = ModEntry.Instance.Localizations.Localize(["midrow", "Imbued Stone Construct", "name"]),
                TitleColor = Colors.midrow,
                Description = ModEntry.Instance.Localizations.Localize(["midrow", "Imbued Stone Construct", "description"], new { atkDmg })
            }
        ];
        if (bubbleShield)
            tooltips.Add(new TTGlossary("midrow.bubbleShield"));
        return tooltips;
    }

    public int AttackDamage() => 1;

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
        Sprite = ModEntry.Instance.GizmoTheFoxCCMod_imbuedStoneConstruct.Sprite;
        DrawWithHilight(g, Sprite, v + GetOffset(g), flipX: false, targetPlayer);
    }
    
}