using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IClickable
{
    MineData data;
    public MineData Data { get => data; }

    private float tickIntervall = 0.25f;

    private float tickCounter;

    [SerializeField]
    private GlobalSettings settings;

    bool isGettingMined;
  
    public float percentMined;




    public string MineName  
    { 
        get => data.mineName; 
        set 
        { 
            data.mineName = value;
            OnNameChanged();        
        }
    }




    bool isLocked;
    public ColorPack ColorPack => settings.GetColorPackByName(data.colorName);

    public event Action OnNameChanged = delegate { };

    public event Action OnMineInitialized = delegate { };

    public event Action OnMineStarted = delegate { };
    public event Action OnMineStopped = delegate { };
    public event Action OnProgressMade = delegate { };

  //  public static event Action OnAnyMineStarted = delegate { };
    public static event Action OnAnyProgressMade = delegate { };
    public event Action OnBlockFinishedMining = delegate { };

    public static event Action<Mine> OnMineClicked = delegate { };
    public static event Action OnAnyColorChanged = delegate { };

    public event Action<ColorPack> OnColorChanged = delegate { };

    public static event Action<MineData> EvtMineDeleted = delegate { };

    public static event Action OnAnyMineStarted = delegate { };



    private void OnEnable()
    {
        Dude.OnMineGotStarted += StopMining;
        Rocket.OnRocketLaunched += LockDuringLaunch;
        Rocket.OnRocketDone += Unlock;

    }

    public void Initialize(MineData newData)
    {
        data = newData;

        gameObject.SetActive(true);



        UpdatePerDaysLists();


        OnMineInitialized();
        OnColorChanged(settings.GetColorPackByName(data.colorName));
    }


    [ContextMenu("update")]
    private void UpdatePerDaysLists()
    {
        if (DayManager.daysSinceBeginning >= data.secondsMinedPerDay.Count)
        {
            for (int i = data.secondsMinedPerDay.Count; i <= DayManager.daysSinceBeginning; i++)
            {
                if (i >= data.secondsMinedPerDay.Count)
                    data.secondsMinedPerDay.Add(0f);
            }
        }
    }



    public void ChangeColor(ColorPack newColorPack)
    {
        data.colorName = newColorPack.name;

        OnColorChanged(newColorPack);
        OnAnyColorChanged();
    }


    [Button]
    void FiveLEftleft() => data.secondsLeftThisCycle = 5f;

    private void Update()
    {
        if (isGettingMined)
        {
            tickCounter += Time.deltaTime;
            if (tickCounter >= tickIntervall)
            {
                Tick(tickCounter);
                tickCounter = 0f;
            }
        }
    }


    public void Tick(float secondsTicked)
    {
        data.secondsLeftThisCycle -= secondsTicked;

        if (data.secondsLeftThisCycle <= 0f)
        {
            OnBlockFinishedMining();
            StopMining();
            Reset();
        }
        else if (data.secondsLeftThisCycle > settings.secondsPerBlock)
        {
            data.secondsLeftThisCycle = settings.secondsPerBlock;
        }

        data.secondsMinedSinceReset += secondsTicked;
        data.secondsMinedPerDay[data.secondsMinedPerDay.Count - 1] += secondsTicked;
        data.secondsInTank += secondsTicked;

        OnAnyProgressMade();
        OnProgressMade();
    }


    public void Reset()
    {
        data.secondsLeftThisCycle = settings.secondsPerBlock;
    }



    public void StartMining()
    {          
            OnMineStarted();
            isGettingMined = true;        
    }  

    public void StopMining()
    {
        isGettingMined = false;
        OnMineStopped();
    }

    public void Click()
    {
        OnMineClicked(this);
    }



    public void DeleteMine()
    {
        EvtMineDeleted(data);
    }




    void LockDuringLaunch()
    {
        StopMining();
        isLocked = true;
    }

    void Unlock() => isLocked = false;


}
