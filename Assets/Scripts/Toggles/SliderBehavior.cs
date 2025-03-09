using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    private double energyMin;
    private double energyMax;


    void Start()
    {
        energySlider();
    }


    public void energySlider()
    {
        energyMin =100;
    }
}
