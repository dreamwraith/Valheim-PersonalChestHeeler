# PersonalChestHeeler

[![GitHub Release](https://img.shields.io/github/v/release/dreamwraith/Valheim-PersonalChestHeeler?logo=github&color=1081c2)](https://github.com/dreamwraith/Valheim-PersonalChestHeeler/releases)
[![GitHub Downloads](https://img.shields.io/github/downloads/dreamwraith/Valheim-PersonalChestHeeler/total?logo=github&color=1081c2)](https://github.com/dreamwraith/Valheim-PersonalChestHeeler/releases)
[![Last Commit](https://img.shields.io/github/last-commit/dreamwraith/Valheim-PersonalChestHeeler?logo=git)](https://github.com/dreamwraith/Valheim-PersonalChestHeeler/commits/main)
[![Publish Status](https://img.shields.io/github/actions/workflow/status/dreamwraith/Valheim-PersonalChestHeeler/publish.yml?label=Publish%20Portals&logo=githubactions)](https://github.com/dreamwraith/Valheim-PersonalChestHeeler/actions)
[![Thunderstore](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fthunderstore.io%2Fapi%2Fexperimental%2Fpackage%2FDreamWraith%2FPersonalChestHeeler%2F&query=%24.latest.version_number&label=Thunderstore&logo=thunderstore&color=2980b9)](https://thunderstore.io/c/valheim/p/DreamWraith/PersonalChestHeeler/)
[![Nexus Mods](https://img.shields.io/badge/Nexus_Mods-PersonalChestHeeler-da8d24?logo=nexusmods)](https://www.nexusmods.com/valheim/mods/4357)
[![Game: Valheim](https://img.shields.io/badge/Valheim-Deep_North_%2F_1.x-1b2838?logo=steam&logoColor=white)](https://store.steampowered.com/app/892970/Valheim/)
[![BepInEx Pack](https://img.shields.io/badge/dynamic/json?url=https%3A%2F%2Fthunderstore.io%2Fapi%2Fexperimental%2Fpackage%2Fdenikson%2FBepInExPack_Valheim%2F&query=%24.latest.version_number&label=BepInEx&color=5B57E7)](https://valheim.thunderstore.io/package/denikson/BepInExPack_Valheim/)
[![Target: .NET 4.8](https://img.shields.io/badge/.NET%20Framework-4.8-512BD4?logo=dotnet)](PersonalChestHeeler.csproj)
[![Client & Server](https://img.shields.io/badge/Type-Client%20%26%20Server-blue)](README.md#compatibility)
[![License](https://img.shields.io/github/license/dreamwraith/Valheim-PersonalChestHeeler?color=blue)](LICENSE.md)
[![AI Philosophy](https://img.shields.io/badge/AI%20Philosophy-Software%20Craft-2ea44f?logo=github)](https://gist.github.com/dreamwraith/77c91d656c842611bf8c40febf8056f2)

Tiered personal locked storage chests scaled across Valheim progression ages with server-synced configurable capacity, crafting recipes, and subtle emissive lock indicators.

> [!NOTE]
> **Multiplayer Requirement:** In multiplayer, this mod must be installed on **both the dedicated server and all connecting clients** because it registers custom piece prefabs. It also works in standalone single-player.

---

## Overview

In vanilla Valheim, private locked storage is limited to a single 6-slot Personal Chest (`piece_chest_private`). **PersonalChestHeeler** introduces a 7-tier progression of secure personal lockboxes and vaults scaled across Valheim's biomes and crafting stations. Each tier features dedicated physical sizing, themed emissive lock glows, custom sound layering, and full multiplayer server synchronization.

### Progression Matrix

| Tier | Prefab Name | Vanilla Donor Asset | Grid (W x H) | Slots | Scale | Lock Glow Hue | Default Recipe | Required Station | Health |
| :--- | :--- | :--- | :---: | :---: | :---: | :--- | :--- | :--- | :---: |
| **Tier 1 (Chained Lockbox)** | `piece_chest_chained_personal` | `TreasureChest_morkhalla` | 3 x 2 | **6** | 0.30x | Gold Amber (`#D2A700`) | `Wood:8,Tin:1,Copper:1,Resin:4,GreydwarfEye:1` | Workbench | 250 |
| **Tier 2 (Bronze Trussed Chest)** | `piece_chest_finewood_personal` | `piece_chest_private` | 4 x 3 | **12** | 0.90x | Forest Green (`#00AA39`) | `RoundLog:12,Bronze:4,Ooze:4,WitheredBone:2,Ectoplasm:1` | Forge | 1,000 |
| **Tier 3 (Ironbound Strongbox)** | `piece_chest_reinforced_personal` | `piece_chest` | 6 x 3 | **18** | 0.60x | Fiery Orange (`#FF7900`) | `ElderBark:16,Iron:6,Guck:4,WolfFang:4,SurtlingCore:1` | Forge | 1,500 |
| **Tier 4 (Ornate Silver Coffer)** | `piece_chest_dvergr_sm_personal` | `TreasureChest_dvergrtower` | 6 x 4 | **24** | 1.10x | Arcane Magenta (`#E360E7`) | `FineWood:20,Silver:8,Tar:4,LinenThread:8,WolfClaw:1` | Artisan Table | 2,500 |
| **Tier 5 (Dvergr Clockwork Vault)** | `piece_chest_dvergr_lg_personal` | `TreasureChest_dvergrtown` | 6 x 5 | **30** | 1.20x | Cobalt Blue (`#2359FF`) | `YggdrasilWood:24,BlackMetal:10,Sap:4,MechanicalSpring:2,Eitr:1` | Black Forge | 4,000 |
| **Tier 6 (Charred Flametal Safehold)** | `piece_chest_charred_personal` | `TreasureChest_charredfortress` | 6 x 6 | **36** | 0.95x | Crimson Red (`#FF0000`) | `Blackwood:28,FlametalNew:12,CharcoalResin:4,CeramicPlate:2,Wisp:1` | Black Forge | 10,000 |
| **Tier 7 (Ancient Morkhalla Reliquary)** | `piece_chest_ancient_personal` | `Morkhalla_ChestAncient` | 7 x 6 | **42** | 0.90x | Pale Glacial Cyan (`#CAFFFF`) | `Grausten:32,Gold:14,OozeMork:4,Frostwood:8,MemorialCoal:1` | Galdr Table | 25,000 |

---

## Features

- **Dedicated Hammer Category**: Appears under a dedicated "Personal Chests" tab in the Hammer build menu using Jötunn's Valheim 1.x category support, while retaining Storage filter tags.
- **Normalized Piece Physics**: All chest colliders are standardized on the `piece` physics layer (layer 10) for consistent grid snapping, reliable repair targeting, and zero ground clipping.
- **Themed Emissive Lock Glow (`ChestGlowController`)**: Each chest features a gentle, lock-centered point light and material emission reflecting its metal/biome tier, instantly distinguishing private storage from shared chests.
- **Access Control & Dismantle Protection**: Enforces creator ownership across multiplayer servers. Prevents deconstruction of chests containing items.
- **Admin Lock Bypass**: Server administrators can optionally bypass personal chest locks (`AdminBypassesLock`).
- **HUD Ownership Indicators**: Contextual hover text highlights lock status (`[Personal Lock: Owned]` in green, `[Personal Lock: Restricted]` in red).
- **Live Configuration Hot-Reload**: Capacities, recipes, crafting stations, health, and visual glow settings synchronize across clients via `ConditionalConfigSync`.
- **Zero Asset Bundle Overhead**: Uses zero-asset prefab cloning from built-in Valheim assets.

---

## Configuration

Settings can be customized directly in-game using the BepInEx **Configuration Manager** (F1) or by editing `BepInEx/config/dreamwraith.PersonalChestHeeler.cfg`:

| Section | Key | Type | Default | Description |
| :--- | :--- | :--- | :--- | :--- |
| `1 - General` | `EnableMod` | `bool` | `true` | Master switch to enable or disable mod functionality. |
| `1 - General` | `AdminBypassesLock` | `bool` | `true` | If true, server administrators bypass personal chest ownership restrictions. |
| `1 - General` | `PieceCategory` | `string` | `Personal Chests` | Hammer build menu category name (supports custom names or vanilla categories). |
| `1 - General` | `EnableChestGlow` | `bool` | `true` | Enables subtle emissive lock indicator and point-light on chests. |
| `1 - General` | `ChestGlowIntensity` | `float` | `1.0` | Intensity multiplier for chest lock glow and point lights (0.0 - 3.0). |
| `1 - General` | `EnableDebugLogs` | `bool` | `false` | Enables verbose diagnostic logging in the BepInEx console. |
| `2 - Chest.Chained` | `Chained Chest Enabled` | `bool` | `true` | Enable Tier 1 Chained Lockbox. |
| `2 - Chest.Chained` | `Chained Chest Health` | `float` | `250.0` | Structural health points. |
| `2 - Chest.Chained` | `Chained Chest Inventory Height` | `int` | `2` | Inventory grid row height (1 - 10). |
| `2 - Chest.Chained` | `Chained Chest Inventory Width` | `int` | `3` | Inventory grid column width (1 - 10) (6 slots total). |
| `2 - Chest.Chained` | `Chained Chest Crafting Station` | `string` | `piece_workbench` | Required crafting station. |
| `2 - Chest.Chained` | `Chained Chest Recipe` | `string` | `Wood:8,Tin:1,Copper:1,Resin:4,GreydwarfEye:1` | Crafting requirements formatted as `ItemPrefab:Amount,...`. |
| `3 - Chest.FineWood` | `Fine Wood Chest Enabled` | `bool` | `true` | Enable Tier 2 Bronze Trussed Chest. |
| `3 - Chest.FineWood` | `Fine Wood Chest Health` | `float` | `1000.0` | Structural health points. |
| `3 - Chest.FineWood` | `Fine Wood Chest Inventory Height` | `int` | `3` | Inventory grid row height (1 - 10). |
| `3 - Chest.FineWood` | `Fine Wood Chest Inventory Width` | `int` | `4` | Inventory grid column width (1 - 10) (12 slots total). |
| `3 - Chest.FineWood` | `Fine Wood Chest Crafting Station` | `string` | `forge` | Required crafting station. |
| `3 - Chest.FineWood` | `Fine Wood Chest Recipe` | `string` | `RoundLog:12,Bronze:4,Ooze:4,WitheredBone:2,Ectoplasm:1` | Crafting requirements. |
| `4 - Chest.Reinforced` | `Reinforced Chest Enabled` | `bool` | `true` | Enable Tier 3 Ironbound Strongbox. |
| `4 - Chest.Reinforced` | `Reinforced Chest Health` | `float` | `1500.0` | Structural health points. |
| `4 - Chest.Reinforced` | `Reinforced Chest Inventory Height` | `int` | `3` | Inventory grid row height (1 - 10). |
| `4 - Chest.Reinforced` | `Reinforced Chest Inventory Width` | `int` | `6` | Inventory grid column width (1 - 10) (18 slots total). |
| `4 - Chest.Reinforced` | `Reinforced Chest Crafting Station` | `string` | `forge` | Required crafting station. |
| `4 - Chest.Reinforced` | `Reinforced Chest Recipe` | `string` | `ElderBark:16,Iron:6,Guck:4,WolfFang:4,SurtlingCore:1` | Crafting requirements. |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Enabled` | `bool` | `true` | Enable Tier 4 Ornate Silver Coffer. |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Health` | `float` | `2500.0` | Structural health points. |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Inventory Height` | `int` | `4` | Inventory grid row height (1 - 10). |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Inventory Width` | `int` | `6` | Inventory grid column width (1 - 10) (24 slots total). |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Crafting Station` | `string` | `piece_artisanstation` | Required crafting station. |
| `5 - Chest.SmallDvergr` | `Small Dvergr Chest Recipe` | `string` | `FineWood:20,Silver:8,Tar:4,LinenThread:8,WolfClaw:1` | Crafting requirements. |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Enabled` | `bool` | `true` | Enable Tier 5 Dvergr Clockwork Vault. |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Health` | `float` | `4000.0` | Structural health points. |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Inventory Height` | `int` | `5` | Inventory grid row height (1 - 10). |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Inventory Width` | `int` | `6` | Inventory grid column width (1 - 10) (30 slots total). |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Crafting Station` | `string` | `blackforge` | Required crafting station. |
| `6 - Chest.LargeDvergr` | `Large Dvergr Chest Recipe` | `string` | `YggdrasilWood:24,BlackMetal:10,Sap:4,MechanicalSpring:2,Eitr:1` | Crafting requirements. |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Enabled` | `bool` | `true` | Enable Tier 6 Charred Flametal Safehold. |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Health` | `float` | `10000.0` | Structural health points. |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Inventory Height` | `int` | `6` | Inventory grid row height (1 - 10). |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Inventory Width` | `int` | `6` | Inventory grid column width (1 - 10) (36 slots total). |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Crafting Station` | `string` | `blackforge` | Required crafting station. |
| `7 - Chest.CharredFortress` | `Charred Fortress Chest Recipe` | `string` | `Blackwood:28,FlametalNew:12,CharcoalResin:4,CeramicPlate:2,Wisp:1` | Crafting requirements. |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Enabled` | `bool` | `true` | Enable Tier 7 Ancient Morkhalla Reliquary. |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Health` | `float` | `25000.0` | Structural health points. |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Inventory Height` | `int` | `6` | Inventory grid row height (1 - 10). |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Inventory Width` | `int` | `7` | Inventory grid column width (1 - 10) (42 slots total). |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Crafting Station` | `string` | `piece_magetable` | Required crafting station. |
| `8 - Chest.AncientMorkhalla` | `Ancient Chest Recipe` | `string` | `Grausten:32,Gold:14,OozeMork:4,Frostwood:8,MemorialCoal:1` | Crafting requirements. |

---

## Compatibility

- **Multiplayer**: Must be installed on both the dedicated server and all connecting clients. Attempting to place custom chests on a server without the mod will result in missing prefab errors.
- **Single-Player**: Fully functional in local single-player worlds.
- **Mod Managers**: Fully compatible with **Gale**, **Thunderstore Mod Manager**, and **r2modman**.
- **Modded Setups**: Cleanly integrates with other building and inventory mods without conflicts.

---

## Installation

> [!TIP]
> **Mod Manager Recommended:** Using a modern mod manager like **Gale** or **Thunderstore** handles installation and dependency updates automatically.

### Mod Manager (Recommended)
1. Install via **Gale**, **Thunderstore Mod Manager**, or **r2modman**.
2. Launch Valheim.

### Manual Installation
1. Ensure **BepInExPack Valheim** is installed.
2. Download and extract the latest release archive.
3. Place `PersonalChestHeeler.dll` into your `Valheim/BepInEx/plugins/` directory.
4. Launch Valheim.

---

## Building from Source

The project uses a portable MSBuild configuration that auto-detects standard Steam paths.

```bash
dotnet build -c Release
```

The compiled assembly will be placed in `bin/Release/net48/PersonalChestHeeler.dll`. Building in `Release` configuration automatically packages the distribution ZIP to `bin/Publish/PersonalChestHeeler-<Version>.zip`. For details on version bumping and standalone archive generation, see [.scripts/README.md](.scripts/README.md).

### Custom & CI Paths
For custom installations, non-standard Steam paths, or mod manager profiles, copy [`Environment.props.example`](Environment.props.example) to `Environment.props` in the project root to configure your paths:
- `VALHEIM_INSTALL`: Root Valheim installation folder.
- `BEPINEX_PATH`: Root BepInEx folder (containing `core`, `plugins`, etc.).

Alternatively, you can set `VALHEIM_INSTALL` and `BEPINEX_PATH` as environment variables.
---

## Packaging, Publishing & Releases

All developer automation tools for release management, packaging, and publishing to **Thunderstore** and **Nexus Mods** are consolidated into the single PowerShell 7 entrypoint [`.scripts/modtools.ps1`](.scripts/modtools.ps1), powered by the shared [**`DW.ValheimModTools`**](https://github.com/DreamWraith/DW-ValheimModTools) module:

- **Release Management**: `pwsh ./.scripts/modtools.ps1 release` compiles in `Release`, creates mod & source archives, extracts changelog notes, and publishes GitHub Releases (Draft by default, or published with `-Publish`) using the `gh` CLI.
- **Packaging & Version Bumping**: `pwsh ./.scripts/modtools.ps1 package -Bump Patch` increments SemVer in `PersonalChestHeeler.csproj`, updates `manifest.json`, and bundles distribution archives.
- **Portal Publishing**: `pwsh ./.scripts/modtools.ps1 publish -Target All` uploads directly to Thunderstore and Nexus Mods APIs.
- **CI/CD Workflow**: [`.github/workflows/publish.yml`](.github/workflows/publish.yml) provides an automated GitHub Actions workflow to publish to Thunderstore and Nexus Mods whenever a GitHub Release is published.

For detailed documentation on flags, workflows, and secret configuration, see [`.scripts/README.md`](.scripts/README.md).


---

## License, Author Notes
- This project is licensed under the GNU General Public License v3.0 - see the [LICENSE.md](LICENSE.md) file for details.
- [**A Small Note from Me about Valheim Modding Specifically**](https://gist.github.com/dreamwraith/98564f8441dc234bfadd7e2b605c694c) - Thoughts on open-source modding, community inclusivity, and anti-gatekeeping.
- [**A Note on AI, Software Craft, and Why This Code Exists**](https://gist.github.com/dreamwraith/77c91d656c842611bf8c40febf8056f2) - Personal essay on software craft, human agency, and engineering responsibility.
