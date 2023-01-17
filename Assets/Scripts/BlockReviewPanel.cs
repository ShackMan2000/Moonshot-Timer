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
        }



        void OpenPanel(Mine mine)
        {
            this.mine = mine;
            quoteText.text = inspirationQuotes.GetRandomQuote();
            panel.gameObject.SetActive(true);
        }
        
        
        public void FinishBlock(bool wasFocused)
        {
            if(wasFocused)
            {
                mine.ConvertSecondsToFocusedSeconds();
            }
            
            ClosePanel();
        }
        
        
        //close panel
        public void ClosePanel()
        {
            panel.gameObject.SetActive(false);
        }
        


        void OnDisable()
        {
            Mine.OnBlockFinished -= OpenPanel;
        }
    }
