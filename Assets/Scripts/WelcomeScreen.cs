using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreen : MonoBehaviour
{
    public GameObject WelcomeScreenCanvas
       ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void LoadMainMenuMBtn()
    {
        SceneManager.LoadScene("Main menu");
    }
}
