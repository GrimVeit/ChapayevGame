using UnityEngine;

[CreateAssetMenu(fileName = "TutorialDescription", menuName = "Game/Tutuorial Description/New Tutuoral Description")]
public class TutorialDescription : ScriptableObject, IIdentify
{
    [SerializeField] private string id;
    [SerializeField] private string description;
    private TutorialDescriptionData data;

    public string Description => description;
    public TutorialDescriptionData Data => data;
    public string GetID() => id;

    internal void SetData(TutorialDescriptionData data)
    {
        this.data = data;
    }
}
