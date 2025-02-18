using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class General : MonoBehaviour

{
    public GameObject CutCanvas;
    public GameObject Event;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            SceneManager.LoadScene("Main menu");
        }
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            bool currentState = CutCanvas.activeSelf;

            //Flip it
            currentState = !currentState;

            //Set the current State to the flipped value
            CutCanvas.SetActive(currentState);
        }
    }
}
