using System.Collections.Generic;

namespace MapCycler;

internal static class CycleState
{
    internal static Dictionary<string, int> Played()
    {
        return StatsManager.instance.dictionaryOfDictionaries["mapCyclerPlayed"];
    }
}
