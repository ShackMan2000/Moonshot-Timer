using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MenuBar : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI todayText;

    [SerializeField] TextMeshProUGUI progressQuickInfo;

    [SerializeField] GlobalSettings settings;

    private void OnEnable()
    {
        DayManager.OnCurrentDaySet += SetCurrentDateText;

        MineCreator.OnMinesAreAllSetUp += UpdateQuickInfo;
        Mine.OnAnyProgressMade += UpdateQuickInfo;
    }

    private void UpdateQuickInfo()
    {
        float progressToday = Progress.GetProgressLastXDays(1) / settings.secondsPerBlock;
        float progressThisWeek = Progress.GetTotalProgressThisWeek() / settings.secondsPerBlock;

        progressQuickInfo.text = progressToday.ToString("F3") + "/" + progressThisWeek.ToStringTrimed(1);
    }

    private void SetCurrentDateText()
    {
        todayText.text = DayManager.GetCurrentDateForDisplay();
    }




    private void OnDisable()
    {

        DayManager.OnCurrentDaySet -= SetCurrentDateText;

        MineCreator.OnMinesAreAllSetUp -= UpdateQuickInfo;
        Mine.OnAnyProgressMade -= UpdateQuickInfo;

    }

}

