using System;
using HarmonyLib;
using Jotunn.Managers;
using PersonalChestHeeler.Configuration;
using PersonalChestHeeler.Localization;

namespace PersonalChestHeeler.Patches
{
    /// <summary>
    /// Harmony patches for Container access enforcement and HUD hover indicators.
    /// </summary>
    public static class ContainerPatches
    {
        [HarmonyPatch(typeof(Container), "CheckAccess")]
        public static class Container_CheckAccess_Patch
        {
            [HarmonyPrefix]
            public static bool Prefix(Container __instance, long playerID, ref bool __result)
            {
                if (ModConfig.EnableMod == null || !ModConfig.EnableMod.Value)
                {
                    return true;
                }

                // If admin bypass is enabled and local player is a verified server admin, grant access
                if (ModConfig.AdminBypassesLock != null && ModConfig.AdminBypassesLock.Value)
                {
                    if (SynchronizationManager.Instance != null && SynchronizationManager.Instance.PlayerIsAdmin)
                    {
                        __result = true;
                        return false;
                    }
                }

                return true; // Fall through to native creator ownership check
            }
        }

        [HarmonyPatch(typeof(Container), nameof(Container.GetHoverText))]
        public static class Container_GetHoverText_Patch
        {
            [HarmonyPostfix]
            public static void Postfix(Container __instance, ref string __result)
            {
                if (ModConfig.EnableMod == null || !ModConfig.EnableMod.Value)
                {
                    return;
                }

                if (__instance.m_privacy != Container.PrivacySetting.Private)
                {
                    return;
                }

                var piece = __instance.GetComponent<Piece>();
                if (piece == null)
                {
                    return;
                }

                long creatorId = piece.GetCreator();
                if (creatorId == 0)
                {
                    return;
                }

                if (Player.m_localPlayer == null || Game.instance == null)
                {
                    return;
                }

                long localPlayerId = Game.instance.GetPlayerProfile().GetPlayerID();
                bool isOwner = creatorId == localPlayerId;
                bool isAdmin = SynchronizationManager.Instance != null && SynchronizationManager.Instance.PlayerIsAdmin;

                string statusLine;
                if (isOwner)
                {
                    string statusText = ModLocalization.Localize(ModLocalization.TokenPromptChestOwnedStatus);
                    statusLine = $"\n[<color=#55FF55>{statusText}</color>]";
                }
                else
                {
                    string statusText = ModLocalization.Localize(ModLocalization.TokenPromptChestLockedStatus);
                    statusLine = $"\n[<color=#FF5555>{statusText}</color>]";
                    if (isAdmin && ModConfig.AdminBypassesLock != null && ModConfig.AdminBypassesLock.Value)
                    {
                        statusLine += " <color=#FFFF55>(Admin Bypass)</color>";
                    }
                }

                __result += statusLine;
            }
        }
    }
}
