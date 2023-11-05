using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem<T> where T : PoolableObject
{
    public T objectToPool;
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
    public List<ObjectPoolItem<PoolableObject>> itemsToPool;
    private Dictionary<Type, List<PoolableObject>> pooledObjects;

    protected override void Init()
    {
        base.Init();
        pooledObjects = new Dictionary<Type, List<PoolableObject>>();
        foreach (var item in itemsToPool)
        {
            for (int i = 0; i < item.amountToPool; i++)
            {
                CreateAndAddPool(item.objectToPool);
            }
        }
    }

    public T GetPooledObject<T>(Transform parent = null) where T : PoolableObject
    {
        var objectType = typeof(T);
        if (pooledObjects.TryGetValue(objectType, out var o))
        {
            foreach (var pooledObject in o)
            {
                if (!pooledObject.gameObject.activeSelf)
                {
                    pooledObject.Init(parent);
                    return (T)pooledObject;
                }
            }
        }

        foreach (var item in itemsToPool)
        {
            if (item.objectToPool.GetType() == objectType && item.shouldExpand)
            {
                var obj = CreateAndAddPool(item.objectToPool);
                obj.Init(parent);
                return (T)obj;
            }
        }

        return null;
    }

    private T CreateAndAddPool<T>(T objectToPool) where T : PoolableObject
    {
        var obj = Instantiate(objectToPool);
        obj.transform.SetParent(transform);
        obj.gameObject.SetActive(false);

        if (!pooledObjects.ContainsKey(obj.GetType()))
        {
            pooledObjects[obj.GetType()] = new List<PoolableObject>();
        }

        pooledObjects[obj.GetType()].Add(obj);

        return obj;
    }
}
