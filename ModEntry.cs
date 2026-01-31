using Nickel;
using Nickel.Common;
using HarmonyLib;
using DragonOfTruth01.GizmoTheFoxCCMod.Cards;
// using DragonOfTruth01.GizmoTheFoxCCMod.Artifacts;
using Microsoft.Extensions.Logging;
using Nanoray.PluginManager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DragonOfTruth01.GizmoTheFoxCCMod;

public sealed class ModEntry : SimpleMod
{
    internal static ModEntry Instance { get; private set; } = null!;
    internal IKokoroApi.IV2 KokoroApi { get; }
    internal readonly Harmony Harmony;
    internal ILocalizationProvider<IReadOnlyList<string>> AnyLocalizations { get; }
    internal ILocaleBoundNonNullLocalizationProvider<IReadOnlyList<string>> Localizations { get; }

    // Card Frames
    internal ISpriteEntry GizmoTheFoxCCMod_Character_CardFrame { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_Potion_CardFrame { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_ShimmeringPotion_CardFrame { get; }

    // Character Panel
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Panel { get; }

    // Custom Card Arts
    internal ISpriteEntry GizmoTheFoxCCMod_Character_DefaultCardBG { get; }

    // Artifact Arts

    // Animation Sprites
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Neutral_0 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Neutral_1 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Neutral_2 { get; }

    internal ISpriteEntry GizmoTheFoxCCMod_Character_Mini_0 { get; }

    internal ISpriteEntry GizmoTheFoxCCMod_Character_Squint_0 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Squint_1 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_Character_Squint_2 { get; }

    // Custom Action Icons
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneEarth { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneWind { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneFire { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneWater { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneEarthAndWater { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AttuneWaterAndEarth { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AddCantrip2 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AddCantrip4 { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AddCantripA { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AddCantripB { get; }
    internal ISpriteEntry GizmoTheFoxCCMod_AddCantripRandom { get; }
    // Custom Status Icons

    internal ISpriteEntry GizmoTheFoxCCMod_Attunement { get; }

    // Custom Decks
    internal IDeckEntry GizmoTheFoxCCMod_Character_Deck { get; }
    internal IDeckEntry GizmoTheFoxCCMod_Potion_Deck { get; }
    internal IDeckEntry GizmoTheFoxCCMod_ShimmeringPotion_Deck { get; }

    // Custom Statuses
    internal IStatusEntry Attunement { get; }

    // Card List Definitions
    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_Character_CommonCard_Types { get; } = [
        typeof(CardConjureManaBlades),
        typeof(CardEvocation),
        typeof(CardPrestidigitation),
        typeof(CardSeaQuake),
        typeof(CardTremor),
        typeof(CardGust),
        typeof(CardFlare),
        typeof(CardWhirlpool),
        typeof(CardManaBladeFire),
        typeof(CardManaBladeIce)
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_Character_UncommonCard_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_Character_RareCard_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_Potion_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_ShimmeringPotion_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_Character_ExeCard_Types { get; } = [
        
    ];

    // Combine all the lists into a single object for reference
    internal static IEnumerable<Type> GizmoTheFoxCCMod_AllCard_Types = [
        .. GizmoTheFoxCCMod_Character_CommonCard_Types,
        .. GizmoTheFoxCCMod_Character_UncommonCard_Types,
        .. GizmoTheFoxCCMod_Character_RareCard_Types,
        .. GizmoTheFoxCCMod_Potion_Types,
        .. GizmoTheFoxCCMod_ShimmeringPotion_Types,
        .. GizmoTheFoxCCMod_Character_ExeCard_Types
    ];

    // Define our artifact lists
    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_CommonArtifact_Types { get; } = [
        
    ];

    internal static IReadOnlyList<Type> GizmoTheFoxCCMod_BossArtifact_Types { get; } = [
        
    ];

    // Combine all the artifacts into a single list
    internal static IEnumerable<Type> GizmoTheFoxCCMod_AllArtifact_Types = [
        .. GizmoTheFoxCCMod_CommonArtifact_Types,
        .. GizmoTheFoxCCMod_BossArtifact_Types
    ];


    public ModEntry(IPluginPackage<IModManifest> package, IModHelper helper, ILogger logger) : base(package, helper, logger)
    {
        Instance = this;

        KokoroApi = helper.ModRegistry.GetApi<IKokoroApi>("Shockah.Kokoro")!.V2;

        Harmony = new Harmony("DragonOfTruth01.GizmoTheFoxCCMod");

        // This can be done in place of all Instance.Harmony.Patch() calls in class constructors.
        // However, this will case your IDE/text editor to think the function is unused (since 
        // the patch hasn't been given visibility via constructor). This is expected behavior.
        Harmony.PatchAll();

        // Setup localization support
        AnyLocalizations = new JsonLocalizationProvider(
            tokenExtractor: new SimpleLocalizationTokenExtractor(),
            localeStreamFunction: locale => package.PackageRoot.GetRelativeFile($"i18n/{locale}.json").OpenRead()
        );
        Localizations = new MissingPlaceholderLocalizationProvider<IReadOnlyList<string>>(
            new CurrentLocaleOrEnglishLocalizationProvider<IReadOnlyList<string>>(AnyLocalizations)
        );

        // Card Frames
        GizmoTheFoxCCMod_Character_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_cardframe.png"));
        GizmoTheFoxCCMod_Potion_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_potion_cardframe.png"));
        GizmoTheFoxCCMod_ShimmeringPotion_CardFrame = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_shimmeringpotion_cardframe.png"));

        // Character Panel
        GizmoTheFoxCCMod_Character_Panel = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_panel.png"));

        // Custom Card Arts
        GizmoTheFoxCCMod_Character_DefaultCardBG = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/CardBGs/GizmoTheFoxCCMod_character_cardbackground.png"));

        // Artifact Arts

        // Animation Sprites
        GizmoTheFoxCCMod_Character_Neutral_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_neutral_0.png"));
        GizmoTheFoxCCMod_Character_Neutral_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_neutral_1.png"));
        GizmoTheFoxCCMod_Character_Neutral_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_neutral_2.png"));

        GizmoTheFoxCCMod_Character_Mini_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_mini_0.png"));

        GizmoTheFoxCCMod_Character_Squint_0 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_squint_0.png"));
        GizmoTheFoxCCMod_Character_Squint_1 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_squint_1.png"));
        GizmoTheFoxCCMod_Character_Squint_2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/character/sprites/GizmoTheFoxCCMod_character_squint_2.png"));

        // Custom Action Icons

        GizmoTheFoxCCMod_AttuneEarth = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneEarth.png"));
        GizmoTheFoxCCMod_AttuneWind = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneWind.png"));
        GizmoTheFoxCCMod_AttuneFire = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneFire.png"));
        GizmoTheFoxCCMod_AttuneWater = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneWater.png"));
        GizmoTheFoxCCMod_AttuneEarthAndWater = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneEarthAndWater.png"));
        GizmoTheFoxCCMod_AttuneWaterAndEarth = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/attuneWaterAndEarth.png"));
        GizmoTheFoxCCMod_AddCantrip2 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/addCantrip2.png"));
        GizmoTheFoxCCMod_AddCantrip4 = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/addCantrip4.png"));
        GizmoTheFoxCCMod_AddCantripA = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/addCantripA.png"));
        GizmoTheFoxCCMod_AddCantripB = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/addCantripB.png"));
        GizmoTheFoxCCMod_AddCantripRandom = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/action/addCantripRandom.png"));

        // Custom Status Icons

        GizmoTheFoxCCMod_Attunement = helper.Content.Sprites.RegisterSprite(package.PackageRoot.GetRelativeFile("assets/status/attunement.png"));

        // Register Custom Decks

        GizmoTheFoxCCMod_Character_Deck = helper.Content.Decks.RegisterDeck("GizmoTheFoxCCModCharacterDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("f4f7f0"),
                titleColor = new Color("000000")
            },

            DefaultCardArt = GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            BorderSprite = GizmoTheFoxCCMod_Character_CardFrame.Sprite,

            Name = AnyLocalizations.Bind(["character", "GizmoTheFoxCCMod_Character", "name"]).Localize,
        });

        // Register the alt starters for the MoreDifficulties mod
        helper.ModRegistry.AwaitApi<IMoreDifficultiesApi>(
            "TheJazMaster.MoreDifficulties",
            new SemanticVersion(1, 3, 0),
            api => api.RegisterAltStarters(
                deck: GizmoTheFoxCCMod_Character_Deck.Deck,
                starterDeck: new StarterDeck
                {
                    cards = [
                        // Alt starters go here
                    ]
                }

            )
        );  

        GizmoTheFoxCCMod_Potion_Deck = helper.Content.Decks.RegisterDeck("GizmoTheFoxCCModPotionDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("de543d"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            BorderSprite = GizmoTheFoxCCMod_Potion_CardFrame.Sprite,

            Name = AnyLocalizations.Bind(["character", "GizmoTheFoxCCMod_Potion", "name"]).Localize,
        });

        GizmoTheFoxCCMod_ShimmeringPotion_Deck = helper.Content.Decks.RegisterDeck("GizmoTheFoxCCModShimmeringPotionDeck", new DeckConfiguration()
        {
            Definition = new DeckDef()
            {
                color = new Color("86cece"),

                titleColor = new Color("000000")
            },

            DefaultCardArt = GizmoTheFoxCCMod_Character_DefaultCardBG.Sprite,
            BorderSprite = GizmoTheFoxCCMod_ShimmeringPotion_CardFrame.Sprite,

            Name = AnyLocalizations.Bind(["character", "GizmoTheFoxCCMod_ShimmeringPotion", "name"]).Localize,
        });

        // Register Animations
        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = GizmoTheFoxCCMod_Character_Deck.Deck.Key(),

            LoopTag = "neutral",

            Frames = new[]
            {
                GizmoTheFoxCCMod_Character_Neutral_0.Sprite,
                GizmoTheFoxCCMod_Character_Neutral_1.Sprite,
                GizmoTheFoxCCMod_Character_Neutral_2.Sprite,
                GizmoTheFoxCCMod_Character_Neutral_0.Sprite,
                GizmoTheFoxCCMod_Character_Neutral_1.Sprite,
                GizmoTheFoxCCMod_Character_Neutral_2.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = GizmoTheFoxCCMod_Character_Deck.Deck.Key(),
            LoopTag = "mini",
            Frames = new[]
            {
                GizmoTheFoxCCMod_Character_Mini_0.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = GizmoTheFoxCCMod_Character_Deck.Deck.Key(),
            LoopTag = "squint",
            Frames = new[]
            {
                GizmoTheFoxCCMod_Character_Squint_0.Sprite,
                GizmoTheFoxCCMod_Character_Squint_1.Sprite,
                GizmoTheFoxCCMod_Character_Squint_2.Sprite,
                GizmoTheFoxCCMod_Character_Squint_0.Sprite,
                GizmoTheFoxCCMod_Character_Squint_1.Sprite,
                GizmoTheFoxCCMod_Character_Squint_2.Sprite
            }
        });

        helper.Content.Characters.V2.RegisterCharacterAnimation(new CharacterAnimationConfigurationV2()
        {
            CharacterType = GizmoTheFoxCCMod_Character_Deck.Deck.Key(),
            LoopTag = "gameover",
            Frames = new[]
            {
                // The squint sprite is okay to use here...
                GizmoTheFoxCCMod_Character_Squint_0.Sprite,
            }
        });
        
        // Register the mod character as a playable character
        helper.Content.Characters.V2.RegisterPlayableCharacter("GizmoTheFoxCCMod", new PlayableCharacterConfigurationV2()
        {
            Deck = GizmoTheFoxCCMod_Character_Deck.Deck,

            Starters = new()
            {
                cards = [
                    // Starter cards go here
                ]
            },

            Description = AnyLocalizations.Bind(["character", "GizmoTheFoxCCMod_Character", "description"]).Localize,

            BorderSprite = GizmoTheFoxCCMod_Character_Panel.Sprite
        });

        // Register Cards
        foreach (var cardType in GizmoTheFoxCCMod_AllCard_Types)
            AccessTools.DeclaredMethod(cardType, nameof(IGizmoTheFoxCCModCard.Register))?.Invoke(null, [helper]);

        // Register Artifacts
        foreach (var artifactType in GizmoTheFoxCCMod_AllArtifact_Types)
            AccessTools.DeclaredMethod(artifactType, nameof(IGizmoTheFoxCCModArtifact.Register))?.Invoke(null, [helper]);

        // Register Custom Statuses
        Attunement = helper.Content.Statuses.RegisterStatus("Attunement", new()
        {
            Definition = new()
            {
                icon = GizmoTheFoxCCMod_Attunement.Sprite,
                color = new("ff687d"),
                isGood = true
            },
            Name = AnyLocalizations.Bind(["status", "Attunement", "name"]).Localize,
            Description = AnyLocalizations.Bind(["status", "Attunement", "description"]).Localize
        });
        
        _ = new AttunementManager();
    }
}
