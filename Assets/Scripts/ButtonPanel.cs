using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPanel : MonoBehaviour
{
  

    [SerializeField]
    private GameObject[]  buttons;
    public bool showAlways;






            private void Awake()
    {
        
        //OnMouseExit();
    }



    public void OnMouseEnter()
    {
        foreach (var button in buttons)
        {
            button.SetActive(true);
        }
      
    }

    //public void OnMouseExit()
    //{
    //    if  (classUnit.IsRunning) return;

    //    foreach (var button in buttons)
    //    {
    //        button.SetActive(false);
    //    }

    //}
}
