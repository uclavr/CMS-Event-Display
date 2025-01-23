using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    public InputDeviceCharacteristics inputDeviceCharacteristics;

    private InputDevice targetDevice;
    // Start is called before the first frame update
    void Start()
    {
        InitializeHand();
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            InitializeHand();
        }
    }

    private void InitializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices);

        if(devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }
}
