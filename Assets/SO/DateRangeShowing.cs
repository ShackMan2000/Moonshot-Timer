using System;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu]
public class DateRangeShowing : ScriptableObject
{
    public DateRangeType dateRangeSelected;

    public event Action EvtDateRangeChanged = delegate { };

    public int StartDay => Detail.StartDay;
    public int EndDay => Detail.EndDay;
    public DateTime StartDate => Detail.StartDate;
    public DateTime EndDate => Detail.EndDate;

    [field: SerializeField] public DateRangeDetail Detail { get; private set; }

    [field: SerializeField] public string StartDateAsString { get; private set; }
    [field: SerializeField] public string EndDateAsString { get; private set; }

    public void SetDateRange(DateRangeType dateRangeType)
    {
        dateRangeSelected = dateRangeType;
        Detail = DayManager.ConvertDateRangeToDetail(dateRangeType);


        StartDateAsString = Detail.StartDate.ToString("dd/MM/yyyy");
        EndDateAsString = Detail.EndDate.ToString("dd/MM/yyyy");
        EvtDateRangeChanged();
    }
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