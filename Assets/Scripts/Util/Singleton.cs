using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                return _instance;
            }
            else 
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                return _instance;
            }
        }
    }
    private static T _instance;

    private void Awake()
    {
        _instance = (T)this;
        Init();
    }

    private void OnDestroy()
    {
        _instance = null;
        DeInit();
    }

    protected virtual void Init() { }

    protected virtual void DeInit() { }
}
