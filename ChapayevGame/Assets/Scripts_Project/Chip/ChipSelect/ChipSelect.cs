using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipSelect : MonoBehaviour
{
    public int Id => currentChip.ID;

    [SerializeField] private Button buttonSelect;
    [SerializeField] private Image imageChipSelect;
    [SerializeField] private GameObject objectSelect;

    private Chip currentChip;

    public void Initialize()
    {
        buttonSelect.onClick.AddListener(() => OnChooseChip?.Invoke(Id));
    }

    public void Dispose()
    {
        buttonSelect.onClick.RemoveListener(() => OnChooseChip?.Invoke(Id));
    }

    public void SetData(Chip chip)
    {
        currentChip = chip;
        imageChipSelect.sprite = currentChip.Sprite;
    }

    public void Select()
    {
        objectSelect.SetActive(true);
    }

    public void Deselect()
    {
        objectSelect.SetActive(false);
    }

    #region Input

    public event Action<int> OnChooseChip;

    #endregion
}
