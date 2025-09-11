using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SavaManager : MonoBehaviour
{
    private FileDataHandler fileDataHandler;
    private GameData gameData;
    private List<ISaveable> allSaveables;

    [SerializeField] private string fileName = "unityalexdev.json";

    private IEnumerator Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        allSaveables = FindISaveables();

        yield return new WaitForSeconds(.1f);

        LoadGame();
    }

    private void LoadGame()
    {
        gameData = fileDataHandler.LoadData();

        if (gameData == null)
        {
            gameData = new GameData();
            return;
        }

        foreach (var saveable in allSaveables)
        {
            saveable.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        foreach (var saveable in allSaveables)
        {
            saveable.SaveData(ref gameData);
        }

        fileDataHandler.SaveData(gameData);
    }

    [ContextMenu("*** Delete Save Data ***")]
    public void DeleteSaveData()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);

        fileDataHandler.Delete();
    }

    private List<ISaveable> FindISaveables()
    {
        return FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None)
            .OfType<ISaveable>()
            .ToList();
    }
}
