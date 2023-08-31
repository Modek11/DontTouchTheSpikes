using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Start()
    {
        score = 0;
        GameHandler.Instance.OnPlayerHitWall += UpdateScore;
    }

    private void UpdateScore(bool isFlyingRight)
    {
        score++;
        scoreText.text = score < 10 ? $"0{score}" : $"{score}";
    }
    
    
}
