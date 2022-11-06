using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{


    [SerializeField]
    private Text currentTimeText;



    private bool isMusicPlaying;

    public int dayOfYear;

    [SerializeField]
    private GameObject overviewPanel;

    public float secondsPerClass;

    [SerializeField]
    private float tickTime;

    private float counter;

    private bool classPaused = true;


    private SaveManager saveManager;
    public SaveData saveData;

 //   private ChallengeProgress rewardBar;
    public int numberOfClasses;


    //[SerializeField]
    //private Text allClassesText, resetsTriggeredText;

    //private int allClassesCount;
    
    //public static int resetsCount;





    private void Awake()
    {
      //  saveManager = FindObjectOfType<SaveManager>();
      //  saveData = saveManager.saveData;
      ////  rewardBar = FindObjectOfType<ChallengeProgress>();
       
      //  dayOfYear = System.DateTime.Now.DayOfYear;
      //  counter = tickTime;

      //  Application.targetFrameRate = 30;

    }





    public void UpdateTimeText(float time)
    {
        currentTimeText.text = FormatTime(time);
    }



    //public void PauseAllclasses()
    //{
    //    foreach (var unit in allClasses)
    //    {
    //        unit.IsRunning = false;
    //    }

    //  // rewardBar.isRunning = false;
    //}

    


    //public void CheckIfAllUnitsDone()
    //{
    //    bool allFinished = true;

    //    //foreach (var unit in saveData.blockData)
    //    //{
    //    //   if(unit.progressInSec < secondsPerClass)
    //    //            allFinished = false;
    //    //}

    //    if(allFinished)
    //    {
    //        //resetsCount = 0;
    //        foreach (var unit in allClasses)
    //        {
    //            unit.Reset();
                
    //        }

    //     //   allClassesText.text = "classes " + allClasses.Length.ToString();
    //       // resetsTriggeredText.text = "resets " + resetsCount.ToString();
    //    }
    //}



    public string FormatTime(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    public void ShowOverview()
    {
        overviewPanel.SetActive(!overviewPanel.activeSelf);
    }

    public void AddDailyProgress(float timePassed)
    {
      //  saveData.progressByDays[dayOfYear] += timePassed / secondsPerClass;
    }
}
