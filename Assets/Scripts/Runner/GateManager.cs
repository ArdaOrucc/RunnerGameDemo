using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;
    [SerializeField] private GateFactory gateFactory;
    [SerializeField] private Transform gateParent;
    private LevelData _currentLevelData;
    private Gate[] _gates;
    private int _spawnAmount;

    private void Start()
    {
        GetLevelData();
        
        _spawnAmount = _currentLevelData.SpawnAmount;
        
        _gates = new Gate[_spawnAmount];
        
        SpawnGates();
    }

    private void GetLevelData()
    {
        _currentLevelData = levelDatas[0];
    }

    private void SpawnGates()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var gateState = _currentLevelData.GateStates[i];
            var gate = gateFactory.CreateGate(gateParent, gateState);
            gate.transform.position = _currentLevelData.GateSpawnPoses[i].position;
            _gates[i] = gate;
        }
    }
}
