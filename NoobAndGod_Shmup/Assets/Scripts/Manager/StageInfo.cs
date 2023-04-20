using UnityEngine;

[CreateAssetMenu(fileName = "StateInfo", menuName = "StageInfo/Create StageInfo")]
public class StageInfo : ScriptableObject
{
    public string stageName;
    public string stageBackground;
    [TextArea(1,3)]
    public string stageDescription;
    public int difficulty;
    public Sprite stageImage;
}
