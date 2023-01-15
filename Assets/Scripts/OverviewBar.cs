using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewBar : MonoBehaviour
{


    public Text dayText;
    [SerializeField] Text progressThatDay;



    public int day;

    [SerializeField] Image barFill;

    GameManager timer;

    void Awake()
    {
        timer = FindObjectOfType<GameManager>();
    }



    public void SetFillAmount(float highestValue)
    {
        //avoid dividing by 0 to prevent nan
        if (highestValue == 0)
            highestValue = 0.1f;

        //barFill.fillAmount = timer.saveData.progressByDays[day] / highestValue;
        SetColor();
        //  progressThatDay.text = timer.saveData.progressByDays[day].ToString("F1");

        RectTransform barRect = barFill.GetComponent<RectTransform>();
        RectTransform progressThatDayRect = progressThatDay.GetComponent<RectTransform>();

        progressThatDayRect.localPosition = new Vector3(0f, ((barRect.rect.height / 2f) * barFill.fillAmount) + 10f, 0f);
        progressThatDayRect.localPosition = new Vector3(0f, (-barRect.rect.height / 2f) + (barRect.rect.height * barFill.fillAmount) + 10f, 0f);

    }




    public void SetColor()
    {
        //float classesDone = timer.saveData.progressByDays[day];
        //    Color newColor;
        //    float green;
        //    float red;

        //    if (classesDone <= 5f)
        //    {
        //        red = 1f;
        //        green = (1f / 5f) * classesDone;
        //    }
        //    else
        //    {
        //        green = 1f;
        //        red = 1f - (1f / 5f) * (classesDone -5f);

        //    }

        //    newColor = new Color(red, green, 0f ,1f);
        //    barFill.color = newColor;

        //}

    }
}
