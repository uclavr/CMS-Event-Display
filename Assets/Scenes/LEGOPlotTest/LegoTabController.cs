using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LegoTabController : MonoBehaviour
{
    public GameObject MenuCanvas;
    //public GameObject CutCanvas; // deprecated
    public GameObject SelectionMenu;
    public GameObject FilterMenu;
    public GameObject OtherMenu;
    public GameObject ControlsMenu;
    public GameObject LegoMenu;
    public List<GameObject> ChildTabs; // lego tab is a special tab, make sure to get the number right. Currently 5th in the list index 4
    public GameObject eventDisp;
    public GameObject cubeboi;

    private int lastActive = 0; // Tracks the last active canvas (0 = Selection, 1 = Filter, 2 = Other)

    // Toggle the MenuCanvas visibility and manage the last active canvas
    public void MenuBtn()
    {
        if (MenuCanvas.activeSelf)
        {
            // Deactivate MenuCanvas and hide all other canvases
            MenuCanvas.SetActive(false);
            HideAllCanvases();
            HideAllTabs();
        }
        else
        {
            // Activate MenuCanvas and show the last active canvas
            MenuCanvas.SetActive(true);
            if (lastActive == 4)
            {
                ChildTabs[4].SetActive(true);
                //MenuCanvas.SetActive(false);
            }
            else
            {
                ShowAllTabs();
            }
            ShowCanvasBasedOnLastActive();
        }
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem
    }

    // Properties button - show the Selection canvas
    public void PropertiesBtn()
    {
        ShowCanvas(0);
    }

    /* deprecated
    // Cuts button - show the Cut canvas
    public void CutsBtn()
    {
        ShowCanvas(1);
    }
    */

    // Visibility button - show the Visibility canvas
    public void FilterBtn()
    {
        ShowCanvas(1);
    }

    // Other button - show the Other menu
    public void OtherBtn()
    {
        ShowCanvas(2);
    }

    public void ControlsBtn()
    {
        ShowCanvas(3);
    }

    // Switch to LEGO plot
    public void SwitchToLEGO()
    {
        if (eventDisp != null)
        {
            if (eventDisp.activeSelf)
            {
                eventDisp.SetActive(false);
                cubeboi.SetActive(true);

                // Change the menus
                HideAllCanvases();
                HideAllTabs();
                MenuCanvas.SetActive(false);
                lastActive = 4;
            }
            else
            {
                cubeboi.SetActive(false);
                eventDisp.SetActive(true);

                // Change the menus
                HideAllCanvases();
                HideAllTabs();
                MenuCanvas.SetActive(false);
                lastActive = 0;
            }
        }
        EventSystem.current.SetSelectedGameObject(null); //fix for the weird button remaining selected problem 
    }

    // FUNCTION DEFINITIONS

    // Hide all tabs
    private void HideAllTabs()
    {
        foreach (GameObject tab in ChildTabs)
        {
            tab.SetActive(false);
        }
    }

    // Show all tabs
    private void ShowAllTabs()
    {
        foreach (GameObject tab in ChildTabs)
        {
            if (tab == ChildTabs[4])
            {
                break;
            }
            tab.SetActive(true);
        }
    }

    // Hide all canvases
    private void HideAllCanvases() //MENUS ARE NOT CANVASES, JUST GAME OBJECTS WITH TEXT
    {
        //SelectionCanvas.SetActive(false);
        SelectionMenu.SetActive(false);
        //CutCanvas.SetActive(false);    //deprecated
        FilterMenu.SetActive(false);
        OtherMenu.SetActive(false);
        ControlsMenu.SetActive(false);
        LegoMenu.SetActive(false);
    }

    // Set the active canvas and update lastActive
    private void ShowCanvas(int canvasIndex)
    {
        HideAllCanvases();  // Hide all canvases first

        switch (canvasIndex)
        {
            case 0:
                SelectionMenu.SetActive(true);
                break;
            /* deprecated
            case 1:
                CutCanvas.SetActive(true);
                break;
            */
            case 1:
                FilterMenu.SetActive(true);
                break;
            case 2:
                OtherMenu.SetActive(true);
                break;
            case 3:
                ControlsMenu.SetActive(true);
                break;
            case 4: // special lego case
                LegoMenu.SetActive(true);
                break;
        }

        lastActive = canvasIndex;  // Update lastActive to the selected canvas
    }

    // Show the last active canvas
    private void ShowCanvasBasedOnLastActive()
    {
        switch (lastActive)
        {
            case 0:
                //SelectionCanvas.SetActive(true);
                SelectionMenu.SetActive(true);
                break;
            /*
            case 1:
                CutCanvas.SetActive(true);
                break;
            */
            case 1:
                FilterMenu.SetActive(true);
                break;
            case 2:
                OtherMenu.SetActive(true);
                break;
            case 3:
                ControlsMenu.SetActive(true);
                break;
            case 4: // special lego case
                LegoMenu.SetActive(true);
                break;
            default:
                SelectionMenu.SetActive(true);  // Default to Selection Menu
                break;
        }
    }
}