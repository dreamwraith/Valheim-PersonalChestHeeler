using System;
using System.Collections.Generic;
using UnityEngine;
using Jotunn.Managers;
using PersonalChestHeeler.Configuration;

namespace PersonalChestHeeler.Pieces
{
    /// <summary>
    /// Controls the point light, emissive gem, and particle flare lock indicator on chests.
    /// </summary>
    public class ChestGlowController : MonoBehaviour
    {
        private static readonly HashSet<ChestGlowController> _activeControllers = new();
        public static IReadOnlyCollection<ChestGlowController> ActiveControllers => _activeControllers;

        private static Material? _demisterBallMaterialTemplate;
        private static GameObject? _flareTemplate;
        private static bool _templatesResolved;

        [SerializeField]
        public string TierPrefabName = string.Empty;

        [SerializeField]
        public Color GlowColor = Color.white;

        private Light? _pointLight;
        private ParticleSystem? _flareParticles;
        private Material? _gemMaterial;
        private GameObject? _gemObject;
        private Transform? _lockRoot;
        private bool _initialized;

        public static void EnsureVisualTemplates()
        {
            if (_templatesResolved && _demisterBallMaterialTemplate != null && _flareTemplate != null)
            {
                return;
            }

            var mistTorch = PrefabManager.Instance?.GetPrefab("piece_groundtorch_mist");
            if (mistTorch != null)
            {
                var ballRenderer = mistTorch.transform.Find("_enabled/demister_ball (1)")?.GetComponent<Renderer>();
                if (ballRenderer != null && ballRenderer.sharedMaterial != null)
                {
                    _demisterBallMaterialTemplate = ballRenderer.sharedMaterial;
                }

                var flareTransform = mistTorch.transform.Find("_enabled/flare");
                if (flareTransform != null)
                {
                    _flareTemplate = flareTransform.gameObject;
                }
            }

            _templatesResolved = true;
        }

        private void OnEnable()
        {
            _activeControllers.Add(this);

            if (!string.IsNullOrEmpty(TierPrefabName))
            {
                GlowColor = GetLockColor(TierPrefabName);
            }

            if (!_initialized)
            {
                InitializeGlow();
            }
            else
            {
                ApplyCurrentSettings();
            }
        }

        private void OnDisable()
        {
            _activeControllers.Remove(this);
        }

        private void OnDestroy()
        {
            _activeControllers.Remove(this);
            if (_gemMaterial != null)
            {
                Destroy(_gemMaterial);
                _gemMaterial = null;
            }
        }

        public void Configure(string prefabName, Color color)
        {
            TierPrefabName = prefabName;
            GlowColor = GetLockColor(prefabName);

            if (_initialized)
            {
                ApplyCurrentSettings();
            }
        }

        public static Vector3 GetLockLocalOffset(string prefabName) => prefabName switch
        {
            ChestPieceManager.Tier1ChainedPrefab => new Vector3(0.00f, 0.743f, 0.596f),
            ChestPieceManager.Tier2BronzePrefab => new Vector3(0.00f, 0.380f, 0.250f),
            ChestPieceManager.Tier3IronboundPrefab => new Vector3(0.00f, 0.746f, 0.476f),
            ChestPieceManager.Tier4SilverPrefab => new Vector3(0.00f, 0.590f, 0.360f),
            ChestPieceManager.Tier5ClockworkPrefab => new Vector3(0.00f, 0.600f, 0.360f),
            ChestPieceManager.Tier6FlametalPrefab => new Vector3(0.00f, 1.350f, 0.630f),
            ChestPieceManager.Tier7ReliquaryPrefab => new Vector3(0.00f, 1.300f, 0.920f),
            _ => new Vector3(0f, 0.50f, 0.50f)
        };

        public static Color GetLockColor(string prefabName) => prefabName switch
        {
            ChestPieceManager.Tier1ChainedPrefab => ChestPieceManager.ColorChained,
            ChestPieceManager.Tier2BronzePrefab => ChestPieceManager.ColorBronze,
            ChestPieceManager.Tier3IronboundPrefab => ChestPieceManager.ColorIronbound,
            ChestPieceManager.Tier4SilverPrefab => ChestPieceManager.ColorSilver,
            ChestPieceManager.Tier5ClockworkPrefab => ChestPieceManager.ColorClockwork,
            ChestPieceManager.Tier6FlametalPrefab => ChestPieceManager.ColorFlametal,
            ChestPieceManager.Tier7ReliquaryPrefab => ChestPieceManager.ColorReliquary,
            _ => Color.white
        };

