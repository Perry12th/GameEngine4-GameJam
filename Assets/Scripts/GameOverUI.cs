using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [SerializeField]
    ParticleSystem ConffettiCelebration;

    // Start is called before the first frame update
    void Start()
    {
        ConffettiCelebration.Play();
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("IntroScene");
    }
}
