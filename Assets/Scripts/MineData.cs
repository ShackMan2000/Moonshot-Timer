using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


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

    public List<float> distractedSecondsMinedPerDay = new List<float>();
    public List<float> focusedSecondsMinedPerDay = new List<float>();


    public string note;
    public bool isArchived;

}
