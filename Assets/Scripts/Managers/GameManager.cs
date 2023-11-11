using System.Collections.Generic;
using UnityEngine;

public enum GameStates
{
    Gameplay = 0,
    Complete,
    Fail
}

public class GameManager : Singleton<GameManager>
{
    private GameStates _gameStates;
    public GameStates GameStates
    {
        get => _gameStates;
        private set
        {
            _gameStates = value;
            ActionManager.OnGameStateChanged?.Invoke(_gameStates);
        }
    }

    public void DebugDemo()
    {
        Debug.Log("Demo");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameStates = GameStates.Complete;
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameStates = GameStates.Fail;
        }
    }
}
