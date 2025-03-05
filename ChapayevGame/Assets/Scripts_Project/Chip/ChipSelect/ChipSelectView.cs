using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChipSelectView : View
{
    [SerializeField] private ChipSelect chipSelectPrefab;
    [SerializeField] private Transform transformContent;
    [SerializeField] private Button buttonSelect;

    private readonly List<ChipSelect> chipSelects = new List<ChipSelect>();

    public void Initialize()
    {

    }

    public void Dispose()
    {
        chipSelects.ForEach(s =>
        {
            s.OnChooseChip -= HandleClickToChooseChip;
            s.Dispose();
        });

        chipSelects.Clear();
    }

    public void SetOpenChip(Chip chip)
    {
        var chipSelect = chipSelects.FirstOrDefault(data => data.Id == chip.ID);

        if (chipSelect == null)
        {
            chipSelect = Instantiate(chipSelectPrefab, transformContent);
            chipSelect.SetData(chip);
            chipSelect.OnChooseChip += HandleClickToChooseChip;
            chipSelect.Initialize();

            chipSelects.Add(chipSelect);
        }
    }

    public void SelectChip(int id)
    {
        chipSelects.FirstOrDefault(s => s.Id == id).Select();
        buttonSelect.gameObject.SetActive(true);
    }

    public void DeselectChip(int id)
    {
        chipSelects.FirstOrDefault(s => s.Id == id).Deselect();
        buttonSelect.gameObject.SetActive(false);
    }

    #region Input

    public event Action<int> OnChooseChip;

    private void HandleClickToChooseChip(int strategyId)
    {
        OnChooseChip?.Invoke(strategyId);
    }

    #endregion
}
