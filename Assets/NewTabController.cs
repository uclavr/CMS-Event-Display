using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NewTabController : MonoBehaviour
{
    public GameObject CutCanvas;

    public GameObject SelectionCanvas;

    public GameObject VisibilityCanvas;

    public void PropertiesBtn()
    {
        CutCanvas.SetActive(false);
        SelectionCanvas.SetActive(true);
        VisibilityCanvas.SetActive(false);
    }

    public void CutsBtn()
    {
        CutCanvas.SetActive(true);
        SelectionCanvas.SetActive(false);
        VisibilityCanvas.SetActive(false);
    }

    public void VisibilityBtn()
    {
        CutCanvas.SetActive(false);
        SelectionCanvas.SetActive(false);
        VisibilityCanvas.SetActive(true);
    }
}
