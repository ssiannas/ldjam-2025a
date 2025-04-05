using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highScoreText;

    [Header("Settings")]
    [SerializeField] private string scorePrefix = "Score: ";
    [SerializeField] private string highScorePrefix = "High: ";

    private int _currentScore = 0;
    public int highScore = 0;

    private void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: persists between scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        LoadHighScore();
        UpdateScoreDisplay();
        UpdateHighScoreDisplay();
    }

    public void AddPoints(int points)
    {
        _currentScore += points;

        if (_currentScore > highScore)
        {
            highScore = _currentScore;
            SaveHighScore();
            UpdateHighScoreDisplay();
        }

        UpdateScoreDisplay();
    }


    public void SetScore(int score)
    {
        _currentScore = score;
        if (_currentScore > highScore)
        {
            highScore = _currentScore;
            SaveHighScore();
            UpdateHighScoreDisplay();
        }

        UpdateScoreDisplay();
    }

    public void ResetScore()
    {
        _currentScore = 0;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
            scoreText.text = $"Score: {_currentScore}";
    }

    private void UpdateHighScoreDisplay()
    {
        if (highScoreText != null)
            highScoreText.text = $"High Score: {highScore}";
    }

    private void LoadHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    private void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        PlayerPrefs.Save();
    }
}