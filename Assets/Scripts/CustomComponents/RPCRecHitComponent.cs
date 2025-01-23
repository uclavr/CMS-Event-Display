using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPCRecHitComponent : MonoBehaviour
{
    // Start is called before the first frame update
    public double[] u1;
    public double[] u2;
    public double[] v1;
    public double[] v2;
    public double[] w1;
    public double[] w2;
    public int region;
    public int ring;
    public int sector;
    public int station;
    public int layer;
    public int subsector;
    public int roll;
    public int detid;

    public string GetData()
    {
        return $"Region: {region}\nRing: {ring}:\nSector: {sector}\nStation: {station}\nLayer: {layer}\nSubsector: {subsector}\nRoll: {roll}";
    }
}
