using UnityEngine;
using UnityEngine.SceneManagement;

namespace ldjam_hellevator
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject tutorialPanel;
        [SerializeField] private AudioChannel audioChannel;

        void Awake()
        {
            audioChannel.PlayAudio(SoundNames.MainTheme);
        }

        public void PlayGame()
        {
            Debug.Log("Starting Game...");
            SceneManager.LoadScene("MainLevel");
        }

        public void ShowTutorial()
        {
            SceneManager.LoadScene("Tutorial");
        }

        public void GoToMainMenu()
        {
            SceneManager.LoadScene("Menu");
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}