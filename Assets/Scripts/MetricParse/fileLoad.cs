using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dummiesman;
using System;
using System.Linq;
using System.Runtime.Versioning;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Net;
using TMPro;

//using static Unity.VisualScripting.Metadata;

public class fileLoad : MonoBehaviour
{
    string objpath = @"C:\Users\uclav\Desktop\Event_1096322990\";
    string eventfolder = @"C:\Users\uclav\Downloads\IGDATA\obj_files\masterclass_11\Event_139707779";
    public List<GameObject> objectsLoaded;

    private int indexer;

    public List<GameObject> allObjects;

    public List<GameObject> superClusterObjects;
    public List<GameObject> trackerMuonObjects;
    public List<GameObject> globalMuonObjects;
    public List<GameObject> standaloneMuonObjects;
    public List<GameObject> electronObjects;
    public List<GameObject> trackObjects;
    public List<GameObject> rpcRecHitObjects;
    public List<GameObject> cscSegmentObjects;
    public List<GameObject> photonObjects;
    public List<GameObject> jetObjects;
    public List<GameObject> vertexObjects;

    public GameObject detectorCanvas;
    public GameObject locatingSphere;

    public GameObject ECALandMenu;

    public JObject totalJson;
    public JObject metJson;
    public JObject calorimetryJson;
    private string jsonpath;
    private string metpath;

    public GameObject LocatingSpherePrefab;
    public GameObject METPrefab;
    public GameObject ToggleForOBJHover;
    private bool meshColliderEnabled = true;
    private Transform parent;

