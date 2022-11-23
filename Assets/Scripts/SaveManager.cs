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
  


    string gameDataFileName0 = "save0.json";
    string gameDataFileName1 = "save1.json";

    int lastSavedSlot = 0;

    public SaveData saveData;

    public static SaveData Data { get; private set; }

    public bool saveGameLoaded;

    [SerializeField]
    private int currentDataID;

    [SerializeField]
    private float saveIntervall;

    public static event Action<SaveData> OnSaveDataReady = delegate { };


    private void Start()
    {
        GetSaveFromDisk();
        StartCoroutine(SaveRoutine());
    }



    private void GetSaveFromDisk()
    {



        string data0AsJson = FileManager.ReadFile(gameDataFileName0);
        string data1AsJson = FileManager.ReadFile(gameDataFileName1);



        //never saved before
        if (data0AsJson == null && data1AsJson == null)
        {
            saveData = new SaveData();
        }
        else
        {
            if (data0AsJson != string.Empty)
                saveData = JsonUtility.FromJson<SaveData>(data0AsJson);
            else if(data1AsJson != string.Empty)
                saveData = JsonUtility.FromJson<SaveData>(data1AsJson);
            else
                saveData = new SaveData();

        }
        Data = saveData;

        OnSaveDataReady(saveData);
    }



    private IEnumerator SaveRoutine()
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

    private void SaveToLocal()
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



    private void OnMineGotDeleted(MineData deletedMine)
    {
        saveData.activeMinesData.Remove(deletedMine);
    }


}










