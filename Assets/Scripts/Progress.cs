using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public static class Progress
{
    delegate List<float> TypeOfSeconds(MineData data);


    // static float GetFocusedSecondsMined(int days)
    // {
    //     return GetProgressLastDaysAllMines(days, mineData => mineData.focusedSecondsMinedPerDay);
    // }
    //
    // static float GetDistractedSecondsMined(int days)
    // {
    //     return GetProgressLastDaysAllMines(days, mineData => mineData.distractedSecondsMinedPerDay);
    // }


    static float GetSecondsMinedLastDaysAllMines(int daysToCheck, TypeOfSeconds typeOfSeconds)
    {
        if (daysToCheck == 0)
        {
            Debug.Log("Don't check for 0 days, use 1 to check for today");
            daysToCheck = 1;
        }

        SaveData data = SaveManager.Data;

        float totalSeconds = 0f;


        //gotta use both minedata lists though, may have archived some
        foreach (var mine in data.activeMinesData)
        {
            totalSeconds += GetSecondsMinedLastDays(daysToCheck, typeOfSeconds, mine);
        }


        return totalSeconds;
    }


    static float GetSecondsMinedLastDays(int daysToCheck, TypeOfSeconds typeOfTypeOfSeconds, MineData mineData)
    {
        float secondsMined = 0f;

        int daysSinceBeginning = DayManager.daysSinceBeginning;

        for (int i = daysSinceBeginning; i > daysSinceBeginning - daysToCheck; i--)
        {
            if (i < 0) break;
            secondsMined += typeOfTypeOfSeconds(mineData)[i];
        }

        return secondsMined;
    }


    public static float GetTotalSecondsTodayAllMines() =>
        GetFocusedSecondsTodayAllMines() + GetDistractedSecondsTodayAllMines();

    public static float GetFocusedSecondsTodayAllMines() =>
        GetSecondsMinedLastDaysAllMines(1, mineData => mineData.focusedSecondsMinedPerDay);

    public static float GetDistractedSecondsTodayAllMines() =>
        GetSecondsMinedLastDaysAllMines(1, mineData => mineData.distractedSecondsMinedPerDay);


    //total seconds this week
    public static float GetTotalSecondsThisWeekAllMines() =>
        GetFocusedSecondsThisWeekAllMines() + GetDistractedSecondsThisWeekAllMines();
    
    public static float GetFocusedSecondsThisWeekAllMines() => GetSecondsMinedLastDaysAllMines(DayManager.GetDayOfWeek(),
        mineData => mineData.focusedSecondsMinedPerDay);
    
    public static float GetDistractedSecondsThisWeekAllMines() => GetSecondsMinedLastDaysAllMines(DayManager.GetDayOfWeek(),
        mineData => mineData.distractedSecondsMinedPerDay);


    //total seconds this month
    public static float GetTotalSecondsThisMonthAllMines =>
        GetFocusedSecondsThisMonthAllMines() + GetDistractedSecondsThisMonthAllMines();
    public static float GetFocusedSecondsThisMonthAllMines() => GetSecondsMinedLastDaysAllMines(DayManager.GetDayOfMonth(), mineData => mineData.focusedSecondsMinedPerDay);
    public static float GetDistractedSecondsThisMonthAllMines() => GetSecondsMinedLastDaysAllMines(DayManager.GetDayOfMonth(), mineData => mineData.distractedSecondsMinedPerDay);

    
    
    
    public static float GetTotalSecondsToday(MineData mineData) => GetFocusedSecondsToday(mineData) + GetDistractedSecondsToday(mineData);
    public static float GetFocusedSecondsToday(MineData mineData) => GetSecondsMinedLastDays(1, m => m.focusedSecondsMinedPerDay, mineData);
    public static float GetDistractedSecondsToday(MineData mineData) => GetSecondsMinedLastDays(1, m => m.distractedSecondsMinedPerDay, mineData);
    
    
    public static float GetTotalSecondsThisWeek(MineData mineData) => GetFocusedSecondsThisWeek(mineData) + GetDistractedSecondsThisWeek(mineData);
    public static float GetFocusedSecondsThisWeek(MineData mineData) => GetSecondsMinedLastDays(DayManager.GetDayOfWeek(), m => m.focusedSecondsMinedPerDay, mineData);
    public static float GetDistractedSecondsThisWeek(MineData mineData) => GetSecondsMinedLastDays(DayManager.GetDayOfWeek(), m => m.distractedSecondsMinedPerDay, mineData);
    
    
    public static float GetTotalSecondsThisMonth(MineData mineData) => GetFocusedSecondsThisMonth(mineData) + GetDistractedSecondsThisMonth(mineData);
    public static float GetFocusedSecondsThisMonth(MineData mineData) => GetSecondsMinedLastDays(DayManager.GetDayOfMonth(), m => m.focusedSecondsMinedPerDay, mineData);
    public static float GetDistractedSecondsThisMonth(MineData mineData) => GetSecondsMinedLastDays(DayManager.GetDayOfMonth(), m => m.distractedSecondsMinedPerDay, mineData);
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // public static float GetTotalProgressToday => GetFocusedSecondsMined(1) + GetDistractedSecondsMined(1);
    // public static float GetFocusedProgressToday => GetFocusedSecondsMined(1);
    // public static float GetTotalProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek()) + GetDistractedSecondsMined(DayManager.GetDayOfWeek());
    // public static float GetFocusedProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek());
    //
    // public static float GetTotalProgressThisMonth() => GetFocusedSecondsMined(DayManager.GetDayOfMonth()) + GetDistractedSecondsMined(DayManager.GetDayOfMonth());
    // public static float GetFocusedProgressThisMonth() => GetFocusedSecondsMined(DayManager.GetDayOfMonth());
}