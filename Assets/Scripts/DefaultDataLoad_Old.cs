using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

public class DefaultDataLoad_Old : MonoBehaviour
{
    private int indexer;

    public List<GameObject> sceneObjects;
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


    public GameObject locatingSphere;
    public GameObject LocatingSpherePrefab;
    public GameObject METPrefab;

    private JToken jsonData;
    public TextAsset jsonText;
    public GameObject eventObj;

    void Awake()
    {
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

        JObject totalJson = (JObject)JToken.Parse(jsonText.text);

        foreach (var objitem in sceneObjects)
        {
            indexer = 0;

            if (objitem.name == "Modified_Event_V2")
            {
                continue;
            }

            GameObject child = objitem.transform.GetChild(0).gameObject;
            List<GameObject> children = AllChilds(objitem);
            foreach (var item in children)
            {
                item.AddComponent<MeshCollider>();
                item.AddComponent<hoverOBJ>();
                item.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
            }

            switch (objitem.name)
            {
                case "SecondaryVertices_V1":
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
                case "PrimaryVertices_V1":
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
                case "PFJets_V2":
                case "PFJets":
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
                case "TrackerMuons_V1":
                case "TrackerMuons_V2":
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
                case "GlobalMuons_V1":
                case "GlobalMuons_V2":
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
                case "StandaloneMuons_V1":
                case "StandaloneMuons_V2":
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
                case "GsfElectrons_V1":
                case "GsfElectrons_V2":
                case "GsfElectrons_V3":
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
                case "EERecHits_V2":
                case "EBRecHits_V2":
                case "ESRecHits_V2":
                    child.GetComponent<MeshRenderer>().material = emMaterial; break;
                case "HBRecHits_V2":
                case "HERecHits_V2":
                case "HORecHits_V2":
                case "HFRecHits_V2":
                    child.GetComponent<MeshRenderer>().material = hadronMaterial; break;
                case "MuonChambers_V1":
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
                case "Photons_V1":
                    child.GetComponent<MeshRenderer>().material = emMaterial; break;
                case "Tracks_V1":
                case "Tracks_V2":
                case "Tracks_V3":
                case "Tracks_V4":
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
                case "SuperClusters_V1":
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
                case "Vertices_V1":
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
                case "CSCSegments_V1":
                case "CSCSegments_V2":
                    Color cscColor = new Color(255 / 255f, 153 / 255f, 229 / 255f); //#ff99ff
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = cscMaterial;
                        item.GetComponent<MeshRenderer>().material.color = cscColor;
                        indexer++;
                    }
                    break;
                case "TrackDets_V1":
                    Color tdColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = tdMaterial;
                        item.GetComponent<MeshRenderer>().material.color = tdColor;
                        indexer++;
                    }
                    break;
                case "SiPixelClusters_V1":
                    Color spColor = new Color(255 / 255f, 102 / 255f, 0 / 255f); //#ff6600
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = pixelMaterial;
                        item.GetComponent<MeshRenderer>().material.color = spColor;
                        indexer++;
                    }
                    break;
                case "SiStripClusters_V1":
                    Color ssColor = new Color(204 / 255f, 51 / 255f, 0 / 255f); //#cc3300
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = stripMaterial;
                        item.GetComponent<MeshRenderer>().material.color = ssColor;
                        indexer++;
                    }
                    break;
                case "DTRecHits_V1":
                    Color dtrColor = new Color(0 / 255f, 255 / 255f, 0 / 255f); //#00ff00
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                        item.GetComponent<MeshRenderer>().material.color = dtrColor;
                        indexer++;
                    }
                    break;
                case "DTRecSegment4D_V1":
                    Color dtrsColor = new Color(255 / 255f, 255 / 255f, 0 / 255f); //#ffff00
                    foreach (var item in children)
                    {
                        child.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                        item.GetComponent<MeshRenderer>().material.color = dtrsColor;
                        indexer++;
                    }
                    break;
                case "CSCRecHit2Ds_V2":
                    Color cscrhColor = new Color(153 / 255f, 255 / 255f, 229 / 255f); //#99ffe5
                    foreach (var item in children)
                    {
                        item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                        item.GetComponent<MeshRenderer>().material.color = cscrhColor;
                        indexer++;
                    }
                    break;
                case "MatchingCSCs_V1":
                    Color mcscsColor = new Color(255 / 255f, 0 / 255f, 0 / 255f, 25 / 255f); //#ff0000
                    foreach (var item in children)
                    {
                        item.GetComponent<MeshRenderer>().material = matchingcscsMaterial;
                        item.GetComponent<MeshRenderer>().material.color = mcscsColor;
                        indexer++;
                    }
                    break;
                case "TrackingRecHits_V1":
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
        }
    }

    // Update is called once per frame
    void Update()
    {

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
