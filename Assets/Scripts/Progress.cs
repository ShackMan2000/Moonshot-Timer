using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public static class Progress
{
    delegate List<float> GetSecondsMined(MineData data);

    
    
    static float GetFocusedSecondsMined(int days)
    {
       return GetProgressLastDays(days, mineData => mineData.focusedSecondsMinedPerDay);
    }
    
    static float GetDistractedSecondsMined(int days)
    {
       return GetProgressLastDays(days, mineData => mineData.distractedSecondsMinedPerDay);
    }
    
    
    static float GetProgressLastDays(int daysToCheck, GetSecondsMined getSecondsMined)
    {
        if (daysToCheck == 0)
        {
            Debug.Log("Don't check for 0 days, use 1 to check for today");
            daysToCheck = 1;
        }

        SaveData data = SaveManager.Data;

        float secondsMined = 0f;

        int daysSinceBeginning = DayManager.daysSinceBeginning;

        //gotta use both minedata lists though, may have archived some
        foreach (var mine in  data.activeMinesData)
        {
            for (int i = daysSinceBeginning; i > daysSinceBeginning - daysToCheck; i--)
            {
                if (i < 0) break;
                secondsMined += getSecondsMined(mine)[i];
            }
        }
        return secondsMined;
    }
    
    
    
    // [Button]
    // public static float GetProgressLastXDays(int daysToCheck)
    // {
    //     if (daysToCheck == 0)
    //     {
    //         Debug.Log("Don't check for 0 days, use 1 to check for today");
    //         daysToCheck = 1;
    //     }
    //
    //     SaveData data = SaveManager.Data;
    //
    //     float secondsMined = 0f;
    //
    //     int daysSinceBeginning = DayManager.daysSinceBeginning;
    //
    //     //gotta use both minedata lists though, may have archived some
    //     foreach (var mine in data.activeMinesData)
    //     {
    //         for (int i = daysSinceBeginning; i > daysSinceBeginning - daysToCheck; i--)
    //         {
    //             if (i < 0) break;
    //             secondsMined += mine.distractedSecondsMinedPerDay[i];
    //         }
    //     }
    //     return secondsMined;
    // }
    //
    


    public static float GetTotalProgressToday => GetFocusedSecondsMined(1) + GetDistractedSecondsMined(1);
    public static float GetFocusedProgressToday => GetFocusedSecondsMined(1);
    public static float GetTotalProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek()) + GetDistractedSecondsMined(DayManager.GetDayOfWeek());
    public static float GetFocusedProgressThisWeek() => GetFocusedSecondsMined(DayManager.GetDayOfWeek());

}
