using System;
using System.Collections.Generic;
using Jotunn;
using Jotunn.Configs;
using Jotunn.Entities;
using Jotunn.Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
using PersonalChestHeeler.Configuration;
using PersonalChestHeeler.Localization;

namespace PersonalChestHeeler.Pieces
{
    public static class ChestPieceManager
    {
        // Custom Prefab Names
        public const string Tier1ChainedPrefab = "piece_chest_chained_personal";
        public const string Tier2BronzePrefab = "piece_chest_finewood_personal";
        public const string Tier3IronboundPrefab = "piece_chest_reinforced_personal";
        public const string Tier4SilverPrefab = "piece_chest_dvergr_sm_personal";
        public const string Tier5ClockworkPrefab = "piece_chest_dvergr_lg_personal";
        public const string Tier6FlametalPrefab = "piece_chest_charred_personal";
        public const string Tier7ReliquaryPrefab = "piece_chest_ancient_personal";

        // Custom Piece Category Default
        public const string DefaultCategoryName = "Personal Chests";

        public static string GetConfiguredCategoryName()
        {
            string cat = ModConfig.PieceCategory?.Value?.Trim() ?? string.Empty;
            return string.IsNullOrWhiteSpace(cat) ? DefaultCategoryName : cat;
        }

        public static Piece.PieceCategory GetConfiguredPieceCategory()
        {
            return PieceManager.Instance.AddPieceCategory(GetConfiguredCategoryName());
        }

        // Donor Vanilla Prefab Names
        public const string NativePersonalChestDonor = "piece_chest_private";
        public const string NativeChainedDonor = "TreasureChest_morkhalla";
        public const string NativeReinforcedDonor = "piece_chest";
        public const string NativeSmallDvergrDonor = "TreasureChest_dvergrtower";
        public const string NativeLargeDvergrDonor = "TreasureChest_dvergrtown";
        public const string NativeCharredDonor = "TreasureChest_charredfortress";
        public const string NativeAncientDonor = "Morkhalla_ChestAncient";

        // Themed Emissive Colors
        public static readonly Color ColorChained = new(0.824f, 0.655f, 0.000f);     // Gold Amber (#D2A700)
        public static readonly Color ColorBronze = new(0.000f, 0.667f, 0.224f);      // Forest Green (#00AA39)
        public static readonly Color ColorIronbound = new(1.000f, 0.475f, 0.000f);   // Fiery Orange (#FF7900)
        public static readonly Color ColorSilver = new(0.890f, 0.376f, 0.906f);      // Arcane Magenta (#E360E7)
        public static readonly Color ColorClockwork = new(0.137f, 0.349f, 1.000f);   // Deep Cobalt Blue (#2359FF)
        public static readonly Color ColorFlametal = new(1.000f, 0.000f, 0.000f);    // Pure Crimson Red (#FF0000)
        public static readonly Color ColorReliquary = new(0.792f, 1.000f, 1.000f);   // Pale Glacial Cyan (#CAFFFF)

        private static bool _piecesRegistered;
        private static Sprite? _cachedContainerBkg;

        public static float GetTierScaleMultiplier(string prefabName) => prefabName switch
        {
            Tier1ChainedPrefab => 0.30f,
            Tier2BronzePrefab => 0.90f,
            Tier3IronboundPrefab => 0.60f,
            Tier4SilverPrefab => 1.10f,
            Tier5ClockworkPrefab => 1.20f,
            Tier6FlametalPrefab => 0.95f,
            Tier7ReliquaryPrefab => 0.90f,
            _ => 1.0f
        };

        public static Color GetDefaultTierColor(string prefabName) => prefabName switch
        {
            Tier1ChainedPrefab => ColorChained,
            Tier2BronzePrefab => ColorBronze,
            Tier3IronboundPrefab => ColorIronbound,
            Tier4SilverPrefab => ColorSilver,
            Tier5ClockworkPrefab => ColorClockwork,
            Tier6FlametalPrefab => ColorFlametal,
            Tier7ReliquaryPrefab => ColorReliquary,
            _ => Color.white
        };

