using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Toggle_Trigger : MonoBehaviour
{
    public GameObject obj;
    public float Time_Delay;
    private float timedelay;

    void Start()
    {
        timedelay = Time_Delay;
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        timedelay -= Time.deltaTime;
       if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && timedelay <=0 )
        {

            //Debug.Log("Does Trigger");
            obj.SetActive(!obj.activeSelf);
            timedelay = Time_Delay;
        }
    }

}
