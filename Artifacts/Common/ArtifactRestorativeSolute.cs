using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactRestorativeSolute : Artifact, IGizmoTheFoxCCModArtifact
{
    public bool hasTriggeredThisCombat = false;

    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Restorative Solute", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactRestorativeSolute.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Restorative Solute", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Restorative Solute", "description"]).Localize
        });
    }

    public override List<Tooltip>? GetExtraTooltips()
    => [
        new GlossaryTooltip($"action.{ModEntry.Instance.Package.Manifest.UniqueName}::Potion")
            {
                Icon = null,
                TitleColor = Colors.card,
                Title = ModEntry.Instance.Localizations.Localize(["action", "Potion", "name"]),
                Description = ModEntry.Instance.Localizations.Localize(["action", "Potion", "description"])
            }
    ];
    
    public override void OnCombatEnd(State state)
    {
        hasTriggeredThisCombat = false;
    }
    
    public override Spr GetSprite()
    {
        return hasTriggeredThisCombat ? ModEntry.Instance.GizmoTheFoxCCMod_ArtifactRestorativeSoluteDisabled.Sprite : ModEntry.Instance.GizmoTheFoxCCMod_ArtifactRestorativeSolute.Sprite;
    }
}
