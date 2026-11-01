using System;
using BepInEx.Configuration;
using UnityEngine;

namespace PersonalChestHeeler.Configuration
{
    public static class ModConfig
    {
        public static ConditionalConfigSync.ConfigSync ConfigSync = null!;

        // General
        public static ConfigEntry<bool> EnableMod = null!;
        public static ConfigEntry<bool> AdminBypassesLock = null!;
        public static ConfigEntry<string> PieceCategory = null!;
        public static ConfigEntry<bool> EnableChestGlow = null!;
        public static ConfigEntry<float> ChestGlowIntensity = null!;
        public static ConfigEntry<bool> EnableDebugLogs = null!;

        // Tier 1: Chained Lockbox
        public static ConfigEntry<bool> EnableChainedChest = null!;
        public static ConfigEntry<int> ChainedWidth = null!;
        public static ConfigEntry<int> ChainedHeight = null!;
        public static ConfigEntry<string> ChainedRecipe = null!;
        public static ConfigEntry<string> ChainedCraftingStation = null!;
        public static ConfigEntry<float> ChainedHealth = null!;

        // Tier 2: Bronze Trussed Chest
        public static ConfigEntry<bool> EnableBronzeChest = null!;
        public static ConfigEntry<int> BronzeWidth = null!;
        public static ConfigEntry<int> BronzeHeight = null!;
        public static ConfigEntry<string> BronzeRecipe = null!;
        public static ConfigEntry<string> BronzeCraftingStation = null!;
        public static ConfigEntry<float> BronzeHealth = null!;

        // Tier 3: Ironbound Strongbox
        public static ConfigEntry<bool> EnableIronboundChest = null!;
        public static ConfigEntry<int> IronboundWidth = null!;
        public static ConfigEntry<int> IronboundHeight = null!;
        public static ConfigEntry<string> IronboundRecipe = null!;
        public static ConfigEntry<string> IronboundCraftingStation = null!;
        public static ConfigEntry<float> IronboundHealth = null!;

        // Tier 4: Ornate Silver Coffer
        public static ConfigEntry<bool> EnableSilverChest = null!;
        public static ConfigEntry<int> SilverWidth = null!;
        public static ConfigEntry<int> SilverHeight = null!;
        public static ConfigEntry<string> SilverRecipe = null!;
        public static ConfigEntry<string> SilverCraftingStation = null!;
        public static ConfigEntry<float> SilverHealth = null!;

        // Tier 5: Dvergr Clockwork Vault
        public static ConfigEntry<bool> EnableClockworkChest = null!;
        public static ConfigEntry<int> ClockworkWidth = null!;
        public static ConfigEntry<int> ClockworkHeight = null!;
        public static ConfigEntry<string> ClockworkRecipe = null!;
        public static ConfigEntry<string> ClockworkCraftingStation = null!;
        public static ConfigEntry<float> ClockworkHealth = null!;

        // Tier 6: Charred Flametal Safehold
        public static ConfigEntry<bool> EnableFlametalChest = null!;
        public static ConfigEntry<int> FlametalWidth = null!;
        public static ConfigEntry<int> FlametalHeight = null!;
        public static ConfigEntry<string> FlametalRecipe = null!;
        public static ConfigEntry<string> FlametalCraftingStation = null!;
        public static ConfigEntry<float> FlametalHealth = null!;

        // Tier 7: Ancient Morkhalla Reliquary
        public static ConfigEntry<bool> EnableReliquaryChest = null!;
        public static ConfigEntry<int> ReliquaryWidth = null!;
        public static ConfigEntry<int> ReliquaryHeight = null!;
        public static ConfigEntry<string> ReliquaryRecipe = null!;
        public static ConfigEntry<string> ReliquaryCraftingStation = null!;
        public static ConfigEntry<float> ReliquaryHealth = null!;

