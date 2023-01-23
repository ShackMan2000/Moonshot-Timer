

    using System;
    using UnityEngine;

    [Serializable]
    public class DateRangeDetail
    {
     [field: SerializeField]   public DateTime StartDate { get; set; }
     [field: SerializeField]    public DateTime EndDate { get; set; }
        
     [field: SerializeField]   public int StartDay { get; set; }
     [field: SerializeField]    public int EndDay { get; set; }
    }
    
    
    