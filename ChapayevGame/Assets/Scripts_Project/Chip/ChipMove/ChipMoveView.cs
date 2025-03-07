using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMoveView : View
{
    [SerializeField] private List<ChipMove> chipMoves = new List<ChipMove>();
    [SerializeField] private float force = 1000;

    private ChipMove currentChipMove;

    private IEnumerator enumeratorMove;

    private bool isDragging;

    private Vector2 startDragPosition;

    private void ActivateMove()
    {
        if (enumeratorMove != null)
            Coroutines.Stop(enumeratorMove);

        isDragging = true;
        currentChipMove.ActivateAim();

        enumeratorMove = Move_Coro();
        Coroutines.Start(enumeratorMove);
    }

    private void DeactivateMove()
    {
        if (enumeratorMove != null)
            Coroutines.Stop(enumeratorMove);

        currentChipMove.DeactivateAim();

        isDragging = false;
    }

    private IEnumerator Move_Coro()
    {
        while (isDragging)
        {
            Vector2 screenPosition = Input.mousePosition;

            float distance = Vector2.Distance(currentChipMove.transform.position, screenPosition);

            Vector3 direction = screenPosition - (Vector2)currentChipMove.transform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;
            currentChipMove.RotateAim(angle);

            Debug.Log(distance);

            //Touch touch = Input.GetTouch(0);

            //Vector2 screenPosition = touch.position;

            //Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);



            //float distance = Vector2.Distance(worldPosition, startDragPosition);    

            AdjustCrocchairScale(distance);

            yield return null;
        }
    }

    private void AdjustCrocchairScale(float distance)
    {
        float minDistance = currentChipMove.RectTransform.sizeDelta.x/2;

        float maxDistance = 300;

        float t = Mathf.InverseLerp(minDistance, maxDistance, distance);

        float newScale = Mathf.Lerp(0.4f, 1, t);

        currentChipMove.ScaleAim(newScale);
    }

    private void HandleDownChip(ChipMove chipMove, PointerEventData pointerEventData)
    {
        if(isDragging) return;

        startDragPosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);

        currentChipMove = chipMove;
        currentChipMove.ZeroForce();

        ActivateMove();
    }

    private void HandleUpChip(ChipMove chipMove, PointerEventData pointerEventData)
    {
        DeactivateMove();

        if(currentChipMove != null)
        {
            Vector2 releasePosition = Camera.main.ScreenToWorldPoint(pointerEventData.position);

            Vector2 direction = (startDragPosition - releasePosition).normalized;

            float forceMagnitude = (startDragPosition - releasePosition).magnitude * force;

            currentChipMove.AddForce(direction * forceMagnitude);
        }
    }

    #region I

    public void Awake()
    {
        chipMoves.ForEach(chip =>
        {
            chip.OnDown += HandleDownChip;
            chip.OnUp += HandleUpChip;
        });
    }

    private void OnDestroy()
    {
        chipMoves.ForEach(chip =>
        {
            chip.OnDown += HandleDownChip;
            chip.OnUp += HandleUpChip;
        });
    }

    #endregion
}
