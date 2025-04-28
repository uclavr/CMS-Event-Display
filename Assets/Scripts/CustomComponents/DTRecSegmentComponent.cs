using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DTRecSegmentComponent : MonoBehaviour
{
    public int detid;
    public double[] pos_1;
    public double[] pos_2;
    public int sectorId;
    public int stationId;
    public int wheelId;

    public string GetData()
    {
        return
            $"DetID: {detid}\n" +
            $"Sector ID: {sectorId}\n" +
            $"Station ID: {stationId}\n" +
            $"Wheel ID: {wheelId}\n" +
            $"Pos 1: ({FormatVec(pos_1)})\n" +
            $"Pos 2: ({FormatVec(pos_2)})";
    }

    private string FormatVec(double[] v)
    {
        return $"{Math.Round(v[0], 2)}, {Math.Round(v[1], 2)}, {Math.Round(v[2], 2)}";
    }
}
