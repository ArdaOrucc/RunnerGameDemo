using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 
public class ObjectPoolItem
{
    public PoolableObject objectToPool;
    public int amountToPool;
    public bool shouldExpand;
}

public abstract class PoolableObject : MonoBehaviour
{
    public virtual void Init(Transform parent = null) 
    {
        gameObject.SetActive(true);
    }

    public virtual void Clear() 
    {
        transform.SetParent(ObjectPool.Instance.transform);
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = Vector3.zero;
    }
}


public class ObjectPool : Singleton<ObjectPool>
{
    public List<ObjectPoolItem> itemsToPool;
    private List<PoolableObject> pooledObjects;

    protected override void Init()
    {
        base.Init();
        pooledObjects = new List<PoolableObject>();
        foreach (var item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                var obj = CrateAndAddPool(item);
            }
        }
    }

    public PoolableObject GetPooledObject(string tag, Transform parent = null)
    {
        foreach (var pooledObject in pooledObjects)
        {
            if (!pooledObject.gameObject.activeSelf && pooledObject.gameObject.CompareTag(tag))
            {
                pooledObject.Init(parent);
                return pooledObject;
            }
        }

        foreach (var item in itemsToPool)
        {
            if (item.objectToPool.CompareTag(tag) && item.shouldExpand)
            {
                var obj = CrateAndAddPool(item);
                obj.Init(parent);
                return obj;
            }
        }

        return null;
    }

    private PoolableObject CrateAndAddPool(ObjectPoolItem item)
    {
        var obj = Instantiate(item.objectToPool);
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);
        pooledObjects.Add(obj);
        return obj;
    }
}
