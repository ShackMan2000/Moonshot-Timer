using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dude : MonoBehaviour, IClickable
{

    [SerializeField]
    private Animator animator;

    [SerializeField] List<SpriteRenderer> renderers;

    [SerializeField]
    private Mine mine;

    [SerializeField] StoneBlock stoneBlock;

    [SerializeField]
    private float miningGap;

    bool isMining;

    //set position based on where the mine end is

    public event Action OnAxeHitsStone = delegate { };

    public static event Action OnMineGotStarted = delegate { };
    public static event Action OnMineGotStopped = delegate { };

    private void OnEnable()
    {
        OnMineGotStarted += StopMiningBusiness;
        mine.OnProgressMade += SetPosition;
        mine.OnColorChanged += SetColor;
        mine.OnBlockFinishedMining += StopMining;
        stoneBlock.OnStoneBlockSetUp += SetUp;
        GetComponent<Animator>().SetFloat("offset", UnityEngine.Random.Range(0.0f, 1.0f));
    }

    private void SetUp()
    {
        SetPosition();
        SetColor(mine.ColorPack);
    }

    private void SetColor(ColorPack colorPack)
    {
        foreach (var r in renderers)
        {
            r.color = colorPack.mainColor;
        }
    }

    public void StartMining()
    {
        OnMineGotStarted();
        animator.SetBool("isMining", true);

        //stop all mines, stop music
        mine.StartMining();

        isMining = true;
    }

    void StopMining()
    {
        mine.StopMining();

        OnMineGotStopped();

        StopMiningBusiness();

    }

    void StopMiningBusiness()
    {
        isMining = false;
        animator.SetBool("isMining", false);

        SetPosition();
    }

    public void SetPosition()
    {
        float newX = stoneBlock.MiningSpot + miningGap;

        transform.position = transform.position.Set(x: newX);
    }


    private void OnDisable()
    {   
        mine.OnProgressMade -= SetPosition;
    }


    public void AxeHitsRock() => OnAxeHitsStone();
   

    public void Click()
    {
        if (isMining)
            StopMining();
        else
            StartMining();
    }
}
