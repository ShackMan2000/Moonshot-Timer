using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneBlock : MonoBehaviour
{

    [SerializeField] Mine mine;


    [SerializeField]List<SpriteRenderer> gemBase;
    [SerializeField]List<SpriteRenderer> gemDark;
    [SerializeField]List<SpriteRenderer> gemHighlight;

    [SerializeField] List<Material> gemBaseMaterials;
    [SerializeField] List<Material> gemDarkMaterials;
    [SerializeField] List<Material> gemHighlightMaterials;


    [SerializeField] GlobalSettings settings;

    [SerializeField] SpriteMask mask;
    public float MiningSpot => mask.transform.position.x + mask.sprite.bounds.size.x;

    float maskWidth;
    ColorPack colorPack;

    Vector3 maskOriginalPosition;

    public event Action OnStoneBlockSetUp = delegate { };


    void OnEnable()
    {
        mine.OnMineInitialized += SetUp;
        mine.OnProgressMade += AdjustCoalMask;
        mine.OnColorChanged += SetGemColor;
        
    }

    void SetUp()
    {
        maskWidth = mask.sprite.bounds.size.x * mask.transform.localScale.x;
        maskOriginalPosition = mask.transform.localPosition;

        SetGemColor(settings.GetColorPackByName(mine.Data.colorName));
        AdjustCoalMask();

        OnStoneBlockSetUp();
    }


    // just move mask by it's own width/percent
    void AdjustCoalMask()
    {
        float percentMined = 1f - (mine.Data.secondsLeftThisCycle / settings.secondsPerBlock);
  
        mask.transform.localPosition = maskOriginalPosition.AdjustBy(x: - percentMined * maskWidth);

    }



    // gemBase set sprite color
    // base, dark highlight set material

    void SetGemColor(ColorPack newColorPack)
    {
        colorPack = newColorPack;
        gemBase[0].color = colorPack.mainColor;
        for (int i = 0; i < gemBase.Count; i++)
        {
            gemBase[i].color = colorPack.mainColor;

            gemBase[i].material.SetColor("_Color", colorPack.gemBaseColor);
            gemHighlight[i].material.SetColor("_Color", colorPack.gemHighlightColor);
        }

        foreach (var dark in gemDark)
        {
            dark.material.SetColor("_Color", colorPack.gemDarkColor);

        }
    }


    void OnDisable()
    {
        mine.OnMineInitialized -= SetUp;
        mine.OnProgressMade -= AdjustCoalMask;
        mine.OnColorChanged -= SetGemColor;
    }
}

