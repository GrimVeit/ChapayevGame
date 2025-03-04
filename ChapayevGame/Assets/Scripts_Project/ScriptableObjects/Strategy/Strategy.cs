using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Strategy", menuName = "Game/Strategy/New Strategy")]
public class Strategy : ScriptableObject
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    private StrategyData strategyData;

    public int ID => id;
    public Sprite Sprite => sprite;

    public StrategyData StrategyData => strategyData;

    internal void SetData(StrategyData strategyData)
    {
        this.strategyData = strategyData;
    }
}
