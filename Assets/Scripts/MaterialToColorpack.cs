using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialToColorpack : MonoBehaviour
{


    [SerializeField] ColorPack pack;

    [SerializeField] Color mainColor;

    [SerializeField] Material baseMaterial, darkMaterial, highlightMaterial;


    [SerializeField] List<SpriteRenderer> spriteRenderer;


    [Button]
    void ConvertToColorPack()
    {
        pack.mainColor= spriteRenderer[0].color;
      //  pack.mainColor = mainColor;

        pack.gemDarkColor = darkMaterial.GetColor("_Color");
        pack.gemBaseColor = baseMaterial.GetColor("_Color");
        pack.gemHighlightColor = highlightMaterial.GetColor("_Color");

        pack.SetDirty();

        mainColor = pack.mainColor;

    }

    [Button]
    void LoadColorPack()
    {
        mainColor = pack.mainColor;

        foreach (var item in spriteRenderer)
        {
            item.color= mainColor;
        }

        baseMaterial.SetColor("_Color", pack.gemBaseColor);
        darkMaterial.SetColor("_Color", pack.gemDarkColor);
        highlightMaterial.SetColor("_Color", pack.gemHighlightColor);
    }
}

