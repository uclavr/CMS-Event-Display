using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerMuonComponent : MonoBehaviour
{
    public int id;
    public double pt;
    public int charge;
    public double[] position;
    public double phi;
    public double eta;

    public int getID() { return id; }
    public double getPT() { return pt; }
    public double getEta() { return eta; }
    public double getCharge() { return charge; }
    public double getPhi()
    {
        if (phi < 0) return phi + 2 * Math.PI;
        return phi;
    }

    public string GetData()
    {
        string data = "";
        data += $"Pt: {pt}\n" + $"Charge: {charge}\n" + $"Position: ({position[0]}, {position[1]}, {position[2]})\n" + $"Phi: {phi}\n" + $"Eta: {eta}";
        return data;
    }
}
