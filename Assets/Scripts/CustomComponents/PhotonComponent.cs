using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class PhotonComponent : MonoBehaviour
{
    public int id;
    public double energy;
    public double et;
    public double eta;
    public double phi;
    public Vector3 position;

    public int getID() { return id; }
    public double getEnergy() { return energy; }
    public double getET() { return et; }
    public double getEta() { return eta;}
    public double getPhi() { return phi; }
    public Vector3 getPosition() { return position; }  
    public string GetData() { return $"Energy: {energy}\nEta: {eta}\nPhi: {phi}\nPosition: {position}"; }
}
