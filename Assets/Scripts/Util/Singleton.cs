using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance
    {
        get
        {
            if (_instance != null)
            {
                Debug.Log($"<color=green> Instance of {typeof(T)} returned </color>");
                return _instance;
            }
            else 
            {
                _instance = FindObjectOfType(typeof(T)) as T;

                if (_instance == null)
                {
                    Debug.LogWarning($"<color=red> Instance of {typeof(T)} not Initialized, and not found in scene, Created a new one! </color>");
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                else
                {
                    Debug.LogWarning($"<color=yellow> Instance of {typeof(T)} not Initialized, found in scene! </color>");
                }
                
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
