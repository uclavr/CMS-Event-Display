using System;
using UnityEngine;

public class CSCRecHitComponent : MonoBehaviour
{
    public double[] u1;
    public double[] u2;
    public double[] v1;
    public double[] v2;
    public double[] w1;
    public double[] w2;

    public int endcap;
    public int station;
    public int ring;
    public int chamber;
    public int layer;

    public double tpeak;
    public double positionWithinStrip;
    public double errorWithinStrip;

    public string strips;
    public string wireGroups;

    public string GetData()
    {
        return
            $"Endcap: {endcap}\n" +
            $"Station: {station}\n" +
            $"Ring: {ring}\n" +
            $"Chamber: {chamber}\n" +
            $"Layer: {layer}\n" +
            $"Tpeak: {Math.Round(tpeak, 2)} \n" +
            $"Position In Strip: {Math.Round(positionWithinStrip, 3)} ± {Math.Round(errorWithinStrip, 3)}\n" +
            $"Strips: {strips}\n" +
            $"Wire Groups: {wireGroups}\n" +
            $"U1, U2: ({FormatVec(u1)}) → ({FormatVec(u2)})\n" +
            $"V1, V2: ({FormatVec(v1)}) → ({FormatVec(v2)})\n" +
            $"W1, W2: ({FormatVec(w1)}) → ({FormatVec(w2)})";
    }

    private string FormatVec(double[] v)
    {
        return $"{Math.Round(v[0], 2)}, {Math.Round(v[1], 2)}, {Math.Round(v[2], 2)}";
    }
}
