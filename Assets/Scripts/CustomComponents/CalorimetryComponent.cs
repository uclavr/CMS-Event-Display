using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalorimetryComponent : MonoBehaviour
{
    public double energy;
    public double eta;
    public double phi;
    public double time;
    public int detid;
    public double[] front_1;
    public double[] front_2;
    public double[] front_3;
    public double[] front_4;
    public double[] back_1;
    public double[] back_2;
    public double[] back_3;
    public double[] back_4;
    public string GetData() { return ""; }
}
