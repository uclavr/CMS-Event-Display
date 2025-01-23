using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabMenu : MonoBehaviour
{
    public TabMenuPair tabItem;
    public GameObject parentMenu;

    void Awake()
    {
        tabItem = GetComponent<TabMenuPair>();
        tabItem.tab.onClick.AddListener(SubmitTabChange);
    }

    public void SubmitTabChange()
    {
        parentMenu.GetComponent<TabMenuController>().ChangeTab(name);
    }
}