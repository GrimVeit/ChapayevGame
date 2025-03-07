using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerView : View
{
    [SerializeField] private ChipMove chipMovePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<Transform> transformsSpawns = new List<Transform>();



    public void SetChip(int indexPosition, Chip chip)
    {
        var chipMove = Instantiate(chipMovePrefab, transformParent);
        chipMove.SetData(chip);
        chipMove.transform.SetPositionAndRotation(transformsSpawns[indexPosition].position, chipMovePrefab.transform.rotation);

        OnSpawnChip?.Invoke(chipMove);
    }

    #region Input

    public event Action<ChipMove> OnSpawnChip;

    #endregion
}
