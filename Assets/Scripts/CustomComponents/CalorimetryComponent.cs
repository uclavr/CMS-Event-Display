using System;
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

    public string GetData()
    {
        string data = "";
        data += $"Energy: {Math.Round(energy, 2)} GeV\n";
        data += $"Eta: {Math.Round(eta, 2)}\n";
        data += $"Phi: {Math.Round(phi < 0 ? phi + 2 * Math.PI : phi, 2)}\n";
        data += $"Time: {Math.Round(time, 2)} ns\n";
        data += $"DetID: {detid}\n";

        data += $"Front Vertices:\n";
        data += $"  1: ({FormatVec(front_1)})\n";
        data += $"  2: ({FormatVec(front_2)})\n";
        data += $"  3: ({FormatVec(front_3)})\n";
        data += $"  4: ({FormatVec(front_4)})\n";

        data += $"Back Vertices:\n";
        data += $"  1: ({FormatVec(back_1)})\n";
        data += $"  2: ({FormatVec(back_2)})\n";
        data += $"  3: ({FormatVec(back_3)})\n";
        data += $"  4: ({FormatVec(back_4)})";

        return data;
    }

    private string FormatVec(double[] v)
    {
        return $"{Math.Round(v[0], 2)}, {Math.Round(v[1], 2)}, {Math.Round(v[2], 2)}";
    }
}
