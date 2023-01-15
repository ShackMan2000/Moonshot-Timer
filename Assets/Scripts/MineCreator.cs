using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System;
using Sirenix.OdinInspector;

public class MineCreator : MonoBehaviour
{
    SaveData saveData;

    [SerializeField] Mine minePF;

    public List<Mine> allMines;


    [SerializeField] GlobalSettings settings;


    [SerializeField] Vector2 firstMinePosition;

    [SerializeField] Transform placeHolderMine;


    public static event Action OnActiveMinesChanged = delegate { };
    public static event Action EvtMinesAreSetUp = delegate { };

    public static event Action OnMinesAreAllSetUp = delegate { };


    void Awake()
    {
        allMines = new List<Mine>();
        Application.targetFrameRate = 60;
    }


    void OnEnable()
    {
        SaveManager.OnSaveDataReady += CreateMinesFromSaveData;
        Mine.EvtMineDeleted += OnMineGotDeleted;
    }


    void CreateMinesFromSaveData(SaveData data)
    {
        saveData = data;

        if (data.activeMinesData.Count == 0)
            CreateNewMineData();

        for (int i = 0; i < data.activeMinesData.Count; i++)
        {
            Mine newMine = CreateMineObjectFromData(data.activeMinesData[i]);
        }

        PositionMines();

        OnMinesAreAllSetUp();
    }


    MineData CreateNewMineData()
    {
        MineData newData = new MineData(GetLowestAvailableID(), "new mine", "colorName");

        newData.secondsLeftThisCycle = settings.secondsPerBlock;
        newData.miningTargetPercent = 0;

        saveData.activeMinesData.Add(newData);

        return newData;
    }


    Mine CreateMineObjectFromData(MineData data)
    {
        Mine newMine = Instantiate(minePF, transform);

        allMines.Add(newMine);

        newMine.Initialize(data);

        OnActiveMinesChanged();


        return newMine;
    }


    [Button]
    public void CreateNewDataAndObject()
    {
        CreateMineObjectFromData(CreateNewMineData());
        PositionMines();
    }


    void PositionMines()
    {
        for (int i = 0; i < allMines.Count; i++)
        {
            allMines[i].transform.position = firstMinePosition + new Vector2(0f, i * -settings.mineHeight);
        }

    }


    int GetLowestAvailableID()
    {
        int id = 0;

        List<MineData> allMines = new List<MineData>();

        foreach (var mine in saveData.activeMinesData)
        {
            allMines.Add(mine);
        }
        allMines.Sort((a, b) => a.id.CompareTo(b.id));

      
        for (int i = 0; i < allMines.Count; i++)
        {
            if (i != allMines[i].id)
            {
                id = i;
                break;
            }
            else if (i == allMines.Count - 1 && i == allMines[i].id)
                id = allMines.Count;
        }
        return id;
    }



 








    public float GetTotalSecondsMined()
    {
        float secondsMined = 0f;

        foreach (var mine in saveData.activeMinesData)
        {
            secondsMined += mine.secondsMinedSinceReset;
        }

        //avoid dividing by 0
        secondsMined = Mathf.Clamp(secondsMined, 1f, secondsMined);

        return secondsMined;
    }


    void OnMineGotDeleted(MineData deletedMine)
    {
        //no longer needed? Mines won't be destroyed anyway, but use pool and disable too many

        //if (dataToMines[deletedMine] == activeMine)
        //    activeMine = null;


        //  Destroy(dataToMines[deletedMine].gameObject);
        //ShowMinesAndAchievments();

        OnActiveMinesChanged();
    }


    [Button]
    void SetPlaceHolderMineAsStartPosition() => firstMinePosition = placeHolderMine.position;


    void OnDisable()
    {
        SaveManager.OnSaveDataReady -= CreateMinesFromSaveData;
        //        Mine.EvtTargetChanged -= SetTargetText;
    }



}
