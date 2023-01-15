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

    void OnEnable()
    {
        DayManager.OnCurrentDaySet += SetCurrentDateText;

        MineCreator.OnMinesAreAllSetUp += UpdateQuickInfo;
        Mine.OnAnyProgressMade += UpdateQuickInfo;
    }

    void UpdateQuickInfo()
    {
        float totalProgressToday = Progress.GetTotalProgressToday / settings.secondsPerBlock;
        float focusedProgressToday = Progress.GetFocusedProgressToday / settings.secondsPerBlock;

        float totalProgressWeek = Progress.GetTotalProgressThisWeek() / settings.secondsPerBlock;
        float focusedProgressWeek = Progress.GetFocusedProgressThisWeek() / settings.secondsPerBlock;

        dailyProgressText.text = totalProgressToday.ToString("F1") + "(" + (focusedProgressToday * 100f /totalProgressToday).ToString("F0") + " %F)";
        weeklyProgressText.text = totalProgressWeek.ToString("F1") + "(" + (focusedProgressWeek * 100f /totalProgressWeek).ToString("F0") + " %F)";
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

