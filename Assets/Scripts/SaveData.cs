using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData {


    public bool beginningIsMarked;

    public string beginning, lastReset;      

    public List<MineData> activeMinesData = new List<MineData>();
  //  public List<MineData> archivedMinesData;
    //   public float timePassed;

    // public List<float> progressByDays;    

}
