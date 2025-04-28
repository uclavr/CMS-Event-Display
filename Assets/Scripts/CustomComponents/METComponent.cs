using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class METComponent : MonoBehaviour
{
 
    public double pt;
    public double px;
    public double py;
    public double pz;
    public double phi;

    public string GetData()
    {
        string data = "";
        data += $"Phi: {Math.Round(phi, 2)}\n" + $"Pt: {Math.Round(pt, 2)} GeV\n" + $"Px: {Math.Round(px, 2)} GeV\n" + $"Py: {Math.Round(py, 2)} GeV\n" + $"Pz: {Math.Round(pz, 2)} GeV";
        return data;
    }

}
