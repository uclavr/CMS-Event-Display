using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject defaultEventMenuCanvas;
    public GameObject controlsCanvas;
    public GameObject optionsCanvas;

    private void Start()
    {
        ShowMainMenuCanvas();
    }

    public GameObject[] menuObjects;

    public void ShowMainMenuCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == mainMenuCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
    }
    public void ShowDefaultEventMenuCanvas()
    {
        print("pressed default");
        foreach (GameObject obj in menuObjects)
        {
            if (obj == defaultEventMenuCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
    }
    public void ShowControlsCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == controlsCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
    }
    public void ShowOptionsCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == optionsCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }

    }
    public void Hto2E2MBtn()
    {
        SceneManager.LoadScene("2E2Mtest");
    }
    public void Hto4MBtn()
    {
        SceneManager.LoadScene("4M");
    }
    public void METBtn()
    {
        SceneManager.LoadScene("MET");
    }
    public void MinBiasBtn()
    {
        SceneManager.LoadScene("Minimum Bias");
    }
    public void BJetBtn()
    {
        SceneManager.LoadScene("B jet plus mu");
    }
    public void CustomBtn()
    {
        SceneManager.LoadScene("LoadingScene");

    }
    public void CustomTestBtn()
    {
        SceneManager.LoadScene("LoadingSceneTest");

    }
    public void LambdaBtn()
    {
        SceneManager.LoadScene("Lambda2Delta");
    }
    public void GluGluBtn()
    {
        SceneManager.LoadScene("GG2H24B");
    }
    public void W2LNuBtn()
    {
        SceneManager.LoadScene("W2LNu");
    }
    public void GFusionHiggsBtn()
    {
        SceneManager.LoadScene("GFusionHiggs");
    }
    public void QuarkPPHiggsBtn()
    {
        SceneManager.LoadScene("QuarkPP");
    }
}
