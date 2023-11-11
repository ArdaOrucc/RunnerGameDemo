using UnityEngine;

public class LevelData : MonoBehaviour
{
    [SerializeField] private bool[] gateStates;
    [SerializeField] private Transform[] gateSpawnPoses;
    
    public int SpawnAmount => GateSpawnPoses.Length;
    
    public Transform[] GateSpawnPoses => gateSpawnPoses;
    public bool[] GateStates => gateStates;
    
}
