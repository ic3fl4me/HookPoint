using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void BestTime()
    {
        SceneManager.LoadScene("BestTimeMenu");
    }
}
