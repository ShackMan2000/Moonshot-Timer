﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{

    public static int daysSinceBeginning;

    [SerializeField] int DEBUGaddDaysToToday;


    public static event Action OnCurrentDaySet = delegate { };

    static DateTime today;

    void OnEnable()
    {
        SaveManager.OnSaveDataReady += CheckToMarkBeginning;
        SaveManager.OnSaveDataReady += SetDaysSinceBeginning;
    }


    public static int GetDayOfWeek() => (int)today.DayOfWeek;
    public static int GetDayOfMonth() => today.Day;


    void CheckToMarkBeginning(SaveData data)
    {
        if (!data.beginningIsMarked)
        {
            DateTime today = DateTime.Today;

            if(DEBUGaddDaysToToday != 0)
                today = today.AddDays(DEBUGaddDaysToToday);

            data.beginning = today.ToString();
            data.beginningIsMarked = true;
        }
    }


    void SetDaysSinceBeginning(SaveData data)
    {
        if (DateTime.TryParse(data.beginning, out DateTime beginning))
        {
            today = DateTime.Today.AddDays(DEBUGaddDaysToToday);
            TimeSpan t = today - beginning;

            daysSinceBeginning = (int)t.TotalDays;
        }
        else
            Debug.LogError("Couldn't parse beginning from data");


        OnCurrentDaySet();  
    }


   

    public static string GetCurrentDateForDisplay()
    {
        return today.ToString("ddd d/M");
    }



    //for debug only
    public int showDaysSinceBeginning;
    public string showTodayAsString;

    void Update()
    {
        showTodayAsString = GetCurrentDateForDisplay();
        showDaysSinceBeginning = daysSinceBeginning;
    }



}
