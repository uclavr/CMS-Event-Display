using Oculus.Platform;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    public GameObject welcomeText;
    public GameObject mainMenuCanvas;
    public GameObject defaultEventMenuCanvas;
    public GameObject controlsCanvas;
    public GameObject optionsCanvas;
    private static bool shownWelcome = false;

    private void Start()
    {
        //print("started");
        //if (PlayerPrefs.GetInt("FirstTime", 1) == 1) 
        //{
        //    print("Welcome Text Show");
        //    ShowWelcomeText();

        //    PlayerPrefs.SetInt("FirstTime", 0);
        //    PlayerPrefs.Save();
        //}
        //else
        //{
        //    print("show main menu");
        //    ShowMainMenuCanvas();
        //}
        ShowMainMenuCanvas();

    }

    public GameObject[] menuObjects;
    public void ShowWelcomeText()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == mainMenuCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
        shownWelcome = true;
        //mainMenuCanvas.SetActive(true);
        //defaultEventMenuCanvas.SetActive(false);
        //controlsCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
    }
    public void ShowMainMenuCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == mainMenuCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
        //mainMenuCanvas.SetActive(true);
        //defaultEventMenuCanvas.SetActive(false);
        //controlsCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
    }
    public void ShowDefaultEventMenuCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == defaultEventMenuCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
        //mainMenuCanvas.SetActive(false);
        //defaultEventMenuCanvas.SetActive(true);
        //controlsCanvas.SetActive(false);
        //optionsCanvas.SetActive(false);
    }
    public void ShowControlsCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == controlsCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
        //mainMenuCanvas.SetActive(false);
        //defaultEventMenuCanvas.SetActive(false);
        //controlsCanvas.SetActive(true);
        //optionsCanvas.SetActive(false);
    }
    public void ShowOptionsCanvas()
    {
        foreach (GameObject obj in menuObjects)
        {
            if (obj == optionsCanvas) obj.SetActive(true);
            else obj.SetActive(false);
        }
        //mainMenuCanvas.SetActive(false);
        //defaultEventMenuCanvas.SetActive(false);
        //controlsCanvas.SetActive(false);
        //optionsCanvas.SetActive(true);
    }
    public void Hto2E2MBtn()
    {
        print("uhhh scene on");//went through
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
    public void LambdaBtn()
    {
        print("uhhh scene on");//went through
        SceneManager.LoadScene("Lambda2Delta");
    }
    public void GluGluBtn()
    {
        print("uhhh scene on");//went through
        SceneManager.LoadScene("GG2H24B");
    }
    public void W2LNuBtn()
    {
        print("uhhh scene on");//went through
        SceneManager.LoadScene("W2LNu");
    }
}
