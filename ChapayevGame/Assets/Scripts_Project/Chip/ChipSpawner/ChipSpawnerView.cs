using System;
using System.Collections.Generic;
using UnityEngine;

public class ChipSpawnerView : View, IIdentify
{
    public string GetID() => Id;

    [SerializeField] private string Id; 
    [SerializeField] private ChipMove chipMovePrefab;
    [SerializeField] private Transform transformParent;
    [SerializeField] private List<Transform> transformsSpawns = new List<Transform>();

    public void SetChip(int indexPosition, Chip chip)
    {
        var chipMove = Instantiate(chipMovePrefab, transformParent);
        chipMove.SetData(chip);
        chipMove.OnDead += HandleDestroyChip;
        chipMove.transform.SetPositionAndRotation(transformsSpawns[indexPosition].position, chipMovePrefab.transform.rotation);

        OnSpawnChip?.Invoke(chipMove);
    }

    #region Input

    public event Action<ChipMove> OnSpawnChip;
    public event Action<ChipMove> OnDestroyChip;

    private void HandleDestroyChip(ChipMove chipMove)
    {
        chipMove.OnDead -= HandleDestroyChip;

        OnDestroyChip?.Invoke(chipMove);

        chipMove.Destroy();
    }

    #endregion
}
