using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerModel
{
    public event Action<int, Chip> OnChipSpawner;

    private int currentCountChip = 0;
    private List<int> indexPositions = new List<int>();

    public void SetStrategy(Strategy strategy)
    {
        indexPositions = strategy.IndexPositions;
    }

    public void SetChip(Chip chip)
    {
        Debug.Log(indexPositions.Count);
        Debug.Log(currentCountChip);

        OnChipSpawner?.Invoke(indexPositions[currentCountChip], chip);

        currentCountChip += 1;
    }
}
