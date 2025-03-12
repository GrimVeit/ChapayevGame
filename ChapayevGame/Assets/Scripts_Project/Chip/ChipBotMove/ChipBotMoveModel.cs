using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class ChipBotMoveModel
{
    public event Action OnDoMotion;
    public event Action OnStoppedChip;

    private readonly IChipBank chipBankBot;
    private readonly IChipBank chipBankPlayer;

    private ChipMove currentChip;
    private Transform transformPlayer;

    private const float aimDuration = 1;
    private const float minForce = 2;
    private const float maxForce = 10;
    private const float maxAngle = 45;

    private IEnumerator coroutineAimShoot;

    public ChipBotMoveModel(IChipBank chipBankBot, IChipBank chipBankPlayer)
    {
        this.chipBankBot = chipBankBot;
        this.chipBankPlayer = chipBankPlayer;
    }

    public void ActivateMove()
    {
        if(currentChip != null)
        {
            currentChip.OnStopped -= HandleStoppedChip;
        }

        currentChip = chipBankBot.GetChipMoves()[Random.Range(0, chipBankBot.GetChipMoves().Count)];
        currentChip.OnStopped += HandleStoppedChip;

        transformPlayer = GetClosestTransformPlayer(currentChip.transform);

        if(coroutineAimShoot != null)
            Coroutines.Stop(coroutineAimShoot);

        coroutineAimShoot = AimShootTarget();
        Coroutines.Start(coroutineAimShoot);
    }

    private IEnumerator AimShootTarget()
    {
        currentChip.ActivateAim();

        Vector2 startPosition = currentChip.transform.position;
        Vector2 targetPosition = transformPlayer.position;

        float elapsedTime = 0;

        Vector2 directionToTarget = (targetPosition - startPosition).normalized;

        while(elapsedTime < aimDuration)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / aimDuration;

            float scale = Mathf.Lerp(0.1f, 1, progress);
            currentChip.ScaleAim(scale);

            float angle = Mathf.Lerp(0, maxAngle, progress);
            currentChip.RotateAim(angle);

            yield return null;
        }

        Vector2 direction = (targetPosition - startPosition).normalized;
        float force = Random.Range(minForce, maxForce);

        currentChip.AddForce(direction * force);
        currentChip.DeactivateAim();

        OnDoMotion?.Invoke();
    }

    
    private Transform GetClosestTransformPlayer(Transform chipBot)
    {
        Transform transform = null;
        float s = float.MaxValue;

        foreach(var chipPlayer in chipBankPlayer.GetChipMoves())
        {
            float distance = Vector2.Distance(chipBot.transform.position, chipPlayer.transform.position);

            if(distance < s)
            {
                s = distance;
                transform = chipPlayer.transform;
            }
        }

        return transform;
    }

    private void HandleStoppedChip(ChipMove chipMove)
    {
        OnStoppedChip?.Invoke();
    }
}
