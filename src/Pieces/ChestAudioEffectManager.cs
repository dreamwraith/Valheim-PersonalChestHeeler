using System;
using System.Collections.Generic;
using Jotunn.Managers;
using PersonalChestHeeler.Configuration;
using UnityEngine;

namespace PersonalChestHeeler.Pieces
{
    /// <summary>
    /// Manages custom audio effects across all 7 chest tiers using Jotunn's PrefabManager.
    /// Builds layered EffectLists for build, hit, destroy, open, and close events.
    /// </summary>
    public static class ChestAudioEffectManager
    {
        private enum SfxCategory
        {
            Base,
            Ambient,
            Magic
        }

        private static bool _initialized;

        private static GameObject? _sfxGlobalMagicBuild;
        private static GameObject? _sfxGlobalMagicHit;
        private static GameObject? _sfxGlobalMagicDestroy;
        private static GameObject? _sfxGlobalMagicOpen;
        private static GameObject? _sfxGlobalMagicClose;

        private static readonly Dictionary<string, GameObject> _customSfxPrefabs = new();

        public static void Initialize()
        {
            if (_initialized) return;
            _initialized = true;

            PrefabManager.OnVanillaPrefabsAvailable += RegisterAllCustomAudioPrefabs;

            Plugin.LogDebug("[AudioEffectManager] Subscribed to PrefabManager.OnVanillaPrefabsAvailable.");
        }

