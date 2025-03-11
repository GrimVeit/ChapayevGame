using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipPunch : MonoBehaviour
{

    public void Initialize()
    {
        Coroutines.Start(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.3f);

        Destroy(gameObject);
    }
}
