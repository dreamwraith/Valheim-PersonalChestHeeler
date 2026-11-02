using System;
using System.Reflection;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Jotunn.Managers;
using Jotunn.Utils;
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
            new ConfigFileWatcher(Config);

            // servers only need the pieces and synced settings
            if (GUIManager.IsHeadless())
            {
                ChestPieceManager.Initialize();
                return;
            }

            ModLocalization.Initialize();

            // audio has to subscribe first so its prefabs exist when the pieces register
            ChestAudioEffectManager.Initialize();
            ChestPieceManager.Initialize();

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
