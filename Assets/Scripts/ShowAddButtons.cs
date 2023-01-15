using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAddButtons : MonoBehaviour
{


    [SerializeField] UIPanel buttonsContainer;



    public void OpenButtonsContainer()
    {
        buttonsContainer.OpenPanel();
    }
  
  
}
