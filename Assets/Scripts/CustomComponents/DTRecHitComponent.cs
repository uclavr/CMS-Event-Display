using System;
using UnityEngine;

public class DTRecHitComponent : MonoBehaviour
{
    public int wireId;
    public int layerId;
    public int superLayerId;
    public int sectorId;
    public int stationId;
    public int wheelId;
    public double digitime;
    public double[] wirePos;
    public double[] lPlusGlobalPos;
    public double[] lMinusGlobalPos;
    public double[] rPlusGlobalPos;
    public double[] rMinusGlobalPos;
    public double[] lGlobalPos;
    public double[] rGlobalPos;
    public double[] axis;
    public double angle;
    public double cellWidth;
    public double cellLength;
    public double cellHeight;

    public string GetData()
    {
        string FormatVec(double[] v) => $"{Math.Round(v[0], 2)}, {Math.Round(v[1], 2)}, {Math.Round(v[2], 2)}";

        return
            $"Wire ID: {wireId}\n" +
            $"Layer: {layerId}, SuperLayer: {superLayerId}, Sector: {sectorId}, Station: {stationId}, Wheel: {wheelId}\n" +
            $"Digitime: {digitime} ns\n" +
            $"Wire Position: ({FormatVec(wirePos)})\n" +
            $"L+ Pos: ({FormatVec(lPlusGlobalPos)})\n" +
            $"L- Pos: ({FormatVec(lMinusGlobalPos)})\n" +
            $"R+ Pos: ({FormatVec(rPlusGlobalPos)})\n" +
            $"R- Pos: ({FormatVec(rMinusGlobalPos)})\n" +
            $"L Pos: ({FormatVec(lGlobalPos)}), R Pos: ({FormatVec(rGlobalPos)})\n" +
            $"Axis: ({FormatVec(axis)}), Angle: {Math.Round(angle, 2)} rad\n" +
            $"Cell: Width = {cellWidth}, Length = {cellLength}, Height = {cellHeight}";
    }
}
