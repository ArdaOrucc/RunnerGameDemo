using UnityEngine;

public class DemoClass : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.DebugDemo();
    }
}