        public static void Initialize()
        {
            PrefabManager.OnVanillaPrefabsAvailable += RegisterCustomPieces;
            PieceManager.OnPiecesRegistered += OnPiecesRegistered;

            ModConfig.ChainedRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier1ChainedPrefab);
            ModConfig.ChainedCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier1ChainedPrefab);
            ModConfig.ChainedHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier1ChainedPrefab);
            ModConfig.ChainedWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier1ChainedPrefab);
            ModConfig.ChainedHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier1ChainedPrefab);

            ModConfig.BronzeRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier2BronzePrefab);
            ModConfig.BronzeCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier2BronzePrefab);
            ModConfig.BronzeHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier2BronzePrefab);
            ModConfig.BronzeWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier2BronzePrefab);
            ModConfig.BronzeHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier2BronzePrefab);

            ModConfig.IronboundRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier3IronboundPrefab);
            ModConfig.IronboundCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier3IronboundPrefab);
            ModConfig.IronboundHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier3IronboundPrefab);
            ModConfig.IronboundWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier3IronboundPrefab);
            ModConfig.IronboundHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier3IronboundPrefab);

            ModConfig.SilverRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier4SilverPrefab);
            ModConfig.SilverCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier4SilverPrefab);
            ModConfig.SilverHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier4SilverPrefab);
            ModConfig.SilverWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier4SilverPrefab);
            ModConfig.SilverHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier4SilverPrefab);

            ModConfig.ClockworkRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier5ClockworkPrefab);
            ModConfig.ClockworkCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier5ClockworkPrefab);
            ModConfig.ClockworkHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier5ClockworkPrefab);
            ModConfig.ClockworkWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier5ClockworkPrefab);
            ModConfig.ClockworkHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier5ClockworkPrefab);

            ModConfig.FlametalRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier6FlametalPrefab);
            ModConfig.FlametalCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier6FlametalPrefab);
            ModConfig.FlametalHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier6FlametalPrefab);
            ModConfig.FlametalWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier6FlametalPrefab);
            ModConfig.FlametalHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier6FlametalPrefab);

            ModConfig.ReliquaryRecipe.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier7ReliquaryPrefab);
            ModConfig.ReliquaryCraftingStation.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier7ReliquaryPrefab);
            ModConfig.ReliquaryHealth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier7ReliquaryPrefab);
            ModConfig.ReliquaryWidth.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier7ReliquaryPrefab);
            ModConfig.ReliquaryHeight.SettingChanged += (_, _) => UpdatePieceConfiguration(Tier7ReliquaryPrefab);

            ModConfig.PieceCategory.SettingChanged += (_, _) => UpdateAllPieceCategories();
            ModConfig.EnableChestGlow.SettingChanged += (_, _) => ChestGlowController.RefreshAll();
            ModConfig.ChestGlowIntensity.SettingChanged += (_, _) => ChestGlowController.RefreshAll();

            Plugin.LogDebug("[PieceManager] Subscribed to vanilla prefab events and config change listeners.");
        }

        private static void RegisterCustomPieces()
        {
            if (_piecesRegistered) return;
            _piecesRegistered = true;

            Plugin.LogDebug("[PieceManager] RegisterCustomPieces triggered.");

            RegisterChestPiece(
                customPrefabName: Tier1ChainedPrefab,
                donorPrefabName: NativeChainedDonor,
                nameToken: ModLocalization.TokenPieceChestChained,
                descToken: ModLocalization.TokenPieceChestChainedDesc,
                glowColor: ColorChained,
                recipeString: ModConfig.ChainedRecipe.Value,
                defaultRecipeString: (string)ModConfig.ChainedRecipe.DefaultValue,
                stationString: ModConfig.ChainedCraftingStation.Value,
                width: ModConfig.ChainedWidth.Value,
                height: ModConfig.ChainedHeight.Value,
                health: ModConfig.ChainedHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier2BronzePrefab,
                donorPrefabName: NativePersonalChestDonor,
                nameToken: ModLocalization.TokenPieceChestBronze,
                descToken: ModLocalization.TokenPieceChestBronzeDesc,
                glowColor: ColorBronze,
                recipeString: ModConfig.BronzeRecipe.Value,
                defaultRecipeString: (string)ModConfig.BronzeRecipe.DefaultValue,
                stationString: ModConfig.BronzeCraftingStation.Value,
                width: ModConfig.BronzeWidth.Value,
                height: ModConfig.BronzeHeight.Value,
                health: ModConfig.BronzeHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier3IronboundPrefab,
                donorPrefabName: NativeReinforcedDonor,
                nameToken: ModLocalization.TokenPieceChestIronbound,
                descToken: ModLocalization.TokenPieceChestIronboundDesc,
                glowColor: ColorIronbound,
                recipeString: ModConfig.IronboundRecipe.Value,
                defaultRecipeString: (string)ModConfig.IronboundRecipe.DefaultValue,
                stationString: ModConfig.IronboundCraftingStation.Value,
                width: ModConfig.IronboundWidth.Value,
                height: ModConfig.IronboundHeight.Value,
                health: ModConfig.IronboundHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier4SilverPrefab,
                donorPrefabName: NativeSmallDvergrDonor,
                nameToken: ModLocalization.TokenPieceChestSilver,
                descToken: ModLocalization.TokenPieceChestSilverDesc,
                glowColor: ColorSilver,
                recipeString: ModConfig.SilverRecipe.Value,
                defaultRecipeString: (string)ModConfig.SilverRecipe.DefaultValue,
                stationString: ModConfig.SilverCraftingStation.Value,
                width: ModConfig.SilverWidth.Value,
                height: ModConfig.SilverHeight.Value,
                health: ModConfig.SilverHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier5ClockworkPrefab,
                donorPrefabName: NativeLargeDvergrDonor,
                nameToken: ModLocalization.TokenPieceChestClockwork,
                descToken: ModLocalization.TokenPieceChestClockworkDesc,
                glowColor: ColorClockwork,
                recipeString: ModConfig.ClockworkRecipe.Value,
                defaultRecipeString: (string)ModConfig.ClockworkRecipe.DefaultValue,
                stationString: ModConfig.ClockworkCraftingStation.Value,
                width: ModConfig.ClockworkWidth.Value,
                height: ModConfig.ClockworkHeight.Value,
                health: ModConfig.ClockworkHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier6FlametalPrefab,
                donorPrefabName: NativeCharredDonor,
                nameToken: ModLocalization.TokenPieceChestFlametal,
                descToken: ModLocalization.TokenPieceChestFlametalDesc,
                glowColor: ColorFlametal,
                recipeString: ModConfig.FlametalRecipe.Value,
                defaultRecipeString: (string)ModConfig.FlametalRecipe.DefaultValue,
                stationString: ModConfig.FlametalCraftingStation.Value,
                width: ModConfig.FlametalWidth.Value,
                height: ModConfig.FlametalHeight.Value,
                health: ModConfig.FlametalHealth.Value
            );

            RegisterChestPiece(
                customPrefabName: Tier7ReliquaryPrefab,
                donorPrefabName: NativeAncientDonor,
                nameToken: ModLocalization.TokenPieceChestReliquary,
                descToken: ModLocalization.TokenPieceChestReliquaryDesc,
                glowColor: ColorReliquary,
                recipeString: ModConfig.ReliquaryRecipe.Value,
                defaultRecipeString: (string)ModConfig.ReliquaryRecipe.DefaultValue,
                stationString: ModConfig.ReliquaryCraftingStation.Value,
                width: ModConfig.ReliquaryWidth.Value,
                height: ModConfig.ReliquaryHeight.Value,
                health: ModConfig.ReliquaryHealth.Value
            );

            PrefabManager.OnVanillaPrefabsAvailable -= RegisterCustomPieces;
        }

        private static void RegisterChestPiece(
            string customPrefabName,
            string donorPrefabName,
            string nameToken,
            string descToken,
            Color glowColor,
            string recipeString,
            string defaultRecipeString,
            string stationString,
            int width,
            int height,
            float health)
        {
            if (!TryParseAndValidateRecipe(recipeString, customPrefabName, out var requirements))
            {
                Plugin.Log.LogError($"[PieceManager] Recipe for '{customPrefabName}' is invalid ('{recipeString}'). Falling back to default: '{defaultRecipeString}'.");
                if (!TryParseAndValidateRecipe(defaultRecipeString, customPrefabName, out requirements))
                {
                    Plugin.Log.LogError($"[PieceManager] Default recipe for '{customPrefabName}' is also invalid. Aborting piece registration.");
                    return;
                }
            }

            GameObject clonedGameObject = PrefabManager.Instance.CreateClonedPrefab(customPrefabName, donorPrefabName);
            if (clonedGameObject == null)
            {
                Plugin.Log.LogError($"[PieceManager] Failed to clone donor prefab '{donorPrefabName}' as '{customPrefabName}'.");
                return;
            }

            // --- Donor Asset Corrections ---
            if (customPrefabName == Tier5ClockworkPrefab ||
                customPrefabName == Tier7ReliquaryPrefab)
            {
                RotateChildren180(clonedGameObject);
            }

            float scaleMultiplier = GetTierScaleMultiplier(customPrefabName);
            clonedGameObject.transform.localScale *= scaleMultiplier;
            Plugin.LogDebug($"[PieceManager] Scaled '{customPrefabName}' by {scaleMultiplier:F3}x.");

            if (!clonedGameObject.TryGetComponent(out ZNetView netView))
            {
                netView = clonedGameObject.AddComponent<ZNetView>();
            }
            netView.m_syncInitialScale = true;

            if (!clonedGameObject.TryGetComponent(out Piece piece))
            {
                piece = clonedGameObject.AddComponent<Piece>();
            }
            piece.m_name = nameToken;
            piece.m_description = descToken;
            piece.m_canBeRemoved = true;
            string categoryName = GetConfiguredCategoryName();
            piece.m_category = PieceManager.Instance.AddPieceCategory(categoryName);
            piece.m_placeEffect = ChestAudioEffectManager.GetPlaceEffects(customPrefabName);

            var container = clonedGameObject.GetComponentInChildren<Container>();
            if (!container)
            {
                container = clonedGameObject.AddComponent<Container>();
            }
            container.m_name = nameToken;
            container.m_privacy = Container.PrivacySetting.Private;
            container.m_checkGuardStone = false;
            container.m_autoDestroyEmpty = false;
            container.m_defaultItems = new DropTable();
            container.m_width = width;
            container.m_height = height;

            if (_cachedContainerBkg == null)
            {
                var donor = PrefabManager.Instance.GetPrefab(NativePersonalChestDonor);
                _cachedContainerBkg = donor?.GetComponentInChildren<Container>()?.m_bkg;
            }
            if (_cachedContainerBkg != null)
            {
                container.m_bkg = _cachedContainerBkg;
            }

            RebindContainerVisuals(clonedGameObject, container);

            container.m_openEffects = ChestAudioEffectManager.GetOpenEffects(customPrefabName);
            container.m_closeEffects = ChestAudioEffectManager.GetCloseEffects(customPrefabName);

            if (customPrefabName == Tier6FlametalPrefab)
            {
                FixTier6Materials(clonedGameObject);
            }

            if (!clonedGameObject.TryGetComponent(out WearNTear wearNTear))
            {
                wearNTear = clonedGameObject.GetComponentInChildren<WearNTear>();
                if (!wearNTear)
                {
                    wearNTear = clonedGameObject.AddComponent<WearNTear>();
                }
            }
            wearNTear.m_health = health;
            wearNTear.m_noRoofWear = true;
            wearNTear.m_noSupportWear = true;
            wearNTear.m_hitEffect = ChestAudioEffectManager.GetHitEffects(customPrefabName);
            wearNTear.m_destroyedEffect = ChestAudioEffectManager.GetDestroyEffects(customPrefabName);

            FixPieceLayers(clonedGameObject);

            if (!clonedGameObject.TryGetComponent(out ChestGlowController glowController))
            {
                glowController = clonedGameObject.AddComponent<ChestGlowController>();
            }
            glowController.Configure(customPrefabName, glowColor);

            try
            {
                var customIcon = RenderManager.Instance.Render(clonedGameObject);
                if (customIcon != null)
                {
                    piece.m_icon = customIcon;
                    Plugin.LogDebug($"[PieceManager] Generated 3D snapshot icon for '{customPrefabName}'.");
                }
            }
            catch (Exception ex)
            {
                Plugin.Log.LogWarning($"[PieceManager] Failed to render 3D icon for '{customPrefabName}': {ex.Message}");
            }

            var pieceConfig = new PieceConfig
            {
                Name = nameToken,
                Description = descToken,
                PieceTable = PieceTables.Hammer,
                Category = categoryName,
                Usage = new[] { PieceUsages.Storage, PieceUsages.Furniture },
                CraftingStation = stationString,
                Requirements = requirements
            };

            var customPiece = new CustomPiece(clonedGameObject, fixReference: true, pieceConfig);
            PieceManager.Instance.AddPiece(customPiece);

            Plugin.LogDebug($"[PieceManager] Successfully registered CustomPiece '{customPrefabName}' in Hammer.");
        }

        private static void FixPieceLayers(GameObject prefab)
        {
            int pieceLayer = LayerMask.NameToLayer("piece");
            if (pieceLayer < 0) pieceLayer = 0;

            prefab.layer = pieceLayer;
            foreach (var col in prefab.GetComponentsInChildren<Collider>(true))
            {
                if (!col.isTrigger)
                {
                    col.gameObject.layer = pieceLayer;
                }
            }
        }



        private static void RebindContainerVisuals(GameObject clonedGameObject, Container container)
        {
            if (container.m_open != null && container.m_open.transform.IsChildOf(clonedGameObject.transform) &&
                container.m_closed != null && container.m_closed.transform.IsChildOf(clonedGameObject.transform))
            {
                return;
            }

            GameObject? openObj = null;
            GameObject? closedObj = null;

            foreach (var t in clonedGameObject.GetComponentsInChildren<Transform>(true))
            {
                if (t.gameObject == clonedGameObject) continue;
                string nameLower = t.name.ToLowerInvariant();

                if (openObj == null && (nameLower.EndsWith("_open") || nameLower == "open" || nameLower.EndsWith("top_open")))
                {
                    openObj = t.gameObject;
                }
                if (closedObj == null && (nameLower.EndsWith("_closed") || nameLower == "closed" || nameLower.EndsWith("top_closed")))
                {
                    closedObj = t.gameObject;
                }
            }

            if (openObj != null)
            {
                container.m_open = openObj;
                container.m_open.SetActive(false);
                Plugin.LogDebug($"[PieceManager] Repaired container.m_open to '{openObj.name}' on '{clonedGameObject.name}'.");
            }
            else
            {
                Plugin.Log.LogWarning($"[PieceManager] Could not find open visual for '{clonedGameObject.name}'.");
            }

            if (closedObj != null)
            {
                container.m_closed = closedObj;
                container.m_closed.SetActive(true);
                Plugin.LogDebug($"[PieceManager] Repaired container.m_closed to '{closedObj.name}' on '{clonedGameObject.name}'.");
            }
            else
            {
                Plugin.Log.LogWarning($"[PieceManager] Could not find closed visual for '{clonedGameObject.name}'.");
            }
        }

        public static Bounds GetRendererBounds(GameObject prefab)
        {
            Bounds desiredBounds = new Bounds();
            bool hasBounds = false;
            foreach (Renderer renderer in prefab.GetComponentsInChildren<Renderer>(true))
            {
                if (renderer is not MeshRenderer && renderer is not SkinnedMeshRenderer) continue;
                if (!hasBounds)
                {
                    desiredBounds = renderer.bounds;
                    hasBounds = true;
                }
                else
                {
                    desiredBounds.Encapsulate(renderer.bounds);
                }
            }
            return desiredBounds;
        }

        private static void OnPiecesRegistered()
        {
            Plugin.LogDebug("[PieceManager] OnPiecesRegistered triggered. Updating piece recipes and stations.");
            UpdateAllPieceConfigurations();
        }

        public static void UpdateAllPieceConfigurations()
        {
            UpdatePieceConfiguration(Tier1ChainedPrefab);
            UpdatePieceConfiguration(Tier2BronzePrefab);
            UpdatePieceConfiguration(Tier3IronboundPrefab);
            UpdatePieceConfiguration(Tier4SilverPrefab);
            UpdatePieceConfiguration(Tier5ClockworkPrefab);
            UpdatePieceConfiguration(Tier6FlametalPrefab);
            UpdatePieceConfiguration(Tier7ReliquaryPrefab);
        }

        public static void UpdateAllPieceCategories()
        {
            string categoryName = GetConfiguredCategoryName();
            var pieceCategory = PieceManager.Instance.AddPieceCategory(categoryName);

            string[] allPrefabs =
            {
                Tier1ChainedPrefab,
                Tier2BronzePrefab,
                Tier3IronboundPrefab,
                Tier4SilverPrefab,
                Tier5ClockworkPrefab,
                Tier6FlametalPrefab,
                Tier7ReliquaryPrefab
            };

            foreach (var prefabName in allPrefabs)
            {
                var customPiece = PieceManager.Instance.GetPiece(prefabName);
                if (customPiece?.PiecePrefab != null)
                {
                    var piece = customPiece.PiecePrefab.GetComponent<Piece>();
                    if (piece != null)
                    {
                        piece.m_category = pieceCategory;
                    }
                }
            }

            if (Player.m_localPlayer != null)
            {
                Player.m_localPlayer.UpdateAvailablePiecesList();
            }

            Plugin.LogDebug($"[PieceManager] Updated piece category to '{categoryName}' in real time.");
        }

        public static void UpdatePieceConfiguration(string prefabName)
        {
            if (SceneManager.GetActiveScene().name != "main")
            {
                Plugin.LogDebug($"[PieceManager] Active scene is '{SceneManager.GetActiveScene().name}'. Deferring piece update for '{prefabName}' until world loads.");
                return;
            }

            var customPiece = PieceManager.Instance.GetPiece(prefabName);
            if (customPiece == null || customPiece.PiecePrefab == null)
            {
                Plugin.LogDebug($"[PieceManager] CustomPiece '{prefabName}' not found in registry for config update.");
                return;
            }

            if (!TryGetPieceConfig(prefabName, out string recipeString, out string stationString, out int width, out int height, out float health))
            {
                return;
            }

            var piece = customPiece.PiecePrefab.GetComponent<Piece>();
            if (piece != null)
            {
                piece.m_category = GetConfiguredPieceCategory();
            }

            if (TryParseAndValidateRecipe(recipeString, prefabName, out var requirements))
            {
                var pieceRequirements = new List<Piece.Requirement>();
                foreach (var req in requirements)
                {
                    var itemPrefab = PrefabManager.Instance.GetPrefab(req.Item);
                    var itemDrop = itemPrefab != null ? itemPrefab.GetComponent<ItemDrop>() : null;
                    if (itemDrop != null)
                    {
                        pieceRequirements.Add(new Piece.Requirement
                        {
                            m_resItem = itemDrop,
                            m_amount = req.Amount,
                            m_amountPerLevel = req.AmountPerLevel,
                            m_recover = req.Recover
                        });
                    }
                    else
                    {
                        Plugin.Log.LogError($"[PieceManager] Could not resolve ItemDrop for requirement '{req.Item}' on piece '{prefabName}'.");
                    }
                }

                if (piece != null)
                {
                    piece.m_resources = pieceRequirements.ToArray();
                }
            }

            if (piece != null)
            {
                string stationName = CraftingStations.GetInternalName(stationString);
                if (string.IsNullOrEmpty(stationName) || stationName.Equals("None", StringComparison.OrdinalIgnoreCase))
                {
                    piece.m_craftingStation = null;
                }
                else
                {
                    var stationPrefab = PrefabManager.Instance.GetPrefab(stationName);
                    if (stationPrefab != null)
                    {
                        piece.m_craftingStation = stationPrefab.GetComponent<CraftingStation>();
                    }
                    else
                    {
                        Plugin.Log.LogWarning($"[PieceManager] Station prefab '{stationName}' not found for piece '{prefabName}'.");
                        piece.m_craftingStation = null;
                    }
                }
            }

            var container = customPiece.PiecePrefab.GetComponent<Container>();
            if (container != null)
            {
                container.m_width = width;
                container.m_height = height;
            }

            var wearNTear = customPiece.PiecePrefab.GetComponent<WearNTear>();
            if (wearNTear != null)
            {
                wearNTear.m_health = health;
            }

            if (Player.m_localPlayer != null)
            {
                Player.m_localPlayer.UpdateKnownRecipesList();
                Player.m_localPlayer.UpdateAvailablePiecesList();
            }

            Plugin.LogDebug($"[PieceManager] Updated '{prefabName}' configuration in real time.");
        }

        public static bool TryGetPieceConfig(
            string prefabName,
            out string recipeString,
            out string stationString,
            out int width,
            out int height,
            out float health)
        {
            if (prefabName == Tier1ChainedPrefab)
            {
                recipeString = ModConfig.ChainedRecipe.Value;
                stationString = ModConfig.ChainedCraftingStation.Value;
                width = ModConfig.ChainedWidth.Value;
                height = ModConfig.ChainedHeight.Value;
                health = ModConfig.ChainedHealth.Value;
                return true;
            }
            if (prefabName == Tier2BronzePrefab)
            {
                recipeString = ModConfig.BronzeRecipe.Value;
                stationString = ModConfig.BronzeCraftingStation.Value;
                width = ModConfig.BronzeWidth.Value;
                height = ModConfig.BronzeHeight.Value;
                health = ModConfig.BronzeHealth.Value;
                return true;
            }
            if (prefabName == Tier3IronboundPrefab)
            {
                recipeString = ModConfig.IronboundRecipe.Value;
                stationString = ModConfig.IronboundCraftingStation.Value;
                width = ModConfig.IronboundWidth.Value;
                height = ModConfig.IronboundHeight.Value;
                health = ModConfig.IronboundHealth.Value;
                return true;
            }
            if (prefabName == Tier4SilverPrefab)
            {
                recipeString = ModConfig.SilverRecipe.Value;
                stationString = ModConfig.SilverCraftingStation.Value;
                width = ModConfig.SilverWidth.Value;
                height = ModConfig.SilverHeight.Value;
                health = ModConfig.SilverHealth.Value;
                return true;
            }
            if (prefabName == Tier5ClockworkPrefab)
            {
                recipeString = ModConfig.ClockworkRecipe.Value;
                stationString = ModConfig.ClockworkCraftingStation.Value;
                width = ModConfig.ClockworkWidth.Value;
                height = ModConfig.ClockworkHeight.Value;
                health = ModConfig.ClockworkHealth.Value;
                return true;
            }
            if (prefabName == Tier6FlametalPrefab)
            {
                recipeString = ModConfig.FlametalRecipe.Value;
                stationString = ModConfig.FlametalCraftingStation.Value;
                width = ModConfig.FlametalWidth.Value;
                height = ModConfig.FlametalHeight.Value;
                health = ModConfig.FlametalHealth.Value;
                return true;
            }
            if (prefabName == Tier7ReliquaryPrefab)
            {
                recipeString = ModConfig.ReliquaryRecipe.Value;
                stationString = ModConfig.ReliquaryCraftingStation.Value;
                width = ModConfig.ReliquaryWidth.Value;
                height = ModConfig.ReliquaryHeight.Value;
                health = ModConfig.ReliquaryHealth.Value;
                return true;
            }

            recipeString = string.Empty;
            stationString = string.Empty;
            width = 3;
            height = 2;
            health = 100f;
            return false;
        }

        public static bool IsValidItemPrefab(string itemName)
        {
            if (string.IsNullOrWhiteSpace(itemName)) return false;
            if (itemName.Equals(RecipeConfigDrawer.DefaultPlaceholderPrefab, StringComparison.OrdinalIgnoreCase)) return false;

            var prefab = PrefabManager.Instance != null ? PrefabManager.Instance.GetPrefab(itemName) : null;
            return prefab && prefab.TryGetComponent<ItemDrop>(out _);
        }

        public static bool TryParseAndValidateRecipe(string recipeString, string pieceName, out RequirementConfig[] requirements)
        {
            requirements = Array.Empty<RequirementConfig>();

            if (string.IsNullOrWhiteSpace(recipeString))
            {
                Plugin.Log.LogError($"[PieceManager] Recipe for piece '{pieceName}' is empty or whitespace.");
                return false;
            }

            var parsedItems = RecipeConfigDrawer.Deserialize(recipeString);
            if (parsedItems.Count == 0)
            {
                Plugin.Log.LogError($"[PieceManager] Recipe for piece '{pieceName}' contained no requirement entries.");
                return false;
            }

            var result = new List<RequirementConfig>();
            foreach (var item in parsedItems)
            {
                if (string.IsNullOrWhiteSpace(item.PrefabName))
                {
                    Plugin.Log.LogError($"[PieceManager] Recipe for piece '{pieceName}' contains an empty prefab name.");
                    return false;
                }

                if (item.PrefabName.Equals(RecipeConfigDrawer.DefaultPlaceholderPrefab, StringComparison.OrdinalIgnoreCase))
                {
                    Plugin.Log.LogError($"[PieceManager] Recipe for piece '{pieceName}' contains unconfigured placeholder '{RecipeConfigDrawer.DefaultPlaceholderPrefab}'.");
                    return false;
                }

                if (item.Amount <= 0)
                {
                    Plugin.Log.LogError($"[PieceManager] Requirement '{item.PrefabName}' for piece '{pieceName}' has invalid amount {item.Amount}.");
                    return false;
                }

                if (PrefabManager.Instance != null && !IsValidItemPrefab(item.PrefabName))
                {
                    Plugin.Log.LogError($"[PieceManager] Requirement '{item.PrefabName}' for piece '{pieceName}' does not exist in ObjectDB/PrefabManager.");
                    return false;
                }

                result.Add(new RequirementConfig(item.PrefabName, item.Amount, 0, true));
            }

            requirements = result.ToArray();
            return true;
        }

        #region Donor Asset Corrections

        private static void RotateChildren180(GameObject prefab)
        {
            var rotation180DegreesY = Quaternion.Euler(0f, 180f, 0f);
            foreach (Transform child in prefab.transform)
            {
                child.localPosition = rotation180DegreesY * child.localPosition;
                child.localRotation = rotation180DegreesY * child.localRotation;
            }

            Plugin.LogDebug($"[PieceManager] Rotated child hierarchy 180 degrees around Y for '{prefab.name}'.");
        }

        private static void FixTier6Materials(GameObject prefab)
        {
            var pieceShader = PrefabManager.Instance.GetPrefab(NativePersonalChestDonor)
                .GetComponentInChildren<MeshRenderer>().sharedMaterial.shader;

            foreach (var renderer in prefab.GetComponentsInChildren<MeshRenderer>(true))
            {
                var originalMaterial = renderer.sharedMaterial;
                if (originalMaterial == null) continue;

                var pieceMaterial = new Material(originalMaterial) { shader = pieceShader };

                var runeMask = originalMaterial.GetTexture("_EmissiveTex");
                pieceMaterial.SetTexture("_EmissionMap", runeMask);
                pieceMaterial.SetTexture("_EmissiveTex", runeMask);
                pieceMaterial.SetColor("_EmissionColor", originalMaterial.GetColor("_EmissionColor"));
                pieceMaterial.EnableKeyword("_EMISSION");

                renderer.sharedMaterials = new[] { pieceMaterial };
            }
        }

        #endregion
    }
}

