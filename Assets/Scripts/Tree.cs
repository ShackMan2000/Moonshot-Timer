using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Tree : MonoBehaviour
{
    [SerializeField] List<Leaf> activeLeafs = new List<Leaf>();
    [SerializeField] List<Leaf> pooledLeafs = new List<Leaf>();
    [SerializeField] List<LeafPosition> leafPositions;

    [SerializeField] Leaf leafPrefab;

    [SerializeField] DateRangeShowing dateRangeShowing;

    [SerializeField] GlobalSettings settings;
    [SerializeField] string leafName = "Leaf";

    [SerializeField] float originalScale;

    int leafCount;


    void OnEnable()
    {
        dateRangeShowing.EvtDateRangeChanged += CreateLeafsForDateRange;
    }

    void CreateLeafsForDateRange()
    {
        List<MineStats> mineStats = Progress.GetMineStatsForRange(dateRangeShowing);

        ResetLeafs();

        for (int i = 0; i < mineStats.Count; i++)
        {
            var colorPack = settings.GetColorPackByName(mineStats[i].mineData.colorName);

            //calc focused units
            int focusedUnits = Mathf.FloorToInt(mineStats[i].focusedSecondsMined / settings.secondsPerBlock);
            float restFocusedUnits = (mineStats[i].focusedSecondsMined - focusedUnits * settings.secondsPerBlock) / settings.secondsPerBlock;

            //calc total units
            int totalUnits = Mathf.FloorToInt(mineStats[i].totalSecondsMined / settings.secondsPerBlock);
            float restTotalUnits = (mineStats[i].totalSecondsMined - totalUnits * settings.secondsPerBlock) / settings.secondsPerBlock;

            //calc distracted by subtracting focused from total
            int distractedUnits = totalUnits - focusedUnits;
            float restDistractedUnits = 0f;
            
            if (restTotalUnits > restFocusedUnits)
            {
                restDistractedUnits = restTotalUnits - restFocusedUnits;
            }


            // create a leaf for each focused unit and initialize by passing in the color
            for (int j = 0; j < focusedUnits; j++)
            {
                CreateLeaf(1f, colorPack, true);
            }

            // create one for each distracted unit
            for (int j = 0; j < distractedUnits; j++)
            {
                CreateLeaf(1f, colorPack, false);
            }
            //create one for the rest and use as scale
            if(restFocusedUnits > 0.05f)
                CreateLeaf(restFocusedUnits, colorPack, true);
            
            if(restDistractedUnits > 0.05f)
                CreateLeaf(restDistractedUnits, colorPack, false);
        }
    }

    void ResetLeafs()
    {
        //return all leafs to pool
        for (int i = 0; i < activeLeafs.Count; i++)
        {
            activeLeafs[i].gameObject.SetActive(false);
            pooledLeafs.Add(activeLeafs[i]);
        }

        activeLeafs.Clear();
        leafCount = 0;
    }

    void CreateLeaf(float scale, ColorPack colorPack, bool isFocused)
    {
        if (leafCount >= leafPositions.Count)
            return;

        //get leaf from pool if there is one, otherwise instantiate
        Leaf leaf;
        if (pooledLeafs.Count > 0)
        {
            leaf = pooledLeafs[0];
            pooledLeafs.RemoveAt(0);
        }
        else
        {
            leaf = Instantiate(leafPrefab, transform);
        }

        leaf.gameObject.SetActive(true);
        leaf.transform.SetPositionAndRotation(leafPositions[leafCount].position, leafPositions[leafCount].rotation);

        leaf.name = leafName + leafCount;
        leaf.Initialize(scale * originalScale, colorPack, isFocused);
        leafCount++;
        activeLeafs.Add(leaf);
    }


    [Button]
    void SetLeafPositionsAndScale()
    {
        originalScale = 0f;
        leafPositions = new List<LeafPosition>();

        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            if (g.name.Contains(leafName))
            {
                // set position and add to list
                leafPositions.Add(new LeafPosition(g.position, g.rotation));
                g.gameObject.SetActive(false);

                if (originalScale == 0f)
                    originalScale = g.localScale.x;
            }
        }
    }


    [System.Serializable]
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