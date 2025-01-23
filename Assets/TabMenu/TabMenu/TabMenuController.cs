using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabMenuController : MonoBehaviour
{
    public TabMenuPair currentTab;
    public List<GameObject> tabs;
    private Dictionary<string, TabMenuPair> menuContents;

    void Start()
    {
        menuContents = new Dictionary<string, TabMenuPair>();
        foreach(GameObject obj in tabs)
        {
            menuContents[obj.name] = obj.GetComponent<TabMenuPair>();
        }
        currentTab = tabs[0].GetComponent<TabMenuPair>();
    }
        
    public void ChangeTab(string argument)
    {
        currentTab.tabContent.transform.gameObject.SetActive(false);
        currentTab.tab.image.color = new Color(0.5f,0.5f,0.5f,(200.0f/255.0f));
        currentTab = menuContents[argument];
        currentTab.tab.image.color = Color.white;
        currentTab.tabContent.transform.gameObject.SetActive(true);
    }
}
