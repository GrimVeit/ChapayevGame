using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Rigidbody2D rb;

    private Vector2 startDragPosition;
    private Vector2 currentDragPosition;

    public RectTransform RectTransform;

    [SerializeField] private float force;
    [SerializeField] private Transform transformAim;

    [SerializeField] private float minDistanceScale;
    [SerializeField] private float maxDistanceScale;
    [SerializeField] private float minScale;
    [SerializeField] private float maxScale;

    private bool isDragging;

    private Vector2 direction;
    private float forceMagnitude;

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

    private void Update()
    {
        if (isDragging)
        {
            //Vector2 releasePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //direction = (startDragPosition - releasePosition).normalized;

            //float forceMagnitude = (startDragPosition - releasePosition).magnitude * force;

            //float scale = Mathf.Lerp(0.2f, 1f, Mathf.InverseLerp(minDistanceScale, maxDistanceScale, ))
        }
    }

    public void AddForce(Vector2 vector)
    {
        rb.AddForce(vector, ForceMode2D.Impulse);
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        OnDown?.Invoke(this, eventData);




        //isDragging = true;

        //startDragPosition = Camera.main.ScreenToWorldPoint(eventData.position);

        //rb.velocity = Vector2.zero;

        //transformAim.gameObject.SetActive(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnUp?.Invoke(this, eventData);




        //isDragging = false;

        //Vector2 releasePosition = Camera.main.ScreenToWorldPoint(eventData.position);

        //direction = (startDragPosition - releasePosition).normalized;

        //forceMagnitude = (startDragPosition - releasePosition).magnitude * force;

        //rb.AddForce(direction *  forceMagnitude, ForceMode2D.Impulse);

        //transformAim.gameObject.SetActive(false);
    }

    #region Input

    public event Action<ChipMove, PointerEventData> OnDown;
    public event Action<ChipMove, PointerEventData> OnUp;

    #endregion
}
