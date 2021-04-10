using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HistoryPuzzleController : MonoBehaviour, ISaveable
{
    [SerializeField]
    AltarRoomController AltarRoomController;
    [SerializeField]
    List<DisplayController> displayControllers;
    [SerializeField]
    CrystalController crystal;
    public bool puzzleCompleted;

    // Update is called once per frame
    void Update()
    {
        if (!puzzleCompleted)
        {
            foreach(DisplayController display in displayControllers)
            {
                if (!display.displayingObject)
                {
                    return;
                }
               
            }
            puzzleCompleted = true;
            crystal.gameObject.SetActive(true);
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("HistoryPuzzleCompleted", puzzleCompleted ? 1 : 0);
        for (int i = 0; i < displayControllers.Count; i++)
        {
            PlayerPrefs.SetInt("Display" + i, displayControllers[i].displayingObject ? 0 : 1);
        }
    }

    public void Load()
    {
        puzzleCompleted = (PlayerPrefs.GetInt("HistoryPuzzleCompleted") == 0 ? false : true);
        if (puzzleCompleted) crystal.gameObject.SetActive(true);
        else crystal.gameObject.SetActive(false);

        for (int i = 0; i < displayControllers.Count; i++)
        {
            displayControllers[i].displayingObject = (PlayerPrefs.GetInt("Display" + i) == 0 ? false : true);
            if (displayControllers[i].displayingObject) displayControllers[i].SetDisplayObject();
        }
    }
}
