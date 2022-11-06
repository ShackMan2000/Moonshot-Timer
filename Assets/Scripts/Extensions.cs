using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions 
{

    public static string ToStringTrimed(this float number, int maxDecimals)
    {
        string parameter = "F" + maxDecimals;
        string s = number.ToString(parameter);

        s = s.TrimEnd('0');
        s = s.TrimEnd('.');

        if (s == "")
            return "0";

        return s;
    }


    public static (float newValue, float changedBy) Cap(this float value, float min, float max)
    {
        float changedBy = 0f;

        if (value > max)
        {
            changedBy = max - value;
            value = max;
        }
        else if (value < min)
        {
            changedBy = min - value;
            value = min;
        }

        return (value, changedBy);
    }





    public static Vector3 Set(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? original.x, y ?? original.y, z ?? original.z);
    }

    public static Vector3 AdjustBy(this Vector3 original, float? x = null, float? y = null, float? z = null)
    {
        float newX = x == null ? original.x : original.x + (float)x;
        float newY = y == null ? original.y : original.y + (float)y;
        float newZ = z == null ? original.z : original.z + (float)z;

        return new Vector3(newX, newY, newZ);
    }


    public static Vector3 DirectionTo(this Vector3 original, Vector3 target)
    {
        return Vector3.Normalize(target - original);
    }


    public static float RandomPositiveOrNegative(this float f, float min, float max)
    {

        float value = Random.Range(min, max);
        float direction = Random.value < 0.5f ? 1 : -1;


        return direction * value;
    }
}
