using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMenuBehavior : MonoBehaviour
{
    public void hideAllMenu()
    {
        foreach (Transform child in transform)
            child.gameObject.SetActive(false);
    }
    public void openSubMenu(GameObject subMenu)
    {
        hideAllMenu();
        foreach (Transform child in transform)
        {
            if(child.gameObject == subMenu)
                child.gameObject.SetActive(true);
        }
    }
}
