using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Sirenix.OdinInspector;
using UnityEngine.UI;

public class HistoryPanel : UIPanel
{
    [SerializeField] GlobalSettings settings;

    [SerializeField] TextMeshProUGUI mineStatsTextPrefab;

    [SerializeField] Transform mineStatsParent;

    [SerializeField] Button showTodayBtn;
    [SerializeField] Button showWeekBtn;
    [SerializeField] Button showMonthBtn;

    List<TextMeshProUGUI> mineStatsTextsPool = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> activeMineStatsTexts = new List<TextMeshProUGUI>();


    void Awake()
    {
        showTodayBtn.onClick.AddListener(() => ShowUnitsMined(TimeRange.Day));
        showWeekBtn.onClick.AddListener(() => ShowUnitsMined(TimeRange.Week));
        showMonthBtn.onClick.AddListener(() => ShowUnitsMined(TimeRange.Month));
    }


    [Button]
    public override void OpenPanel()
    {
        base.OpenPanel();
        ShowUnitsMined(TimeRange.Day);
    }


    public void ShowUnitsMined(TimeRange range)
    {
        foreach (var s in activeMineStatsTexts)
        {
            mineStatsTextsPool.Add(s);
            s.gameObject.SetActive(false);
        }
        
        activeMineStatsTexts.Clear();

        float totalSecondsAllMines = 0f;
        float focusSecondsAllMines = 0f;


        foreach (var mine in SaveManager.Data.activeMinesData)
        {
            float totalSeconds = 0f;
            float focusSeconds = 0f;

            switch (range)
            {
                case TimeRange.Day:
                    totalSeconds = Progress.GetTotalSecondsToday(mine);
                    focusSeconds = Progress.GetFocusedSecondsToday(mine);
                    break;
                case TimeRange.Week:
                    totalSeconds = Progress.GetTotalSecondsThisWeek(mine);
                    focusSeconds = Progress.GetFocusedSecondsThisWeek(mine);
                    break;
                case TimeRange.Month:
                    totalSeconds = Progress.GetTotalSecondsThisMonth(mine);
                    focusSeconds = Progress.GetFocusedSecondsThisMonth(mine);
                    break;
            }

            totalSecondsAllMines += totalSeconds;
            focusSecondsAllMines += focusSeconds;

            if (totalSeconds > 10f)
            {
                string statsAsString =
                    mine.mineName + " " + (totalSeconds / settings.secondsPerBlock).ToString("F1") + " (" +
                    ((focusSeconds / totalSeconds) * 100f).ToString("F0") + "% focused)";
                ShowMineStats(statsAsString);
            }
        }


        ShowMineStats("");


        string total =
            "Total " + (totalSecondsAllMines / settings.secondsPerBlock).ToString("F1") + " blocks mined (" +
            ((focusSecondsAllMines / totalSecondsAllMines) / settings.secondsPerBlock).ToString("F1") + "%)";
        ShowMineStats(total);
    }


    void ShowMineStats(string statsAsString)
    {
        //if there is an inactive text object in the pool, activate it and assign the text
        if (mineStatsTextsPool.Count > 0)
        {
            TextMeshProUGUI text = mineStatsTextsPool[0];
            mineStatsTextsPool.Remove(text);
            text.gameObject.SetActive(true);
            text.text = statsAsString;
            activeMineStatsTexts.Add(text);
        }
        //otherwise instantiate one and assign to parent
        else
        {
            TextMeshProUGUI text = Instantiate(mineStatsTextPrefab, mineStatsParent);
            text.gameObject.SetActive(true);
            text.text = statsAsString;
            activeMineStatsTexts.Add(text);
        }
    }
}


public enum TimeRange
{
    Day,
    Week,
    Month
}