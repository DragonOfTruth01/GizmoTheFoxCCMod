using Nickel;
using System.Collections.Generic;
using System.Reflection;
using DragonOfTruth01.GizmoTheFoxCCMod.Midrow;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactBismuthCrown : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Bismuth Crown", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactBismuthCrown.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Bismuth Crown", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Bismuth Crown", "description"]).Localize
        });
    }

    public int turnCounter = 0;
    public readonly int artifactTriggerAmt = 3;

    public override int? GetDisplayNumber(State s)
    {
        return turnCounter;
    }

    public override List<Tooltip>? GetExtraTooltips()
    {
        int atkDmg = MidrowImbuedStoneConstruct.AttackDamage();
        return [
            new GlossaryTooltip($"{ModEntry.Instance.Package.Manifest.UniqueName}::MidrowImbuedStoneConstruct")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_imbuedStoneConstructSmall.Sprite,
                Title = ModEntry.Instance.Localizations.Localize(["midrow", "Imbued Stone Construct", "name"]),
                TitleColor = Colors.midrow,
                Description = ModEntry.Instance.Localizations.Localize(["midrow", "Imbued Stone Construct", "description"], new { atkDmg })
            }
        ];
    }

    public override void OnTurnStart(State state, Combat combat)
    {
        if(++turnCounter >= artifactTriggerAmt)
        {
            Pulse();
            combat.QueueImmediate(
                new ASpawn()
                {
                    thing = new MidrowImbuedStoneConstruct()
                    {
                        yAnimation = 0.0
                    }
                }
            );
            turnCounter = 0;
        }
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactBismuthCrown.Sprite;
    }
}
