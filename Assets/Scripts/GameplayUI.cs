using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameplayUI : MonoBehaviour
{
    [SerializeField]
    GameObject gameplayUI;
    [SerializeField]
    GameObject pauseMenuUI;
    public bool isPaused { get; private set; }
    public void OnPause(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            pauseMenuUI.SetActive(true);
            gameplayUI.SetActive(false);
            isPaused = true;
        }    
        
    }
    public void OnResume()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        gameplayUI.SetActive(true);
        isPaused = false;
    }
    public void OnQuit()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
