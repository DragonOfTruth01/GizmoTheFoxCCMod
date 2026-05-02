using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactResiduumPouch : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Residuum Pouch", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.EventOnly]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumPouch.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "starter", "Residuum Pouch", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "starter", "Residuum Pouch", "description"]).Localize
        });
    }

    public override List<Tooltip>? GetExtraTooltips()
    => [
        .. StatusMeta.GetTooltips(ModEntry.Instance.Attunement.Status, 1)
    ];
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumPouch.Sprite;
    }
}
