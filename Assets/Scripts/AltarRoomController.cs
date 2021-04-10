using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AltarRoomController : MonoBehaviour, ISaveable
{
    [SerializeField]
    List<CrystalController> crystalControllers;
    [SerializeField]
    Collider victoryCollider;
    bool puzzleCompleted;

    public void Load()
    {
        puzzleCompleted = (PlayerPrefs.GetInt("AltarPuzzleCompleted") == 0 ? false : true);

        for (int i = 0; i < crystalControllers.Count; i++)
        {
            crystalControllers[i].isOnAltar = (PlayerPrefs.GetInt("Cystal" + i) == 0 ? false : true);
            if (crystalControllers[i].isOnAltar)
            {
                crystalControllers[i].gameObject.SetActive(true);
                crystalControllers[i].SetAtFinishLocation();
            }
            else
            {
                crystalControllers[i].SetAtStartingLocation();
            }
        }
    }

    public void OnCrystalGained()
    {
        foreach (CrystalController crystal in crystalControllers)
        {
            if(!crystal.isOnAltar)
            {
                return;
            }
            
        }
        puzzleCompleted = true;
    }

    public void Save()
    {
        PlayerPrefs.SetInt("AltarPuzzleCompleted", puzzleCompleted? 1 : 0);
        for (int i = 0; i < crystalControllers.Count; i++)
        {
            PlayerPrefs.SetInt("Cystal" + i, crystalControllers[i].isOnAltar ? 1 : 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (puzzleCompleted && other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }
}
