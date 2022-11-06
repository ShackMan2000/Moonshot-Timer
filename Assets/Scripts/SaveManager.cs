using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private string gameDataFileName;



    public SaveData saveData;

    public static SaveData Data { get; private set; }


    public bool saveGameLoaded;


    [SerializeField]
    private int currentDataID;

    [SerializeField]
    private float saveIntervall; 

    public static event Action<SaveData> OnSaveDataReady = delegate { };

    //private void OnEnable()
    //{
    //    Mine.EvtMineDeleted += OnMineGotDeleted;
    //}




    private void Start()
    {
        GetSaveFromDisk();
        StartCoroutine(SaveRoutine());
       
    }





    private void GetSaveFromDisk()
    {
        string fileP = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        string dataAsjson = File.ReadAllText(fileP);
        saveData = JsonUtility.FromJson<SaveData>(dataAsjson);

        Data = saveData;

        OnSaveDataReady(saveData);
    }



    private IEnumerator SaveRoutine()
    {
        if (saveIntervall < 1f)
            saveIntervall = 10f;

        WaitForSeconds waitSaveIntercall = new WaitForSeconds(saveIntervall);

        while (true)
        {
            yield return waitSaveIntercall;


            SaveToLocal();
        }
    }


    private void SaveToLocal()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, gameDataFileName);
        string dataAsJson = JsonUtility.ToJson(saveData);
        File.WriteAllText(filePath, dataAsJson);
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










