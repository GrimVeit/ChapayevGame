using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChipMove : MonoBehaviour
{
    [SerializeField] private protected Rigidbody2D rb;

    public int ID => currentChipData.ID;
    public RectTransform RectTransform;

    [SerializeField] private protected Transform transformAim;
    [SerializeField] private protected Image imageChip;
    [SerializeField] private protected Image imageChip_2;

    private protected Chip currentChipData;

    public void SetData(Chip chip)
    {
        currentChipData = chip;
        imageChip.sprite = currentChipData.Sprite;
        imageChip_2.sprite = currentChipData.Sprite;
    }

    public void ActivateAim()
    {
        transformAim.gameObject.SetActive(true);
    }

    public void DeactivateAim()
    {
        transformAim.gameObject.SetActive(false);
    }

    public void RotateAim(float angle)
    {
        transformAim.rotation = Quaternion.Euler(0, 0, angle);
    }

    public void ScaleAim(float scale)
    {
        transformAim.localScale = new Vector3(scale, scale, scale);
    }

    public void ZeroForce()
    {
        rb.velocity = Vector3.zero;
    }

    public void AddForce(Vector2 vector)
    {
        rb.AddForce(vector, ForceMode2D.Impulse);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    #region Input

    public event Action<ChipMove> OnDead;

    public void DeadTrigger()
    {
        OnDead?.Invoke(this);
    }

    #endregion
}
