using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    // need 2 saves, always alternate
    // just save the time, Time.Time should be enough

    // when loading the game, get both. if one is empty, use the other one.

    // public void SaveData()
    // {
    //     try
    //     {
    //         string json = JsonConvert.SerializeObject(example);
    //         File.WriteAllText(savePath, json, Encoding.UTF8, FileOptions.WriteThrough);
    //         Debug.Log("Data saved successfully!");
    //     }
    //     catch (IOException e)
    //     {
    //         Debug.LogError("Error saving data: " + e.Message);
    //         // You can implement a recovery strategy here, such as saving to a backup location or retrying the save operation.
    //     }
    //     catch (Exception e)
    //     {
    //         Debug.LogError("Error saving data: " + e.Message);
    //     }
    // }

    string gameDataFileName0 = "save0.json";
    string gameDataFileName1 = "save1.json";

    int lastSavedSlot = 0;

    public SaveData saveData;

    public static SaveData Data { get; private set; }

    public bool saveGameLoaded;

    [SerializeField] int currentDataID;

    [SerializeField] float saveIntervall;

    public static event Action<SaveData> OnSaveDataReady = delegate { };


    void Start()
    {
        GetSaveFromDisk();
        StartCoroutine(SaveRoutine());
    }


    void GetSaveFromDisk()
    {
        string data0AsJson = FileManager.ReadFile(gameDataFileName0);
        string data1AsJson = FileManager.ReadFile(gameDataFileName1);

        bool loadedSave = true;

        //data seems to get corrupted when PC crashes. This can handle it

        try
        {
            saveData = JsonUtility.FromJson<SaveData>(data0AsJson);
        }
        catch (Exception e)
        {
            loadedSave = false;
            Debug.Log("couldn't load save 0" + e);
        }

        if (!loadedSave)
            try
            {
                saveData = JsonUtility.FromJson<SaveData>(data1AsJson);
            }
            catch (Exception e)
            {
                loadedSave = false;
            }


        //never saved before
        if (!loadedSave)
        {
            saveData = new SaveData();
        }


        Data = saveData;

        OnSaveDataReady(saveData);
    }


    IEnumerator SaveRoutine()
    {
        if (saveIntervall < 1f)
            saveIntervall = 10f;

        WaitForSeconds waitSaveIntervall = new WaitForSeconds(saveIntervall);

        while (true)
        {
            yield return waitSaveIntervall;


            SaveToLocal();
        }
    }

    // File.WriteAllText(filePath, dataAsJson);
    //string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);

    void SaveToLocal()
    {
        string dataAsJson = JsonUtility.ToJson(saveData);

        if (lastSavedSlot == 0)
        {
            FileManager.WriteToFile(gameDataFileName1, dataAsJson);
            lastSavedSlot = 1;
        }
        else
        {
            FileManager.WriteToFile(gameDataFileName0, dataAsJson);
            lastSavedSlot = 0;
        }
    }


    [Button]
    void ClearSaveData()
    {
        saveData = new SaveData();
        SaveToLocal();
    }


    void OnMineGotDeleted(MineData deletedMine)
    {
        saveData.activeMinesData.Remove(deletedMine);
    }
}