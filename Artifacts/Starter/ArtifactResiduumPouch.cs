using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactResiduumPouch : Artifact, IGizmoTheFoxCCModArtifact
{
    public bool isConsumed = false;

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
        new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::Potion")
            {
                Icon = ModEntry.Instance.GizmoTheFoxCCMod_Potion.Sprite,
                TitleColor = Colors.card,
                Title = ModEntry.Instance.Localizations.Localize(["action", "Potion", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["action", "Potion", "description"])
            }
    ];

    public override void OnCombatEnd(State state)
    {
        isConsumed = false;
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumPouch.Sprite;
    }
}
