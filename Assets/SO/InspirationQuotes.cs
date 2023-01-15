using System.Collections.Generic;
using UnityEngine;


    [CreateAssetMenu]
    public class InspirationQuotes : ScriptableObject
    {
        // a list of strings filled with inspirational quotes
     
       [field : SerializeField]  public List<string> Quotes { get; private set; }
       
       
       
       //get random quote
         public string GetRandomQuote()
         {
              return Quotes[Random.Range(0, Quotes.Count)];
         }
         
         
         
        
            
         
       
    }