        private static void RegisterAllCustomAudioPrefabs()
        {
            try
            {

                _sfxGlobalMagicBuild = CreateCustomSfxPrefab("sfx_pch_magic_build", new[] { "Magic_CrystalBlast7" }, 1.0f, 1.0f, SfxCategory.Magic);
                _sfxGlobalMagicHit = CreateCustomSfxPrefab("sfx_pch_magic_hit", new[] { "magic_spell_electro_23" }, 0.85f, 1.0f, SfxCategory.Magic);
                _sfxGlobalMagicDestroy = CreateCustomSfxPrefab("sfx_pch_magic_destroy", new[] { "Magic_Spell16" }, 1.0f, 1.0f, SfxCategory.Magic);
                _sfxGlobalMagicOpen = CreateCustomSfxPrefab("sfx_pch_magic_open", new[] { "Magic_EnergyRingPass8" }, 0.90f, 1.0f, SfxCategory.Magic);
                _sfxGlobalMagicClose = CreateCustomSfxPrefab("sfx_pch_magic_close", new[] { "Magic_Poof3" }, 0.90f, 1.0f, SfxCategory.Magic);

                CreateTierPrefabs(
                    ChestPieceManager.Tier1ChainedPrefab,
                    buildClips: new[] { "UI_Build_Wood_01", "UI_Build_Wood_02", "UI_Build_Wood_03", "UI_Build_Wood_04", "UI_Build_Wood_05" },
                    hitClips: new[] {
                        "Enemy_TrainingDummy_Melee_Attack_Heavy_Impact_01", "Enemy_TrainingDummy_Melee_Attack_Heavy_Impact_02",
                        "Enemy_TrainingDummy_Melee_Attack_Heavy_Impact_03", "Enemy_TrainingDummy_Melee_Attack_Heavy_Impact_04",
                        "Enemy_TrainingDummy_Melee_Attack_Light_Impact_01", "Enemy_TrainingDummy_Melee_Attack_Light_Impact_02",
                        "Enemy_TrainingDummy_Melee_Attack_Light_Impact_03", "Enemy_TrainingDummy_Melee_Attack_Light_Impact_04"
                    },
                    destroyClips: new[] { "UI_Build_Wood_Break_M_01", "UI_Build_Wood_Break_M_02", "UI_Build_Wood_Break_M_03", "UI_Build_Wood_Break_M_04" },
                    openClips: new[] {
                        "Enemy_TrainingDummy_Idle_01", "Enemy_TrainingDummy_Idle_02", "Enemy_TrainingDummy_Idle_03",
                        "Enemy_TrainingDummy_Idle_04", "Enemy_TrainingDummy_Idle_05"
                    },
                    closeClips: new[] {
                        "Enemy_TrainingDummy_Idle_06", "Enemy_TrainingDummy_Idle_07", "Enemy_TrainingDummy_Idle_08",
                        "Enemy_TrainingDummy_Idle_09", "Enemy_TrainingDummy_Idle_10"
                    },
                    ambientOverlayClips: new[] { "Enemy_FrozenKing_PunchAOE_Chain_End_03" },
                    ambientBaseline: 1.0f
                );

                CreateAmbientOverlayPrefab(ChestPieceManager.Tier2BronzePrefab, new[] { "Obliterator_Lever_Pull" }, 1.0f);
                CreateAmbientOverlayPrefab(ChestPieceManager.Tier3IronboundPrefab, new[] { "Obliterator_Lever_Pull" }, 1.0f);

                CreateTierPrefabs(
                    ChestPieceManager.Tier4SilverPrefab,
                    buildClips: new[] {
                        "Weapons_ArbalestCrossbow_Shot_01", "Weapons_ArbalestCrossbow_Shot_02", "Weapons_ArbalestCrossbow_Shot_03",
                        "Weapons_ArbalestCrossbow_Shot_04", "Weapons_ArbalestCrossbow_Shot_05", "Weapons_ArbalestCrossbow_Shot_06"
                    },
                    hitClips: new[] { "Branch_Hit_01", "Branch_Hit_02", "Branch_Hit_03", "Branch_Hit_04" },
                    destroyClips: new[] { "UI_Build_TreasureChest_Break_01", "UI_Build_TreasureChest_Break_02", "UI_Build_TreasureChest_Break_03", "UI_Build_TreasureChest_Break_04" },
                    openClips: new[] { "Objects_Gate_Darkwood_Open_01", "Objects_Gate_Darkwood_Open_02", "Objects_Gate_Darkwood_Open_03" },
                    closeClips: new[] { "Objects_StoneOven_Door_Close_01", "Objects_StoneOven_Door_Close_02", "Objects_StoneOven_Door_Close_03" },
                    ambientOverlayClips: new[] { "Items_Trap_Open" },
                    ambientBaseline: 1.0f,
                    openPitch: 1.25f
                );

                CreateTierPrefabs(
                    ChestPieceManager.Tier5ClockworkPrefab,
                    buildClips: new[] {
                        "Battering_Ram_Piston_Impact_01", "Battering_Ram_Piston_Impact_02", "Battering_Ram_Piston_Impact_03",
                        "Battering_Ram_Piston_Impact_04", "Battering_Ram_Piston_Impact_05", "Battering_Ram_Piston_Impact_06"
                    },
                    hitClips: new[] {
                        "Player_Block_MetalShield_M_01", "Player_Block_MetalShield_M_02", "Player_Block_MetalShield_M_03",
                        "Player_Block_MetalShield_Overlay_A_M_01", "Player_Block_MetalShield_Overlay_A_M_02", "Player_Block_MetalShield_Overlay_A_M_03"
                    },
                    destroyClips: new[] { "UI_Build_TreasureChest_Break_01", "UI_Build_TreasureChest_Break_02", "UI_Build_TreasureChest_Break_03", "UI_Build_TreasureChest_Break_04" },
                    openClips: new[] { "Siege_Catapult_Legs_Up_02", "Siege_Catapult_Legs_Up_03" },
                    closeClips: new[] {
                        "Enemy_TrainingDummy_Heavy_Attack_01", "Enemy_TrainingDummy_Heavy_Attack_02",
                        "Enemy_TrainingDummy_Heavy_Attack_03", "Enemy_TrainingDummy_Heavy_Attack_04"
                    },
                    ambientOverlayClips: new[] { "Items_Trap_Open" },
                    ambientBaseline: 1.0f,
                    openPitch: 1.50f,
                    closePitch: 1.50f
                );

                CreateTierPrefabs(
                    ChestPieceManager.Tier6FlametalPrefab,
                    buildClips: new[] {
                        "Enemy_CharredArcher_BowRelease_01", "Enemy_CharredArcher_BowRelease_02", "Enemy_CharredArcher_BowRelease_03",
                        "Enemy_CharredArcher_BowRelease_04", "Enemy_CharredArcher_BowRelease_05"
                    },
                    hitClips: new[] {
                        "Enemy_Father_Fissure_Pillar_01", "Enemy_Father_Fissure_Pillar_02", "Enemy_Father_Fissure_Pillar_03",
                        "Enemy_Father_Fissure_Pillar_04", "Enemy_Father_Fissure_Pillar_05"
                    },
                    destroyClips: new[] {
                        "LavaBomb_Rocks_01", "LavaBomb_Rocks_02", "LavaBomb_Rocks_03",
                        "LavaBomb_Rocks_04", "LavaBomb_Rocks_05", "LavaBomb_Rocks_06"
                    },
                    openClips: new[] {
                        "Enemy_CharredArcher_BowDraw_01", "Enemy_CharredArcher_BowDraw_02", "Enemy_CharredArcher_BowDraw_03",
                        "Enemy_CharredArcher_BowDraw_04", "Enemy_CharredArcher_BowDraw_05"
                    },
                    closeClips: new[] {
                        "Enemy_Firetroll_Attack_Club_SwingDown_01", "Enemy_Firetroll_Attack_Club_SwingDown_02",
                        "Enemy_Firetroll_Attack_Club_SwingDown_03", "Enemy_Firetroll_Attack_Club_SwingDown_04"
                    },
                    ambientOverlayClips: new[] { "Enemy_Father_FireWall_FireBurst_05" },
                    ambientBaseline: 0.80f,
                    openPitch: 0.75f,
                    closePitch: 0.75f
                );

                CreateTierPrefabs(
                    ChestPieceManager.Tier7ReliquaryPrefab,
                    buildClips: new[] { "Weapons_IronSledge_Impact_S_01", "Weapons_IronSledge_Impact_S_02", "Weapons_IronSledge_Impact_S_03" },
                    hitClips: new[] {
                        "Player_Weapons_Ice_Hit_01", "Player_Weapons_Ice_Hit_02", "Player_Weapons_Ice_Hit_03",
                        "Player_Weapons_Ice_Hit_04", "Player_Weapons_Ice_Hit_05"
                    },
                    destroyClips: new[] { "Smash_HugeRockRip1", "Smash_HugeRockRip2" },
                    openClips: new[] {
                        "Enemy_FrozenKing_Tendril_Grow_01", "Enemy_FrozenKing_Tendril_Grow_02",
                        "Enemy_FrozenKing_Tendril_Grow_03", "Enemy_FrozenKing_Tendril_Grow_04"
                    },
                    closeClips: new[] {
                        "Battering_Ram_Piston_Impact_Rock_01", "Battering_Ram_Piston_Impact_Rock_03",
                        "Battering_Ram_Piston_Impact_Rock_04", "Battering_Ram_Piston_Impact_Rock_05"
                    },
                    ambientOverlayClips: new[] { "Scrape_Stone_Loop" },
                    ambientBaseline: 0.55f,
                    openPitch: 1.25f,
                    closePitch: 1.10f
                );

                Plugin.LogDebug("[AudioEffectManager] Successfully created and registered all custom chest SFX prefabs via Jotunn.");
            }
            catch (Exception ex)
            {
                Plugin.Log.LogError($"[AudioEffectManager] Failed to register custom audio prefabs: {ex}");
            }
        }

