using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;

public class yurr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject obj = new OBJLoader().Load(@"C:\Users\Joseph\Desktop\Event_1096322990\jets.obj");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
