using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimeConverter 
{



    public static string ConvertSecondsToMinutesString(float time)
    {
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }






    public static string ConverSecondsToHoursString(float time)
    {
        int hours = (int)time / 3600;
        int mins = (int)(time % 3600) / 60;
        int seconds = (int)(time % 60);
        // Make sure you use the appropriate decimal separator
        return string.Format("{0:D2}:{1:D2}:{2:D2}", hours, mins, seconds);
        //  return string.Format("{0:00}:{1:00}:{2:00}", hours, minutes, seconds);
    }



    //public static DateTime ConvertStringToDateTime(string timeAsString)
    //{
    //    DateTime dateTime;

    //    if (DateTime.TryParse(timeAsString, out dateTime))
    //    {
    //        return dateTime;
    //    }
    //    else
    //        return null;
    //}


    //public static string ConvertDateTimeToString(DateTime dateTime)
    //{




    //}


}
