using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MapCycler;

internal static class LevelPicker
{
    // Thin wrapper: Unity types in, Unity type out.
    internal static Level? Pick(List<Level> levels, Dictionary<string, int> played, Level? previous)
    {
        List<Level> filteredLevels = levels.Where(l => l != null).ToList();
        List<string> names = filteredLevels.Select(l => l.name).ToList();
        string? picked = PickName(names, played, previous?.name, n => UnityEngine.Random.Range(0, n));
        if (picked == null) return null;
        return filteredLevels.FirstOrDefault(l => l.name == picked);
    }

    // Core logic: only strings, so you can test it outside the game.
    internal static string? PickName(
        IReadOnlyList<string> available,
        Dictionary<string, int> played,
        string? previousName,
        Func<int, int> nextInt)          // nextInt(n) returns 0..n-1
    {
        if (available.Count == 0) return null;

        List<string> keys = played.Keys.ToList(); 
        foreach (var k in keys)
        {
            if (!available.Contains(k))
            {
                played.Remove(k);
            }
        }

        List<string> remaining = available.Where(n => !played.ContainsKey(n)).ToList();

        if (remaining.Count == 0)
        {
            played.Clear();
            remaining = available.ToList();
            if (remaining.Count > 1 && previousName != null)
            {
                remaining.Remove(previousName);
            }
        }

        return remaining[nextInt(remaining.Count)];
    }
}
