using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private List<PoolableObject> _cars = new List<PoolableObject>();
        
    private void Start()
    {
        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        while (true)
        {
            for (int i = 0; i < 5; i++)
            {
                Car car = ObjectPool.Instance.GetPooledObject<Car>(transform);
                _cars.Add(car);
                yield return null;
            }

            yield return null;
            
            for (int i = 0; i < _cars.Count; i++)
            {
                _cars[i].Clear();
                _cars.Remove(_cars[i]);
                yield return null;
            }
        }
    }
}
