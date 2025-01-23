using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PFJetV2Component : MonoBehaviour
{
    // Start is called before the first frame update
    public int id;
    public double et;
    public double eta;
    public double theta;
    public double phi;
    public double[] vertex;

    public int getID() { return id; }
    public double getET() { return et; }
    public double getTheta() { return theta; }
    public double getPhi() { return phi; }
    public double[] getVertex() { return vertex; }
    public double getEta() { return eta; }
    public string GetData() { return $"ET: {et}\n" + $"Phi: {phi}\n" + $"Eta: {eta}\n" + $"Theta: {theta}\n" + $"Vertex: {vertex}";}
}
