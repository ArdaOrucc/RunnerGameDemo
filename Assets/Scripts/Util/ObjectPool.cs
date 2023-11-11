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
        if(parent != null) transform.SetParent(parent);
    }

    public virtual void Clear()
    {
        gameObject.SetActive(false);
        transform.SetParent(ObjectPool.Instance.transform);
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
                CreateAndAddToPool(item.objectToPool);
            }
        }
    }

    public T GetPooledObject<T>(Transform parent = null) where T : PoolableObject
    {
        var objectType = typeof(T);
        if (pooledObjects.TryGetValue(objectType, out var typeList))
        {
            foreach (var poolableObject in typeList)
            {
                if (!poolableObject.gameObject.activeSelf)
                {
                    poolableObject.Init(parent);
                    return (T)poolableObject;
                }
            }
        }

        foreach (var item in itemsToPool)
        {
            if (item.objectToPool.GetType() == objectType && item.shouldExpand)
            {
                var obj = CreateAndAddToPool(item.objectToPool);
                obj.Init();
                return (T)obj;
            }
        }

        return null;
    }

    private T CreateAndAddToPool<T>(T item) where T : PoolableObject
    {
        var obj = Instantiate(item);
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
