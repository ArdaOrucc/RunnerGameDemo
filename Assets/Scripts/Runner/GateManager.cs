using UnityEngine;

public class GateManager : MonoBehaviour
{
    [SerializeField] private Transform gateParent;
    [SerializeField] private int spawnAmount;
    private Gate[] _gates;
    private ObjectPool _objectPool;

    private void Start()
    {
        _gates = new Gate[spawnAmount];
        _objectPool = ObjectPool.Instance;
        SpawnGates();
    }

    private void SpawnGates()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var gate = _objectPool.GetPooledObject<Gate>(gateParent);
            _gates[i] = gate;
        }
    }
}
