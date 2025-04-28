using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scale_2 : MonoBehaviour
{
    public GameObject GeometryObjects;
    public GameObject Loader;
    public float zoom_speed = 0.02f;
    public float max_zoom = 40.0f;
    public float min_zoom = 1f; 
    void Start()
    {
        zoom_speed = 0.02f;
        min_zoom = 1f;
        max_zoom = 40.0f;
    }

    // Update is called once per frame
    void Update()
    {
        //OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            if (GeometryObjects.gameObject.transform.localScale.x >= max_zoom) ;
            else GeometryObjects.gameObject.transform.localScale += new Vector3(zoom_speed, zoom_speed, zoom_speed);

            if (Loader.gameObject.transform.localScale.x >= max_zoom) ;
            else Loader.gameObject.transform.localScale += new Vector3(zoom_speed, zoom_speed, zoom_speed);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            if (GeometryObjects.gameObject.transform.localScale.x <= min_zoom) ;
            else GeometryObjects.gameObject.transform.localScale += new Vector3(-zoom_speed, -zoom_speed, -zoom_speed);

            if (Loader.gameObject.transform.localScale.x <= min_zoom) ;
            else Loader.gameObject.transform.localScale += new Vector3(-zoom_speed, -zoom_speed, -zoom_speed);
        }
    }

}
