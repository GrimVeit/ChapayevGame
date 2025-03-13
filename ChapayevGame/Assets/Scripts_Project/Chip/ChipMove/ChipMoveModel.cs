using System;

public class ChipMoveModel
{
    public event Action<ChipMove_Player> OnAddChip;
    public event Action<ChipMove_Player> OnRemoveChip;


    public event Action OnActivateChips;
    public event Action OnDeactivateChips;

    public event Action OnDoMotion;
    public event Action OnStoppedChip;

    private ITutorialDescriptionProvider tutorialDescriptionProvider;
    public ChipMoveModel(ITutorialDescriptionProvider tutorialDescriptionProvider)
    {
        this.tutorialDescriptionProvider = tutorialDescriptionProvider;
    }

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

    public void StartDrag()
    {
        tutorialDescriptionProvider.LockTutorial("StartGrabChip");

        tutorialDescriptionProvider.ActivateTutorial("DragAndDropChip");
    }

    public void EndDrag()
    {

    }

    public void DoMotion()
    {
        tutorialDescriptionProvider.LockTutorial("DragAndDropChip");

        OnDoMotion?.Invoke();
    }

    public void StopChip()
    {
        OnStoppedChip?.Invoke();
    }
}
