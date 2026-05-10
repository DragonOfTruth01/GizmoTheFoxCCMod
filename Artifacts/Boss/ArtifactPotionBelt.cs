using Nickel;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactPotionBelt : Artifact, IGizmoTheFoxCCModArtifact
{
    public int numTriggersRemainingThisCombat = 2;

    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Potion Belt", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactPotionBelt.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Potion Belt", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Potion Belt", "description"]).Localize
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
            },
        new TTGlossary("cardtrait.retain")
    ];
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactPotionBelt.Sprite;
    }
}
