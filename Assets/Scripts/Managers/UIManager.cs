using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI completeText;

    private void Start()
    {
        ActionManager.OnScoreChanged += UpdateCompleteText;
    }

    private void OnDestroy()
    {
        ActionManager.OnScoreChanged -= UpdateCompleteText;
    }


    private void UpdateCompleteText(int score)
    {
        completeText.text = $"Complete {score}";
    }
}
