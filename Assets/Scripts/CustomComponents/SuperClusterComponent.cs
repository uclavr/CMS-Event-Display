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
        data += $"Energy: {energy}\n" + $"Position: ({position[0]}, {position[1]}, {position[2]})\n" + $"Eta: {eta}\n" + $"Phi: {phi}\n" + $"Algo: {algo}\n" + $"EtaWidth: {etaWidth}\n" + $"PhiWidth: {phiWidth}\n" + $"RawEnergy: {rawEnergy}\n" + $"PreshowerEnergy: {preshowerEnergy}";

        return data;
    }
}
