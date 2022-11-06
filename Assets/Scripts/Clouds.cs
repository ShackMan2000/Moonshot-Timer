using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clouds : MonoBehaviour
{


    //actually move rocket, just spawn clouds ahead of rocket and recycle when they are too low


    [SerializeField] List<GameObject> cloudPrefabs;


    List<GameObject> inactiveClouds = new List<GameObject>();
    List<GameObject> activeClouds = new List<GameObject>();


    
    [SerializeField] Vector2 spawnPerDistance;

    float nextSpawnAt;

    float distanceCounter;

    [SerializeField] Transform rocket;


    bool spawnClouds;


    [Button]
    void OnRocketLaunched()
    {



    }

    void ShowClouds()
    {
       



    }


    private void Update()
    {
        if(spawnClouds)
        {





        }
    }




}

