using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;
    public bool LoadOnStart;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }
   
    public void Save()
    {
        foreach (ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
        {
            saveable.Save();
        }
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("AltarPuzzleCompleted"))
        {
            foreach (ISaveable saveable in FindObjectsOfType<MonoBehaviour>().OfType<ISaveable>())
            {
                saveable.Load();
            }
        }
    }
    public void SetLoadOnStart(bool start)
    {
        LoadOnStart = start;
    }
}
