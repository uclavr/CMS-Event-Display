using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    public GameObject cam;
    public Vector3 planeVector;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;
        planeVector = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z);
        transform.LookAt(planeVector);
    }
}
