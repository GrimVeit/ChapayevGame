using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ChipGroup", menuName = "Game/Chip/New Chip Group")]
public class ChipGroup : ScriptableObject
{
    public List<Chip> Chips = new();

    public Chip GetChipById(int id)
    {
        return Chips.FirstOrDefault(data => data.ID == id);
    }
}
