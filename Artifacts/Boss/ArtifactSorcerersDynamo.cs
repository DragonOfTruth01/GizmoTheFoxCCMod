using Nickel;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactSorcerersDynamo : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Sorcerer's Dynamo", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactSorcerersDynamo.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Sorcerer's Dynamo", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Sorcerer's Dynamo", "description"]).Localize
        });
    }

    public override List<Tooltip>? GetExtraTooltips()
    => [
        .. StatusMeta.GetTooltips(ModEntry.Instance.Attunement.Status, 1)
    ];

    public override void OnTurnStart(State state, Combat combat)
    {
        combat.Queue(
            new AEnergy()
            {
                changeAmount = 1
            }
        );
        combat.Queue(
            new AStatus
            {
                status = ModEntry.Instance.Attunement.Status,
                mode = AStatusMode.Set,
                statusAmount = 0,
                targetPlayer = true
            }
        );
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactSorcerersDynamo.Sprite;
    }
}
