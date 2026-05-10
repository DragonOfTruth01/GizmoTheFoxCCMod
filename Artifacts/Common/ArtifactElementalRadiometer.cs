using Nickel;
using System.Collections.Generic;
using System.Reflection;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactElementalRadiometer : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Elemental Radiometer", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactElementalRadiometer.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Elemental Radiometer", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Elemental Radiometer", "description"]).Localize
        });
    }

    public int attuneCounter = 0;
    public readonly int artifactTriggerAmt = 7;

    public override int? GetDisplayNumber(State s)
    {
        return attuneCounter;
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactElementalRadiometer.Sprite;
    }

    // This will get called once for each individual attunement action
    public void CheckIfTriggered(State state, Combat combat)
    {
        if(++attuneCounter >= artifactTriggerAmt)
        {
            Pulse();
            combat.QueueImmediate(
                new ADrawCard()
                {
                    count = 2
                }
            );
            attuneCounter = 0;
        }
    }
}