    private void Start()
    {
        parent = GameObject.Find("AllObjects").GetComponent<Transform>();
        foreach (GameObject child in allObjects)
        {
            child.transform.SetParent(parent);
        }
    }
    void Awake()
    {
        //parent = GameObject.Find("AllObjects").GetComponent<Transform>();
        objectsLoaded.Add(ECALandMenu);
        Material muonMaterial = Resources.Load<Material>("Muon Track Material");
        Material mMaterial = Resources.Load<Material>("Chamber Material");
        Material vertexMaterial = Resources.Load<Material>("Vertex Material");
        Material secondaryVertexMaterial = Resources.Load<Material>("Secondary Vertex Material");
        Material hadronMaterial = Resources.Load<Material>("Hadronic Material");
        Material emMaterial = Resources.Load<Material>("EM Material");
        Material trackMaterial = Resources.Load<Material>("Track Material");
        Material jetMaterial = Resources.Load<Material>("Jet Material");
        Material tdMaterial = Resources.Load<Material>("TrackDet Material");
        Material cscMaterial = Resources.Load<Material>("CSC Segment Material");
        Material rpcMaterial = Resources.Load<Material>("RPCRecHit Material");
        Material pixelMaterial = Resources.Load<Material>("SiPixelCluster Material");
        Material stripMaterial = Resources.Load<Material>("SiStripCluster Material");
        Material dtrechitMaterial = Resources.Load<Material>("DTRecHit Material");
        Material dtrecsegmentMaterial = Resources.Load<Material>("DTRecSegment Material");
        Material cscrechitMaterial = Resources.Load<Material>("CSCRecHit Material");
        Material matchingcscsMaterial = Resources.Load<Material>("CSCs Material");
        Material trackingrechitsMaterial = Resources.Load<Material>("Tracking Rec Hits Material");
        Material calotowerMaterial = Resources.Load<Material>("Calo Tower Material");
        
        string[] paths = {"DTRecHits_V1.obj", "DTRecSegment4D_V1.obj","CSCRecHit2Ds_V2.obj","CaloTowers_V2", "MatchingCSCs_V1.obj","CSCSegments_V2.obj","PFJets.obj","PFJets_V2.obj","TrackDets_V1.obj","TrackingRecHits_V1.obj","SiPixelClusters_V1.obj","SiStripClusters_V1.obj","EBRecHits_V2.obj","EERecHits_V2.obj", "ESRecHits_V2.obj", "RPCRecHits_V1.obj","CSCSegments_V1.obj",
                "GsfElectrons_V1.obj","GsfElectrons_V2.obj","GsfElectrons_V3.obj","HBRecHits_V2.obj","HERecHits_V2.obj","HFRecHits_V2.obj",
                "HORecHits_V2.obj","MuonChambers_V1.obj","TrackerMuons_V1.obj","TrackerMuons_V2.obj","GlobalMuons_V1.obj", "GlobalMuons_V2.obj",
                "StandaloneMuons_V1.obj","StandaloneMuons_V2.obj","Photons_V1.obj","Tracks_V1.obj","Tracks_V2.obj","Tracks_V3.obj","Tracks_V4.obj","SuperClusters_V1.obj","Vertices_V1.obj","PrimaryVertices_V1.obj","SecondaryVertices_V1.obj"};

        if (UnityEngine.Application.isEditor == false)
        {
            objpath = @"/data/local/tmp/obj/";

            jsonpath = @"/data/local/tmp/obj/totaldata.json";
            metpath = @"/data/local/tmp/obj/METData.json";

            using (StreamReader file = File.OpenText(jsonpath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                totalJson = (JObject)JToken.ReadFrom(reader);
            }
            try
            {
                using (StreamReader file = File.OpenText(metpath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    metJson = (JObject)JToken.ReadFrom(reader);
                    if (metJson["phi"] == null)
                    {
                    }
                    else
                    {
                        Quaternion rotator = Quaternion.Euler(0, 0, (float)(metJson["phi"].Value<double>() * (180 / Math.PI)));
                        GameObject METObject = Instantiate(METPrefab, Vector3.zero, rotator);
                        METObject.name = "MET";
                        METObject.transform.GetChild(0).gameObject.name = "Missing ET";
                        METComponent METItem = METObject.AddComponent<METComponent>();
                        METItem.phi = metJson["phi"].Value<double>();
                        METItem.pt = metJson["pt"].Value<double>();
                        METItem.px = metJson["px"].Value<double>();
                        METItem.py = metJson["py"].Value<double>();
                        METItem.pz = metJson["pz"].Value<double>();
                        
                        objectsLoaded.Add(METObject);
                    }
                }
            }

            catch (Exception e) { }
                foreach (var path in paths)
                {
                try { 
                    indexer = 0;
                    //Check that the obj file actually contains any data
                    FileInfo f;
                    UnityEngine.Debug.Log("test");
                    GameObject loadedObject;
                    try
                    {
                        f = new FileInfo($@"{objpath}{path}");
                        if ((f.Length == 0) || f == null)
                        {
                            continue;
                        }

                        loadedObject = new OBJLoader().Load($"{objpath}{path}");
                    }
                    catch (Exception e) {
                        continue;
                    }

                    loadedObject.transform.localScale = Vector3.one;
                    string name = path.Split('.')[0];
                    loadedObject.name = name;
                    allObjects.Add(loadedObject);


                    GameObject child = loadedObject.transform.GetChild(0).gameObject;
                    List<GameObject> children = AllChilds(loadedObject);
                    foreach(var item in children)
                    {
                        item.AddComponent<MeshCollider>();
                        item.AddComponent<hoverOBJ>();
                        item.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
                        item.name = name;
                        print($"fileload{item.name}");
                    }
                    //Handle the materials based on the items
                    switch (path)
                    {
                        case "SecondaryVertices_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = secondaryVertexMaterial;
                                JToken json = totalJson["secondaryVertexDatas"][indexer];
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "PrimaryVertices_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = vertexMaterial;
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                JToken json = totalJson["primaryVertexDatas"][indexer];

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                GameObject locatingsphere = Instantiate(LocatingSpherePrefab, new Vector3((float)component.position[0], (float)(component.position[1]), (float)(component.position[2])), Quaternion.identity);

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "PFJets_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = jetMaterial;
                                JetComponent component = item.AddComponent<JetComponent>();

                                var jsonValues = totalJson["jetDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.et = jsonValues["et"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.theta = jsonValues["theta"].Value<double>();
                                component.phi = jsonValues["phi"].Value<double>();

                                indexer++;
                                jetObjects.Add(item);
                            }
                            break;
                        case "PFJets.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = jetMaterial;
                                JetComponent component = item.AddComponent<JetComponent>();

                                var jsonValues = totalJson["jetDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.et = jsonValues["et"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.theta = jsonValues["theta"].Value<double>();
                                component.phi = jsonValues["phi"].Value<double>();

                                indexer++;
                                jetObjects.Add(item);
                            }
                            break;
                        case "TrackerMuons_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                TrackerMuonComponent component = item.AddComponent<TrackerMuonComponent>();

                                var jsonValues = totalJson["trackerMuonDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();

                                indexer++;
                                trackerMuonObjects.Add(item);
                            }
                            break;
                        case "TrackerMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                TrackerMuonComponent component = item.AddComponent<TrackerMuonComponent>();

                                var jsonValues = totalJson["trackerMuonDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();

                                indexer++;
                                trackerMuonObjects.Add(item);
                            }
                            break;
                        case "GlobalMuons_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                GlobalMuonComponent component = item.AddComponent<GlobalMuonComponent>();

                                var jsonValues = totalJson["globalMuonDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.caloEnergy = jsonValues["caloEnergy"].Value<double>();

                                globalMuonObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "GlobalMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                GlobalMuonComponent component = item.AddComponent<GlobalMuonComponent>();

                                var jsonValues = totalJson["globalMuonDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.caloEnergy = jsonValues["caloEnergy"].Value<double>();

                                globalMuonObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "StandaloneMuons_V1.obj": case "StandaloneMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                StandaloneMuonComponent component = item.AddComponent<StandaloneMuonComponent>();

                                var jsonValues = totalJson["standaloneMuonDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.caloEnergy = jsonValues["caloEnergy"].Value<double>();

                                standaloneMuonObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "GsfElectrons_V1.obj":
                        case "GsfElectrons_V2.obj":
                        case "GsfElectrons_V3.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = emMaterial;
                                ElectronComponent component = item.AddComponent<ElectronComponent>();

                                JToken jsonValues = totalJson["electronDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["pos"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.direction = jsonValues["dir"].ToObject<double[]>();

                                electronObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "EERecHits_V2.obj":
                        case "EBRecHits_V2.obj":
                        case "ESRecHits_V2.obj":
                            child.GetComponent<MeshRenderer>().material = emMaterial; break;
                        case "HBRecHits_V2.obj":
                        case "HERecHits_V2.obj":
                        case "HORecHits_V2.obj":
                        case "HFRecHits_V2.obj":
                            child.GetComponent<MeshRenderer>().material = hadronMaterial; break;
                        case "MuonChambers_V1.obj":
                            /*int id = 0;
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = mMaterial;
                                Vector3 centroid;
                                Vector3 vertical;
                                Vector3 horizontal;
                                Quaternion rotation;
                                Quaternion rotation2;
                                float sign;
                                float verticalProjectionAngle;
                                float horizontalProjectionAngle;

                                JToken json = totalJson["muonChamberDatas"][id];

                                centroid = json["centroid"].ToObject<Vector3>();
                                vertical = json["vertical"].ToObject<Vector3>();
                                horizontal = json["horizontal"].ToObject<Vector3>();

                                if (Mathf.Acos((Vector3.Dot(horizontal.normalized, Vector3.forward))) >= 5.0f)
                                {
                                    rotation = Quaternion.Euler(Mathf.Acos(Vector3.Dot(vertical.normalized, Vector3.up)), 90, 0);
                                }
                                //if (Mathf.Acos((Vector3.))) { }
                            }*/
                            foreach (var item in children) { item.GetComponent<MeshRenderer>().material = mMaterial; }

                            break;
                        case "Photons_V1.obj":
                            child.GetComponent<MeshRenderer>().material = emMaterial; break;
                        case "RPCRecHits_V1.obj":
                            foreach(var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = trackMaterial;
                            }
                            indexer++;
                            break;
                        case "Tracks_V1.obj":
                        case "Tracks_V2.obj":
                        case "Tracks_V3.obj":
                        case "Tracks_V4.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = trackMaterial;
                                TrackComponent component = item.AddComponent<TrackComponent>();

                                JToken json = totalJson["trackDatas"][indexer];

                                component.id = json["id"].Value<int>();
                                component.pos = json["pos"].ToObject<double[]>();
                                component.dir = json["dir"].ToObject<double[]>();
                                component.pt = json["pt"].Value<double>();
                                component.phi = json["phi"].Value<double>();
                                component.eta = json["eta"].Value<double>();
                                component.charge = json["charge"].Value<int>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                trackObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "SuperClusters_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                SuperClusterComponent component = item.AddComponent<SuperClusterComponent>();

                                JToken json = totalJson["superClusters"][indexer];

                                component.id = json["id"].Value<int>();
                                component.energy = json["energy"].Value<double>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.eta = json["eta"].Value<double>();
                                component.phi = json["phi"].Value<double>();
                                component.algo = json["algo"].Value<string>();
                                component.etaWidth = json["etaWidth"].Value<double>();
                                component.phiWidth = json["phiWidth"].Value<double>();
                                component.rawEnergy = json["rawEnergy"].Value<double>();
                                component.preshowerEnergy = json["preshowerEnergy"].Value<double>();

                                superClusterObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "Vertices_V1.obj":
                            foreach (var item in children)
                            {
                                //item.GetComponent<MeshRenderer>().material =
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                JToken json = totalJson["vertexDatas"][indexer];

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "CSCSegments_V1.obj":
                        case "CSCSegments_V2.obj":
                            Color cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = cscMaterial;
                                item.GetComponent<MeshRenderer>().material.color = cscColor;
                                indexer++;
                            }
                            break;
                        case "TrackDets_V1.obj":
                            Color tdColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = tdMaterial;
                                item.GetComponent<MeshRenderer>().material.color = tdColor;
                                indexer++;
                            }
                            break;
                        case "SiPixelClusters_V1.obj":
                            Color spColor = new Color(255 / 255f, 102 / 255f, 0 / 255f); //#ff6600
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = pixelMaterial;
                                item.GetComponent<MeshRenderer>().material.color = spColor;
                                indexer++;
                            }
                            break;
                        case "SiStripClusters_V1.obj":
                            Color ssColor = new Color(204 / 255f, 51 / 255f, 0 / 255f); //#cc3300
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = stripMaterial;
                                item.GetComponent<MeshRenderer>().material.color = ssColor;
                                indexer++;
                            }
                            break;
                        case "DTRecHits_V1.obj":
                            Color dtrColor = new Color(0 / 255f, 255 / 255f, 0 / 255f); //#00ff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                                item.GetComponent<MeshRenderer>().material.color = dtrColor;
                                indexer++;
                            }
                            break;
                        case "DTRecSegment4D_V1.obj":
                            Color dtrsColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                                item.GetComponent<MeshRenderer>().material.color = dtrsColor;
                                indexer++;
                            }
                            break;
                        case "CSCRecHit2Ds_V2.obj":
                            Color cscrhColor = new Color(153 / 255f, 255 / 255f, 229 / 255f); //#99ffe5
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                                item.GetComponent<MeshRenderer>().material.color = cscrhColor;
                                indexer++;
                            }
                            break;
                        case "MatchingCSCs_V1.obj":
                            Color mcscsColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 25 / 255f); //#ff0000
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = matchingcscsMaterial;
                                item.GetComponent<MeshRenderer>().material.color = mcscsColor;
                                indexer++;
                            }
                            break;
                        case "TrackingRecHits_V1.obj":
                            Color trhColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = trackingrechitsMaterial;
                                item.GetComponent<MeshRenderer>().material.color = trhColor;
                                indexer++;
                            }
                            break;
                        case "CaloTowers_V2":
                            Color ctColor = new Color(255 / 255f, 255 / 255f, 255 / 255f); //#ffffff
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = calotowerMaterial;
                                item.GetComponent<MeshRenderer>().material.color = ctColor;
                                indexer++;
                            }
                            break;
                        default: break;
                    }
                    objectsLoaded.Add(loadedObject);
                } catch (Exception ex)
                {
                    continue;
                }
            }
        }
        //Code that runs in editor
        else
        {
           
            jsonpath = @"C:\Users\uclav\Downloads\IGDATA\json_data_files\totaldata(masterclass139707779).json";
            //metpath = @"C:\Users\uclav\Desktop\Event_1096322990\METData.json";
            //metpath = @"C:\Users\uclav\Desktop\Event_1096322990\METData.json";
            try
            {
                using (StreamReader file = File.OpenText(jsonpath))
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    totalJson = (JObject)JToken.ReadFrom(reader);
                }
            }
            catch (FileNotFoundException ex)
            {
                print($"Error: The file '{jsonpath}' was not found. Please check the path.");
                print(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                print($"Error: Access denied to the file '{jsonpath}'.");
                print(ex.Message);
            }
            catch (JsonReaderException ex)
            {
                print("Error: The file does not contain valid JSON.");
                print(ex.Message);
            }
            catch (Exception ex)
            {
                print("An unexpected error occurred.");
                print(ex.Message);
            }

            /*using (StreamReader file = File.OpenText(metpath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                metJson = (JObject)JToken.ReadFrom(reader);
                if (metJson["phi"] == null)
                {
                }
                else
                {
                    Quaternion rotator = Quaternion.Euler(0, 0, (float)(metJson["phi"].Value<double>() * (180 / Math.PI)));
                    GameObject METObject = Instantiate(METPrefab, Vector3.zero, rotator);
                    METObject.name = "MET";
                    METObject.transform.GetChild(0).gameObject.name = "Missing ET";
                    METComponent METItem = METObject.AddComponent<METComponent>();
                    METItem.phi = metJson["phi"].Value<double>();
                    METItem.pt = metJson["pt"].Value<double>();
                    METItem.px = metJson["px"].Value<double>();
                    METItem.py = metJson["py"].Value<double>();
                    METItem.pz = metJson["pz"].Value<double>();
                    objectsLoaded.Add(METObject);
                }
            }*/


            foreach (var path in paths) {
                try {
                    indexer = 0;

                    FileInfo f;
                    f = new FileInfo($@"C:\Users\uclav\Downloads\IGDATA\obj_files\masterclass_11\Event_139707779\{path}");
                    if (f.Length == 0)
                    {
                        continue;
                    }
                    var fs = new FileStream($@"C:\Users\uclav\Downloads\IGDATA\obj_files\masterclass_11\Event_139707779\{path}", FileMode.Open, FileAccess.Read);
                    GameObject loadedObject = new OBJLoader().Load(fs);

                    loadedObject.transform.localScale = Vector3.one;
                    string name = path.Split('.')[0];
                    loadedObject.name = name;
                    allObjects.Add(loadedObject);
                    //loadedObject.transform.SetParent(parent);

                    GameObject child = loadedObject.transform.GetChild(0).gameObject;
                    List<GameObject> children = AllChilds(loadedObject);
                    foreach(var item in children)
                    {           
                        item.AddComponent<MeshCollider>();
                        item.AddComponent<hoverOBJ>();
                        item.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

                    }
                    switch (path)
                    {
                        case "SecondaryVertices_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = secondaryVertexMaterial;
                                JToken json = totalJson["secondaryVertexDatas"][indexer];
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "PrimaryVertices_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = vertexMaterial;
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                JToken json = totalJson["primaryVertexDatas"][indexer];

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                GameObject locatingsphere = Instantiate(LocatingSpherePrefab, new Vector3((float)component.position[0], (float)(component.position[1]), (float)(component.position[2])), Quaternion.identity);

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "PFJets_V2.obj":
                        case "PFJets.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = jetMaterial;
                                JetComponent component = item.AddComponent<JetComponent>();

                                var jsonValues = totalJson["jetDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.et = jsonValues["et"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.theta = jsonValues["theta"].Value<double>();
                                component.phi = jsonValues["phi"].Value<double>();

                                indexer++;
                                jetObjects.Add(item);
                            }
                            break;
                        case "TrackerMuons_V1.obj":
                        case "TrackerMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                TrackerMuonComponent component = item.AddComponent<TrackerMuonComponent>();

                                var jsonValues = totalJson["trackerMuonDatas"][indexer];
                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();

                                indexer++;
                                trackerMuonObjects.Add(item);
                            }
                            break;
                        case "GlobalMuons_V1.obj":
                        case "GlobalMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                GlobalMuonComponent component = item.AddComponent<GlobalMuonComponent>();

                                var jsonValues = totalJson["globalMuonDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.caloEnergy = jsonValues["caloEnergy"].Value<double>();

                                globalMuonObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "StandaloneMuons_V1.obj":
                        case "StandaloneMuons_V2.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                StandaloneMuonComponent component = item.AddComponent<StandaloneMuonComponent>();

                                var jsonValues = totalJson["standaloneMuonDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["position"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.caloEnergy = jsonValues["caloEnergy"].Value<double>();

                                standaloneMuonObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "GsfElectrons_V1.obj":
                        case "GsfElectrons_V2.obj":
                        case "GsfElectrons_V3.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = emMaterial;
                                ElectronComponent component = item.AddComponent<ElectronComponent>();

                                JToken jsonValues = totalJson["electronDatas"][indexer];

                                component.id = jsonValues["id"].Value<int>();
                                component.pt = jsonValues["pt"].Value<double>();
                                component.charge = jsonValues["charge"].Value<int>();
                                component.position = jsonValues["pos"].ToObject<double[]>();
                                component.phi = jsonValues["phi"].Value<double>();
                                component.eta = jsonValues["eta"].Value<double>();
                                component.direction = jsonValues["dir"].ToObject<double[]>();

                                electronObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "EERecHits_V2.obj":
                        case "EBRecHits_V2.obj":
                        case "ESRecHits_V2.obj":
                            child.GetComponent<MeshRenderer>().material = emMaterial; break;
                        case "HBRecHits_V2.obj":
                        case "HERecHits_V2.obj":
                        case "HORecHits_V2.obj":
                        case "HFRecHits_V2.obj":
                            child.GetComponent<MeshRenderer>().material = hadronMaterial; break;
                        case "MuonChambers_V1.obj":
                            /*int id = 0;
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                Vector3 centroid;
                                Vector3 vertical;
                                Vector3 horizontal;
                                Quaternion rotation;
                                Quaternion rotation2;
                                float sign;
                                float verticalProjectionAngle;
                                float horizontalProjectionAngle;

                                JToken json = totalJson["muonChamberDatas"][id];

                                centroid = json["centroid"].ToObject<Vector3>();
                                vertical = json["vertical"].ToObject<Vector3>();
                                horizontal = json["horizontal"].ToObject<Vector3>();

                                if (Mathf.Acos((Vector3.Dot(horizontal.normalized, Vector3.forward))) >= 5.0f)
                                {
                                    rotation = Quaternion.Euler(Mathf.Acos(Vector3.Dot(vertical.normalized, Vector3.up)), 90, 0);
                                }
                                
                            }*/
                            foreach (var item in children) { item.GetComponent<MeshRenderer>().material = mMaterial; }

                            break;
                        case "Photons_V1.obj":
                            child.GetComponent<MeshRenderer>().material = emMaterial; break;
                        case "Tracks_V1.obj":
                        case "Tracks_V2.obj":
                        case "Tracks_V3.obj":
                        case "Tracks_V4.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = trackMaterial;
                                TrackComponent component = item.AddComponent<TrackComponent>();

                                JToken json = totalJson["trackDatas"][indexer];

                                component.id = json["id"].Value<int>();
                                component.pos = json["pos"].ToObject<double[]>();
                                component.dir = json["dir"].ToObject<double[]>();
                                component.pt = json["pt"].Value<double>();
                                component.phi = json["phi"].Value<double>();
                                component.eta = json["eta"].Value<double>();
                                component.charge = json["charge"].Value<int>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                trackObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "SuperClusters_V1.obj":
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = muonMaterial;
                                SuperClusterComponent component = item.AddComponent<SuperClusterComponent>();

                                JToken json = totalJson["superClusters"][indexer];

                                component.id = json["id"].Value<int>();
                                component.energy = json["energy"].Value<double>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.eta = json["eta"].Value<double>();
                                component.phi = json["phi"].Value<double>();
                                component.algo = json["algo"].Value<string>();
                                component.etaWidth = json["etaWidth"].Value<double>();
                                component.phiWidth = json["phiWidth"].Value<double>();
                                component.rawEnergy = json["rawEnergy"].Value<double>();
                                component.preshowerEnergy = json["preshowerEnergy"].Value<double>();

                                superClusterObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "Vertices_V1.obj":
                            foreach (var item in children)
                            {
                                //item.GetComponent<MeshRenderer>().material =
                                VertexComponent component = item.AddComponent<VertexComponent>();

                                JToken json = totalJson["vertexDatas"][indexer];

                                component.isValid = json["isValid"].Value<int>();
                                component.isFake = json["isFake"].Value<int>();
                                component.position = json["pos"].ToObject<double[]>();
                                component.xError = json["xError"].Value<double>();
                                component.yError = json["yError"].Value<double>();
                                component.zError = json["zError"].Value<double>();
                                component.chi2 = json["chi2"].Value<double>();
                                component.ndof = json["ndof"].Value<double>();

                                vertexObjects.Add(item);
                                indexer++;
                            }
                            break;
                        case "CSCSegments_V1.obj":
                        case "CSCSegments_V2.obj":
                            Color cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = cscMaterial;
                                item.GetComponent<MeshRenderer>().material.color = cscColor;
                                indexer++;
                            }
                            break;
                        case "TrackDets_V1.obj":
                            Color tdColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = tdMaterial;
                                item.GetComponent<MeshRenderer>().material.color = tdColor;
                                indexer++;
                            }
                            break;
                        case "SiPixelClusters_V1.obj":
                            Color spColor = new Color(255 / 255f, 102 / 255f, 0 / 255f); //#ff6600
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = pixelMaterial;
                                item.GetComponent<MeshRenderer>().material.color = spColor;
                                indexer++;
                            }
                            break;
                        case "SiStripClusters_V1.obj":
                            Color ssColor = new Color(204 / 255f, 51 / 255f, 0 / 255f); //#cc3300
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = stripMaterial;
                                item.GetComponent<MeshRenderer>().material.color = ssColor;
                                indexer++;
                            }
                            break;
                        case "DTRecHits_V1.obj":
                            Color dtrColor = new Color(0 / 255f, 255 / 255f, 0 / 255f); //#00ff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                                item.GetComponent<MeshRenderer>().material.color = dtrColor;
                                indexer++;
                            }
                            break;
                        case "DTRecSegment4D_V1.obj":
                            Color dtrsColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                                item.GetComponent<MeshRenderer>().material.color = dtrsColor;
                                indexer++;
                            }
                            break;
                        case "CSCRecHit2Ds_V2.obj":
                            Color cscrhColor = new Color(153 / 255f, 255 / 255f, 229 / 255f); //#99ffe5
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                                item.GetComponent<MeshRenderer>().material.color = cscrhColor;
                                indexer++;
                            }
                            break;
                        case "MatchingCSCs_V1.obj":
                            Color mcscsColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 25/255f); //#ff0000
                            foreach (var item in children)
                            {
                                item.GetComponent<MeshRenderer>().material = matchingcscsMaterial;
                                item.GetComponent<MeshRenderer>().material.color = mcscsColor;
                                indexer++;
                            }
                            break;
                        case "TrackingRecHits_V1.obj":
                            Color trhColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = trackingrechitsMaterial;
                                item.GetComponent<MeshRenderer>().material.color = trhColor;
                                indexer++;
                            }
                            break;
                        case "CaloTowers_V2":
                            Color ctColor = new Color(255 / 255f, 255 / 255f, 255 / 255f); //#ffffff
                            foreach (var item in children)
                            {
                                child.GetComponent<MeshRenderer>().material = calotowerMaterial;
                                item.GetComponent<MeshRenderer>().material.color = ctColor;
                                indexer++;
                            }
                            break;
                        default: break;
                    }
                    objectsLoaded.Add(loadedObject);
                } 
                
            catch (Exception ex) { }
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

    public void ToggleBtnOBJHover()
    {
        meshColliderEnabled = !meshColliderEnabled; // Flip the state

        foreach (var item in objectsLoaded)
        {
            foreach (Transform child in item.transform)
            {
                MeshCollider meshCollider = child.GetComponent<MeshCollider>();

                if (meshCollider != null) // If a MeshCollider exists
                {
                    meshCollider.enabled = meshColliderEnabled; // Enable/Disable MeshCollider based on the state
                }
            }
        }
    }

}