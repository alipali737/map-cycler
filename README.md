# MapCycler

Stops the same level coming up again and again. Instead of picking a completely random level every round, MapCycler plays **every level once, in a random order, before any level can repeat**. Then it starts a new cycle.

With three levels (A, B, C) it might go:

`A → C → B` — all played — `C → A → B` — all played — and so on.

## Features

- **Random, but fair.** The order is random within each cycle, but no level repeats until all of them have been played.
- **Works with modded levels.** Every level the game has loaded is included, vanilla and modded. If you add a level mod part-way through a cycle, its level joins the current cycle. If you remove one, it is dropped, so a cycle can never get stuck waiting for a level that isn't installed.
- **Remembers between sessions.** Progress through the cycle is stored in your save file, so quitting and coming back later carries on where you left off. Each save has its own cycle, and a new save starts a fresh one.
- **Only completed levels count.** A level is marked as played when you complete it. If you die, or quit part-way through, it stays available.
- **No repeats across cycles.** When a cycle ends, the level you just finished is left out of the first pick of the next one (when more than one level is available).

## Multiplayer

Only the **host** needs this mod. The host picks the level and everyone else is told which one to load, so players without the mod can join as normal.

## Installation

Install with a mod manager (r2modman or the Thunderstore Mod Manager), or put `MapCycler.dll` in `BepInEx/plugins`.

## Compatibility and notes

- Run levels only. The shop, lobby and arena are unchanged.
- If another mod also replaces how the next level is chosen, the two may conflict. Only one of them can decide the level.
- After fully restarting the game, the first pick of a new cycle might repeat the level you finished last, because the game doesn't remember that between sessions. The rest of the cycle is unaffected.
- The cycle is stored in your save file under its own entry. If you uninstall the mod, the game ignores that entry and your save is unaffected.

## Reporting problems

Please open an issue at <https://github.com/alipali737/map-cycler/issues> and include your `BepInEx/LogOutput.log`. Lines starting with `[MapCycler]` show which level was picked and which were marked as played.
