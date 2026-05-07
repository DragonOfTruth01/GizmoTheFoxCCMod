using Nickel;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;

[HarmonyPatch]
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

    [HarmonyPostfix]
    [HarmonyPatch(typeof(Combat), nameof(Combat.TryPlayCard))]
    public static void Combat_TryPlayCard_Postfix(Combat __instance, State s, Card card, bool playNoMatterWhatForFree, bool exhaustNoMatterWhat, ref bool __result)
    {
        // If the card played successfully...
        if(__result == true)
        {
            // If the played card was a potion...
            if(card.GetMeta().deck == ModEntry.Instance.GizmoTheFoxCCMod_Potion_Deck.Deck)
            {
                var restorativeSolute = s.EnumerateAllArtifacts().OfType<ArtifactRestorativeSolute>().FirstOrDefault();

                // If we have restorative solute...
                if (restorativeSolute != null)
                {
                    // And it hasn't proc'd yet this combat...
                    if (!restorativeSolute.hasTriggeredThisCombat)
                    {
                        // Queue a heal action
                        __instance.Queue(
                            new AHeal()
                            {
                                healAmount = 1,
                                targetPlayer = true
                            }
                        );

                        // Then disable the artifact
                        restorativeSolute.Pulse();
                        restorativeSolute.hasTriggeredThisCombat = true;
                    }
                }
            }
        }
    }
}
