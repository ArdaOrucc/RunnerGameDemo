using System;

public static class ActionManager
{
    public static Action<GameStates> OnGameStateChanged;
    public static Action<int> OnScoreChanged;
}