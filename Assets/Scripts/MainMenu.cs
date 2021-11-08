using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Handling Buttons in MainMenu
    public void ToLevelOne() {
        SceneManager.LoadScene("Game");
    }
    public void ToLevelTwo() {
        SceneManager.LoadScene("Game 1");
    }

    public void ToLevelThree() {
        SceneManager.LoadScene("Game 2");
    }

    public void Controls() {
        SceneManager.LoadScene("Controls");
    }

    public void LevelSelect() {
        Debug.Log("HERE");
        SceneManager.LoadScene("SelectLevel");
    }

    // Exit (Stops Running The Game)
    public void Exit() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
