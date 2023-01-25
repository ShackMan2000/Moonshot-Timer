using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Tree : MonoBehaviour
{
    [SerializeField] List<Transform> leafes;
    List<LeafPosition> leafPositions; 

    [SerializeField] GameObject leafPrefab;
    
    [SerializeField] DateRangeShowing dateRangeShowing;

    [SerializeField] GlobalSettings settings;
    [SerializeField] string leafName = "Leaf";


    void OnEnable()
    {
        dateRangeShowing.EvtDateRangeChanged += CreateLeafsForDateRange;
    }

    void CreateLeafsForDateRange()
    {
        
        
        
        
        
    }


    [Button]
    void SetLeafPositions()
    {
        leafPositions = new List<LeafPosition>();
        
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.name.Contains(leafName))
            {
                // set position and add to list
                leafPositions.Add(new LeafPosition(g.position, g.rotation));
                g.gameObject.SetActive(false);
            }
        }
    }


    [Button]
    void CreateLeafs(int count)
    {
        // for (int i = 0; i < count; i++)
        // {
        //     var newLeaf = Instantiate(leafPrefab, originalLeafes[i].position, originalLeafes[i].rotation);
        //     newLeaf.transform.SetParent(transform);
        //     leafes.Add(newLeaf.transform);
        // }
    }


    struct LeafPosition
    {

        public Vector3 position;
        public Quaternion rotation;
        

        public LeafPosition(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
        }
    }
  
    
    
    // get the range showing
    // for each block, create a leaf. for the rest, create a leaf and scale it accordingly
    // make sure there are enough leafs
    
    
    
    
    
    //create single leaf with effect
    // for finishing a mine, focus camera on leaf, scale it up over x seconds and add a particle effect


    void OnDisable()
    {
        dateRangeShowing.EvtDateRangeChanged -= CreateLeafsForDateRange;
    }
}