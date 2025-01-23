using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

//public struct KeypadInput
//{
//    private TMP_InputField inputField;
//    private int numDecimals;
//    public void setInputField(TMP_InputField newInputField)
//    {
//        inputField = newInputField;
//    }
//    public void setNumDecimals(int num = 0) { numDecimals = num; }
//    public readonly int getNumDecimals() { return numDecimals; }

//}

public class CopyKeypad : MonoBehaviour
{
    public TMP_InputField starterField;
    public GameObject currKeypad;
    private TMP_InputField inputField;
    // private KeypadInput input;
    private int numDecimals = 0;
    private bool isNegative = false;
    int caretPosition;
    int selectionStart, selectionEnd;

    void Start()
    {
        inputField = starterField;
        currKeypad.SetActive(false);
    }

    //public void hideKeypad()
    //{
    //    currKeypad.SetActive(false);
    //}

    public void setLastSelectedField(TMP_InputField field)
    {
        inputField = field;
        inputField.selectionColor = Color.gray;
        inputField.enabled = false;
        inputField.enabled = true;
        inputField.caretPosition = inputField.text.Length;
    }
    public void insertNum(string num) // allow for insertion over a selection
    {
        //if (inputField)
        //{
            int stringDeleteLength = inputField.selectionFocusPosition - inputField.selectionAnchorPosition;
            if (stringDeleteLength != 0)
            {
                if(num != "-")
                {
                    int newCaret = (inputField.selectionAnchorPosition < inputField.selectionFocusPosition) ?
                    inputField.selectionAnchorPosition : inputField.selectionFocusPosition;
                    deleteNum();
                    inputField.text = inputField.text.Insert(inputField.caretPosition, num);
                    inputField.caretPosition = newCaret + 1;
                }
            }
            else
            {
                if (num != "-")
                {
                    inputField.text = inputField.text.Insert(inputField.caretPosition, num);
                    inputField.caretPosition++;
                }
                else
                {
                    isNegative = !isNegative;
                    if (isNegative)
                    {
                        inputField.text = "-" + inputField.text;
                        inputField.caretPosition++;
                    }
                    else
                    {
                        bool endCaret = false;
                        if (inputField.caretPosition == inputField.text.Length) endCaret = true;
                        inputField.text = inputField.text.Remove(0, 1);
                        if(!endCaret) inputField.caretPosition--;

                    }
                }
            }
        //}
        
    }

    public void deleteNum()     // allow for selection deletion
    {
        if(inputField.text.Length > 0)
        {
            int stringDeleteLength = inputField.selectionFocusPosition - inputField.selectionAnchorPosition;
            if(stringDeleteLength != 0)
            {
                int startPosition = inputField.text.Length - 1;
                if (stringDeleteLength > 0) startPosition = inputField.selectionAnchorPosition;
                if (stringDeleteLength < 0) startPosition = inputField.selectionFocusPosition;
                stringDeleteLength = Math.Abs(stringDeleteLength);
                //if(inputField.text.Substring(startPosition, stringDeleteLength).Contains('.')) numDecimals--;
                if (inputField.text.Substring(startPosition, stringDeleteLength).Contains('-')) isNegative = !isNegative;
                inputField.text = inputField.text.Remove(startPosition, stringDeleteLength);
                inputField.caretPosition = startPosition;
            }
            else
            {
                if (inputField.caretPosition == 0) return;
                //if (inputField.text[inputField.caretPosition - 1] == '.') numDecimals--;
                if (inputField.text[inputField.caretPosition - 1] == '-') isNegative = !isNegative;
                inputField.text = inputField.text.Remove(inputField.caretPosition - 1, 1);
                if (inputField.caretPosition != inputField.text.Length) inputField.caretPosition--;
            }

        }
    }
    public string getInput()
    {
        return inputField.text;
    }
    public void insert0() { insertNum("0"); }
    public void insert1() { insertNum("1"); }
    public void insert2() { insertNum("2"); }
    public void insert3() { insertNum("3"); }
    public void insert4() { insertNum("4"); }
    public void insert5() { insertNum("5"); }
    public void insert6() { insertNum("6"); }
    public void insert7() { insertNum("7"); }
    public void insert8() { insertNum("8"); }
    public void insert9() { insertNum("9"); }
    public void insertDecimal() {
        // int stringDeleteLength = inputField.selectionFocusPosition - inputField.selectionAnchorPosition;
        int deciIndex = inputField.text.IndexOf('.');
        if (deciIndex == -1 || (inputField.selectionAnchorPosition <= deciIndex && deciIndex < inputField.selectionFocusPosition) ||
            (inputField.selectionAnchorPosition > deciIndex && deciIndex >= inputField.selectionFocusPosition))
        {
            insertNum(".");
            //numDecimals++;
        }
    }

    public void insertNegative()
    {
        insertNum("-");
    }
}
