using System;
using System.Collections.Generic;
using Jotunn.Managers;

namespace PersonalChestHeeler.Localization
{
    /// <summary>
    /// Registers localized display names and descriptions.
    /// </summary>
    public static class ModLocalization
    {
        public const string TokenPieceChestChained = "$piece_chest_chained_personal";
        public const string TokenPieceChestChainedDesc = "$piece_chest_chained_personal_desc";

        public const string TokenPieceChestBronze = "$piece_chest_finewood_personal";
        public const string TokenPieceChestBronzeDesc = "$piece_chest_finewood_personal_desc";

        public const string TokenPieceChestIronbound = "$piece_chest_reinforced_personal";
        public const string TokenPieceChestIronboundDesc = "$piece_chest_reinforced_personal_desc";

        public const string TokenPieceChestSilver = "$piece_chest_dvergr_sm_personal";
        public const string TokenPieceChestSilverDesc = "$piece_chest_dvergr_sm_personal_desc";

        public const string TokenPieceChestClockwork = "$piece_chest_dvergr_lg_personal";
        public const string TokenPieceChestClockworkDesc = "$piece_chest_dvergr_lg_personal_desc";

        public const string TokenPieceChestFlametal = "$piece_chest_charred_personal";
        public const string TokenPieceChestFlametalDesc = "$piece_chest_charred_personal_desc";

        public const string TokenPieceChestReliquary = "$piece_chest_ancient_personal";
        public const string TokenPieceChestReliquaryDesc = "$piece_chest_ancient_personal_desc";

        public const string TokenPromptChestOwnerStatus = "$prompt_chest_owner_status";
        public const string TokenPromptChestLockedStatus = "$prompt_chest_locked_status";
        public const string TokenPromptChestOwnedStatus = "$prompt_chest_owned_status";

        public static void Initialize()
        {
            var customLocalization = LocalizationManager.Instance.GetLocalization();

            customLocalization.AddTranslation("English", new Dictionary<string, string>
            {
                { TokenPieceChestChained, "Chained Lockbox" },
                { TokenPieceChestChainedDesc, "A rustic lockbox hewn from raw timber, sealed with pine resin and locked behind primitive copper and tin latches with a watchful Greydwarf eye." },

                { TokenPieceChestBronze, "Bronze Trussed Chest" },
                { TokenPieceChestBronzeDesc, "A sturdy chest built from split core wood logs and trussed with forged bronze bands, caulked with swamp ooze, pegged with carved bone tumblers, and chilled by an ethereal ectoplasm ward." },

                { TokenPieceChestIronbound, "Ironbound Strongbox" },
                { TokenPieceChestIronboundDesc, "A heavy strongbox crafted from rot-resistant ancient bark and reinforced with wrought iron bands, sealed with glowing guck, locked behind serrated wolf-fang catches, and stoked by a smoldering surtling core." },

                { TokenPieceChestSilver, "Ornate Silver Coffer" },
                { TokenPieceChestSilverDesc, "An ornate coffer crafted from polished fine wood and chased with silver bands, weather-sealed in dark tar, cushioned with woven linen, and snapped shut with a spring-loaded wolf claw catch." },

                { TokenPieceChestClockwork, "Dvergr Clockwork Vault" },
                { TokenPieceChestClockworkDesc, "A complex mechanical vault built from sacred Yggdrasil timber and armored in black metal, preserved in tree sap, driven by clockwork springs, and energized by pulsing eitr." },

                { TokenPieceChestFlametal, "Charred Flametal Safehold" },
                { TokenPieceChestFlametalDesc, "A scorching safehold framed with dense blackwood beams and plated in forged flametal, insulated by ceramic tiles, sealed with charcoal resin, and guided by a trapped wisp." },

                { TokenPieceChestReliquary, "Ancient Morkhalla Reliquary" },
                { TokenPieceChestReliquaryDesc, "A monumental reliquary sculpted from heavy Grausten slabs and glacial frostwood, inlaid with gleaming gold, mortared with caustic Morkhalla bile, and consecrated around an ancient memorial coal." },

                { TokenPromptChestOwnerStatus, "Owner: {0}" },
                { TokenPromptChestLockedStatus, "Personal Lock (Restricted)" },
                { TokenPromptChestOwnedStatus, "Personal Lock (Owned)" }
            });

            Plugin.LogDebug("[ModLocalization] Default English localizations registered.");
        }

        /// <summary>
        /// Translates a given token key using Jotunn's CustomLocalization.
        /// </summary>
        public static string Localize(string token, params string[] arguments)
        {
            string translatedText = LocalizationManager.Instance.GetLocalization().TryTranslate(token);
            if (arguments != null && arguments.Length > 0)
            {
                translatedText = string.Format(translatedText, arguments);
            }
            return translatedText;
        }
    }
}
