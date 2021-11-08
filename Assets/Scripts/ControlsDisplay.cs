using UnityEngine;
using UnityEngine.SceneManagement;
public class ControlsDisplay : MonoBehaviour
{
    // Handling Buttons in the Control's Menu Option
    public void BackToMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void More() {
        SceneManager.LoadScene("Controls 1");
    }

    public void Previous() {
        SceneManager.LoadScene("Controls");
    }



}
