using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChipMoveView : View
{
    [SerializeField] private List<ChipMove_Player> chipMoves = new List<ChipMove_Player>();
    [SerializeField] private float force = 1000;

    private ChipMove currentChipMove;

    private IEnumerator enumeratorMove;

    private bool isDragging;

    private Vector2 startDragPosition;

    public void AddChip(ChipMove_Player chipMove)
    {
        chipMove.OnDown += HandleDownChip;
        chipMove.OnUp += HandleUpChip;

        chipMoves.Add(chipMove);
    }

    public void RemoveChip(ChipMove_Player chipMove)
    {
        var chip = chipMoves.FirstOrDefault(data => data.ID == chipMove.ID);

        if(chip != null)
        {
            chip.OnDown -= HandleDownChip;
            chip.OnUp -= HandleUpChip;

            chipMoves.Remove(chipMove);

            Debug.Log("DESTROY");
        }
    }

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
            Vector2 screenPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float distance = Vector2.Distance(currentChipMove.RectTransform.position, screenPosition);

            Vector3 direction = screenPosition - (Vector2)currentChipMove.RectTransform.position;
            direction.Normalize();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 270;
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
        float minDistance = 0.25f;

        float maxDistance = 2;

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
    private void OnDestroy()
    {
        chipMoves.ForEach(chip =>
        {
            chip.OnDown -= HandleDownChip;
            chip.OnUp -= HandleUpChip;
        });
    }

    #endregion
}
