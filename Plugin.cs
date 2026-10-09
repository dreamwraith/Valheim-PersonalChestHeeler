using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using PersonalChestHeeler.Configuration;
using PersonalChestHeeler.Localization;
using PersonalChestHeeler.Pieces;

namespace PersonalChestHeeler
{
    [BepInPlugin(ModGUID, ModName, ModVersion)]
    [BepInDependency("com.jotunn.jotunn")]
    [BepInDependency("_shudnal.ConditionalConfigSync")]
    public class Plugin : BaseUnityPlugin
    {
        public const string ModGUID = "dreamwraith.PersonalChestHeeler";
        public const string ModName = "PersonalChestHeeler";
        public const string ModVersion = VersionInfo.Version;

        internal static Plugin? Instance { get; private set; }
        internal static ManualLogSource Log = null!;
        private Harmony? _harmony;

        private void Awake()
        {
            Instance = this;
            Log = Logger;

            ModConfig.Initialize(Config);
            ModLocalization.Initialize();
            ChestAudioEffectManager.Initialize();
            ChestPieceManager.Initialize();

            if (!ModConfig.EnableMod.Value)
            {
                Log.LogInfo($"{ModName} is disabled in configuration.");
                return;
            }

            _harmony = Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), ModGUID);
            Log.LogInfo($"{ModName} v{ModVersion} loaded successfully.");
        }

        private void OnDestroy()
        {
            _harmony?.UnpatchSelf();
        }

        internal static void LogDebug(string message)
        {
            if (ModConfig.EnableDebugLogs != null && ModConfig.EnableDebugLogs.Value)
            {
                Log.LogInfo($"[DEBUG] {message}");
            }
        }
    }
}
