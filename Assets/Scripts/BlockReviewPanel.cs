using System;
using UnityEngine;


    public class BlockReviewPanel : MonoBehaviour
    {

        [SerializeField] GameObject panel;
        
        //subscribe to mine when finished block event, activate panel object


        void OnEnable()
        {
            Mine.OnBlockFinished += OpenPanel;
        }



        void OpenPanel()
        {
            panel.gameObject.SetActive(true);
        }


        void OnDisable()
        {
            Mine.OnBlockFinished -= OpenPanel;
        }
    }
