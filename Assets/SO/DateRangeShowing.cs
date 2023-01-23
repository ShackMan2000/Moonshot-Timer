using System;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu]
public class DateRangeShowing : ScriptableObject
{
    public DateRangeType dateRangeSelected;
    
    public event Action<DateRangeType> OnDateRangeChanged = delegate { };

    [field: SerializeField] public DateRangeDetail Detail { get; private set; }
    
    [field: SerializeField] public string StartDateAsString { get; private set; }
    [field: SerializeField] public string EndDateAsString { get; private set; }

    public void SetDateRange(DateRangeType dateRangeType)
    {
        dateRangeSelected = dateRangeType;
        Detail = DayManager.ConvertDateRangeToDetail(dateRangeType);
        
        StartDateAsString = Detail.StartDate.ToString("dd/MM/yyyy");
        EndDateAsString = Detail.EndDate.ToString("dd/MM/yyyy");
        OnDateRangeChanged(dateRangeType);
    }
    
    
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
        
    public int StartDay { get; set; }
    public int EndDay { get; set; }

}




public enum DateRangeType
{
    Today,
    Week,
    Month,
    Last7Days,
    Last30Days,
    Custom
}

