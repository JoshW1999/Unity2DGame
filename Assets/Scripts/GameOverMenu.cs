using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    // Handles buttons in the various 'GameOver' scenes
    public void ToLevelOne() {
        SceneManager.LoadScene("Game");
    }

    public void ToLevelTwo() {
       // gameObject.GetComponent<InventoryManager>().LoadInventory(); 
        SceneManager.LoadScene("Game 1");
    }

    public void ToLevelThree() {
        //gameObject.GetComponent<InventoryManager>().LoadInventory();
        SceneManager.LoadScene("Game 2");
    }

    public void BackToMainMenu() {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevelOneCompleteScreen() {
        SceneManager.LoadScene("LevelComplete");
    }

    public void LoadLevelTwoCompleteScreen() {
        SceneManager.LoadScene("LevelComplete 1");
    }
}

