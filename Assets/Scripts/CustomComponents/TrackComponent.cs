using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackComponent : MonoBehaviour
{
    public int id;
    public double[] pos;
    public double[] dir;
    public double pt;
    public double phi;
    public double eta;
    public int charge;
    public double chi2;
    public double ndof;

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
        data += $"Pt: {Math.Round(pt, 2)} GeV\n" + $"Phi: {Math.Round(phi, 2)}\n" + $"Eta: {Math.Round(eta, 2)}\n" + $"Charge: {charge}\n" + $"Chi 2: {Math.Round(chi2, 2)}\n" + $"NDoF: {Math.Round(ndof, 2)}";
        //$"Position: ({position[0]}, {position[1]}, {position[2]})\n" + $"Direction: ({direction[0]}, {direction[1]}, {direction[2]})"
        return data;
    }
}