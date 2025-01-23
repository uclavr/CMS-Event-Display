using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.RawButton.LIndexTrigger))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); //replace the ray declaration to the controller ray

            if (Physics.Raycast(ray, out hit))
            { //insert command to make object highlighted
                if (hit.collider != null)
                {
                    hit.collider.enabled = false;
                }
            }
        }
    }
}
