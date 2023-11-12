using UnityEngine;

[CreateAssetMenu(fileName = "LevelDataList", menuName = "Game/LevelDataList")]
public class LevelDataListSo : ScriptableObject
{
    public LevelData[] Levels;
}
