using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class GlobalSettings : ScriptableObject
{
    public float secondsPerBlock;

    public float blocksForFullTank;

    public List<ColorPack> allColorPacks;

    public float maxBlocksAboveTarget;

    public float rocketFlightTime;

    public float mineHeight;
    
    public Color greenTargetColor;

    public List<Color> targetColors;





    public List<float> tankLevels;

    //use that for now, levels later
    public float tankSize;



    public ColorPack GetColorPackByName(string colorName)
    {
        ColorPack matchingPack = allColorPacks[0];

        foreach (var pack in allColorPacks)
        {
            if (pack.name == colorName)
            {
                matchingPack = pack;
                break;
            }
        }

        return matchingPack;
    }



    //public int GetTankLevel(float secondsInTank)
    //{
    //    float units = secondsInTank / secondsPerClass;
    //    for (int i = 0; i < tankLevels.Count; i++)
    //    {
    //        if (units < tankLevels[i])
    //            return i;
    //    }


    //    return tankLevels.Count -1;
    //}


    //public float GetTankSizeInSeconds(float secondsInTank)
    //{
    //    int level = GetTankLevel(secondsInTank);

    //    return tankLevels[level] * secondsPerClass;

    //}



}
