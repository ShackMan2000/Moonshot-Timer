using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileManager : MonoBehaviour
{

    public static bool WriteToFile(string fileName, string contents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            File.WriteAllText(fullPath, contents);  
            return true;

        }
        catch (Exception e)
        {
            Debug.LogException(e);


        }
        return false;
    }

    public static string ReadFile(string fileName)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
           var data = File.ReadAllText(fullPath);
            return data;

        }
        catch (Exception e)
        {

            Debug.Log($"no file {e}");

        }

        return null;
    }

}
