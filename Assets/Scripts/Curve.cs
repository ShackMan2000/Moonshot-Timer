using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Curve
{

    [SerializeField] AnimationCurve curve;

    [field:SerializeReference] public float TotalTime { get; set; }

 

    public float ValueInTime(float timeStamp)
    {

        float percentOfTotalTime = Mathf.Clamp01(timeStamp / TotalTime);
         
        return curve.Evaluate(percentOfTotalTime);
    }



}
