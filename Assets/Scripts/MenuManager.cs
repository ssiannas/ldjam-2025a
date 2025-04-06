using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject tutorialPanel;

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
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
