using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    public void OnPlayGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnQuitGame()
    {
        Application.Quit(0);
    }
}
