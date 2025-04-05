using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_hellevator
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GmChannel gmChannel;
        private readonly string firstLevel = "Level1";
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            DontDestroyOnLoad(this);
            if (gmChannel == null)
            {
                throw new System.Exception("No Game Manager Channel Assigned");
            }
            gmChannel.OnGameStart += StartGame;
        }

        void StartGame()
        {
            SceneManager.LoadScene(firstLevel);
        }

    }
}
