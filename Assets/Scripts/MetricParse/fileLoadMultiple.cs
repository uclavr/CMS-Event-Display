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
using System.IO;
using System.Text.RegularExpressions;

//using static Unity.VisualScripting.Metadata;

public class fileLoadMultiple : MonoBehaviour
{
    //new multi-event stuff
    public int currentEvent = 0;
    public int subItemIndex = 0;
    public HashSet<string> dataTypesWithSubitems = new HashSet<string>
    {
        "SecondaryVertices_V1",
        "PrimaryVertices_V1",
        "Vertices_V1",
        "PFJets_V2",
        "PFJets",
        "TrackerMuons_V1",
        "TrackerMuons_V2",
        "GlobalMuons_V1",
        "GlobalMuons_V2",
        "StandaloneMuons_V1",
        "StandaloneMuons_V2",
        "GsfElectrons_V1",
        "GsfElectrons_V2",
        "GsfElectrons_V3",
        "Tracks_V1",
        "Tracks_V2",
        "Tracks_V3",
        "Tracks_V4",
        "CSCSegments_V2",
        "DTRecSegment4D_V1",
        "DTRecHits_V1",
        "CSCRecHit2Ds_V2"
    };

    public List<GameObject> subObjects = new List<GameObject>();
    public GameObject currentObjectSub;
    public GameObject currentSubItemPopup = null;
    public string currentSubItemType = "";
    private Material currentSubItemMaterial;

    public Dictionary<string, bool> activeMap = new Dictionary<string, bool>
    {
        { "Photons_V1", true },
        { "Vertices_V1", true },
        { "SecondaryVertices_V1", true },
        { "PrimaryVertices_V1", true },
        { "PFJets_V2", true },
        { "PFJets", true },
        { "TrackerMuons_V1", true },
        { "TrackerMuons_V2", true },
        { "GlobalMuons_V1", true },
        { "GlobalMuons_V2", true },
        { "StandaloneMuons_V1", true },
        { "StandaloneMuons_V2", true },
        { "GsfElectrons_V1", true },
        { "GsfElectrons_V2", true },
        { "GsfElectrons_V3", true },
        { "EERecHits_V2", true },
        { "EBRecHits_V2", true },
        { "ESRecHits_V2", true },
        { "HBRecHits_V2", true },
        { "HERecHits_V2", true },
        { "HORecHits_V2", true },
        { "HFRecHits_V2", true },
        { "MuonChambers_V1", true },
        { "RPCRecHits_V1", true },
        { "Tracks_V1", true },
        { "Tracks_V2", true },
        { "Tracks_V3", true },
        { "Tracks_V4", true },
        { "SuperClusters_V1", true },
        { "CSCSegments_V1", true },
        { "CSCSegments_V2", true },
        { "TrackDets_V1", true },
        { "SiPixelClusters_V1", true },
        { "SiStripClusters_V1", true },
        { "DTRecHits_V1", true },
        { "DTRecSegment4D_V1", true },
        { "CSCRecHit2Ds_V2", true },
        { "MatchingCSCs_V1", true },
        { "TrackingRecHits_V1", true },
        { "CaloTowers_V2", true }
    };
    public HashSet<string> dataTypes = new HashSet<string>
    {
        "CaloTowers_V2",
        "CSCRecHit2Ds_V2",
        "CSCSegments_V1",
        "CSCSegments_V2",
        "DTRecHits_V1",
        "DTRecSegment4D_V1",
        "EBRecHits_V2",
        "EERecHits_V2",
        "ESRecHits_V2",
        "GlobalMuons_V1",
        "GlobalMuons_V2",
        "GsfElectrons_V1",
        "GsfElectrons_V2",
        "GsfElectrons_V3",
        "HBRecHits_V2",
        "HERecHits_V2",
        "HFRecHits_V2",
        "HORecHits_V2",
        "MatchingCSCs_V1",
        "MuonChambers_V1",
        "PFJets",
        "PFJets_V2",
        "Photons_V1",
        "PrimaryVertices_V1",
        "RPCRecHits_V1",
        "SecondaryVertices_V1",
        "SiPixelClusters_V1",
        "SiStripClusters_V1",
        "StandaloneMuons_V1",
        "StandaloneMuons_V2",
        "SuperClusters_V1",
        "TrackDets_V1",
        "TrackerMuons_V1",
        "TrackerMuons_V2",
        "TrackingRecHits_V1",
        "Tracks_V1",
        "Tracks_V2",
        "Tracks_V3",
        "Tracks_V4",
        "Vertices_V1"
    };

    string objpath;
    public List<GameObject> objectsLoaded;
    public string[] eventPaths;
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
    public List<GameObject> dtrecSegmentObjects;
    public List<GameObject> dtRecHitObjects;
    public List<GameObject> cscRecHitObjects;

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

    public GameObject scrollViewPrefab;
    public GameObject selectionMenu;

    private GameObject currentScrollViewData;

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
        //objectsLoaded.Add(ECALandMenu); // why does this exist??
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
            //objpath = @"/data/local/tmp/obj/";
            objpath = Directory.GetDirectories(@"/data/local/tmp/obj/")[0];
            eventPaths = Directory.GetDirectories(objpath);
            Array.Sort(eventPaths, (a, b) =>
            {
                int numA = ExtractNumber(a);
                int numB = ExtractNumber(b);
                return numA.CompareTo(numB);
            });

            //move editor to headset stuff for testing 
            GameObject scrollViewGO = Instantiate(scrollViewPrefab, selectionMenu.transform);
            RectTransform scrollRect = scrollViewGO.GetComponent<RectTransform>();
            scrollRect.sizeDelta = new Vector2(scrollRect.sizeDelta.x, 200);
            Transform eventContent = scrollViewGO.transform.Find("Viewport/Content");
            RectTransform eventContentRect = eventContent.GetComponent<RectTransform>();
            float eventButtonSpacingY = 51f;
            float eventContentHeight = 552;//eventPaths.Length * eventButtonSpacingY;
            eventContentRect.sizeDelta = new Vector2(eventContentRect.sizeDelta.x, eventContentHeight);

