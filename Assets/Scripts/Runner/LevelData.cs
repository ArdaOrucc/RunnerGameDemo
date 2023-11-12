using UnityEngine;

[System.Serializable] public class LevelData
{
    public GateData[] GateDatas;
    public int SpawnAmount => GateDatas.Length;
}

[System.Serializable] public struct GateData
{
    public Vector3 Position;
    public bool State;
}


