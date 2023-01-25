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
    [SerializeField] TextMeshProUGUI dateRangeText;

    [SerializeField] Transform mineStatsParent;
    
    [SerializeField] DateRangeShowing dateRangeShowing;

    
    List<TextMeshProUGUI> mineStatsTextsPool = new List<TextMeshProUGUI>();
    List<TextMeshProUGUI> activeMineStatsTexts = new List<TextMeshProUGUI>();


    protected override void OnEnable()
    {
        base.OnEnable();
        dateRangeShowing.EvtDateRangeChanged += ShowStatsForDateRange;
    }


    [Button]
    public override void OpenPanel()
    {
        base.OpenPanel();
        ShowStatsForDateRange();
    }

    void ShowStatsForDateRange()
    {
        var dateRange = dateRangeShowing.dateRangeSelected;
        dateRangeText.text = dateRangeShowing.Detail.StartDate.ToString("dd/MM/yyyy") + " - " + dateRangeShowing.Detail.EndDate.ToString("dd/MM/yyyy");
        
        ShowUnitsMined();
    }

    public void ShowUnitsMined()
    {
        List<MineStats> mineStats = Progress.GetMineStatsForRange(dateRangeShowing);
        
        
        foreach (var s in activeMineStatsTexts)
        {
            mineStatsTextsPool.Add(s);
            s.gameObject.SetActive(false);
        }
        
        //sort minestatsTextsPool by order in hierarchy
        mineStatsTextsPool.Sort((a, b) => a.transform.GetSiblingIndex().CompareTo(b.transform.GetSiblingIndex()));
        
        
        activeMineStatsTexts.Clear();
        
        float totalSecondsAllMines = 0f;
        float focusSecondsAllMines = 0f;
        
        
        foreach (var stat in mineStats)
        {
            totalSecondsAllMines += stat.totalSecondsMined;
            focusSecondsAllMines += stat.focusedSecondsMined;
        
            if (stat.totalSecondsMined > 10f)
            {
                string statsAsString =
                    stat.mineData.mineName + " " + (stat.totalSecondsMined / settings.secondsPerBlock).ToString("F1") + " (" +
                    ((stat.focusedSecondsMined / stat.totalSecondsMined) * 100f).ToString("F0") + "% focused)";
                ShowMineStats(statsAsString);
            }
        }
        
        //empty line in layout group
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


    protected override void OnDisable()
    {
        base.OnDisable();
        dateRangeShowing.EvtDateRangeChanged -= ShowStatsForDateRange;
    }
}

