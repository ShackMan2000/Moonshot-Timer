using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HistoryPanel : UIPanel
{



    //get all mines, including the archived ones, and add to the total
    //is there any point in seeing which mine each day? not really
    //so just get the number.

 

    [SerializeField]
    private SaveManager saveMan;


    [SerializeField]
    private TextMeshProUGUI totalText, coreText, otherText, achievementText;



    public override void OpenPanel()
    {
        base.OpenPanel();

        //save last opening
        ShowUnitsMined(DayManager.daysSinceBeginning, 1);
    }

    //need to get core of today and this week

    


    public void  ShowUnitsMined(int startingDay, int daysToCheck)
    {
        //float unitsMinedCore = 0f;
        //float unitsMinedOther = 0f;
        //float unitsAchievement = 0f;   
        

        //foreach (var mine in saveMan.saveData.mineDatas)
        //{
        //    for (int i = startingDay; i > startingDay - daysToCheck; i--)
        //    {

        //        if (i < 0) break;

        //        if (i < mine.unitsPerDay.Count)
        //        {
        //            if (mine.isCore)
        //                unitsMinedCore += mine.unitsPerDay[i];
        //            else
        //                unitsMinedOther += mine.unitsPerDay[i];
        //        }
        //    }
        //}


      


        //totalText.text = "Total: " + (unitsMinedCore + unitsMinedOther + unitsAchievement).ToString("F1");
        //coreText.text = "Core : " + unitsMinedCore.ToString("F1");
        //otherText.text = "Other " + unitsMinedOther.ToString("F1");
        //achievementText.text = "Achievements: " + unitsAchievement.ToStringTrimed(2);        
    }








    public void ShowLastX(int lastX)
    {
        ShowUnitsMined(DayManager.daysSinceBeginning, lastX);
    }



    public void ShowThisWeek()
    {
        // start today, but only show last x days until monday
        // so get day of week, 
        int dayOfWeek =  (int) DateTime.Now.DayOfWeek;
        if (dayOfWeek == 0)
            dayOfWeek = 7;

        ShowUnitsMined(DayManager.daysSinceBeginning, dayOfWeek);     
    }




  





    public void ShowLastWeek()
    {
        //int dayOfWeek = (int)DateTime.Now.DayOfWeek;
        //if (dayOfWeek == 0)
        //    dayOfWeek = 7;

     //   ShowUnitsMined(DayManager.currentDay - 7, dayOfWeek);

    }





    private int daysShowing;
    [SerializeField]


    private GameManager gameManager;


    [SerializeField]
    private TextMeshProUGUI[] progressTexts;

    [SerializeField]
    private TextMeshProUGUI sevenDayTotalText, loosingTodayText, gainingTodayText, last7LessTodayText;


    //[SerializeField]
    //private Text[] yInfos;

    private int maxBarsAtNormalScale;

    private List<float> daysShowingValue;
    private List<OverviewBar> barsShowing;


    [SerializeField]
    private GameObject barPrefab;
    [SerializeField]
    private GameObject barContainer;
    private float highestProgress = 0f;



    private float last7;




    float refreshCounter;


    private void OnEnable()
    {
        // ChangeNumberOfDaysDisplayed("7");
        // ShowLast20Days();
        // Show7DayTotal();
        refreshCounter = 1f;
    }


    public float GetWeeklyProgress()
    {

        return 0f;

    }


    private void Show7DayTotal()
    {
        last7 = 0f;

        for (int i = 1; i < 8; i++)
        {

            //  last7 += gameManager.saveData.progressByDays[gameManager.dayOfYear - i];
            //    print(i + "adding " + gameManager.saveData.progressByDays[gameManager.dayOfYear - i] + "= " + last7);
        }

        sevenDayTotalText.SetText(last7.ToString("F1"));


        //float loosingToday = gameManager.saveData.progressByDays[gameManager.dayOfYear - 7];
        //float gainingToday = gameManager.saveData.progressByDays[gameManager.dayOfYear];
        //float last7LessToday = (last7 + gainingToday) - loosingToday;

        //loosingTodayText.text = (-loosingToday).ToString("F1");
        //gainingTodayText.text = (+gainingToday).ToString("F1");
        //last7LessTodayText.text = last7LessToday.ToString("F1");


    }


    private void Update()
    {
        refreshCounter -= Time.deltaTime;

        if (refreshCounter < 0)
        {
            refreshCounter = 1f;
            //Show7DayTotal();

        }
    }








    private void ShowLast20Days()
    {

        for (int i = 0; i < progressTexts.Length; i++)
        {
            // progressTexts[i].SetText(gameManager.saveData.progressByDays[gameManager.dayOfYear-i].ToString("F2"));
        }
    }




    public void ChangeNumberOfDaysDisplayed(string newNumber)
    {
        daysShowing = System.Convert.ToInt32(newNumber);
        daysShowingValue = new List<float>();
        if (barsShowing != null)
            foreach (var bar in barsShowing)
            {
                Destroy(bar.gameObject);
            }
        barsShowing = new List<OverviewBar>();


        for (int i = 0; i < daysShowing; i++)
        {
            var dayFetched = System.DateTime.Now.DayOfYear - daysShowing + i;
            if (dayFetched <= 0)
                dayFetched += 365;

            //  daysShowingValue.Add(timer.saveData.progressByDays[dayFetched]);

            OverviewBar newBar = Instantiate(barPrefab).GetComponent<OverviewBar>();
            newBar.dayText.text = (i + 1).ToString();
            newBar.day = dayFetched;
            newBar.transform.SetParent(barContainer.transform);
            newBar.transform.localScale = new Vector3(1, 1, 1);


            barsShowing.Add(newBar);

            if (daysShowingValue[i] > highestProgress)
                highestProgress = daysShowingValue[i];

        }
        foreach (var bar in barsShowing)
        {

            bar.SetFillAmount(highestProgress);
        }

        //SetYinfo();

    }



    //private void SetYinfo()
    //{
    //    float step = 0.25f;

    //    for (int i = 0; i < yInfos.Length; i++)
    //    {
    //        yInfos[i].text = (highestProgress - (highestProgress * i * step)).ToString("F1");
    //    }
    //}

}
