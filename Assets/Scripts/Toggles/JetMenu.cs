using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetMenu : MonoBehaviour
{
    public GameObject obj;
    private DisplayJets displayJetsScript;

    //void Start()
    //{
    //    displayJetsScript = OnOff.GetComponent<DisplayJets>();
    //    // displayJetsScript.enabled = false;
    //    OnOff.SetActive(false);
    //}
    
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            UnityEngine.Debug.Log("Does Trigger");
        }
    }
}
