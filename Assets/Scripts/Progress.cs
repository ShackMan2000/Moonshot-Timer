using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public static class Progress
{
    delegate List<float> TypeOfSeconds(MineData data);

    static float GetSecondsMinedAllMines(int startDay, int endDay, TypeOfSeconds typeOfSeconds)
    {
        
        SaveData data = SaveManager.Data;

        //avoid dividing by zero
        float totalSeconds = 0.00001f;


        foreach (var mine in data.activeMinesData)
        {
            totalSeconds += GetSecondsMinedLastDays(startDay, endDay, typeOfSeconds, mine);
        }


        return totalSeconds;
    }


    static float GetSecondsMinedLastDays(int startDay, int endDay, TypeOfSeconds typeOfTypeOfSeconds, MineData mineData)
    {
        float secondsMined = 0.0001f;

        for (int i = startDay; i <= endDay; i++)
        {
            secondsMined += typeOfTypeOfSeconds(mineData)[i];
        }

        return secondsMined;
    }


    
    
    public static List<MineStats> GetMineStatsForRange(DateRangeShowing rangeShowing)
    {
        var list = new List<MineStats>();
        
        foreach (var mine in SaveManager.Data.activeMinesData)
        {
            float secondsMined = GetTotalSecondsForRangeShowing(mine, rangeShowing);
            float focusedSecondsMined = GetFocusedSecondsForRangeShowing(mine, rangeShowing);
            
            list.Add(new MineStats(mine, secondsMined, focusedSecondsMined));
        }
        
        
        
        
        return list;
    }
    
    
    public static float GetTotalSecondsAllMinesForRangeShowing(DateRangeShowing dateRangeShowing)
    {
        int startDay = dateRangeShowing.StartDay;
        int endDay = dateRangeShowing.EndDay;

        return GetTotalSecondsAllMines(startDay, endDay);
    }
    
    public static float GetFocusedSecondsAllMinesForRangeShowing(DateRangeShowing dateRangeShowing)
    {
        int startDay = dateRangeShowing.StartDay;
        int endDay = dateRangeShowing.EndDay;

        return GetFocusedSecondsAllMines(startDay, endDay);
    }

    public static float GetTotalSecondsAllMines(int startDay, int endDay) =>
        GetFocusedSecondsAllMines(startDay, endDay) + GetDistractedSecondsAllMines(startDay, endDay);

    public static float GetFocusedSecondsAllMines(int startDay, int endDay) =>
        GetSecondsMinedAllMines(startDay, endDay, mineData => mineData.focusedSecondsMinedPerDay);

    public static float GetDistractedSecondsAllMines(int startDay, int endDay) =>
        GetSecondsMinedAllMines(startDay, endDay, mineData => mineData.distractedSecondsMinedPerDay);


    
    
   
    public static float GetTotalSecondsForRangeShowing(MineData mineData, DateRangeShowing rangeShowing) 
        => GetFocusedSecondsForRangeShowing(mineData, rangeShowing) + GetDistractedSecondsForRangeShowing(mineData, rangeShowing);
    public static float GetFocusedSecondsForRangeShowing(MineData mineData, DateRangeShowing rangeShowing) 
        => GetSecondsMinedLastDays(rangeShowing.StartDay, rangeShowing.EndDay, m => m.focusedSecondsMinedPerDay, mineData);
    public static float GetDistractedSecondsForRangeShowing(MineData mineData, DateRangeShowing rangeShowing) 
        => GetSecondsMinedLastDays(rangeShowing.StartDay, rangeShowing.EndDay, m => m.distractedSecondsMinedPerDay, mineData);
  
    
    
    
    
    
    
    // public static float GetTotalProgressToday => GetFocusedSecondsMined(1) + GetDistractedSecondsMined(1);
    // public static float GetFocusedProgressToday => GetFocusedSecondsMined(1);
    // public static float GetTotalProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek()) + GetDistractedSecondsMined(DayManager.GetDayOfWeek());
    // public static float GetFocusedProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek());
    //
    // public static float GetTotalProgressThisMonth() => GetFocusedSecondsMined(DayManager.GetDayOfMonth()) + GetDistractedSecondsMined(DayManager.GetDayOfMonth());
    // public static float GetFocusedProgressThisMonth() => GetFocusedSecondsMined(DayManager.GetDayOfMonth());
}


public class MineStats
{
    public MineData mineData;
    public float totalSecondsMined;
    public float focusedSecondsMined;

    public MineStats(MineData mineData, float totalSecondsMined, float focusedSecondsMined)
    {
        this.mineData = mineData;
        this.totalSecondsMined = totalSecondsMined;
        this.focusedSecondsMined = focusedSecondsMined;
    }
}