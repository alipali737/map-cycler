using HarmonyLib;

namespace MapCycler;

[HarmonyPatch(typeof(RunManager))]
static class RunManagerPatches
{
    [HarmonyPrefix, HarmonyPatch(nameof(RunManager.SetRunLevel))]
    private static bool SetRunLevel_Prefix(RunManager __instance)
    {
        if (!SemiFunc.IsMasterClientOrSingleplayer()) return true;

        Level? previous = __instance.previousRunLevel;
        Level? picked = LevelPicker.Pick(__instance.levels, CycleState.Played(), previous);
        if (picked == null) return true;
        __instance.levelCurrent = picked;

        MapCycler.Logger.LogInfo($"Picked {picked.name}, Levels Played: {CycleState.Played().Count}, Total Levels: {__instance.levels.Count}");
        return false;
    }

    [HarmonyPrefix, HarmonyPatch(nameof(RunManager.ChangeLevel))]
    private static void ChangeLevel_Prefix(RunManager __instance,
        bool _completedLevel, bool _levelFailed, RunManager.ChangeLevelType _changeLevelType)
    {
        if (!SemiFunc.IsMasterClientOrSingleplayer()) return;

        if (__instance.levelCurrent == null) return;
        if (_completedLevel && !_levelFailed && _changeLevelType == RunManager.ChangeLevelType.Normal && __instance.levels.Contains(__instance.levelCurrent))
        {
            CycleState.Played()[__instance.levelCurrent.name] = 1;
            MapCycler.Logger.LogInfo($"Marked {__instance.levelCurrent.name} as played ({CycleState.Played().Count}/{__instance.levels.Count})");
        }
    }
}
