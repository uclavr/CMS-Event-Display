using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexComponent : MonoBehaviour
{
    public int isValid;
    public int isFake;
    public double[] position;
    public double xError;
    public double yError;
    public double zError;
    public double chi2;
    public double ndof;

    public string GetData()
    {
        string data = "";
        data += $"Valid: {isValid}\n";
        data += $"Fake: {isFake}\n";
        data += $"Position: ({Math.Round(position[0], 2)}, {Math.Round(position[1], 2)}, {Math.Round(position[2], 2)})\n";
        data += $"Errors: (x: {Math.Round(xError, 2)}, y: {Math.Round(yError, 2)}, z: {Math.Round(zError, 2)})\n";
        data += $"Chi^2: {Math.Round(chi2, 2)}\n";
        data += $"Ndof: {Math.Round(ndof, 2)}";
        return data;
    }
}
