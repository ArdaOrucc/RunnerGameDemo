using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private Transform[] gateSpawnPoses;
    public Transform[] GateSpawnPoses => gateSpawnPoses;
    public int SpawnAmount => GateSpawnPoses.Length;
}