        private static void CreateTierPrefabs(
            string tierKey,
            string[] buildClips,
            string[] hitClips,
            string[] destroyClips,
            string[] openClips,
            string[] closeClips,
            string[] ambientOverlayClips,
            float ambientBaseline = 1.0f,
            float openPitch = 1.0f,
            float closePitch = 1.0f)
        {
            var buildPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_build", buildClips, 1.0f, 1.0f, SfxCategory.Base);
            var hitPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_hit", hitClips, 1.0f, 1.0f, SfxCategory.Base);
            var destroyPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_destroy", destroyClips, 1.0f, 1.0f, SfxCategory.Base);
            var openPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_open", openClips, 1.0f, openPitch, SfxCategory.Base);
            var closePrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_close", closeClips, 1.0f, closePitch, SfxCategory.Base);
            var ambientPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_amb", ambientOverlayClips, ambientBaseline, 1.0f, SfxCategory.Ambient);

            if (buildPrefab != null) _customSfxPrefabs[$"{tierKey}_build"] = buildPrefab;
            if (hitPrefab != null) _customSfxPrefabs[$"{tierKey}_hit"] = hitPrefab;
            if (destroyPrefab != null) _customSfxPrefabs[$"{tierKey}_destroy"] = destroyPrefab;
            if (openPrefab != null) _customSfxPrefabs[$"{tierKey}_open"] = openPrefab;
            if (closePrefab != null) _customSfxPrefabs[$"{tierKey}_close"] = closePrefab;
            if (ambientPrefab != null) _customSfxPrefabs[$"{tierKey}_amb"] = ambientPrefab;
        }

