using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturePuzzleController : MonoBehaviour, ISaveable
{
    [SerializeField]
    AltarRoomController AltarRoomController;
    [SerializeField]
    DisplayController displayController;
    [SerializeField]
    CrystalController crystal;
    public bool puzzleCompleted;

    // Update is called once per frame
    void Update()
    {
        if (!puzzleCompleted && displayController.displayingObject)
        {
            puzzleCompleted = true;
            crystal.gameObject.SetActive(true);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("NaturePuzzleCompleted", puzzleCompleted ? 1 : 0);
    }

    public void Load()
    {
        puzzleCompleted = (PlayerPrefs.GetInt("NaturePuzzleCompleted") == 0 ? false : true);
        if (puzzleCompleted)
        {
            crystal.gameObject.SetActive(true);
            displayController.SetDisplayObject();
        }
        else crystal.gameObject.SetActive(false);
    }
}
