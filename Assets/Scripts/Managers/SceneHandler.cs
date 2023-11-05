using UnityEngine;

public class SceneHandler : MonoBehaviour
{
    private void Start()
    {
        ActionManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        ActionManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates gameStates)
    {
        
    }
}