        private static void CreateAmbientOverlayPrefab(string tierKey, string[] ambientClips, float baseline)
        {
            var ambPrefab = CreateCustomSfxPrefab($"sfx_pch_{tierKey}_amb", ambientClips, baseline, 1.0f, SfxCategory.Ambient);
            if (ambPrefab != null)
            {
                _customSfxPrefabs[$"{tierKey}_amb"] = ambPrefab;
            }
        }

        private static GameObject? CreateCustomSfxPrefab(string customName, string[] clipNames, float baselineVolume, float pitch, SfxCategory category)
        {
            try
            {
                var resolvedClips = new List<AudioClip>();
                float longestClip = 1.5f;

                foreach (var clipName in clipNames)
                {
                    if (string.IsNullOrWhiteSpace(clipName)) continue;
                    var clip = PrefabManager.Cache.GetPrefab<AudioClip>(clipName.Trim());
                    if (clip != null)
                    {
                        resolvedClips.Add(clip);
                        if (clip.length > longestClip)
                        {
                            longestClip = clip.length;
                        }
                    }
                    else
                    {
                        Plugin.Log.LogWarning($"[AudioEffectManager] Failed to find AudioClip '{clipName}' via Jotunn Cache.");
                    }
                }

                if (resolvedClips.Count == 0)
                {
                    Plugin.Log.LogError($"[AudioEffectManager] No audio clips resolved for custom SFX '{customName}'.");
                    return null;
                }

                var sfxPrefab = PrefabManager.Instance.CreateClonedPrefab(customName, "sfx_chest_open");
                if (sfxPrefab == null)
                {
                    Plugin.Log.LogError($"[AudioEffectManager] Failed to clone 'sfx_chest_open' for '{customName}'.");
                    return null;
                }

                for (int i = sfxPrefab.transform.childCount - 1; i >= 0; i--)
                {
                    UnityEngine.Object.Destroy(sfxPrefab.transform.GetChild(i).gameObject);
                }

                var zsfxComponent = sfxPrefab.GetComponent<ZSFX>();
                if (zsfxComponent == null)
                {
                    zsfxComponent = sfxPrefab.AddComponent<ZSFX>();
                }

                float volumeMultiplier = category switch
                {
                    SfxCategory.Base => 1.25f,
                    SfxCategory.Ambient => 0.85f,
                    SfxCategory.Magic => 0.10f,
                    _ => 1.0f
                };

                float initialVolume = baselineVolume * volumeMultiplier;
                zsfxComponent.m_audioClips = resolvedClips.ToArray();
                zsfxComponent.m_minVol = initialVolume * 0.95f;
                zsfxComponent.m_maxVol = initialVolume * 1.05f;
                zsfxComponent.m_minPitch = pitch * 0.97f;
                zsfxComponent.m_maxPitch = pitch * 1.03f;
                zsfxComponent.m_playOnAwake = true;

                var timedDestruction = sfxPrefab.GetComponent<TimedDestruction>();
                if (timedDestruction != null)
                {
                    timedDestruction.m_timeout = Mathf.Max(2.5f, (longestClip / Mathf.Max(0.1f, pitch)) + 0.6f);
                    timedDestruction.m_triggerOnAwake = true;
                }

                Plugin.LogDebug($"[AudioEffectManager] Created custom SFX prefab '{customName}' with {resolvedClips.Count} clip(s) (Pitch: {pitch:F2}, Baseline: {baselineVolume:F2}).");
                return sfxPrefab;
            }
            catch (Exception exception)
            {
                Plugin.Log.LogError($"[AudioEffectManager] Exception creating SFX prefab '{customName}': {exception}");
                return null;
            }
        }

        public static EffectList GetPlaceEffects(string tierPrefabName)
        {
            var effectsList = new List<EffectList.EffectData>();

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_build", out var buildSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = buildSfxPrefab, m_enabled = true });
            }
            else
            {
                var defaultPieceComponent = PrefabManager.Instance.GetPrefab(ChestPieceManager.NativePersonalChestDonor)?.GetComponent<Piece>()
                                         ?? PrefabManager.Instance.GetPrefab(ChestPieceManager.NativeReinforcedDonor)?.GetComponent<Piece>();
                if (defaultPieceComponent != null && defaultPieceComponent.m_placeEffect != null && defaultPieceComponent.m_placeEffect.m_effectPrefabs != null)
                {
                    foreach (var effect in defaultPieceComponent.m_placeEffect.m_effectPrefabs)
                    {
                        if (effect?.m_prefab != null) effectsList.Add(effect);
                    }
                }
            }

