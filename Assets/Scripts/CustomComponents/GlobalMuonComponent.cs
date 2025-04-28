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
        data += $"Pt: {Math.Round(pt, 2)} GeV\n" + $"Phi: {Math.Round(phi, 2)}\n" + $"Eta: {Math.Round(eta, 2)}\n" + $"Charge: {charge}\n" + $"Position: ({Math.Round(position[0], 2)}, {Math.Round(position[1], 2)}, {Math.Round(position[2], 2)})\n" + $"CaloEnergy: {Math.Round(caloEnergy, 2)} GeV";
        return data;
    }
}
