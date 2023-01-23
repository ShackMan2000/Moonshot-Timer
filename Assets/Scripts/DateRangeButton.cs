using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public class DateRangeButton : MonoBehaviour
{
    [SerializeField] DateRangeType dateRangeType;
    [SerializeField] DateRangeShowing dateRangeShowing;
    [SerializeField] TextMeshProUGUI label;


    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeDateRangeShowing);
    }

    void ChangeDateRangeShowing()
    {
        dateRangeShowing.SetDateRange(dateRangeType);
    }


    void OnValidate()
    {
        label.text = dateRangeType.ToString();
        gameObject.name = dateRangeType.ToString() + " Button";
    }
}