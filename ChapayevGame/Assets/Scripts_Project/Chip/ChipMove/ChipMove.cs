using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Rigidbody2D rb;

    public RectTransform RectTransform;

    [SerializeField] private Transform transformAim;

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


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown?.Invoke(this, eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke(this, eventData);
    }

    #region Input

    public event Action<ChipMove, PointerEventData> OnDown;
    public event Action<ChipMove, PointerEventData> OnUp;

    #endregion
}
