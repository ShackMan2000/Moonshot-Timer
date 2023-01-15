using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MineData
{

    public MineData(int id, string mineName, string colorName)
    {
        this.id = id;
        this.mineName = mineName;
        this.colorName = colorName;
    }




    public int id;
    public string mineName, colorName;

    public bool hasTarget;

    public float secondsLeftThisCycle;
    public float secondsMinedSinceReset;
    public float miningTargetPercent;

    public List<float> secondsMinedPerDay = new List<float>();
    public List<float> secondsMinedFocusedPerDay = new List<float>();

    public float secondsInTank;

    public string note;
    public bool isArchived;

}
