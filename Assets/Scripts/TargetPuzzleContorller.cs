using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPuzzleContorller : MonoBehaviour, ISaveable
{
    [SerializeField]
    AltarRoomController AltarRoomController;
    [SerializeField]
    CrystalController crystal;
    public bool puzzleCompleted;
    [SerializeField]
    List<DonutTargetController> donutTargetControllers;
    private int targetIndex = 0;

    public void OnTargetHit()
    {
        donutTargetControllers[targetIndex].gameObject.SetActive(false);
        targetIndex++;
        if (targetIndex >= donutTargetControllers.Count)
        {
            puzzleCompleted = true;
            crystal.gameObject.SetActive(true);
        }
        else
        {
            donutTargetControllers[targetIndex].gameObject.SetActive(true);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("TargetPuzzleCompleted", puzzleCompleted ? 1 : 0);
        PlayerPrefs.SetInt("TargetIndex", targetIndex);
    }

    public void Load()
    {
        puzzleCompleted = (PlayerPrefs.GetInt("TargetPuzzleCompleted") == 0 ? false : true);
        targetIndex = PlayerPrefs.GetInt("TargetIndex");
        if (!puzzleCompleted)
        {
            donutTargetControllers[targetIndex].gameObject.SetActive(true);
        }
        else
        {
            crystal.gameObject.SetActive(true);
        }
    }
}
