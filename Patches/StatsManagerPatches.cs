using HarmonyLib;

namespace MapCycler;

[HarmonyPatch(typeof(StatsManager))]
static class StatsManagerPatches
{
    [HarmonyPostfix, HarmonyPatch("Start")]
    private static void Start_Postfix(StatsManager __instance)
    {
        if (!__instance.dictionaryOfDictionaries.ContainsKey("mapCyclerPlayed"))
        {
            __instance.dictionaryOfDictionaries.Add("mapCyclerPlayed", new());
        }
    }

    [HarmonyPostfix, HarmonyPatch(nameof(StatsManager.LoadGame))]
    private static void LoadGame_Postfix()
    {
        MapCycler.Logger.LogInfo($"mapCyclerPlayed: {CycleState.Played().Count} elements");
        foreach (var ele in CycleState.Played())
        {
            MapCycler.Logger.LogInfo($"Key: {ele.Key}, Value: {ele.Value}");
        }
    }
}
