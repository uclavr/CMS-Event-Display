using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTester : MonoBehaviour
{
    public GameObject obj;

    public void showObj(bool value)
    {
        obj.SetActive(value);
    }
}
