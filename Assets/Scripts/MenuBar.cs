using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.Serialization;

public class MenuBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI todayText;

    [SerializeField] TextMeshProUGUI dailyProgressText;
    [SerializeField] TextMeshProUGUI weeklyProgressText;

    [SerializeField] GlobalSettings settings;

    [SerializeField] DateRangeShowing dateRangeShowing;

    void OnEnable()
    {
        DayManager.OnCurrentDaySet += SetCurrentDateText;
        MineCreator.OnMinesAreAllSetUp += UpdateQuickInfo;
        Mine.OnAnyProgressMade += UpdateQuickInfo;
        
        dateRangeShowing.EvtDateRangeChanged += UpdateQuickInfo;
    }

    void UpdateQuickInfo()
    {
        int today = DayManager.daysSinceBeginning;

        float totalProgressToday = Progress.GetTotalSecondsAllMines(today, today) / settings.secondsPerBlock;
        float focusedProgressToday = Progress.GetFocusedSecondsAllMines(today, today) / settings.secondsPerBlock;


        // show whatever the range is
        float totalProgressRange = Progress.GetTotalSecondsAllMinesForRangeShowing(dateRangeShowing) / settings.secondsPerBlock;
        float focusedProgressRange = Progress.GetTotalSecondsAllMinesForRangeShowing(dateRangeShowing) / settings.secondsPerBlock;

        dailyProgressText.text = totalProgressToday.ToString("F1") + "(" + (focusedProgressToday * 100f / totalProgressToday).ToString("F0") + " %F)";
        weeklyProgressText.text = totalProgressRange.ToString("F1") + "(" + (focusedProgressRange * 100f / totalProgressRange).ToString("F0") + " %F)";
    }

    void SetCurrentDateText()
    {
        todayText.text = DayManager.GetCurrentDateForDisplay();
    }


    void OnDisable()
    {
        DayManager.OnCurrentDaySet -= SetCurrentDateText;

        MineCreator.OnMinesAreAllSetUp -= UpdateQuickInfo;
        Mine.OnAnyProgressMade -= UpdateQuickInfo;
    }
}