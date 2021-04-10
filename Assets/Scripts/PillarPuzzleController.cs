using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarPuzzleController : MonoBehaviour, ISaveable
{
    [SerializeField]
    AltarRoomController AltarRoomController;
    [SerializeField]
    CrystalController crystal;
    public bool puzzleCompleted = false;

    // Update is called once per frame
    void Update()
    {
        if (crystal.isOnAltar)
        {
            puzzleCompleted = true;
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("PillarPuzzleCompleted", puzzleCompleted ? 1 : 0);
    }

    public void Load()
    {
        puzzleCompleted = (PlayerPrefs.GetInt("PillarPuzzleCompleted") == 0 ? false : true);
    }
}
