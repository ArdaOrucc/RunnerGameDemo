using UnityEngine;

public class GateFactory : MonoBehaviour
{
    private ObjectPool _objectPool;

    private void Awake()
    {
        _objectPool = ObjectPool.Instance;
    }

    public Gate CreateGate(Transform parent, bool gateState)
    {
        var gate = _objectPool.GetPooledObject<Gate>(parent);
        gate.IsGoodGate = gateState;
        gate.Init(parent);
        return gate;
    }
}