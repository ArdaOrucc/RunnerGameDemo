using System;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) //save
        {
            DataHandler.Save(_currentLevelData, DataHandler.LevelDataKey);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            var json = DataHandler.GetJsonData(DataHandler.LevelDataKey);
            Debug.Log(json);
        }
    }

    private void GetLevelData()
    {
        var levelIndex = DataHandler.LevelIndex % levelDataListSo.Levels.Length;
        _currentLevelData = levelDataListSo.Levels[levelIndex];
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