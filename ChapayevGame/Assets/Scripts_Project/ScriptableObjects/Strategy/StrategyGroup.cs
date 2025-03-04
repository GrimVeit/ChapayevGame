using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "StrategyGroup", menuName = "Game/Strategy/New Strategy Group")]
public class StrategyGroup : ScriptableObject
{
    public List<Strategy> Strategies = new();

    public Strategy GetStrategyById(int id)
    {
        return Strategies.FirstOrDefault(data => data.ID == id);
    }
}
