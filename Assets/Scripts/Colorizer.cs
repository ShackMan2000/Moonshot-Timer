using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Colorizer : MonoBehaviour
{


    [SerializeField] Image mainImage;


    [SerializeField] List<Image> images;


    [SerializeField] Color mainColor;


    [SerializeField] List<Vector3> colorChangesX10;

    [Header("1 Shadow")]
    [Header("0 Highlight")]


    public ColorPack colorPack;


    void Start()
    {
        if (colorPack != null)
            LoadColorPack(colorPack);
    }


    public void SetColors(Color newColor)
    {
        mainColor = newColor;
        mainImage.color = mainColor;

        //convert the mainColor to HSV for easier adjustments

        Color.RGBToHSV(mainColor, out float mainH, out float mainS, out float mainV);



        //go through the list of images and set color
        for (int i = 0; i < images.Count; i++)
        {
            if (images[i] != null && i < colorChangesX10.Count)
            {

                float newHue = (mainH + colorChangesX10[i].x / 10f);
                newHue = ClampToHueRange(newHue);

                float newSaturation = (mainS + colorChangesX10[i].y / 10f);
                newSaturation = Mathf.Clamp(newSaturation, 0f, 1f);

                float newValue = (mainV + colorChangesX10[i].z / 10f);
                newValue = Mathf.Clamp(newValue, 0f, 1f);



                images[i].color = Color.HSVToRGB(newHue, newSaturation, newValue);

            }
        }
    }


    float ClampToHueRange(float input)
    {
        //adding or subtracting values to the original Hue might make it smaller than 0 or bigger than 1
        //make sure it stays in that range, so e.g. 1.3f becomes 0.3f, and -0.2f becomes 0.8f

        if (input < 0f)
        {
            float remainder = input % 1f;
            input = 1f + remainder;
        }
        else if (input > 1f)
            input = input % 1f;

        return input;
    }


    void OnValidate()
    {
        if (mainImage != null)
            SetColors(mainColor);
    }







    [ContextMenu("Load")]
    void LoadColorPackInEditor()
    {
        if (colorPack != null)
            LoadColorPack(colorPack);
    }




    public void LoadColorPack(ColorPack loadedPack)
    {
        colorPack = loadedPack;
        mainColor = colorPack.mainColor;


        colorChangesX10 = new List<Vector3>();

        //for (int i = 0; i < colorPack.colorChanges.Count; i++)
        //{
        //    colorChangesX10.Add(colorPack.colorChanges[i]);
        //}

        SetColors(mainColor);
    }


    [ContextMenu("Save")]
    public void SavePack()
    {
        //if (colorPack == null) return;


        //colorPack.mainColor = mainColor;

        //colorPack.colorChanges = new List<Vector3>();

        //for (int i = 0; i < colorChangesX10.Count; i++)
        //{
        //    colorPack.colorChanges.Add(colorChangesX10[i]);
        //}

    }





}
