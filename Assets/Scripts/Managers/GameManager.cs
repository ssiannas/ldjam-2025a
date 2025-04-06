using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_hellevator
{
    public class GameManager : MonoBehaviour
    {
        
        private readonly string firstLevel = "Level1";
        [Header("Channels")]
        [SerializeField] private GmChannel gmChannel;
        [SerializeField] private AudioChannel audioChannel;
        
        [Header("Debug")]
        [SerializeField] private bool playMusicOnLoad = false;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Awake()
        {
            DontDestroyOnLoad(this);
            if (gmChannel == null)
            {
                throw new System.Exception("No Game Manager Channel Assigned");
            }

            if (playMusicOnLoad)
            {
                audioChannel.PlayAudio(SoundNames.MainTheme);
            }
            gmChannel.OnGameStart += StartGame;
        }

        public void StartGame()
        {
            SceneManager.LoadScene(firstLevel);
        }
        
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
