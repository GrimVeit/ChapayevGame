using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZoneTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider != null)
        {
            if(collider.TryGetComponent(out ChipMove chip))
            {
                chip.DeadTrigger();
            }
        }
    }
}