# Changelog

All notable changes to **PersonalChestHeeler** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [1.1.0] - 2026-10-19

### Changed
- Switch to compile-time publicized game assemblies via Jötunn prebuild task (`DoPrebuild.props`, `JotunnLib`).
- Use `nameof(...)` for `Container.CheckAccess` Harmony patch target.
- Remove Hexium publish target and tooling.
- Cleaned up component lookups with native `TryGetComponent` and simplified Unity/config null checks.

## [1.0.1] - 2026-10-10

### Added
- Configured Nexus Mods integration with Mod ID `4357` in project metadata and portal publishing tooling.
- Enabled automated GitHub Actions CI/CD workflow (`.github/workflows/publish.yml`) for portal deployments to Thunderstore, Hexium, and Nexus Mods.

## [1.0.0] - 2026-10-10

### Added
- **7-Tier Personal Locked Storage Progression**:
  - **Tier 1 (Chained Lockbox)**: Crypt treasure chest model (`TreasureChest_morkhalla`), compact scaling (0.30x), 6 slots (3 x 2), 250 HP, crafted at Workbench.
  - **Tier 2 (Bronze Trussed Chest)**: Native personal chest model (`piece_chest_private`), scaled 0.90x, 12 slots (4 x 3), 1,000 HP, crafted at Forge.
  - **Tier 3 (Ironbound Strongbox)**: Reinforced iron chest model (`piece_chest`), scaled 0.60x, 18 slots (6 x 3), 1,500 HP, crafted at Forge.
  - **Tier 4 (Ornate Silver Coffer)**: Dwarven lockbox model (`TreasureChest_dvergrtower`), scaled 1.10x, 24 slots (6 x 4), 2,500 HP, crafted at Artisan Table.
  - **Tier 5 (Dvergr Clockwork Vault)**: Large dwarven vault model (`TreasureChest_dvergrtown`), scaled 1.20x, 30 slots (6 x 5), 4,000 HP, crafted at Black Forge.
  - **Tier 6 (Charred Flametal Safehold)**: Ashlands fortress vault model (`TreasureChest_charredfortress`), scaled 0.95x, 36 slots (6 x 6), 10,000 HP, crafted at Black Forge.
  - **Tier 7 (Ancient Morkhalla Reliquary)**: Ancient monument reliquary model (`Morkhalla_ChestAncient`), scaled 0.90x, 42 slots (7 x 6), 25,000 HP, crafted at Galdr Table.
- **Dedicated Hammer Category**:
  - Registered under a dedicated "Personal Chests" tab in the Hammer build menu using Jötunn's Valheim 1.x dynamic category support, with configurable category name and fallback tags.
- **Real-Time 3D Build Menu Snapshots (`RenderManager`)**:
  - Automatically captures off-screen camera rendered 3D snapshot icons for all 7 chest prefabs in the hammer placement menu.
- **Themed Emissive Lock Glow (`ChestGlowController`)**:
  - Lock-centered point lights and material emissions tailored per metal/biome tier (Amber, Forest Green, Fiery Orange, Arcane Magenta, Cobalt Blue, Crimson Red, and Pale Glacial Cyan).
  - Synchronized across clients and fully toggleable/scalable via server settings (`EnableChestGlow`, `ChestGlowIntensity`).
- **Multi-Layered Dynamic Audio FX (`ChestAudioEffectManager`)**:
  - Multi-layered placement, hit, destroy, open, close, and ambient overlay audio using native Valheim `AudioClip` caching and spatial 3D `ZSFX` components with tier-specific pitch adjustments.
- **Visual & Material Conformance**:
  - Authentic `Custom/Piece` shader mapping with `Twitcher_Spawner_e` rune emission bloom and WearNTear structural highlights on Ashlands Flametal models.
  - Automatic piece collider and layer conformance (Layer 10 `piece`) enabling hammer targeting, snapping, and repair for all dungeon prop donors.
- **Access Enforcement & Security**:
  - Full creator-ownership lock enforcement across multiplayer servers via native Valheim `Container` privacy logic.
  - Native protection against deconstructing chests containing items (`m_canBeRemoved`).
  - Configurable server administrator lock bypass (`AdminBypassesLock`).
  - Color-coded HUD hover indicators showing ownership status (`[Personal Lock: Owned]` / `[Personal Lock: Restricted]`).
- **Live Configuration Hot-Reload**:
  - Real-time synchronization of grid capacities, crafting recipes, stations, and structural health via `ConditionalConfigSync`.
  - Interactive IMGUI crafting recipe editor with `[PrefabName : Amount]` controls in BepInEx ConfigurationManager.