        private void InitializeGlow()
        {
            _initialized = true;
            EnsureVisualTemplates();

            var childRoot = transform.Find("LockRoot") ?? transform.Find("LockGlowLight");
            GameObject lockRoot;
            if (childRoot == null)
            {
                lockRoot = new GameObject("LockRoot");
                lockRoot.transform.SetParent(transform, false);
            }
            else
            {
                lockRoot = childRoot.gameObject;
                lockRoot.name = "LockRoot";
            }

            _lockRoot = lockRoot.transform;
            _lockRoot.localPosition = GetLockLocalOffset(TierPrefabName);
            _lockRoot.localRotation = Quaternion.identity;
            UpdateLockRootScale();

            var gemTransform = lockRoot.transform.Find("LockGem");
            if (gemTransform == null)
            {
                _gemObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                _gemObject.name = "LockGem";
                _gemObject.transform.SetParent(lockRoot.transform, false);
                _gemObject.transform.localPosition = Vector3.zero;
                _gemObject.transform.localRotation = Quaternion.identity;
                _gemObject.transform.localScale = new Vector3(0.065f, 0.065f, 0.035f);

                var gemCollider = _gemObject.GetComponent<Collider>();
                if (gemCollider != null)
                {
                    Destroy(gemCollider);
                }
            }
            else
            {
                _gemObject = gemTransform.gameObject;
            }

            var gemRenderer = _gemObject.GetComponent<MeshRenderer>();
            if (gemRenderer != null && _demisterBallMaterialTemplate != null)
            {
                if (_gemMaterial == null)
                {
                    _gemMaterial = new Material(_demisterBallMaterialTemplate);
                    _gemMaterial.name = $"LockGemMat_{TierPrefabName}";
                }
                gemRenderer.sharedMaterial = _gemMaterial;
            }

            var flareTransform = lockRoot.transform.Find("LockFlare");
            GameObject? flareGameObject = null;
            if (flareTransform == null && _flareTemplate != null)
            {
                flareGameObject = Instantiate(_flareTemplate, lockRoot.transform, false);
                flareGameObject.name = "LockFlare";
                flareGameObject.transform.localPosition = Vector3.zero;
                flareGameObject.transform.localRotation = Quaternion.identity;

                foreach (var lightComponent in flareGameObject.GetComponentsInChildren<Light>(true))
                {
                    Destroy(lightComponent);
                }

                foreach (var audioSource in flareGameObject.GetComponentsInChildren<AudioSource>(true))
                {
                    Destroy(audioSource);
                }
            }
            else if (flareTransform != null)
            {
                flareGameObject = flareTransform.gameObject;
            }

            if (flareGameObject != null)
            {
                _flareParticles = flareGameObject.GetComponent<ParticleSystem>();
                if (_flareParticles != null)
                {
                    var main = _flareParticles.main;
                    main.simulationSpace = ParticleSystemSimulationSpace.Local;
                    main.startSize = 0.22f;
                    main.loop = true;
                    main.playOnAwake = true;
                }
            }

            _pointLight = lockRoot.GetComponent<Light>();
            if (_pointLight == null)
            {
                _pointLight = lockRoot.AddComponent<Light>();
            }

            _pointLight.type = LightType.Point;
            _pointLight.renderMode = LightRenderMode.ForcePixel;
            _pointLight.shadows = LightShadows.None;
            _pointLight.range = 2.4f;
            _pointLight.intensity = 1.6f;
            _pointLight.color = GlowColor;

            ApplyCurrentSettings();
        }

        private void UpdateLockRootScale()
        {
            if (_lockRoot == null) return;
            Vector3 parentScale = transform.localScale;
            if (Mathf.Abs(parentScale.x) > 0.0001f && Mathf.Abs(parentScale.y) > 0.0001f && Mathf.Abs(parentScale.z) > 0.0001f)
            {
                _lockRoot.localScale = new Vector3(
                    1f / parentScale.x,
                    1f / parentScale.y,
                    1f / parentScale.z
                );
            }
        }

        public void ApplyCurrentSettings()
        {
            if (_lockRoot == null)
            {
                var child = transform.Find("LockRoot") ?? transform.Find("LockGlowLight");
                if (child != null) _lockRoot = child;
            }
            if (_lockRoot != null && !string.IsNullOrEmpty(TierPrefabName))
            {
                GlowColor = GetLockColor(TierPrefabName);
                _lockRoot.localPosition = GetLockLocalOffset(TierPrefabName);
                UpdateLockRootScale();
            }

            bool glowEnabled = ModConfig.EnableChestGlow != null && ModConfig.EnableChestGlow.Value;
            float intensityMultiplier = ModConfig.ChestGlowIntensity != null ? ModConfig.ChestGlowIntensity.Value : 1.0f;
            bool active = glowEnabled && intensityMultiplier > 0.01f;

            if (_pointLight != null)
            {
                _pointLight.color = GlowColor;
                _pointLight.range = 2.4f;
                _pointLight.intensity = 1.6f * intensityMultiplier;
                _pointLight.enabled = active;
            }

            if (_gemMaterial != null)
            {
                _gemMaterial.SetColor("_Color", GlowColor);
                if (active)
                {
                    _gemMaterial.EnableKeyword("_EMISSION");
                    _gemMaterial.SetColor("_EmissionColor", GlowColor * (2.2f * intensityMultiplier));
                }
                else
                {
                    _gemMaterial.DisableKeyword("_EMISSION");
                    _gemMaterial.SetColor("_EmissionColor", Color.black);
                }
            }

            if (_gemObject != null)
            {
                _gemObject.SetActive(active);
            }

            if (_flareParticles != null)
            {
                if (active)
                {
                    var main = _flareParticles.main;
                    float alpha = GlowColor.a > 0.01f ? GlowColor.a * 0.80f : 0.80f;
                    main.startColor = new ParticleSystem.MinMaxGradient(new Color(GlowColor.r, GlowColor.g, GlowColor.b, alpha * intensityMultiplier));
                    _flareParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    _flareParticles.Play();
                }
                else
                {
                    if (_flareParticles.isPlaying)
                    {
                        _flareParticles.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
                    }
                }
            }
        }

        public static void RefreshAll()
        {
            foreach (var controller in _activeControllers)
            {
                if (controller != null)
                {
                    controller.ApplyCurrentSettings();
                }
            }
        }
    }
}

