using UnityEngine;

public class GateFactory : MonoBehaviour
{
    private ObjectPool _objectPool;

    private void Awake()
    {
        _objectPool = ObjectPool.Instance;
    }

    public Gate CreateGate(Transform parent)
    {
        return _objectPool.GetPooledObject<Gate>(parent);
    }
}
