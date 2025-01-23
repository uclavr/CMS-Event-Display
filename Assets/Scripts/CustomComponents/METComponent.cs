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
        data += $"Phi: {phi}\n" + $"Pt: {pt}\n" + $"Px: {px}\n" + $"Py: {py}\n" + $"Pz: {pz}";
        return data;
    }

}
