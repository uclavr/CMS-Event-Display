using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using UnityEngine;

public class Scale : MonoBehaviour
{
    public List<GameObject> objects;
    public float scaler;
    Vector3 scaleVector;
    Vector3 maxVector = new Vector3(10.0f, 10.0f, 10.0f);

    void Start()
    {
        var loader = GameObject.Find("Loader");
        objects = loader.GetComponent<fileLoad>().objectsLoaded;
        scaler = 0.2f;
    }

    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            if (scaleVector.magnitude >= 10.0f) { return; }
            else
            {
                foreach (var obj in objects)
                {
                    obj.transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
                    scaleVector = obj.transform.localScale;
                }
                //Event.gameObject.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
            }
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            if (scaleVector.x <= 0.2f) { return; }
            else
            {
                foreach (var obj in objects)
                {
                    obj.transform.localScale  += new Vector3(-0.01f, -0.01f, -0.01f);
                    scaleVector = obj.transform.localScale;
                }
                //Event.gameObject.transform.localScale += new Vector3(-0.01f, -0.01f, -0.01f);
            }
        }
        //Scale up/down up to 10 times
        /*if (OVRInput.Get(OVRInput.RawButton.RIndexTrigger) &&( scaleVector.x < 10.0f))
        {
            Vector3 scale = new Vector3(scaler, scaler, scaler);
            scaleVector = scaleVector + scale;
            foreach (var obj in objects)   
            {   
                obj.transform.localScale = scaleVector;
            }
        }else if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger) &&( scaleVector.x > 0.52f))
        {
            Vector3 scale1 = new Vector3(scaler, scaler, scaler);
            scaleVector = scaleVector - scale1;
            foreach (var obj in objects)
            {
                obj.transform.localScale = scaleVector;
            }
        }*/
    }
}
