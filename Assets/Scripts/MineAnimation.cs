﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineAnimation : MonoBehaviour
{



    [SerializeField]
    private GlobalSettings settings;


    [SerializeField]
    private float maxPileSize;
    //just scale it for now? maybe later use different sprites

    private float pileSize;

    private Mine mine;

    [SerializeField]
    private Transform pileTransform;

    //[SerializeField]
    //private SpriteRenderer pileMainColor, pileShadowColor, pileHighlightColor;



    //placeholder until there is an elevator to pick it up

    private float pickUpTime, pickUpCounter;


    //when mine ticks, adjust size of pile



    private void Awake()
    {
        mine = GetComponent<Mine>();
       // mine.EvtThisMineTicked += OnMineTicked;
      //  mine.EvtMineStopped += OnMineStopped;

        pickUpTime = 5f;
        //colorPile, but also set scale to 0 since all the limbo got taken away

    }


    //private void Update()
    //{
    //    pickUpCounter -= Time.deltaTime;
    //    if(pickUpCounter < 0f)
    //    {
    //        pickUpCounter = pickUpTime;
    //      //  mine.MoveFromLimboToTank(mine.UnitsInLimbo);

    //        AdjustPileSize();
    //    }
    //}




    //private void OnMineTicked()
    //{
    //    AdjustPileSize();
    //}


    private void OnMineStopped()
    {

        


    }

    //private void AdjustPileSize()
    //{
    //    float newScale;

    //    if (mine.UnitsInLimbo > maxPileSize)
    //        newScale = 1f;
    //    else
    //    {
    //        newScale = Mathf.Clamp(mine.UnitsInLimbo / maxPileSize, 0f, mine.UnitsInLimbo / maxPileSize);
    //    }

    //    pileTransform.localScale = new Vector3(newScale, newScale, 1f);

    //}











}
