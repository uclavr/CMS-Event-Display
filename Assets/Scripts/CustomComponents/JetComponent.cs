using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetComponent : MonoBehaviour
{
    public int id;
    public double et;
    public double eta;
    public double theta;
    public double phi;

    public int getID() { return id; }
    public double getET() { return et; }
    public double getEta() { return eta; }
    public double getTheta() { return theta; }
    public double getPhi()
    {
        if (phi < 0) return phi + 2 * Math.PI;
        return phi;
    }

    public string GetData()
    {
        string data = "";
        data += $"ET: {Math.Round(et, 2)} GeV\n" + $"Phi: {Math.Round(phi, 2)}\n" + $"Eta: {Math.Round(eta, 2)}\n" + $"Theta: {Math.Round(theta, 2)}\n";
        /*data.Add($"ET: {et}");
        data.Add($"Phi: {phi}");
        data.Add($"Eta: {eta}");
        data.Add($"Theta: {theta}");*/
        return data;
    }
}
