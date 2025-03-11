using System;

public class ChipMoveModel
{
    public event Action<ChipMove_Player> OnAddChip;
    public event Action<ChipMove_Player> OnRemoveChip;


    public event Action OnActivateChips;
    public event Action OnDeactivateChips;

    public event Action OnDoMotion;

    public void AddChip(ChipMove_Player chipMove)
    {
        OnAddChip?.Invoke(chipMove);
    }

    public void RemoveChip(ChipMove_Player chipMove)
    {
        OnRemoveChip?.Invoke(chipMove);
    }

    public void ActivateChips()
    {
        OnActivateChips?.Invoke();
    }

    public void DeactivateChips()
    {
        OnDeactivateChips?.Invoke();
    }

    public void DoMotion()
    {
        OnDoMotion?.Invoke();
    }
}
