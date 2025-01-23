using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public GameObject obj;

    public GameObject button1;
    public GameObject button2;

    public RectTransform menu;
    public float speed;

    private float scrollSpeed;
    private float maxYPosition;
    private float minYPosition;

    private Behaviour button1Toggle;
    private Behaviour button2Toggle;

    void Awake()
    {
        scrollSpeed = speed;
        if(speed == 0.0f)
        {
            scrollSpeed = 1.0f;
        }

        maxYPosition = menu.position.y + menu.anchorMax.y;
        minYPosition = menu.position.y + menu.anchorMin.y;

        button1Toggle = button1.GetComponent<Toggle_Trigger> ();
        button2Toggle = button2.GetComponent<Toggle_Trigger> ();

    }

    void Start()
    {
        button1Toggle.enabled = true;
        button2Toggle.enabled = false;
    }
    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y != 0.0f)
        {
            if(OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y > 0.0f)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, button1.transform.position.y, obj.transform.position.z);

                button1Toggle.enabled = true;
                button2Toggle.enabled = false;
            }
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y < 0.0f)
            {
                obj.transform.position = new Vector3(obj.transform.position.x, button2.transform.position.y, obj.transform.position.z);

                button1Toggle.enabled = false;
                button2Toggle.enabled = true;
            }

            // float yIncrement = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick).y * (maxYPosition - minYPosition) / scrollSpeed;
            // moveScroll(yIncrement);

        }
    }

//    public void moveScroll(float yIncrement)
//    {
//        if((obj.transform.position.y + yIncrement) <= maxYPosition && (obj.transform.position.y + yIncrement) >= minYPosition)
//        {
//            obj.transform.Translate(Vector3.up * yIncrement);
//        }
//        else if(obj.transform.position.y + yIncrement > maxYPosition)
//        {
//            obj.transform.position = new Vector3 (obj.transform.position.x, maxYPosition, obj.transform.position.z);
//        }
//        else if(obj.transform.position.y + yIncrement < minYPosition)
//        {
//            obj.transform.position = new Vector3(obj.transform.position.x, minYPosition, obj.transform.position.z);
//        }
//        else { }
//    }
}