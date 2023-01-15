using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;

public class Progress : MonoBehaviour
{

    [Button]
    public static float GetProgressLastXDays(int daysToCheck)
    {
        if (daysToCheck == 0)
        {
          //  Debug.Log("Don't check for 0 days, use 1 to check for today");
            daysToCheck = 1;
        }

        SaveData data = SaveManager.Data;

        float secondsMined = 0f;

        int daysSinceBeginning = DayManager.daysSinceBeginning;

        //gotta use both minedata lists though, may have archived some
        foreach (var mine in data.activeMinesData)
        {
            for (int i = daysSinceBeginning; i > daysSinceBeginning - daysToCheck; i--)
            {
                if (i < 0) break;
                secondsMined += mine.distractedSecondsMinedPerDay[i];
            }
        }
        return secondsMined;
    }


    public static float GetTotalProgressToday => GetProgressLastXDays(1);
    public static float GetTotalProgressThisWeek() => GetProgressLastXDays(DayManager.GetDayOfWeek());




    //don't fill up, feels weird and not that important because there won't be free % anyway


}
