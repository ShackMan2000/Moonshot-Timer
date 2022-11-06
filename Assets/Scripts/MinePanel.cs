using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MinePanel : UIPanel
{

    [SerializeField] TextWithEditor nameEditor;
    [SerializeField] TextWithEditor targetEditor;

    [SerializeField] TextMeshProUGUI unassignedTargetText;

    [SerializeField]
    private MineCreator mineMan;

    [SerializeField] Mine mine;

    MineData mineData => mine.Data;

    [SerializeField]
    private GlobalSettings settings;

    [SerializeField]
    private Transform colorButtonContainer;

    [SerializeField]
    private PickColorBTN pickColorBTNprefab;

    public static event Action OnTargetChanged = delegate{};

    public void InjectMineData(Mine m)
    {
        mine = m;
        nameEditor.MainText = mine.Data.mineName;
        targetEditor.MainText = "Target " + (mine.Data.miningTargetPercent * 100f).ToString("F0") + "%";       
    }


    protected override void OnEnable()
    {
        base.OnEnable();
        PickColorBTN.EvtColorPicked += ChangeMineColor;
        nameEditor.EvtInputFieldWasOpened += SetNamePlaceHolder;
        nameEditor.EvtTextWasEdited += ChangeName;

        targetEditor.EvtInputFieldWasOpened += SetTargetPlaceHolder;
        targetEditor.EvtTextWasEditedAsFloat += ChangeMiningTarget;

        UpdateTargetText();
    }


    private void Start()
    {
        SetUpPickColorButtons();        
    }

    private void SetNamePlaceHolder()
    {
        nameEditor.SetPlaceHolderText(mineData.mineName);
    }

    private void SetTargetPlaceHolder()
    {
        targetEditor.SetPlaceHolderText((mineData.miningTargetPercent * 100f).ToString("F0") + "%");
    }

    void UpdateTargetText()
    {
        targetEditor.MainText = (mineData.miningTargetPercent * 100f).ToString("F0") + "%";
        unassignedTargetText.text = $" {(TargetManager.GetUnassignedTargetPercent() * 100f).ToString("F0")}% free";
    }

    //private void SetNotePlaceHolder()
    //{
    //    noteText.SetPlaceHolderText(mine.data.note);
    //}

 


    private void SetUpPickColorButtons()
    {
        foreach (var colorPack in settings.allColorPacks)
        {
            PickColorBTN newBTN = Instantiate(pickColorBTNprefab, colorButtonContainer);
            newBTN.InjectColorPack(colorPack);
        }
    }


    private void ChangeName(string newName) => mine.MineName = newName;
    


    public void ChangeMiningTarget(float newTarget)
    {
        newTarget /= 100f;
        float percentFree = TargetManager.GetUnassignedTargetPercent();

        if (newTarget > 0f && newTarget <= percentFree + mineData.miningTargetPercent)
        {
            mineData.miningTargetPercent = newTarget;  
            UpdateTargetText();
            OnTargetChanged();
        }
    }


    private void ChangeMineColor(ColorPack newColor)
    {
        mine.ChangeColor(newColor);
    }



    private void ChangeRank(string obj)
    {
     
    }



    public void DeleteMine()
    {
        //mine.DeleteMine();
    }




    protected override void OnDisable()
    {
        base.OnDisable();
        PickColorBTN.EvtColorPicked -= ChangeMineColor;
    }




}
