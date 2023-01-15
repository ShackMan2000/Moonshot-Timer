using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ResetPanel : UIPanel
{

    [SerializeField] SaveManager saveMan;

    [SerializeField] TextMeshProUGUI lastResetText, hoursSinceResetText;

    [SerializeField] GlobalSettings settings;

    [SerializeField] SaveManager saveManager;

    protected override void OnEnable()
    {
        base.OnEnable();
        SaveManager.OnSaveDataReady += UpdateInfoText;
        UpdateInfoText();
    }


    void UpdateInfoText(SaveData saveData = null)
    {
        
        //float hoursSinceReset = 0f;

        //foreach (var mine in saveManager.saveData.mineDatas)
        //{
        //    hoursSinceReset += mine.secondsMinedSinceReset;
        //}

        //hoursSinceReset /= settings.secondsPerBlock;

        //hoursSinceResetText.text = hoursSinceReset.ToString("F1");

    }






    public void ClickReset()
    {

        foreach (var mineData in SaveManager.Data.activeMinesData)
        {
            mineData.secondsMinedSinceReset = 0f;           
        }

       // UpdateInfoText();
    }





    protected override void OnDisable()
    {
        base.OnDisable();
        SaveManager.OnSaveDataReady -= UpdateInfoText;
        UpdateInfoText();
    }




}
