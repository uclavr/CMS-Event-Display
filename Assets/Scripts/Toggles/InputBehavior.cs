using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InputBehavior : MonoBehaviour
{
    public GameObject currKeypad;
    private TMP_InputField inputField;
    private string prevText;
    // Start is called before the first frame update
    //void Start()
    //{
    //    inputField = GetComponent<TMP_InputField>();
    //    inputField.onSelect.AddListener(x => openKeypad());
    //}

    public void openKeypad(TMP_InputField field)
    {
        inputField = field;
        currKeypad.GetComponent<Keypad>().setLastSelectedField(inputField);
        currKeypad.SetActive(true);
    }
    public void leaveField()
    {
        currKeypad.SetActive(false);
        if(inputField != null)
        {
            inputField.ReleaseSelection(); // ** NEED TO FIX TO REMOVE CARET + WHEN 'X BUTTON IS PRESSED
            inputField.text = prevText;
        }
    }
    // update prevText if the field is save
    // strip of extra zeroes
    public void saveField()
    {
        if (inputField.text.Length == 0 || inputField.text == "-")
        {
            prevText = "";
            leaveField();
            return;
        }
        int i = 0;
        int sign = (inputField.text[0] == '-') ? -1 : 1;
        bool isZero = false;
        for (; i < inputField.text.Length; i++)
        {
            if (inputField.text[i] == '0') isZero = true;
            if (inputField.text[i] == '-') continue;
            if (inputField.text[i] != '0') break;
        }
        prevText = inputField.text.Substring(i);
        int decimalIndex = prevText.IndexOf('.');
        if(decimalIndex != -1)
        {
            for (i = prevText.Length - 1; i >= 0; i--)
            {
                if (prevText[i] == '.')
                {
                    i--;
                    break;
                }
                if (prevText[i] != '0') break;
            }
            prevText = prevText.Substring(0, i + 1);
        }

        if (prevText.Length == 0 && isZero) prevText = "0";
        if (prevText.Length != 0 && prevText[0] == '.') prevText = "0" + prevText;
        if (prevText != "0" && sign == -1) prevText = "-" + prevText;
        leaveField();


        //int i = 0, j = inputField.text.Length - 1;
        //int sign = (inputField.text[0] == '-') ? -1 : 1;
        //bool isZero = false;
        //for(; i < inputField.text.Length; i++)
        //{
        //    if (inputField.text[i] == '0') isZero = true;
        //    if (inputField.text[i] == '-') continue;
        //    if (inputField.text[i] != '0') break;
        //}
        //prevText = inputField.text.Substring(i);
        //if(prevText.Length == 0 && isZero) prevText = "0";
        //if(prevText != "0" && sign == -1) prevText = "-" + prevText;
        //leaveField();
    }

}
