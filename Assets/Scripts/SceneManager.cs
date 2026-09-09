using UnityEngine;

public class SceneManager : MonoBehaviour
{
    // Load Game Scene
    public void LoadGameScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
    // Load Title Scene
    public void LoadTitleScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Title");
    }

    //Quit Application
    public void QuitGame()
    {
        Application.Quit();
    }
}