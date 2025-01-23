using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System.IO;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class MuonElectronLoad : MonoBehaviour
{
    private string[] folderPaths = { "\\3_standaloneMuons", "\\1_trackerMuons", "\\2_globalMuons", "\\4_gsfElectrons" };
    private JObject totalJson;
    private string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)+"\\Event_1096322990";

    private delegate void AddToList(GameObject gameObject);
    private delegate GameObject GetJSON(string name,JObject json, GameObject gameObject, int index);

    void Start()
    {
        Material muonMaterial = Resources.Load<Material>("Muon Track Material");
        Material emMaterial = Resources.Load<Material>("EM Material");
        totalJson = GetComponent<fileLoad>().totalJson;

        int dataIndex;
        AddToList adder;
        GetJSON adderJSON;
        string objectName;

        foreach(string folder in folderPaths) 
        {
            dataIndex = 0;
            Material mat;
            DirectoryInfo dir = new DirectoryInfo(desktopPath+folder);
            switch (folder[1])
            {
                case '1':
                    mat = muonMaterial;
                    adder = GetComponent<fileLoad>().trackerMuonObjects.Add;
                    objectName = "trackerMuonDatas";
                    adderJSON = (name,json, gameObject,index) =>
                    {
                        var jsonValues = json[name][index];
                        gameObject.AddComponent<TrackerMuonComponent>();
                        TrackerMuonComponent component = gameObject.GetComponent<TrackerMuonComponent>();
                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["position"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        
                        return gameObject;
                    };
                    break;
                case '2':
                    mat = muonMaterial;
                    adder = GetComponent<fileLoad>().globalMuonObjects.Add;
                    objectName = "globalMuonDatas";
                    adderJSON = (name,json,gameObject,index) =>
                    {
                        var jsonValues = json[name][index];
                        gameObject.AddComponent<GlobalMuonComponent>();
                        GlobalMuonComponent component = gameObject.GetComponent<GlobalMuonComponent>();
                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["position"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        component.caloEnergy = jsonValues["caloEnergy"].Value<double>();
                        return gameObject;
                    };
                    break;
                case '3':
                    mat = muonMaterial;
                    adder = GetComponent<fileLoad>().standaloneMuonObjects.Add;
                    objectName = "standaloneMuonDatas";
                    adderJSON = (name,json, gameObject, index) =>
                    {
                        var jsonValues = json[name][index];
                        gameObject.AddComponent<StandaloneMuonComponent>();
                        StandaloneMuonComponent component = gameObject.GetComponent<StandaloneMuonComponent>();
                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["position"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        component.caloEnergy = jsonValues["caloEnergy"].Value<double>();
                        return gameObject;
                    };
                    break;
                case '4':
                    mat = emMaterial;
                    adder = GetComponent<fileLoad>().electronObjects.Add;
                    objectName = "electronData";
                    adderJSON = (name,json,gameObject, index) =>
                    {
                        JToken jsonValues = json[name][index];
                        gameObject.AddComponent<ElectronComponent>();
                        ElectronComponent component = gameObject.GetComponent<ElectronComponent>();
                        component.id = jsonValues["id"].Value<int>();
                        component.pt = jsonValues["pt"].Value<double>();
                        component.charge = jsonValues["charge"].Value<int>();
                        component.position = jsonValues["pos"].ToObject<double[]>();
                        component.phi = jsonValues["phi"].Value<double>();
                        component.eta = jsonValues["eta"].Value<double>();
                        component.direction = jsonValues["dir"].ToObject<double[]>();
                        return gameObject;
                    };
                    break;
                default:
                    mat = emMaterial;
                    adder = GetComponent<fileLoad>().globalMuonObjects.Add;
                    adderJSON = (name,json, gameObject, index) => { return gameObject; };
                    objectName = " ";
                    break;
            }
            foreach(var file in dir.GetFiles()) 
            { 
                GameObject obj = new OBJLoader().Load(file.FullName);
                GameObject child = obj.transform.GetChild(0).gameObject;
                child.GetComponent<MeshRenderer>().material = mat;
                adder(child);
                adderJSON(objectName,totalJson, child, dataIndex);
                dataIndex++;
            }
        }
    }
}