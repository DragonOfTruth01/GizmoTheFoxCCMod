using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactArcanePrism : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Arcane Prism", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactArcanePrism.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Arcane Prism", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Arcane Prism", "description"]).Localize
        });
    }

    public override List<Tooltip>? GetExtraTooltips()
    => [
        .. StatusMeta.GetTooltips(ModEntry.Instance.Accumulate.Status, 1)
    ];

    public override void OnCombatStart(State state, Combat combat)
    {
        combat.QueueImmediate(
            new AStatus()
            {
                status = ModEntry.Instance.Accumulate.Status,
                statusAmount = 1,
                targetPlayer = true
            }
        );
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactArcanePrism.Sprite;
    }
}
