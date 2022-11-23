using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Sirenix.OdinInspector;

public class TextWithEditor : MonoBehaviour
{

    public bool onlyFloats;

    public bool onlyAvailableName;

    private List<string> unavailableNames;


    //name already taken is difficult bc it has no clue what kind of data it needs to check against. So either give it a method 
    // e.g. minemanager.IsNameTaken to check or a list of taken names? List sounds more agile.


    //maybe use that for changing target?
    //public bool onlyClampedFloat;
    //private float minValue, maxValue;

    [SerializeField]
    private EditTextField editField;

    private bool inEditMode;

    [SerializeField]
    private TextMeshProUGUI mainText, infoText;

    [SerializeField]
    private TMP_InputField inputField;

    //[SerializeField]
    //private Button resetBtn;



    private bool allowLineBreaks;

    [SerializeField]
    private string main, info;



    public string MainText
    {
        set
        {
            main = value;
            mainText.text = value;
        }
    }


    public string InfoText
    {
        set
        {
            info = value;
            infoText.text = value;
        }
    }


    public event Action<string> EvtTextWasEdited = delegate { };
    public event Action<float> EvtTextWasEditedAsFloat = delegate { };
    public event Action EvtInputFieldWasOpened = delegate { };
    //public static event Action EvtInputFieldOpenedStatic = delegate { };




    //when the edit button is clicked, open the edit field and listen to the input. So still do the name checking etc. here, so 
    //all the info doesn't have to be passed on. Also listen to when the panel gets closed...but why??
    //sorting should be done by field itself. 
    //They can just use the same text field? Except for position... nah fuck it, for now just have them have their own



    //protected override void OnEnable()
    //{
    //    EvtInputFieldOpenedStatic += CloseInputField;
    //}


    private void Awake()
    {

        infoText.text = info;
        //inputField.onEndEdit.AddListener(delegate { ChangeText(inputField.text); });
    }


    public void LimitToAvailableNames(List<string> takenNames)
    {
        unavailableNames = takenNames;
        onlyAvailableName = true;
    }



    public void OpenEditField()
    {
        editField.OpenEditField(transform);
        EvtInputFieldWasOpened();
    }


    //public void ToggleEditMode()
    //{


    //    //if (!inEditMode)
    //    //{
    //        //first bc it closes all input fields
    //        //EvtInputFieldOpenedStatic();


    //       // inEditMode = true;
    //        //inputField.gameObject.SetActive(true);
    //       // resetBtn.gameObject.SetActive(true);

    //    //    EvtInputFieldWasOpened();
    //    //    //save current text so it can be reset
    //    //}
    //    //else
    //    //{
    //    //    inEditMode = false;
    //    //    inputField.gameObject.SetActive(false);
    //    //    resetBtn.gameObject.SetActive(false);
    //    //}
    //}


    public void SetPlaceHolderText(string text)
    {
        //  infoText.text = info;
        inputField.text = text;
    }






    private void CheckInput(string input)
    {
        if (onlyFloats)
        {
            if (float.TryParse(input, out float inputAsFloat))
            {
                infoText.text = "cool";
            }
            else
            {
                infoText.text = "Not a number";
            }
        }
    }






    public void ChangeText()
    {
        string input = inputField.text;
        if (onlyFloats)
        {
            if (float.TryParse(input, out float inputAsFloat))
            {
                EvtTextWasEditedAsFloat(inputAsFloat);
                //ToggleEditMode();
            }
            else
            {
                infoText.text = "Not a number";
            }
        }
        else
        {
            EvtTextWasEdited(input);
            mainText.text = input;
        }

        editField.gameObject.SetActive(false);
    }



    public void CancelEdit()
    {

        editField.gameObject.SetActive(false);


    }


    //private void OnDisable()
    //{
    //    CloseInputField();
    //    EvtInputFieldOpenedStatic -= CloseInputField;
    //}


    public int nCounter;
    public int vCounter;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            ValidateInput("g");
            //set text to none
        }
    }

    private void ValidateInput(string input)
    {
        if (input == "n")
            nCounter++;
        else if (input == "v")
            vCounter++;

    }
}
