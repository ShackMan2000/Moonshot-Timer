using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAddButtons : MonoBehaviour
{


    [SerializeField]
    private UIPanel buttonsContainer;



    public void OpenButtonsContainer()
    {
        buttonsContainer.OpenPanel();
    }
  
  
}
