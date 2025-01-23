using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeypadButtons : MonoBehaviour
{
    Keypad keypad;
    TextMeshProUGUI buttonText;

    void Start()
    {
        keypad = GetComponentInParent<Keypad>();
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        
    }
}
