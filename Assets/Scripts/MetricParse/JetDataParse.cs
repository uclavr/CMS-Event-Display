using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using System.IO;
public struct JetData
{
    private int m_num_param; // number of parameters that identify a JetData Object
                             // Does not count id as a parameter
    private int m_id;
    private double m_et;
    private double m_eta;
    private double m_theta;
    private double m_phi;

    public void setNumParam(int numParam) { m_num_param = numParam;}
    public readonly int getNumParam() { return m_num_param;}

    public void setID(int id) { m_id = id;}
    public readonly int getID() { return m_id;}

    public void setET(double et) { m_et = et;}
    public readonly double getET() { return m_et;}

    public void setEta(double eta) { m_eta = eta;}
    public readonly double getEta() { return m_eta;}

    public void setTheta(double theta) { m_theta = theta;}
    public readonly double getTheta() { return m_theta;}

    public void setPhi(double phi) {  m_phi = phi;}
    public readonly double getPhi() { return m_phi;}
}

public class JetDataParse : MonoBehaviour
{
    private string dataFilePath = @"C:\Users\uclav\Desktop\Event_1096322990\jetData.json";
    public List<JetData> jetDataList = new List<JetData>();
    public List<double> etData;


    void Awake()
    {
        var json = File.OpenText(dataFilePath);
        JsonTextReader reader = new JsonTextReader(json);
        JObject jetData = (JObject)JToken.ReadFrom(reader);

        foreach (JToken jetItem in jetData["jetData"][0])
        {
            var children = jetItem.Children().Values<double>().ToList();

            JetData jet = new JetData();
            jet.setID((int)children[0]);
            jet.setET(children[1]);
            jet.setEta(children[2]);
            jet.setTheta(children[3]);
            jet.setPhi(children[4]);
            jet.setNumParam(children.Count - 1);
            // UnityEngine.Debug.Log(jet.numParameters);
            jetDataList.Add(jet);
            //etData.Add(jet.et);
            // UnityEngine.Debug.Log(jet.phi);
        }
    }

}
