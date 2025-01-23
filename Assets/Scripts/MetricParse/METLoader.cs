using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System;
public class METLoader : MonoBehaviour
{
    private string metdata = @"/data/local/tmp/obj/METData.json";
    private bool METhasData;
    public GameObject METPrefab;
    public METData METItem;
    
    void SetMetDataState(bool state)
    {
        METhasData = state;
    }
    void SetMetActiveState(bool state)
    {
        METItem.activeState = state;
    }
    bool GetMetDataState()
    {
        return METhasData;
    }
    bool GetMetActiveState()
    {
        return METItem.activeState;
    }
    void Awake()
    {
        using (StreamReader file = File.OpenText(metdata))
        using (JsonTextReader reader = new JsonTextReader(file))
        {
            JObject metJson = (JObject)JToken.ReadFrom(reader);
            if (metJson["phi"] == null)
            {
                SetMetDataState(false);
            }
            else
            {
                SetMetDataState(true);
                METItem = new METData();
                METItem.phi = metJson["phi"].Value<double>();
                METItem.pt = metJson["pt"].Value<double>();
                METItem.px = metJson["px"].Value<double>();
                METItem.py = metJson["py"].Value<double>();
                METItem.pz = metJson["pz"].Value<double>();
                Quaternion rotator = Quaternion.Euler(0,0,(float)(METItem.phi*(180/Math.PI)));
                Instantiate(METPrefab, Vector3.zero, rotator);
            }
        }
    }
}
public struct METData
{
    public double phi;
    public double pt;
    public double px;
    public double py;
    public double pz;
    public bool activeState;
}
