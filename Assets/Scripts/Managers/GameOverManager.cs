using UnityEngine;
using TMPro;

namespace ldjam_hellevator
{
    public class GameOverManager : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField]
        private TMP_Text scoreText;

        [SerializeField]
        private ScoreManagerChannel scoreManagerChannel;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            int score = scoreManagerChannel.GetScore();
            scoreText.text = "Your Score:\n" + score;
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
