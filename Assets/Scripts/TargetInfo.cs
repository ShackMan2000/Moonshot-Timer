using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetInfo : MonoBehaviour
{
    [SerializeField] GlobalSettings settings;

    [SerializeField] TextMeshProUGUI targetText;
    [SerializeField] TextMeshProUGUI realMinedText;

    [SerializeField] TextMeshProUGUI timeNeededToReachTarget;

    [SerializeField] Image fullOpacityBar;
    [SerializeField] Image halfOpacityBar;


    [SerializeField] Color belowTargetColor;
    [SerializeField] Color aboveTargetColor;

    [SerializeField] Mine mine;
    [SerializeField] MineData data => mine.Data;

    [SerializeField] RectTransform targetPointer;
    [SerializeField] RectTransform realMinedPointer;

    //float differenceBarAlpha;
    //float realMinedBarAlpha;

    float barStartPositionYworld;
    float barHeightWorld;


    public bool moveToBar;


    public Transform pointerrrrr;
    public Vector3 barPosition;
    private void Update()
    {
        barPosition = fullOpacityBar.transform.position;
       // barPosition.y += fullOpacityBar.rectTransform.sizeDelta.y;

        if (moveToBar)
            pointerrrrr.transform.position = barPosition;
    }


  



    [Button]
    void MarkBarPositions()
    {
        Vector3[] v = new Vector3[4];
        fullOpacityBar.rectTransform.GetWorldCorners(v);
        barStartPositionYworld = v[0].y;

        barHeightWorld = v[1].y - v[0].y;
    
    
    }

    private void OnEnable()
    {
        mine.OnMineInitialized += Initialize;
        MinePanel.OnTargetChanged += UpdateTargetText;

        Mine.OnAnyProgressMade += UpdatePercentMined;
        mine.OnColorChanged += SetColorOfBars;
    }


    private void Start()
    {
        MarkBarPositions();
        UpdatePercentMined();
    }

    private void Initialize()
    {
        UpdateTargetText();
        UpdatePercentMined();
        SetBars();
        SetColorOfBars(settings.GetColorPackByName(mine.Data.colorName));
    }


    void UpdateTargetText()
    {
        targetText.text = "T: " + (data.miningTargetPercent * 100f).ToString("F0") + "%";
    }


    public void UpdatePercentMined()
    {
        float realMinedPercent = TargetManager.GetRealMinedPercent(data);
        float totalSecondsMined = TargetManager.GetTotalSecondsMinedSinceReset();
        float secondsTillTarget = 0f;

        float target = data.miningTargetPercent;

        if (realMinedPercent > data.miningTargetPercent)
        {
            secondsTillTarget = data.secondsMinedSinceReset - (target * totalSecondsMined);
            secondsTillTarget *= 1f / target;
            timeNeededToReachTarget.color = aboveTargetColor;
        }
        else
        {
            secondsTillTarget = (target * totalSecondsMined) - data.secondsMinedSinceReset;
            secondsTillTarget *= (1f / (1f - target));

            timeNeededToReachTarget.color = belowTargetColor;
        }

        realMinedText.text = (TargetManager.GetRealMinedPercent(data) * 100f).ToString("F0") + "%";

        if (target > 0.01f)
            timeNeededToReachTarget.text = TimeConverter.ConverSecondsToHoursString(secondsTillTarget);
        else
            timeNeededToReachTarget.text = "";

        SetBars();
    }



    void SetBars()
    {
        float realMinedPercent = TargetManager.GetRealMinedPercent(data);
        float miningTarget = mine.Data.miningTargetPercent;
        // start with setting bars. If realmined is above target, then lowOpacityBar is real mined value
        // else, lowOpacityBar is target, and real mined is full.


        float realMinedPointerHeight = barStartPositionYworld + barHeightWorld * realMinedPercent;
        realMinedPointer.position = realMinedPointer.position.Set(y: realMinedPointerHeight);

        float targetPointerHeight = barStartPositionYworld + barHeightWorld * miningTarget;
        targetPointer.position = targetPointer.position.Set(y: targetPointerHeight);


        if (realMinedPercent > miningTarget)
        {
            fullOpacityBar.fillAmount = miningTarget;
            halfOpacityBar.fillAmount = realMinedPercent;
        }
        else
        {
            fullOpacityBar.fillAmount = realMinedPercent;
            halfOpacityBar.fillAmount = miningTarget;
        }


      
    }


    void SetColorOfBars(ColorPack pack)
    {
        fullOpacityBar.color = new Color(pack.mainColor.r, pack.mainColor.g, pack.mainColor.b, fullOpacityBar.color.a);
        halfOpacityBar.color = new Color(pack.mainColor.r, pack.mainColor.g, pack.mainColor.b, halfOpacityBar.color.a);
    }



}