            Transform content = scrollViewGO.transform.Find("Viewport/Content/Organizer");
            float eventButtonYpos = 223; //scrollview
            int eventIterator = 0;



            foreach (string eventPath in eventPaths)
            {
                string eventName = Path.GetFileName(eventPath);
                GameObject eventObject = new GameObject(eventName);
                allObjects.Add(eventObject);
                //jsonpath = $@"{eventPath}\\totaldata.json"; look into this... BUG?
                jsonpath = $@"{eventPath}/totaldata.json";
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


                //editor to headset 
                if (eventIterator > 0)
                {
                    eventObject.SetActive(false);
                }

                GameObject eventButton = new GameObject(Path.GetFileName(eventPath) + "Button", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button));
                UnityEngine.UI.Image eventButtonImage = eventButton.GetComponent<UnityEngine.UI.Image>();
                if (eventIterator == 0)
                {
                    eventButtonImage.color = new Color(0.290f, 0.816f, 1f, 1f);
                }
                else
                {
                    eventButtonImage.color = new Color(0.745f, 0.745f, 0.745f, 1f);
                }
                eventButton.transform.SetParent(content, false);
                UnityEngine.UI.Outline outline = eventButton.AddComponent<UnityEngine.UI.Outline>();
                outline.effectColor = Color.black;
                RectTransform eventButtonRect = eventButton.GetComponent<RectTransform>();
                eventButtonRect.sizeDelta = new Vector2(680, 50);
                eventButtonRect.anchoredPosition = new Vector2(0, eventButtonYpos - eventIterator * eventButtonSpacingY);
                eventIterator += 1;
                eventButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    for (int i = 0; i < allObjects.Count; i++)
                    {
                        GameObject child = allObjects[i];
                        UnityEngine.UI.Image eventButtonImage = content.GetChild(i).GetComponent<UnityEngine.UI.Image>();  // Accounting for Lambda closure issue
                        if (child == eventObject)
                        {
                            eventButtonImage.color = new Color(0.290f, 0.816f, 1f, 1f);
                            child.SetActive(true);
                            currentEvent = i; //sets current event to the index... indexed by allObjects 
                        }
                        else
                        {
                            eventButtonImage.color = new Color(0.745f, 0.745f, 0.745f, 1f);
                            child.SetActive(false);
                        }
                    }
                    try
                    {
                        currentObjectSub = null;
                        currentSubItemType = "";
                        subItemIndex = 0;
                        subObjects.Clear();
                        if (currentSubItemPopup != null)
                        {
                            Destroy(currentSubItemPopup);
                            currentSubItemPopup = null;
                        }
                        makeDataButtons();
                    }
                    catch (Exception ex)
                    {
                        print(ex.Message);
                    }
                    ResetStaticVariables reset = new ResetStaticVariables();
                    reset.ResetStatics();
                });

                GameObject textObj = new GameObject("Text (TMP)", typeof(RectTransform), typeof(TextMeshProUGUI));
                textObj.transform.SetParent(eventButton.transform, false);
                RectTransform textObjRect = textObj.GetComponent<RectTransform>();
                textObjRect.sizeDelta = new Vector2(600, 50);
                TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
                //tmp.text = Path.GetFileName(eventPath);
                tmp.text = eventPath.Substring(eventPath.IndexOf("obj_files\\") + "obj_files\\".Length); ;
                tmp.alignment = TextAlignmentOptions.Left;
                tmp.fontSize = 32;
                tmp.color = Color.white;
                tmp.raycastTarget = false;

                foreach (var path in paths)
                {
                    try
                    {
                        indexer = 0;
                        //Check that the obj file actually contains any data
                        FileInfo f;
                        GameObject loadedObject;
                        try
                        {
                            f = new FileInfo($@"{eventPath}{path}");
                            if ((f.Length == 0) || f == null)
                            {
                                continue;
                            }

                            loadedObject = new OBJLoader().Load($"{eventPath}{path}");
                        }
                        catch (Exception e)
                        {
                            continue;
                        }

                        loadedObject.transform.localScale = Vector3.one;
                        string name = path.Split('.')[0];
                        loadedObject.name = name;
                        loadedObject.transform.SetParent(eventObject.transform);

                        GameObject child = loadedObject.transform.GetChild(0).gameObject;
                        List<GameObject> children = AllChilds(loadedObject);
                        foreach (var item in children)
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
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = rpcMaterial;
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
                                Color cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                                foreach (var item in children)
                                {
                                    child.GetComponent<MeshRenderer>().material = cscMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscColor;
                                    indexer++;
                                }
                                break;
                            case "CSCSegments_V2.obj":
                                cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                                foreach (var item in children)
                                {
                                    child.GetComponent<MeshRenderer>().material = cscMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscColor;
                                    CSCSegmentComponent component = item.AddComponent<CSCSegmentComponent>();

                                    JToken json = totalJson["CSCSegments_V2"][indexer];

                                    component.detid = json["detid"].Value<int>();
                                    component.pos_1 = json["pos_1"].ToObject<double[]>();
                                    component.pos_2 = json["pos_2"].ToObject<double[]>();
                                    component.endcap = json["endcap"].Value<int>();
                                    component.station = json["station"].Value<int>();
                                    component.ring = json["ring"].Value<int>();
                                    component.chamber = json["chamber"].Value<int>();
                                    component.layer = json["layer"].Value<int>();

                                    cscSegmentObjects.Add(item);
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
                                    item.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = dtrColor;

                                    DTRecHitComponent component = item.AddComponent<DTRecHitComponent>();
                                    var json = totalJson["DTRecHits_V1Data"][indexer];

                                    component.wireId = json["wireId"].Value<int>();
                                    component.layerId = json["layerId"].Value<int>();
                                    component.superLayerId = json["superLayerId"].Value<int>();
                                    component.sectorId = json["sectorId"].Value<int>();
                                    component.stationId = json["stationId"].Value<int>();
                                    component.wheelId = json["wheelId"].Value<int>();
                                    component.digitime = json["digitime"].Value<double>();

                                    component.wirePos = json["wirePos"].ToObject<double[]>();
                                    component.lPlusGlobalPos = json["lPlusGlobalPos"].ToObject<double[]>();
                                    component.lMinusGlobalPos = json["lMinusGlobalPos"].ToObject<double[]>();
                                    component.rPlusGlobalPos = json["rPlusGlobalPos"].ToObject<double[]>();
                                    component.rMinusGlobalPos = json["rMinusGlobalPos"].ToObject<double[]>();
                                    component.lGlobalPos = json["lGlobalPos"].ToObject<double[]>();
                                    component.rGlobalPos = json["rGlobalPos"].ToObject<double[]>();
                                    component.axis = json["axis"].ToObject<double[]>();
                                    component.angle = json["angle"].Value<double>();
                                    component.cellWidth = json["cellWidth"].Value<double>();
                                    component.cellLength = json["cellLength"].Value<double>();
                                    component.cellHeight = json["cellHeight"].Value<double>();

                                    dtRecHitObjects.Add(item); // Add to list
                                    indexer++;
                                }
                                break;
                            case "DTRecSegment4D_V1.obj":
                                Color dtrsColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); // #ffff00
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = dtrsColor;

                                    DTRecSegmentComponent component = item.AddComponent<DTRecSegmentComponent>();
                                    JToken json = totalJson["DTRecSegment4D_V1Data"][indexer];

                                    component.detid = json["detid"].Value<int>();
                                    component.pos_1 = json["pos_1"].ToObject<double[]>();
                                    component.pos_2 = json["pos_2"].ToObject<double[]>();
                                    component.sectorId = json["sectorId"].Value<int>();
                                    component.stationId = json["stationId"].Value<int>();
                                    component.wheelId = json["wheelId"].Value<int>();
                                    dtrecSegmentObjects.Add(item);
                                    indexer++;
                                }
                                break;
                            case "CSCRecHit2Ds_V2.obj":
                                Color cscrhColor = new Color(153 / 255f, 255 / 255f, 229 / 255f); // #99ffe5
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscrhColor;

                                    CSCRecHitComponent component = item.AddComponent<CSCRecHitComponent>();
                                    var json = totalJson["CSCRecSegments2DsV2Datas"][indexer];

                                    component.u1 = json["u1"].ToObject<double[]>();
                                    component.u2 = json["u2"].ToObject<double[]>();
                                    component.v1 = json["v1"].ToObject<double[]>();
                                    component.v2 = json["v2"].ToObject<double[]>();
                                    component.w1 = json["w1"].ToObject<double[]>();
                                    component.w2 = json["w2"].ToObject<double[]>();

                                    component.endcap = json["endcap"].Value<int>();
                                    component.station = json["station"].Value<int>();
                                    component.ring = json["ring"].Value<int>();
                                    component.chamber = json["chamber"].Value<int>();
                                    component.layer = json["layer"].Value<int>();

                                    component.tpeak = json["tpeak"].Value<double>();
                                    component.positionWithinStrip = json["positionWithinStrip"].Value<double>();
                                    component.errorWithinStrip = json["errorWithinStrip"].Value<double>();

                                    component.strips = json["strips"].Value<string>();
                                    component.wireGroups = json["WireGroups"].Value<string>();

                                    cscRecHitObjects.Add(item);
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
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
                try
                {
                    makeDataButtons();
                }
                catch (Exception ex)
                {
                    print(ex.Message);
                }

            }

            //jsonpath = @"/data/local/tmp/obj/totaldata.json";
            //metpath = @"/data/local/tmp/obj/METData.json";

            //try
            //{
            //    using (StreamReader file = File.OpenText(metpath))
            //    using (JsonTextReader reader = new JsonTextReader(file))
            //    {
            //        metJson = (JObject)JToken.ReadFrom(reader);
            //        if (metJson["phi"] == null)
            //        {
            //        }
            //        else
            //        {
            //            Quaternion rotator = Quaternion.Euler(0, 0, (float)(metJson["phi"].Value<double>() * (180 / Math.PI)));
            //            GameObject METObject = Instantiate(METPrefab, Vector3.zero, rotator);
            //            METObject.name = "MET";
            //            METObject.transform.GetChild(0).gameObject.name = "Missing ET";
            //            METComponent METItem = METObject.AddComponent<METComponent>();
            //            METItem.phi = metJson["phi"].Value<double>();
            //            METItem.pt = metJson["pt"].Value<double>();
            //            METItem.px = metJson["px"].Value<double>();
            //            METItem.py = metJson["py"].Value<double>();
            //            METItem.pz = metJson["pz"].Value<double>();

            //            objectsLoaded.Add(METObject);
            //        }
            //    }
            //}
            //catch (Exception e) { }

        }
        //Code that runs in editor
        else
        {
            //objpath = @"C:\Users\uclav\Downloads\IGDATA\obj_files\SphaleronRECO";
            objpath = @"C:\Users\uclav\Downloads\IGDATA\obj_files\masterclass_11_test";
            eventPaths = Directory.GetDirectories(objpath);
            Array.Sort(eventPaths, (a, b) =>
            {
                int numA = ExtractNumber(a);
                int numB = ExtractNumber(b);
                return numA.CompareTo(numB);
            }); 
            // Creating the scrollable list of events
            GameObject scrollViewGO = Instantiate(scrollViewPrefab, selectionMenu.transform);
            RectTransform scrollRect = scrollViewGO.GetComponent<RectTransform>();
            //scrollRect.Transform.position = new Vector3(scrollRect.Transform.x, scrollRect.Transform., scrollRect.Transform.z);
            scrollRect.sizeDelta = new Vector2(scrollRect.sizeDelta.x, 200);

            //RectTransform rectTransformData = currentScrollViewData.GetComponent<RectTransform>();
            //Vector2 newPosData = rectTransformData.anchoredPosition;
            //newPosData.x = -37f;
            //newPosData.y = 2f; // nathan added
            //rectTransformData.anchoredPosition = newPosData;

            Transform eventContent = scrollViewGO.transform.Find("Viewport/Content");
            RectTransform eventContentRect = eventContent.GetComponent<RectTransform>();
            float eventButtonSpacingY = 51f;
            float eventContentHeight = 552;//eventPaths.Length * eventButtonSpacingY;
            eventContentRect.sizeDelta = new Vector2(eventContentRect.sizeDelta.x, eventContentHeight);

            Transform content = scrollViewGO.transform.Find("Viewport/Content/Organizer");
            float eventButtonYpos = 223; //scrollview
            int eventIterator = 0;

            //GameObject scrollViewData = Instantiate(scrollViewPrefab, selectionMenu.transform);
            //RectTransform rectTransformData = scrollViewData.GetComponent<RectTransform>();
            //Vector2 newPosData = rectTransformData.anchoredPosition; 
            //newPosData.x = 37f;
            //rectTransformData.anchoredPosition = newPosData;
            //Transform dataContent = scrollViewData.transform.Find("Viewport/Content");
            //RectTransform dataContentRect = dataContent.GetComponent<RectTransform>();
            //dataContentRect.sizeDelta = new Vector2(dataContentRect.sizeDelta.x, 2000);
            //Transform dataOrganizer = scrollViewData.transform.Find("Viewport/Content/Organizer");

            foreach (string eventPath in eventPaths)
            {
                string eventName = Path.GetFileName(eventPath);
                GameObject eventObject = new GameObject(eventName);
                allObjects.Add(eventObject);
                jsonpath = $@"{eventPath}\\totaldata.json";
                //jsonpath = @"C:\Users\uclav\Downloads\IGDATA\json_data_files\totaldata(masterclass139707779).json";
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

                // Adding to list of events NATHAN STUFF
                if (eventIterator > 0)
                {
                    eventObject.SetActive(false);
                }

                GameObject eventButton = new GameObject(Path.GetFileName(eventPath) + "Button", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button));
                UnityEngine.UI.Image eventButtonImage = eventButton.GetComponent<UnityEngine.UI.Image>();
                if (eventIterator == 0)
                {
                    eventButtonImage.color = new Color(0.290f, 0.816f, 1f, 1f);
                }
                else
                {
                    eventButtonImage.color = new Color(0.745f, 0.745f, 0.745f, 1f);
                }
                eventButton.transform.SetParent(content, false);
                UnityEngine.UI.Outline outline = eventButton.AddComponent<UnityEngine.UI.Outline>();
                outline.effectColor = Color.black;
                RectTransform eventButtonRect = eventButton.GetComponent<RectTransform>();
                eventButtonRect.sizeDelta = new Vector2(680, 50);
                eventButtonRect.anchoredPosition = new Vector2(0, eventButtonYpos - eventIterator * eventButtonSpacingY);
                eventIterator += 1;
                eventButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    for (int i = 0; i < allObjects.Count; i++)
                    {
                        GameObject child = allObjects[i];
                        UnityEngine.UI.Image eventButtonImage = content.GetChild(i).GetComponent<UnityEngine.UI.Image>();  // Accounting for Lambda closure issue
                        if (child == eventObject)
                        {
                            eventButtonImage.color = new Color(0.290f, 0.816f, 1f, 1f);
                            child.SetActive(true);
                            currentEvent = i; //sets current event to the index... indexed by allObjects 
                        }
                        else
                        {
                            eventButtonImage.color = new Color(0.745f, 0.745f, 0.745f, 1f);
                            child.SetActive(false);
                        }
                    }
                    try
                    {
                        currentObjectSub = null;
                        currentSubItemType = "";
                        subItemIndex = 0;
                        subObjects.Clear();
                        if (currentSubItemPopup != null)
                        {
                            Destroy(currentSubItemPopup);
                            currentSubItemPopup = null;
                        }
                        makeDataButtons();
                    }
                    catch (Exception ex)
                    {
                        print(ex.Message);
                    }
                    ResetStaticVariables reset = new ResetStaticVariables();
                    reset.ResetStatics();
                });

                GameObject textObj = new GameObject("Text (TMP)", typeof(RectTransform), typeof(TextMeshProUGUI));
                textObj.transform.SetParent(eventButton.transform, false);
                RectTransform textObjRect = textObj.GetComponent<RectTransform>();
                textObjRect.sizeDelta = new Vector2(600, 50);
                TextMeshProUGUI tmp = textObj.GetComponent<TextMeshProUGUI>();
                //tmp.text = Path.GetFileName(eventPath);
                tmp.text = eventPath.Substring(eventPath.IndexOf("obj_files\\") + "obj_files\\".Length); ;
                tmp.alignment = TextAlignmentOptions.Left;
                tmp.fontSize = 32;
                tmp.color = Color.white;
                tmp.raycastTarget = false;
                //  Run_NUMBER-Event_NUMBER
                foreach (var path in paths)
                {
                    try
                    {
                        indexer = 0;

                        FileInfo f;
                        f = new FileInfo($@"{eventPath}\{path}");
                        if (f.Length == 0)
                        {
                            continue;
                        }
                        var fs = new FileStream($@"{eventPath}\{path}", FileMode.Open, FileAccess.Read);
                        GameObject loadedObject = new OBJLoader().Load(fs);
                        loadedObject.transform.localScale = Vector3.one;
                        string name = path.Split('.')[0];
                        loadedObject.name = name;
                        //allObjects.Add(loadedObject);
                        loadedObject.transform.SetParent(eventObject.transform);
                        GameObject child = loadedObject.transform.GetChild(0).gameObject;
                        List<GameObject> children = AllChilds(loadedObject);
                        foreach (var item in children)
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
                            case "RPCRecHits_V1.obj":
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = rpcMaterial;
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
                                Color cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                                foreach (var item in children)
                                {
                                    child.GetComponent<MeshRenderer>().material = cscMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscColor;
                                    indexer++;
                                }
                                break;
                            case "CSCSegments_V2.obj":
                                cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                                foreach (var item in children)
                                {
                                    child.GetComponent<MeshRenderer>().material = cscMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscColor;
                                    CSCSegmentComponent component = item.AddComponent<CSCSegmentComponent>();

                                    JToken json = totalJson["CSCSegments_V2"][indexer];

                                    component.detid = json["detid"].Value<int>();
                                    component.pos_1 = json["pos_1"].ToObject<double[]>();
                                    component.pos_2 = json["pos_2"].ToObject<double[]>();
                                    component.endcap = json["endcap"].Value<int>();
                                    component.station = json["station"].Value<int>();
                                    component.ring = json["ring"].Value<int>();
                                    component.chamber = json["chamber"].Value<int>();
                                    component.layer = json["layer"].Value<int>();

                                    cscSegmentObjects.Add(item);
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
                                    item.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = dtrColor;

                                    DTRecHitComponent component = item.AddComponent<DTRecHitComponent>();
                                    var json = totalJson["DTRecHits_V1Data"][indexer];

                                    component.wireId = json["wireId"].Value<int>();
                                    component.layerId = json["layerId"].Value<int>();
                                    component.superLayerId = json["superLayerId"].Value<int>();
                                    component.sectorId = json["sectorId"].Value<int>();
                                    component.stationId = json["stationId"].Value<int>();
                                    component.wheelId = json["wheelId"].Value<int>();
                                    component.digitime = json["digitime"].Value<double>();

                                    component.wirePos = json["wirePos"].ToObject<double[]>();
                                    component.lPlusGlobalPos = json["lPlusGlobalPos"].ToObject<double[]>();
                                    component.lMinusGlobalPos = json["lMinusGlobalPos"].ToObject<double[]>();
                                    component.rPlusGlobalPos = json["rPlusGlobalPos"].ToObject<double[]>();
                                    component.rMinusGlobalPos = json["rMinusGlobalPos"].ToObject<double[]>();
                                    component.lGlobalPos = json["lGlobalPos"].ToObject<double[]>();
                                    component.rGlobalPos = json["rGlobalPos"].ToObject<double[]>();
                                    component.axis = json["axis"].ToObject<double[]>();
                                    component.angle = json["angle"].Value<double>();
                                    component.cellWidth = json["cellWidth"].Value<double>();
                                    component.cellLength = json["cellLength"].Value<double>();
                                    component.cellHeight = json["cellHeight"].Value<double>();

                                    dtRecHitObjects.Add(item); // Add to list
                                    indexer++;
                                }
                                break;
                            case "DTRecSegment4D_V1.obj":
                                Color dtrsColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); // #ffff00
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = dtrsColor;

                                    DTRecSegmentComponent component = item.AddComponent<DTRecSegmentComponent>();
                                    JToken json = totalJson["DTRecSegment4D_V1Data"][indexer];

                                    component.detid = json["detid"].Value<int>();
                                    component.pos_1 = json["pos_1"].ToObject<double[]>();
                                    component.pos_2 = json["pos_2"].ToObject<double[]>();
                                    component.sectorId = json["sectorId"].Value<int>();
                                    component.stationId = json["stationId"].Value<int>();
                                    component.wheelId = json["wheelId"].Value<int>();
                                    dtrecSegmentObjects.Add(item);
                                    indexer++;
                                }
                                break;
                            case "CSCRecHit2Ds_V2.obj":
                                Color cscrhColor = new Color(153 / 255f, 255 / 255f, 229 / 255f); // #99ffe5
                                foreach (var item in children)
                                {
                                    item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                                    item.GetComponent<MeshRenderer>().material.color = cscrhColor;

                                    CSCRecHitComponent component = item.AddComponent<CSCRecHitComponent>();
                                    var json = totalJson["CSCRecSegments2DsV2Datas"][indexer];

                                    component.u1 = json["u1"].ToObject<double[]>();
                                    component.u2 = json["u2"].ToObject<double[]>();
                                    component.v1 = json["v1"].ToObject<double[]>();
                                    component.v2 = json["v2"].ToObject<double[]>();
                                    component.w1 = json["w1"].ToObject<double[]>();
                                    component.w2 = json["w2"].ToObject<double[]>();

                                    component.endcap = json["endcap"].Value<int>();
                                    component.station = json["station"].Value<int>();
                                    component.ring = json["ring"].Value<int>();
                                    component.chamber = json["chamber"].Value<int>();
                                    component.layer = json["layer"].Value<int>();

                                    component.tpeak = json["tpeak"].Value<double>();
                                    component.positionWithinStrip = json["positionWithinStrip"].Value<double>();
                                    component.errorWithinStrip = json["errorWithinStrip"].Value<double>();

                                    component.strips = json["strips"].Value<string>();
                                    component.wireGroups = json["WireGroups"].Value<string>();

                                    cscRecHitObjects.Add(item);
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
                    }

                    catch (Exception ex) { }
                }
            }
            try
            {
                makeDataButtons();
            }
            catch (Exception ex)
            {
                print(ex.Message);
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

    public List<GameObject> getEventObjects(string name, int i)
    {
        Dictionary<string, List<string>> categoryMap = new Dictionary<string, List<string>>
        {
            { "vertex", new List<string> { "SecondaryVertices_V1", "PrimaryVertices_V1", "Vertices_V1" } },
            { "jet", new List<string> { "PFJets_V2", "PFJets" } },
            { "trackerMuon", new List<string> { "TrackerMuons_V1", "TrackerMuons_V2" } },
            { "globalMuon", new List<string> { "GlobalMuons_V1", "GlobalMuons_V2" } },
            { "standaloneMuon", new List<string> { "StandaloneMuons_V1", "StandaloneMuons_V2" } },
            { "electron", new List<string> { "GsfElectrons_V1", "GsfElectrons_V2", "GsfElectrons_V3" } },
            { "photon", new List<string> { "Photons_V1" } },
            { "track", new List<string> { "Tracks_V1", "Tracks_V2", "Tracks_V3", "Tracks_V4" } }
            //{ "superCluster", new List<string> { "SuperClusters_V1" } },
            //{ "cscSegment", new List<string> { "CSCSegments_V1", "CSCSegments_V2" } },
            //{ "rpcRecHit", new List<string> { "RPCRecHits_V1" } }
        };

        if (!categoryMap.ContainsKey(name))
        {
            UnityEngine.Debug.LogError($"Invalid type: {name}");
            throw new ArgumentException($"Invalid type: {name}");
        }

        List<string> targetTypes = categoryMap[name];



        // 1) grab parent object of event 2) go to its children 3) if children match one of the object lists... grab its children and add it to the list
        GameObject event_ = allObjects[i]; // the name of the event
        Transform parent = event_.transform;
        List<GameObject> result = new List<GameObject>();

        foreach (string type in targetTypes)
        {
            Transform child = parent.Find(type);
            if (child == null)
            {
                continue;
            }
            foreach (Transform grandChild in child)
            {
                result.Add(grandChild.gameObject);
            }
        }
        return result;
    }
    public List<string> getAvailableDataTypes(int i)
    {
        List<string> result = new List<string>();
        GameObject event_ = allObjects[i]; 
        Transform parent = event_.transform;

        foreach (string type in dataTypes)
        {
            Transform child = parent.Find(type);
            if (child == null)
            {
                continue;
            }
            result.Add(type);
        }
        return result;
    }

    public JObject getJson(int i)
    {
        JObject totalJson_ = new JObject();
        string eventPath = eventPaths[i];
        jsonpath = $@"{eventPath}\\totaldata.json";
        try
        {
            using (StreamReader file = File.OpenText(jsonpath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                totalJson_ = (JObject)JToken.ReadFrom(reader);
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
        return totalJson_;
    }


    public int getCurrentEvent()
    {
        return currentEvent;
    }
    public int ExtractNumber(string path)
    {
        Match match = Regex.Match(path, @"\d+");
        return match.Success ? int.Parse(match.Value) : 0;
    }

    public void makeDataButtons()
    {
        float eventButtonSpacingY = 51f;

        if (currentScrollViewData != null)
        {
            Destroy(currentScrollViewData);
        }
        currentScrollViewData = Instantiate(scrollViewPrefab, selectionMenu.transform);
        RectTransform rectTransformData = currentScrollViewData.GetComponent<RectTransform>();
        Vector2 newPosData = rectTransformData.anchoredPosition;
        newPosData.x = -37f;
        newPosData.y = 2f; // nathan added
        rectTransformData.anchoredPosition = newPosData;
        Transform dataContent = currentScrollViewData.transform.Find("Viewport/Content");
        RectTransform dataContentRect = dataContent.GetComponent<RectTransform>();
        //dataContentRect.sizeDelta = new Vector2(dataContentRect.sizeDelta.x, 2000);
        float contentHeight = 2100; // dataTypes.Count * eventButtonSpacingY * 1.1f
        dataContentRect.sizeDelta = new Vector2(dataContentRect.sizeDelta.x, contentHeight);
        Transform dataOrganizer = currentScrollViewData.transform.Find("Viewport/Content/Organizer");
        
        foreach (Transform child in dataOrganizer)
        {
            GameObject.Destroy(child.gameObject);
        }

        float dataButtonYpos = 950f; //scrollview between 1000 and 750 good
        int dataIterator = 0;
        foreach (string dataType in dataTypes)
        {
            List<string> dataInScene = getAvailableDataTypes(getCurrentEvent());
            GameObject dataButton = new GameObject(dataType + "Button", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button));
            UnityEngine.UI.Image buttonImage = dataButton.GetComponent<UnityEngine.UI.Image>();
            if (dataInScene.Contains(dataType))
            {
                if (activeMap[dataType])
                {
                    buttonImage.color = new Color(0.6f, 1f, 0.6f); // Light green

                }
                else
                {
                    buttonImage.color = new Color(1f, 0.6f, 0.6f); // Light red
                }
            }
            else
            {
                buttonImage.color = new Color(0.745f, 0.745f, 0.745f, 1f);
            }


            dataButton.transform.SetParent(dataOrganizer, false);
            UnityEngine.UI.Outline dataOutline = dataButton.AddComponent<UnityEngine.UI.Outline>();
            dataOutline.effectColor = Color.black;
            RectTransform dataButtonRect = dataButton.GetComponent<RectTransform>();
            dataButtonRect.sizeDelta = new Vector2(680, 50);
            dataButtonRect.anchoredPosition = new Vector2(0, dataButtonYpos - dataIterator * eventButtonSpacingY);
            dataIterator += 1;

            dataButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
            {
                if (dataInScene.Contains(dataType))
                {
                    activeMap[dataType] = !activeMap[dataType]; //flip the state 

                    if (activeMap[dataType])
                    {
                        buttonImage.color = new Color(0.6f, 1f, 0.6f); // Light green

                    }
                    else
                    {
                        buttonImage.color = new Color(1f, 0.6f, 0.6f); // Light red
                    }
                }
                updateVisibility();
            });

            GameObject dataTextObj = new GameObject("Text (TMP)", typeof(RectTransform), typeof(TextMeshProUGUI));
            dataTextObj.transform.SetParent(dataButton.transform, false);
            RectTransform dataTextObjRect = dataTextObj.GetComponent<RectTransform>();
            dataTextObjRect.sizeDelta = new Vector2(600, 50);
            TextMeshProUGUI dataTmp = dataTextObj.GetComponent<TextMeshProUGUI>();
            dataTmp.text = dataType;
            dataTmp.alignment = TextAlignmentOptions.Left;
            dataTmp.fontSize = 32;
            dataTmp.color = Color.white;
            dataTmp.raycastTarget = false;

            //if it has subitems, make a seperate button for view more 
            if (dataTypesWithSubitems.Contains(dataType) && dataInScene.Contains(dataType))
            {
                GameObject viewMoreButton = new GameObject("ViewMoreButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button));
                viewMoreButton.transform.SetParent(dataButton.transform, false);

                RectTransform viewMoreRect = viewMoreButton.GetComponent<RectTransform>();
                viewMoreRect.anchorMin = new Vector2(1, 0.5f);
                viewMoreRect.anchorMax = new Vector2(1, 0.5f);
                viewMoreRect.pivot = new Vector2(1, 0.5f);
                viewMoreRect.sizeDelta = new Vector2(120, 38);
                viewMoreRect.anchoredPosition = new Vector2(-10, 0);

                // Light blue background
                UnityEngine.UI.Image viewMoreImage = viewMoreButton.GetComponent<UnityEngine.UI.Image>();
                viewMoreImage.color = new Color(0.8f, 0.8f, 1f); // Light blue
                viewMoreImage.type = UnityEngine.UI.Image.Type.Sliced;

                // Outline
                UnityEngine.UI.Outline viewOutline = viewMoreButton.AddComponent<UnityEngine.UI.Outline>();
                viewOutline.effectColor = Color.black;

                // TMP Text inside button
                GameObject viewMoreText = new GameObject("Text", typeof(RectTransform), typeof(TextMeshProUGUI));
                viewMoreText.transform.SetParent(viewMoreButton.transform, false);

                RectTransform viewTextRect = viewMoreText.GetComponent<RectTransform>();
                viewTextRect.anchorMin = Vector2.zero;
                viewTextRect.anchorMax = Vector2.one;
                viewTextRect.offsetMin = Vector2.zero;
                viewTextRect.offsetMax = Vector2.zero;

                TextMeshProUGUI tmp = viewMoreText.GetComponent<TextMeshProUGUI>();
                tmp.text = "View Subitems";
                tmp.alignment = TextAlignmentOptions.Center;
                tmp.fontSize = 18; // smaller font to fit nicely
                tmp.color = Color.white; // match main button text
                tmp.raycastTarget = false;

                viewMoreButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    //print($"Viewing more subitems for {dataType}");
                     ShowSubItems(dataType); 
                });
            }

        }
    }


    public void setCurrentObject(string dataType, int i)
    {
        // 1) grab parent object of event 2) go to its children 3) if children match one of the object lists... grab its children and add it to the list
        GameObject event_ = allObjects[i]; // the name of the event
        Transform parent = event_.transform;
        Transform child = parent.Find(dataType);
        if (child == null)
        {
            print($"Type '{dataType}' not found under {event_.name}");
            currentObjectSub = null;
        }
        else
        {
            currentObjectSub = child.gameObject;
        }
    }
    public void ShowSubItems(string dataType)
    {
        if (subObjects != null && currentSubItemMaterial != null)
        {
            foreach (GameObject obj in subObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = currentSubItemMaterial;
                }
            }
        }
        currentSubItemPopup = null;
        currentSubItemType = "";
        currentSubItemMaterial = null;
        subItemIndex = 0;


        setCurrentObject(dataType, getCurrentEvent());
        if (currentObjectSub == null)
        {
            print($"Current object for {dataType} was not found.");
            return;
        }

        subObjects = AllChilds(currentObjectSub);
        if (subObjects.Count == 0)
        {
            print($"No children found under {currentObjectSub.name}");
            return;
        }
        string subItemText = $"Selected Item: {currentObjectSub.name}\nCurrently selected subitem ({subItemIndex+1}/{subObjects.Count}): {subObjects[subItemIndex].name}\n{GetDataFromPhysicsObject(dataType, subItemIndex)}";
        Transform existing = GameObject.Find("SubitemPopup")?.transform;
        if (existing != null) Destroy(existing.gameObject);

        GameObject popup = new GameObject("SubitemPopup", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image));
        popup.transform.SetParent(selectionMenu.transform, false);
        popup.transform.localScale = selectionMenu.transform.localScale;
        RectTransform popupRect = popup.GetComponent<RectTransform>();
        popupRect.sizeDelta = new Vector2(70, 28);

        // Match scrollView position and move slightly up
        RectTransform scrollRect = currentScrollViewData.GetComponent<RectTransform>();
        Vector2 scrollPos = scrollRect.anchoredPosition;
        popupRect.anchorMin = new Vector2(0, 1);
        popupRect.anchorMax = new Vector2(0, 1);
        popupRect.pivot = new Vector2(0, 1);
        //popupRect.anchoredPosition = new Vector2(scrollPos.x+80f, scrollPos.y-50f); // just 10f above scrollview top
        popupRect.anchoredPosition = new Vector2(50f, -34f);

        // Style the popup background
        UnityEngine.UI.Image popupImage = popup.GetComponent<UnityEngine.UI.Image>();
        popupImage.color = new Color(0.2f, 0.2f, 0.2f, 0.95f);
        popup.AddComponent<UnityEngine.UI.Outline>().effectColor = Color.black;

        // Text
        GameObject popupText = new GameObject("PopupText", typeof(RectTransform), typeof(TextMeshProUGUI));
        popupText.transform.SetParent(popup.transform, false);
        RectTransform popupTextRect = popupText.GetComponent<RectTransform>();
        popupTextRect.anchorMin = Vector2.zero;
        popupTextRect.anchorMax = Vector2.one;
        popupTextRect.offsetMin = new Vector2(2, 4);
        popupTextRect.offsetMax = new Vector2(-30, 0);

        TextMeshProUGUI tmpText = popupText.GetComponent<TextMeshProUGUI>();
        tmpText.text = $"{subItemText}";
        if (dataType == "DTRecHits_V1" || dataType == "CSCRecHit2Ds_V2")
        {
            tmpText.fontSize = 1.5f;
        }
        else
        {
            tmpText.fontSize = 2.3f;
        }
        tmpText.color = Color.white;
        tmpText.alignment = TextAlignmentOptions.MidlineLeft;
        tmpText.raycastTarget = false;
        tmpText.enableWordWrapping = false;

        // Close button
        GameObject closeButton = new GameObject("CloseButton", typeof(RectTransform), typeof(CanvasRenderer), typeof(UnityEngine.UI.Image), typeof(UnityEngine.UI.Button));
        closeButton.transform.SetParent(popup.transform, false);

        RectTransform closeRect = closeButton.GetComponent<RectTransform>();
        closeRect.anchorMin = new Vector2(1, 1);
        closeRect.anchorMax = new Vector2(1, 1);
        closeRect.pivot = new Vector2(1, 1);
        closeRect.sizeDelta = new Vector2(3, 3);
        closeRect.anchoredPosition = new Vector2(-2, -1);

        UnityEngine.UI.Image closeImage = closeButton.GetComponent<UnityEngine.UI.Image>();
        closeImage.color = new Color(0.8f, 0.4f, 0.4f);

        GameObject closeTextObj = new GameObject("CloseText", typeof(RectTransform), typeof(TextMeshProUGUI));
        closeTextObj.transform.SetParent(closeButton.transform, false);
        RectTransform closeTextRect = closeTextObj.GetComponent<RectTransform>();
        closeTextRect.anchorMin = Vector2.zero;
        closeTextRect.anchorMax = Vector2.one;
        closeTextRect.offsetMin = Vector2.zero;
        closeTextRect.offsetMax = Vector2.zero;

        TextMeshProUGUI closeText = closeTextObj.GetComponent<TextMeshProUGUI>();
        closeText.text = "X";
        closeText.fontSize = 2;
        closeText.alignment = TextAlignmentOptions.Center;
        closeText.color = Color.white;
        closeText.raycastTarget = false;

        closeButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            foreach (GameObject obj in subObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && currentSubItemMaterial != null)
                {
                    renderer.material = currentSubItemMaterial;
                }
            }
            currentSubItemPopup = null;
            currentSubItemType = "";
            currentSubItemMaterial = null;
            subItemIndex = 0;

            Destroy(popup);
        });
        currentSubItemPopup = popup;
        currentSubItemType = dataType;
        currentSubItemMaterial = subObjects[subItemIndex].GetComponent<Renderer>().sharedMaterial;
        highlightSubItem();
    }

    public void highlightSubItem()
    {
        if (subItemIndex >= 0 && subItemIndex < subObjects.Count)
        {
            Material highlightMat = new Material(Shader.Find("Standard"));
            highlightMat.color = new Color(1f, 1f, 1f, 0.4f);
            highlightMat.SetFloat("_Mode", 3);
            highlightMat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            highlightMat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            highlightMat.SetInt("_ZWrite", 0);
            highlightMat.DisableKeyword("_ALPHATEST_ON");
            highlightMat.EnableKeyword("_ALPHABLEND_ON");
            highlightMat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            highlightMat.renderQueue = 3000;

            for (int i = 0; i < subObjects.Count; i++)
            {
                GameObject subObject = subObjects[i];
                Renderer renderer = subObject.GetComponent<Renderer>();
                if (i == subItemIndex)
                { 
                    renderer.material = highlightMat;
                }
                else
                {
                    renderer.sharedMaterial = currentSubItemMaterial;
                }
            }
        }
        else
        {
            UnityEngine.Debug.LogWarning("Index out of range: " + subItemIndex);
        }
    }
    public void NavigateSubitems(int direction)
    {
        if (currentSubItemPopup == null || !currentSubItemPopup.activeSelf)
        {
            return;
        }
        subItemIndex = (subItemIndex + direction + subObjects.Count) % subObjects.Count;

        if (currentSubItemPopup != null && !string.IsNullOrEmpty(currentSubItemType))
        {
            string updatedText = $"Selected Item: {currentObjectSub.name}\nCurrently selected subitem ({subItemIndex + 1}/{subObjects.Count}): {subObjects[subItemIndex].name}\n{GetDataFromPhysicsObject(currentSubItemType, subItemIndex)}";
            TextMeshProUGUI textComp = currentSubItemPopup.transform.Find("PopupText")?.GetComponent<TextMeshProUGUI>();
            if (textComp != null)
            {
                textComp.text = updatedText;
            }
            else
            {
                print("textComp = null");
            }
        }
        highlightSubItem();
    }
    //this needs the name of the datatype + the list of subObjects which gets set to whatever datatype you're looking at
    private string GetDataFromPhysicsObject(string name, int index2)
    {
        string dataString;
        switch (name)
        {
            case "PFMET":
                dataString = this.subObjects[index2].GetComponent<METComponent>().GetData();
                break;
            case "PFJets":
            case "PFJets_V2": 
                dataString = this.subObjects[index2].GetComponent<JetComponent>().GetData();
                break;
            case "EBRecHits":
                dataString = "";
                break;
            case "EERecHits":
                dataString = "";
                break;
            case "ESRecHits":
                dataString = "";
                break;
            case "GsfElectrons_V1":
            case "GsfElectrons_V2":
            case "GsfElectrons_V3":
                dataString = this.subObjects[index2].GetComponent<ElectronComponent>().GetData();
                break;
            case "HBRecHits":
                dataString = "";
                break;
            case "HERecHits":
                dataString = "";
                break;
            case "HFRecHits":
                dataString = "";
                break;
            case "HORecHits":
                dataString = "";
                break;
            case "MuonChambers":
                dataString = "";
                break;
            case "TrackerMuons_V1":
            case "TrackerMuons_V2":
                dataString = this.subObjects[index2].GetComponent<TrackerMuonComponent>().GetData();
                break;
            case "GlobalMuons_V1":
            case "GlobalMuons_V2":
                dataString = this.subObjects[index2].GetComponent<GlobalMuonComponent>().GetData();
                break;
            case "StandaloneMuons_V1":
            case "StandaloneMuons_V2":
                dataString = this.subObjects[index2].GetComponent<StandaloneMuonComponent>().GetData();
                break;
            case "Photons_V1":
                dataString = ""; // Fill in if you add a PhotonComponent later
                break;
            case "Tracks_V1":
            case "Tracks_V2":
            case "Tracks_V3":
            case "Tracks_V4":
                dataString = this.subObjects[index2].GetComponent<TrackComponent>().GetData();
                break;
            case "SuperClusters_V1":
                dataString = this.subObjects[index2].GetComponent<SuperClusterComponent>().GetData();
                break;
            case "PrimaryVertices_V1":
            case "SecondaryVertices_V1":
            case "Vertices_V1":
                dataString = this.subObjects[index2].GetComponent<VertexComponent>().GetData();
                break;
            case "DTRecSegment4D_V1":
                dataString = this.subObjects[index2].GetComponent<DTRecSegmentComponent>().GetData();
                break;
            case "DTRecHits_V1":
                dataString = this.subObjects[index2].GetComponent<DTRecHitComponent>().GetData();
                break;
            case "CSCRecHit2Ds_V2":
                dataString = this.subObjects[index2].GetComponent<CSCRecHitComponent>().GetData();
                break;
            case "CSCSegments_V2":
                dataString = this.subObjects[index2].GetComponent<CSCSegmentComponent>().GetData();
                break;
            default: dataString = ""; break;
        }
        return dataString;
    }
    public void updateVisibility()
    {
        foreach (GameObject item in objectsLoaded) //make items active or inactive
        {
            item.SetActive(activeMap[item.name]);
        }
    }

}