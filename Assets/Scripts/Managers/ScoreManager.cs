using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int _score;

    private void Start()
    {
        ActionManager.OnGameStateChanged += OnGameStateChanged;
    }

    private void OnDestroy()
    {
        ActionManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void OnGameStateChanged(GameStates newGameState)
    {
        if (newGameState == GameStates.Complete)
        {
            UpdateScore(1);
        }
    }

    private void UpdateScore(int addedScore)
    {
        _score += addedScore;
        scoreText.text = _score.ToString();
        ActionManager.OnScoreChanged?.Invoke(_score);
    }
}