            if (_sfxGlobalMagicBuild != null)
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = _sfxGlobalMagicBuild, m_enabled = true });
            }

            return new EffectList { m_effectPrefabs = effectsList.ToArray() };
        }

        public static EffectList GetHitEffects(string tierPrefabName)
        {
            var effectsList = new List<EffectList.EffectData>();

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_hit", out var hitSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = hitSfxPrefab, m_enabled = true });
            }
            else
            {
                var defaultWearNTearComponent = PrefabManager.Instance.GetPrefab(ChestPieceManager.NativePersonalChestDonor)?.GetComponent<WearNTear>()
                                             ?? PrefabManager.Instance.GetPrefab(ChestPieceManager.NativeReinforcedDonor)?.GetComponent<WearNTear>();
                if (defaultWearNTearComponent != null && defaultWearNTearComponent.m_hitEffect != null && defaultWearNTearComponent.m_hitEffect.m_effectPrefabs != null)
                {
                    foreach (var effect in defaultWearNTearComponent.m_hitEffect.m_effectPrefabs)
                    {
                        if (effect?.m_prefab != null) effectsList.Add(effect);
                    }
                }
            }

            if (_sfxGlobalMagicHit != null)
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = _sfxGlobalMagicHit, m_enabled = true });
            }

            return new EffectList { m_effectPrefabs = effectsList.ToArray() };
        }

        public static EffectList GetDestroyEffects(string tierPrefabName)
        {
            var effectsList = new List<EffectList.EffectData>();

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_destroy", out var destroySfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = destroySfxPrefab, m_enabled = true });
            }
            else
            {
                var defaultWearNTearComponent = PrefabManager.Instance.GetPrefab(ChestPieceManager.NativePersonalChestDonor)?.GetComponent<WearNTear>()
                                             ?? PrefabManager.Instance.GetPrefab(ChestPieceManager.NativeReinforcedDonor)?.GetComponent<WearNTear>();
                if (defaultWearNTearComponent != null && defaultWearNTearComponent.m_destroyedEffect != null && defaultWearNTearComponent.m_destroyedEffect.m_effectPrefabs != null)
                {
                    foreach (var effect in defaultWearNTearComponent.m_destroyedEffect.m_effectPrefabs)
                    {
                        if (effect?.m_prefab != null) effectsList.Add(effect);
                    }
                }
            }

            if (_sfxGlobalMagicDestroy != null)
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = _sfxGlobalMagicDestroy, m_enabled = true });
            }

            return new EffectList { m_effectPrefabs = effectsList.ToArray() };
        }

        public static EffectList GetOpenEffects(string tierPrefabName)
        {
            var effectsList = new List<EffectList.EffectData>();

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_open", out var openSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = openSfxPrefab, m_enabled = true });
            }
            else
            {
                var defaultOpen = PrefabManager.Instance.GetPrefab("sfx_chest_open");
                if (defaultOpen != null)
                {
                    effectsList.Add(new EffectList.EffectData { m_prefab = defaultOpen, m_enabled = true });
                }
            }

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_amb", out var ambientSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = ambientSfxPrefab, m_enabled = true });
            }

            if (_sfxGlobalMagicOpen != null)
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = _sfxGlobalMagicOpen, m_enabled = true });
            }

            return new EffectList { m_effectPrefabs = effectsList.ToArray() };
        }

        public static EffectList GetCloseEffects(string tierPrefabName)
        {
            var effectsList = new List<EffectList.EffectData>();

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_close", out var closeSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = closeSfxPrefab, m_enabled = true });
            }
            else
            {
                var defaultClose = PrefabManager.Instance.GetPrefab("sfx_chest_close");
                if (defaultClose != null)
                {
                    effectsList.Add(new EffectList.EffectData { m_prefab = defaultClose, m_enabled = true });
                }
            }

            if (_customSfxPrefabs.TryGetValue($"{tierPrefabName}_amb", out var ambientSfxPrefab))
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = ambientSfxPrefab, m_enabled = true });
            }

            if (_sfxGlobalMagicClose != null)
            {
                effectsList.Add(new EffectList.EffectData { m_prefab = _sfxGlobalMagicClose, m_enabled = true });
            }

            return new EffectList { m_effectPrefabs = effectsList.ToArray() };
        }
    }
}

