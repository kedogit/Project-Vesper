using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void PlayButton()
    {
        GameManager.Instance?.SceneManager.ChangeScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
