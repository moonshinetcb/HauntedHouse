using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    public static ScoreScript Instance;

    public int score = 0;
    public TextMeshProUGUI scoreText;

    private void Awake()
    {
        // Make this a global reference
        if (Instance == null) 
            Instance = this;
        else 
            Destroy(gameObject);
    }

    void Start()
    {
        UpdateScoreUI();
    }

    public void AddPoints(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }
}