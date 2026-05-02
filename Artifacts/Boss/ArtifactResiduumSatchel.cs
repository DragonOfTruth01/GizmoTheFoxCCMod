using Nickel;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactResiduumSatchel : Artifact, IGizmoTheFoxCCModArtifact
{
    public int numTriggersRemainingThisCombat = 2;

    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Residuum Satchel", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Boss]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumSatchel.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Residuum Satchel", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "boss", "Residuum Satchel", "description"]).Localize
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

    public override int? GetDisplayNumber(State s)
    {
        return s.route is Combat ? numTriggersRemainingThisCombat : null;
    }

    public override void OnCombatEnd(State s)
    {
        numTriggersRemainingThisCombat = 2;
    }

    public override void OnReceiveArtifact(State s)
    {
        foreach(Character c in s.characters)
        {
            foreach(Artifact a in c.artifacts)
            {
                if(a.GetType() == typeof(ArtifactResiduumPouch))
                {
                    a.OnRemoveArtifact(s);
                }
            }

            c.artifacts.RemoveAll((Artifact a) => a.GetType() == typeof(ArtifactResiduumPouch));
        }
    }
    
    public override Spr GetSprite()
    {
        return numTriggersRemainingThisCombat == 0 ? ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumSatchelDisabled.Sprite : ModEntry.Instance.GizmoTheFoxCCMod_ArtifactResiduumSatchel.Sprite;
    }
}
