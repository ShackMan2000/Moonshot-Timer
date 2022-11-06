using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MineNameAndTimeDisplay : MonoBehaviour
{



    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI timeLeftThisCycleText;


    [SerializeField] Mine mine;


    // needs to know when name was changed

    private void OnEnable()
    {
        mine.OnMineInitialized += ConnectToMineData;
        mine.OnProgressMade += UpdateTimeText;
        mine.OnNameChanged += SetNameText;
    }

    private void ConnectToMineData()
    {
        SetNameText();
        UpdateTimeText();
    }

    public void UpdateTimeText()
    {
        timeLeftThisCycleText.text = TimeConverter.ConvertSecondsToMinutesString(mine.Data.secondsLeftThisCycle);
    }
   
    void SetNameText() => nameText.text = mine.Data.mineName;
    



    private void OnDisable()
    {
        mine.OnMineInitialized -= ConnectToMineData;
        mine.OnProgressMade -= UpdateTimeText;
    }
}

