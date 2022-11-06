using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickColorBTN : MonoBehaviour
{

    [SerializeField]
    private Image mainImage;


    private ColorPack colorPack;

    public static event Action<ColorPack> EvtColorPicked = delegate { };


    public void InjectColorPack(ColorPack newPack)
    {
        colorPack = newPack;
        mainImage.color = colorPack.mainColor;
    }


    public void Click()
    {
        EvtColorPicked(colorPack);
    }

  
  
}
