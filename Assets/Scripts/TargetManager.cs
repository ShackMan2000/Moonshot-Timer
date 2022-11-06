using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;

public class TargetManager : MonoBehaviour
{

    [SerializeField]
    private GlobalSettings settings;

    //private List<Mine> activeM, minesBelowTarget;



    [SerializeField]
    private MineCreator mineMan;






    public static float GetUnassignedTargetPercent()
    {
        var data = SaveManager.Data;

        float unassignedPercent = 1f;

        foreach (var m in data.activeMinesData)
        {
            unassignedPercent -= m.miningTargetPercent;
        }

        return unassignedPercent;
    }
 
   

    public static float GetRealMinedPercent(MineData d)
    {
        float totalSecondsMined = GetTotalSecondsMinedSinceReset();

        return d.secondsMinedSinceReset / totalSecondsMined;
    }

    
  

    public static float GetTotalSecondsMinedSinceReset()
    {
        //to not divide by 0
        float totalSecondsMined = 1f;
        foreach (var m in SaveManager.Data.activeMinesData)
        {
            totalSecondsMined += m.secondsMinedSinceReset;
        }

        return totalSecondsMined;
    }




    //public Color GetTargetLightColor(Mine mine)
    //{
    //    if (activeM.Contains(mine))
    //        return settings.greenTargetColor;


    //    int rank = minesBelowTarget.IndexOf(mine);

    //    if (rank >= settings.targetColors.Count)
    //        return settings.targetColors[settings.targetColors.Count - 1];

    //    if (rank >= 0 && rank < settings.targetColors.Count)
    //    {
    //        return settings.targetColors[rank];

    //    }



    //    return settings.targetColors[0];

    //}













}