        public static void Initialize(ConfigFile config)
        {
            ConfigSync = new ConditionalConfigSync.ConfigSync(Plugin.ModGUID)
            {
                DisplayName = Plugin.ModName,
                CurrentVersion = Plugin.ModVersion,
                MinimumRequiredVersion = Plugin.ModVersion,
                ModRequired = true
            };

            // General
            EnableMod = BindSynced(config, "1 - General", "EnableMod", true,
                "Master switch to enable or disable the functionality of this mod.", 100);

            AdminBypassesLock = BindSynced(config, "1 - General", "AdminBypassesLock", true,
                "If true, server administrators bypass personal chest ownership and lock restrictions.", 90);

            PieceCategory = BindSynced(config, "1 - General", "PieceCategory", "Personal Chests",
                "Hammer build menu category name for all personal chests. Supports custom names or vanilla categories (Furniture, Misc, etc.).", 85);

            EnableChestGlow = BindSynced(config, "1 - General", "EnableChestGlow", true,
                "Enables subtle emissive material glow and lock point-light indicator on personal chests.", 80);

            ChestGlowIntensity = BindSynced(config, "1 - General", "ChestGlowIntensity", 1.0f,
                "Intensity multiplier for personal chest emissive glow and lock point lights (0.0 - 3.0).", 70,
                new AcceptableValueRange<float>(0.0f, 3.0f));

            EnableDebugLogs = BindClient(config, "1 - General", "EnableDebugLogs", false,
                "Enables verbose diagnostic logging in the BepInEx console.", 60);

            // Tier 1: Chained Lockbox
            EnableChainedChest = BindSynced(config, "2 - Chest.Chained", "Chained Chest Enabled", true,
                "Enable Tier 1 Chained Lockbox (Wood/Tin/Copper/Resin/Greydwarf Eye).", 100);
            ChainedHealth = BindSynced(config, "2 - Chest.Chained", "Chained Chest Health", 250.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            ChainedHeight = BindSynced(config, "2 - Chest.Chained", "Chained Chest Inventory Height", 2,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            ChainedWidth = BindSynced(config, "2 - Chest.Chained", "Chained Chest Inventory Width", 3,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            ChainedCraftingStation = BindSynced(config, "2 - Chest.Chained", "Chained Chest Crafting Station", "piece_workbench",
                "Required crafting station (e.g. piece_workbench, forge, or empty string for hammer only).", 60);
            ChainedRecipe = BindSynced(config, "2 - Chest.Chained", "Chained Chest Recipe", "Wood:8,Tin:1,Copper:1,Resin:4,GreydwarfEye:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 2: Bronze Trussed Chest
            EnableBronzeChest = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Enabled", true,
                "Enable Tier 2 Bronze Trussed Chest (Core Wood/Bronze/Ooze/Withered Bone/Ectoplasm).", 100);
            BronzeHealth = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Health", 1000.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            BronzeHeight = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Inventory Height", 3,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            BronzeWidth = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Inventory Width", 4,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            BronzeCraftingStation = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Crafting Station", "forge",
                "Required crafting station (e.g. piece_workbench, forge, or empty string for hammer only).", 60);
            BronzeRecipe = BindSynced(config, "3 - Chest.FineWood", "Fine Wood Chest Recipe", "RoundLog:12,Bronze:4,Ooze:4,WitheredBone:2,Ectoplasm:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 3: Ironbound Strongbox
            EnableIronboundChest = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Enabled", true,
                "Enable Tier 3 Ironbound Strongbox (Ancient Bark/Iron/Guck/Wolf Fang/Surtling Core).", 100);
            IronboundHealth = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Health", 1500.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            IronboundHeight = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Inventory Height", 3,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            IronboundWidth = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Inventory Width", 6,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            IronboundCraftingStation = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Crafting Station", "forge",
                "Required crafting station (e.g. piece_workbench, forge, or empty string for hammer only).", 60);
            IronboundRecipe = BindSynced(config, "4 - Chest.Reinforced", "Reinforced Chest Recipe", "ElderBark:16,Iron:6,Guck:4,WolfFang:4,SurtlingCore:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 4: Ornate Silver Coffer
            EnableSilverChest = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Enabled", true,
                "Enable Tier 4 Ornate Silver Coffer (Fine Wood/Silver/Tar/Linen Thread/Fenris Claw).", 100);
            SilverHealth = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Health", 2500.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            SilverHeight = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Inventory Height", 4,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            SilverWidth = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Inventory Width", 6,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            SilverCraftingStation = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Crafting Station", "piece_artisanstation",
                "Required crafting station (e.g. piece_artisanstation, blackforge, or empty string for hammer only).", 60);
            SilverRecipe = BindSynced(config, "5 - Chest.SmallDvergr", "Small Dvergr Chest Recipe", "FineWood:20,Silver:8,Tar:4,LinenThread:8,WolfClaw:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 5: Dvergr Clockwork Vault
            EnableClockworkChest = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Enabled", true,
                "Enable Tier 5 Dvergr Clockwork Vault (Yggdrasil Wood/Black Metal/Sap/Mechanical Springs/Refined Eitr).", 100);
            ClockworkHealth = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Health", 4000.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            ClockworkHeight = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Inventory Height", 5,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            ClockworkWidth = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Inventory Width", 6,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            ClockworkCraftingStation = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Crafting Station", "blackforge",
                "Required crafting station (e.g. blackforge, forge, or empty string for hammer only).", 60);
            ClockworkRecipe = BindSynced(config, "6 - Chest.LargeDvergr", "Large Dvergr Chest Recipe", "YggdrasilWood:24,BlackMetal:10,Sap:4,MechanicalSpring:2,Eitr:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 6: Charred Flametal Safehold
            EnableFlametalChest = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Enabled", true,
                "Enable Tier 6 Charred Flametal Safehold (Ashwood/Flametal/Charcoal Resin/Ceramic Plates/Wisp).", 100);
            FlametalHealth = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Health", 10000.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            FlametalHeight = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Inventory Height", 6,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            FlametalWidth = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Inventory Width", 6,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            FlametalCraftingStation = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Crafting Station", "blackforge",
                "Required crafting station (e.g. blackforge, piece_magetable, or empty string for hammer only).", 60);
            FlametalRecipe = BindSynced(config, "7 - Chest.CharredFortress", "Charred Fortress Chest Recipe", "Blackwood:28,FlametalNew:12,CharcoalResin:4,CeramicPlate:2,Wisp:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            // Tier 7: Ancient Morkhalla Reliquary
            EnableReliquaryChest = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Enabled", true,
                "Enable Tier 7 Ancient Morkhalla Reliquary (Grausten/Gold/Ooze Mork/Frostwood/Memorial Coal).", 100);
            ReliquaryHealth = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Health", 25000.0f,
                "Chest structural health points (50.0 - 50000.0).", 90,
                new AcceptableValueRange<float>(50.0f, 50000.0f));
            ReliquaryHeight = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Inventory Height", 6,
                "Inventory grid row height (1 - 10).", 80,
                new AcceptableValueRange<int>(1, 10));
            ReliquaryWidth = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Inventory Width", 7,
                "Inventory grid column width (1 - 10).", 70,
                new AcceptableValueRange<int>(1, 10));
            ReliquaryCraftingStation = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Crafting Station", "piece_magetable",
                "Required crafting station (e.g. piece_magetable, blackforge, or empty string for hammer only).", 60);
            ReliquaryRecipe = BindSynced(config, "8 - Chest.AncientMorkhalla", "Ancient Chest Recipe", "Grausten:32,Gold:14,OozeMork:4,Frostwood:8,MemorialCoal:1",
                "Crafting recipe string formatted as ItemPrefab:Amount,... parsed against ObjectDB.", 50,
                customDrawer: RecipeConfigDrawer.Draw);

            Plugin.LogDebug("Configuration initialized and bound with ConditionalConfigSync.");
        }


        private static ConfigEntry<T> BindSynced<T>(
            ConfigFile config,
            string section,
            string key,
            T defaultValue,
            string description,
            int order,
            AcceptableValueBase? acceptableValues = null,
            Action<ConfigEntryBase>? customDrawer = null)
        {
            var attributes = new ConfigurationManagerAttributes
            {
                Order = order,
                CustomDrawer = customDrawer
            };
            var configDescription = new ConfigDescription(description, acceptableValues, attributes);
            var entry = config.Bind(section, key, defaultValue, configDescription);
            ConfigSync.AddConfigEntry(entry);
            return entry;
        }

        private static ConfigEntry<T> BindClient<T>(
            ConfigFile config,
            string section,
            string key,
            T defaultValue,
            string description,
            int order,
            AcceptableValueBase? acceptableValues = null,
            Action<ConfigEntryBase>? customDrawer = null)
        {
            var attributes = new ConfigurationManagerAttributes
            {
                Order = order,
                CustomDrawer = customDrawer
            };
            var configDescription = new ConfigDescription(description, acceptableValues, attributes);
            return config.Bind(section, key, defaultValue, configDescription);
        }
    }
}
