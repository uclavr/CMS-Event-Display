using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using System.Collections.Specialized;

public class LegoPlotScript : MonoBehaviour
{
    public List<GameObject> sceneObjects;

    public List<GameObject> electronObjects;
    public List<GameObject> trackerMuonObjects;
    public List<GameObject> globalMuonObjects;
    public List<GameObject> trackObjects;
    public JObject jsonFile;
    public TextAsset jsonText;

    void Start()
    {

    }
    void Awake()
    {
        Material emMaterial = Resources.Load<Material>("EM Material");
        Material muonMaterial = Resources.Load<Material>("Muon Track Material");
        Material trackMaterial = Resources.Load<Material>("Track Material");
        Material mat;

        jsonFile = (JObject)JToken.Parse(jsonText.text);
        JToken jsonData;
        foreach (var obj in sceneObjects)
        {
            var subObjects = AllChilds(obj);
            switch (obj.name)
            {
                case "gsfElectrons":
                    mat = emMaterial;
                    jsonData = jsonFile["electronDatas"];
                    for (int i = 0; i < subObjects.Count; i += 2)
                    {
                        subObjects[i].gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
                        ElectronComponent component = subObjects[i].AddComponent<ElectronComponent>();
                        JToken jsonValues = jsonData[(i / 2)];
                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["pos"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        component.direction = jsonValues["dir"].ToObject<double[]>();
                        electronObjects.Add(subObjects[i]);
                    }
                    break;
                case "TrackerMuons":
                    mat = muonMaterial;
                    jsonData = jsonFile["trackerMuonDatas"];
                    for (int i = 0; i < subObjects.Count; i += 2)
                    {
                        subObjects[i].gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
                        TrackerMuonComponent component = subObjects[i].AddComponent<TrackerMuonComponent>();
                        JToken jsonValues = jsonData[(i / 2)];

                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["position"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        trackerMuonObjects.Add(subObjects[i]);
                    }
                    break;
                case "globalMuons":
                    mat = muonMaterial;
                    jsonData = jsonFile["globalMuonDatas"];

                    for (int i = 0; i < subObjects.Count; i += 2)
                    {
                        subObjects[i].gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
                        GlobalMuonComponent component = subObjects[i].AddComponent<GlobalMuonComponent>();
                        JToken jsonValues = jsonData[i / 2];

                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["position"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        component.caloEnergy = jsonValues["caloEnergy"].Value<double>();
                        globalMuonObjects.Add(subObjects[i]);
                    }
                    break;
                case "Tracks":
                    mat = trackMaterial;
                    jsonData = jsonFile["trackDatas"];
                    for (int i = 0; i < subObjects.Count; i += 2)
                    {
                        subObjects[i].gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().material = mat;
                        TrackComponent component = subObjects[i].AddComponent<TrackComponent>();
                        JToken json = jsonData[i / 2];
                        component.id = json["id"].Value<int>();
                        component.pos = json["pos"].ToObject<double[]>();
                        component.dir = json["dir"].ToObject<double[]>();
                        component.pt = json["pt"].Value<double>();
                        component.phi = json["phi"].Value<double>();
                        component.eta = json["eta"].Value<double>();
                        component.charge = json["charge"].Value<int>();
                        component.chi2 = json["chi2"].Value<double>();
                        component.ndof = json["ndof"].Value<double>();
                        trackObjects.Add(subObjects[i]);
                    }
                    break;
                default: mat = emMaterial; break;
            }
        }
    }
    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }
}