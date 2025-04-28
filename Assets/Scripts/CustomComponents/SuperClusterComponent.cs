using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperClusterComponent : MonoBehaviour
{
    public int id;
    public double energy;
    public double[] position;
    public double eta;
    public double phi;
    public string algo;
    public double etaWidth;
    public double phiWidth;
    public double rawEnergy;
    public double preshowerEnergy;

    public int getID() { return id; }
    public double getEnergy() { return energy; }
    public double getEta() { return eta; }
    public double getPhi()
    {
        if (phi < 0) return phi + 2 * Math.PI;
        return phi;
    }
    public double getEtaWidth() { return etaWidth; }
    public double getPhiWidth() {  return phiWidth; } // might need adjustment
    public double getRawEnergy() {  return rawEnergy; }
    public double getPreshowerEnergy() {  return preshowerEnergy; }

    public string GetData()
    {
        string data = "";
        data += $"Energy: {Math.Round(energy, 2)} GeV\n" + $"Position: ({Math.Round(position[0], 2)}, {Math.Round(position[1], 2)}, {Math.Round(position[2], 2)})\n" + $"Eta: {Math.Round(eta, 2)}\n" + $"Phi: {Math.Round(phi, 2)}\n" + $"Algo: {algo}\n" + $"EtaWidth: {Math.Round(etaWidth, 2)}\n" + $"PhiWidth: {Math.Round(phiWidth, 2)}\n" + $"RawEnergy: {Math.Round(rawEnergy, 2)} GeV\n" + $"PreshowerEnergy: {Math.Round(preshowerEnergy, 2)} GeV";

        return data;
    }
}
