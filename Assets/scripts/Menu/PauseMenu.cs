using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public static bool controllerPausePressed = false;

    [SerializeField]
    private GameObject pauseMenuUI;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || controllerPausePressed)
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // Resume the game time

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gameIsPaused = false;
        controllerPausePressed = false;
    }

    private void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // Stop the game time

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        gameIsPaused = true;
    }

    public void LoadMenu(string sceneName)
    {
        Time.timeScale = 1f; // Resume the game time
        gameIsPaused = false;
        // Load the main menu scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
