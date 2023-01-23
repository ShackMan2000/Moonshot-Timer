using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

public class Tree : MonoBehaviour
{
    [SerializeField] List<Transform> originalLeafes;
    [SerializeField] List<Transform> leafes;
    
    [SerializeField] GameObject leafPrefab;

 
    [Button]
    void MarkLeafs()
    {
        originalLeafes = new List<Transform>();
        
        foreach (Transform g in transform.GetComponentsInChildren<Transform>())
        {
            Debug.Log(g.name);
            if (g.name.Contains("Leaf"))
            {
                originalLeafes.Add(g);
                g.gameObject.SetActive(false);
            }
        }
    }




    [Button]
    void CreateLeafs(int count)
    {
        for (int i = 0; i < count; i++)
        {
            var newLeaf = Instantiate(leafPrefab, originalLeafes[i].position, originalLeafes[i].rotation);
            newLeaf.transform.SetParent(transform);
            leafes.Add(newLeaf.transform);
        }
    }
    
    
}