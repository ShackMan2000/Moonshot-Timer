using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    public static int daysSinceBeginning;

    [SerializeField] int DEBUGaddDaysToToday;

    static DateTime beginningDate;

    public static event Action OnCurrentDaySet = delegate { };

    static DateTime today;
    [SerializeField] DateRangeShowing dateRangeShowing;
   
  

    void OnEnable()
    {
        SaveManager.OnSaveDataReady += CheckToMarkBeginning;
        SaveManager.OnSaveDataReady += SetDaysSinceBeginning;
    }


    public static int GetDayOfWeek()
    {
        if (today.DayOfWeek == 0)
            return 7;

        return (int)today.DayOfWeek;
    }

    public static int GetDayOfMonth() => today.Day;


    void CheckToMarkBeginning(SaveData data)
    {
        if (!data.beginningIsMarked)
        {
            DateTime today = DateTime.Today;

            if (DEBUGaddDaysToToday != 0)
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
            
            beginningDate = beginning;

            daysSinceBeginning = (int)t.TotalDays;
        }
        else
            Debug.LogError("Couldn't parse beginning from data");


        dateRangeShowing.SetDateRange(dateRangeShowing.dateRangeSelected);
        OnCurrentDaySet();
    }


    public static string GetCurrentDateForDisplay()
    {
        return today.ToString("ddd d/M");
    }


    public static DateRangeDetail ConvertDateRangeToDetail(DateRangeType range)
    {
        DateRangeDetail detail = new DateRangeDetail();
        detail.EndDate = today;
        detail.EndDay = daysSinceBeginning;

        // switch on range, set startday and startdate
        switch (range)
        {
            case DateRangeType.Today:
                detail.StartDay = daysSinceBeginning;
                detail.StartDate = today;
                break;
            case DateRangeType.Week:
                detail.StartDay = (daysSinceBeginning + 1) - GetDayOfWeek();
                detail.StartDate = today.AddDays(1 - GetDayOfWeek());
                break;
            case DateRangeType.Last7Days:
                detail.StartDay = daysSinceBeginning - 6;
                detail.StartDate = today.AddDays(-6);
                break;
            // case DateRangeType.Month:
            //     detail.StartDay = daysSinceBeginning - 30;
            //     detail.StartDate = today.AddDays(-30);
            //     break;
        }
        
        if (detail.StartDay < 1)
        {
            detail.StartDay = 1;
            detail.StartDate = beginningDate;
        }
        return detail;
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