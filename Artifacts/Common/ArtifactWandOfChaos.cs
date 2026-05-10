using Nickel;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

internal sealed class ArtifactWandOfChaos : Artifact, IGizmoTheFoxCCModArtifact
{
    public static void Register(IModHelper helper)
    {
        helper.Content.Artifacts.RegisterArtifact("Wand of Chaos", new()
        {
            ArtifactType = MethodBase.GetCurrentMethod()!.DeclaringType!,
            Meta = new()
            {
                owner = ModEntry.Instance.GizmoTheFoxCCMod_Character_Deck.Deck,
                pools = [ArtifactPool.Common]
            },
            Sprite = ModEntry.Instance.GizmoTheFoxCCMod_ArtifactWandOfChaos.Sprite,
            Name = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Wand of Chaos", "name"]).Localize,
            Description = ModEntry.Instance.AnyLocalizations.Bind(["artifact", "common", "Wand of Chaos", "description"]).Localize
        });
    }
    
    public override Spr GetSprite()
    {
        return ModEntry.Instance.GizmoTheFoxCCMod_ArtifactWandOfChaos.Sprite;
    }

    public override void OnCombatStart(State state, Combat combat)
    {
        List<Deck> charDecks = state.storyVars.GetUnlockedChars().ToList();

        Deck chosenDeck;
		Card chosenCard;

        chosenDeck = charDecks[state.rngCardOfferingsMidcombat.NextInt() % charDecks.Count()];

		chosenCard = CardReward.GetOffering(
                s: state,
                count: 1,
                limitDeck: chosenDeck,
                inCombat: true,
                isEvent: false,
				makeAllCardsTemporary: true,
				discount: -99
			)[0];

        combat.Queue(
            new AAddCard()
            {
                card = chosenCard,
                destination = CardDestination.Hand,
                amount = 1
            }
        );
    }
}
