using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMotionView : View
{
    [SerializeField] private Transform transformBot;
    [SerializeField] private Transform transformPlayer;

    public event Action<float> OnSpin;
    public event Action<bool> OnEndSpin;

    [SerializeField] private Vector3 spinVector;
    [SerializeField] private Transform spinTransform;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private float minSpinSpeed;
    [SerializeField] private float maxSpinSpeed;
    [SerializeField] private float minDuration;
    [SerializeField] private float maxDuration;

    private IEnumerator rotateSpin_Coroutine;

    public void Initialize()
    {

    }

    public void Dispose()
    {

    }


    public void StartSpin()
    {
        if (rotateSpin_Coroutine != null)
            Coroutines.Stop(rotateSpin_Coroutine);

        rotateSpin_Coroutine = RotateSpin_Coroutine();
        Coroutines.Start(rotateSpin_Coroutine);
    }

    private IEnumerator RotateSpin_Coroutine()
    {
        float elapsedTime = 0f;
        float startSpeed = UnityEngine.Random.Range(minSpinSpeed, maxSpinSpeed);
        float duration = UnityEngine.Random.Range(minDuration, maxDuration);
        float endSpeed = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float currentSpeed = Mathf.Lerp(startSpeed, endSpeed, elapsedTime / duration);
            OnSpin?.Invoke(currentSpeed);

            spinTransform.Rotate(currentSpeed * Time.deltaTime * spinVector);

            yield return null;
        }

        OnEndSpin?.Invoke(IsPlayer());
    }

    private bool IsPlayer()
    {
        return Vector2.Distance(transformPlayer.position, centerPoint.position) < Vector2.Distance(transformBot.position, centerPoint.position);
    }
}