using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private LevelData[] levelDatas;
    [SerializeField] private Transform gateParent;
    private LevelData _currentLevelData;
    private Gate[] _gates;
    private ObjectPool _objectPool;
    private int _spawnAmount;

    private void Start()
    {
        GetLevelData();
        
        _spawnAmount = _currentLevelData.SpawnAmount;
        
        _gates = new Gate[_spawnAmount];
        _objectPool = ObjectPool.Instance;
        
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
            var gate = _objectPool.GetPooledObject<Gate>(gateParent);
            gate.transform.position = _currentLevelData.GateSpawnPoses[i].position;
            _gates[i] = gate;
        }
    }
}
