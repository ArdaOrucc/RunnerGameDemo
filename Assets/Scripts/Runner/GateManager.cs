using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private LevelDataListSo levelDataListSo;
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
        
        ServiceProvider.GetService<GameManager>().DebugDemo();
    }

    private void GetLevelData()
    {
        _currentLevelData = levelDataListSo.Levels[0];
    }

    private void SpawnGates()
    {
        for (int i = 0; i < _spawnAmount; i++)
        {
            var gateState = _currentLevelData.GateDatas[i].State;
            var gate = gateFactory.CreateGate(gateParent, gateState);
            gate.transform.position = _currentLevelData.GateDatas[i].Position;
            _gates[i] = gate;
        }
    }
}
