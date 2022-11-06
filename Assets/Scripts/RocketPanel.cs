using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPanel : UIPanel
{

   public delegate void LaunchRocket();
    LaunchRocket launchRocket;


    public void Initialize(LaunchRocket launch)
    {
        launchRocket = launch;
    }


    public void ClickLaunch()
    {

     launchRocket();
        ClosePanel();
    }

}

