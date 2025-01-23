using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalMuonComponent : MonoBehaviour
{
    public int id;
    public double pt;
    public int charge;
    public double[] position;
    public double phi;
    public double eta;
    public double caloEnergy;

    public int getID() { return id; }
    public double getPT() { return pt; }
    public double getEta() { return eta; }
    public double getCharge() { return charge; }
    public double getPhi()
    {
        if (phi < 0) return phi + 2 * Math.PI;
        return phi;
    }
    public double getCaloEnergy() { return caloEnergy; }

    public string GetData()
    {
        string data = "";
        data += $"Pt: {pt}\n" + $"Phi: {phi}\n" + $"Eta: {eta}\n" + $"Charge: {charge}\n" + $"Position: ({position[0]}, {position[1]}, {position[2]})\n" + $"CaloEnergy: {caloEnergy}";
        return data;
    }
}
