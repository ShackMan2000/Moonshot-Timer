using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;


[CreateAssetMenu]
public class ColorPack : ScriptableObject
{




    [ColorUsage(true, true)] public Color mainColor;
    [ColorUsage(true, true)] public Color gemBaseColor;

    [ColorUsage(true, true)] public Color gemDarkColor;
    [ColorUsage(true, true)] public Color gemHighlightColor;


    [SerializeField]
    public float shadowHueShift, shadowValueShift, shadowSaturationShift;
    [SerializeField]
    public float highlightHueShift, highlightValueShift, highlightSaturationShift;
    [SerializeField]
    public float frameHueShift, frameValueShift, frameSaturationShift;



    public string UID;
    private void OnValidate()
    {
       

//#if UNITY_EDITOR
//        if (UID == "")
//        {
//            UID = GUID.Generate().ToString();
//            EditorUtility.SetDirty(this);
//        }
//#endif
    }


    public void SetDirty() { }// => EditorUtility.SetDirty(this);

}


