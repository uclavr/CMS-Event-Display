using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scale_2 : MonoBehaviour
{
    public GameObject GeometryObjects;
    public GameObject Loader;
    public float zoom_speed = 0.01f;
    public float max_zoom = 10.0f;
    public float min_zoom = 0.2f;
    private Camera mainCamera;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickUp))
        {
            if (GeometryObjects.gameObject.transform.localScale.magnitude >= max_zoom) ;
            else GeometryObjects.gameObject.transform.localScale += new Vector3(zoom_speed, zoom_speed, zoom_speed);

            if (Loader.gameObject.transform.localScale.magnitude >= max_zoom) ;
            else Loader.gameObject.transform.localScale += new Vector3(zoom_speed, zoom_speed, zoom_speed);
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstickDown))
        {
            if (GeometryObjects.gameObject.transform.localScale.magnitude <= min_zoom) ;
            else GeometryObjects.gameObject.transform.localScale += new Vector3(-zoom_speed, -zoom_speed, -zoom_speed);

            if (Loader.gameObject.transform.localScale.magnitude <= min_zoom) ;
            else Loader.gameObject.transform.localScale += new Vector3(-zoom_speed, -zoom_speed, -zoom_speed);
        }
    }

}
