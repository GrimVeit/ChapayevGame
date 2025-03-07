using System;
using System.Collections.Generic;

public class ChipSpawnerModel
{
    public event Action<int, Chip> OnChipSpawner;

    private int currentCountChip = 0;
    private List<int> indexPositions;

    public void SetStrategy(Strategy strategy)
    {
        currentCountChip = strategy.ChipCount;
        indexPositions = strategy.IndexPositions;
    }

    public void SetChip(Chip chip)
    {
        OnChipSpawner?.Invoke(indexPositions[currentCountChip], chip);
    }
}
