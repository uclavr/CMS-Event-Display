using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSCSegmentComponent : MonoBehaviour
{
    public int detid;
    public double[] pos_1;
    public double[] pos_2;
    public int endcap;
    public int station;
    public int ring;
    public int chamber;
    public int layer;

    public string GetData()
    {
        return $"DetID: {detid}\n" +
               $"Pos 1: ({FormatVec(pos_1)})\n" +
               $"Pos 2: ({FormatVec(pos_2)})\n" +
               $"Endcap: {endcap}, Station: {station}, Ring: {ring}\n" +
               $"Chamber: {chamber}, Layer: {layer}";
    }

    private string FormatVec(double[] v)
    {
        return $"{Math.Round(v[0], 2)}, {Math.Round(v[1], 2)}, {Math.Round(v[2], 2)}";
    }
}
