using UnityEngine;
using TMPro;

namespace ldjam_hellevator
{
    public class ScoreManager : MonoBehaviour
    {

        [Header("UI References")] [SerializeField]
        private TMP_Text scoreText;

        [SerializeField] private TMP_Text highScoreText;

        [Header("Settings")] 
        [SerializeField] private string scorePrefix = "Depth: ";
        [SerializeField] private string highScorePrefix = "High: ";
        
        [SerializeField] private ScoreManagerChannel scoreManagerChannel;
        
        private bool isCounting = false;
        
        private int _currentScore = 0;
        public int highScore = 0;

        private float timer;
        public float scoreTimer = 1.5f;

        private void Awake()
        {

            //DontDestroyOnLoad(this);
            if (scoreManagerChannel == null)
            {
                throw new System.Exception("No Score Manager Channel Assigned");
            }
            scoreManagerChannel.OnAddPoints += AddPoints;
            scoreManagerChannel.OnGetScore = GetScore;
            scoreManagerChannel.OnStopCounting += StopCounting;
            isCounting = true;
            LoadHighScore();
            UpdateScoreDisplay();
            UpdateHighScoreDisplay();
        }

        private void StopCounting()
        {
            isCounting = false;
        }
        
        public void LateUpdate()
        {
            timer += Time.deltaTime;

            if (timer >= scoreTimer && isCounting)
            {
                AddPoints(1);
                timer = 0f;
            }    
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
            if (scoreText is not null)
                scoreText.text = $"Depth: {_currentScore}";
        }

        private void UpdateHighScoreDisplay()
        {
            if (highScoreText is not null)
                highScoreText.text = $"Max Depth: {highScore}";
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
        
        private int GetScore()
        {
            return _currentScore;
        }
    }
}