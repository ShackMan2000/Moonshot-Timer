using System;
using TMPro;
using UnityEngine;


    public class BlockReviewPanel : MonoBehaviour
    {

        [SerializeField] GameObject panel;
        [SerializeField] InspirationQuotes inspirationQuotes;
        
        [SerializeField] TextMeshProUGUI quoteText;
        
        
        //subscribe to mine when finished block event, activate panel object

        Mine mine;

        void OnEnable()
        {
            Mine.OnBlockFinished += OpenPanel;
            quoteText.text = inspirationQuotes.GetRandomQuote();
        }



        void OpenPanel(Mine mine)
        {
            this.mine = mine;
            panel.gameObject.SetActive(true);
        }
        
        
        public void ClosePanel(bool wasFocused)
        {
            if(wasFocused)
            {
                mine.ConvertSecondsToFocusedSeconds();
            }
            
            
            panel.gameObject.SetActive(false);
        }


        void OnDisable()
        {
            Mine.OnBlockFinished -= OpenPanel;
        }
    }
