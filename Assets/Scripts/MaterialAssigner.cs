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


public class MaterialAssigner : MonoBehaviour
{
    public GameObject allObjectsInScene;
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

    public HashSet<string> dataTypes = new HashSet<string>
    {
        "SecondaryVertices_V1",
        "PrimaryVertices_V1",
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
        "EERecHits_V2",
        "EBRecHits_V2",
        "ESRecHits_V2",
        "HBRecHits_V2",
        "HERecHits_V2",
        "HORecHits_V2",
        "HFRecHits_V2",
        "MuonChambers_V1",
        "RPCRecHits_V1",
        "Tracks_V1",
        "Tracks_V2",
        "Tracks_V3",
        "Tracks_V4",
        "SuperClusters_V1",
        "CSCSegments_V1",
        "CSCSegments_V2",
        "TrackDets_V1",
        "SiPixelClusters_V1",
        "SiStripClusters_V1",
        "DTRecHits_V1",
        "DTRecSegment4D_V1",
        "CSCRecHit2Ds_V2",
        "MatchingCSCs_V1",
        "TrackingRecHits_V1",
        "CaloTowers_V2"
    };

    void Start()
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

        List<GameObject> objects = AllChilds(allObjectsInScene);
        foreach (var obj in objects)
        {
            List<GameObject> children = AllChilds(obj);
            string objectName = Path.GetFileNameWithoutExtension(obj.name);
            if (!dataTypes.Contains(objectName))
            {
                continue;
            }
            foreach (var item in children)
            {
                if (item.GetComponent<MeshRenderer>() == null)
                {
                    item.AddComponent<MeshRenderer>();
                }
                if (item.GetComponent<MeshCollider>() == null)
                {
                    item.AddComponent<MeshCollider>();
                }
                if (item.GetComponent<hoverOBJ>() == null)
                {
                    item.AddComponent<hoverOBJ>();
                }
                if (item.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>() == null)
                {
                    item.AddComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
                }
                switch (objectName)
                {
                    case "SecondaryVertices_V1":
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
                        break;

                    case "PrimaryVertices_V1":
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
                        break;

                    case "PFJets_V2":
                    case "PFJets":
                        item.GetComponent<MeshRenderer>().material = jetMaterial;
                        break;

                    case "TrackerMuons_V1":
                    case "TrackerMuons_V2":
                        item.GetComponent<MeshRenderer>().material = muonMaterial;
                        break;

                    case "GlobalMuons_V1":
                    case "GlobalMuons_V2":
                        item.GetComponent<MeshRenderer>().material = muonMaterial;
                        break;

                    case "StandaloneMuons_V1":
                    case "StandaloneMuons_V2":
                        item.GetComponent<MeshRenderer>().material = muonMaterial;
                        break;

                    case "GsfElectrons_V1":
                    case "GsfElectrons_V2":
                    case "GsfElectrons_V3":
                        item.GetComponent<MeshRenderer>().material = emMaterial;
                        break;

                    case "EERecHits_V2":
                    case "EBRecHits_V2":
                    case "ESRecHits_V2":
                        item.GetComponent<MeshRenderer>().material = emMaterial;
                        break;

                    case "HBRecHits_V2":
                    case "HERecHits_V2":
                    case "HORecHits_V2":
                    case "HFRecHits_V2":
                        item.GetComponent<MeshRenderer>().material = hadronMaterial;
                        break;

                    case "MuonChambers_V1":
                        item.GetComponent<MeshRenderer>().material = mMaterial;
                        break;

                    case "RPCRecHits_V1":
                        item.GetComponent<MeshRenderer>().material = rpcMaterial;
                        break;

                    case "Tracks_V1":
                    case "Tracks_V2":
                    case "Tracks_V3":
                    case "Tracks_V4":
                        item.GetComponent<MeshRenderer>().material = trackMaterial;
                        break;

                    case "SuperClusters_V1":
                        item.GetComponent<MeshRenderer>().material = muonMaterial;
                        break;

                    case "Vertices_V1":
                        // No material assigned for this case (no material assignment required)
                        break;

                    case "CSCSegments_V1":
                    case "CSCSegments_V2":
                        item.GetComponent<MeshRenderer>().material = cscMaterial;
                        break;

                    case "TrackDets_V1":
                        item.GetComponent<MeshRenderer>().material = tdMaterial;
                        break;

                    case "SiPixelClusters_V1":
                        item.GetComponent<MeshRenderer>().material = pixelMaterial;
                        break;

                    case "SiStripClusters_V1":
                        item.GetComponent<MeshRenderer>().material = stripMaterial;
                        break;

                    case "DTRecHits_V1":
                        item.GetComponent<MeshRenderer>().material = dtrechitMaterial;
                        break;

                    case "DTRecSegment4D_V1":
                        item.GetComponent<MeshRenderer>().material = dtrecsegmentMaterial;
                        break;

                    case "CSCRecHit2Ds_V2":
                        item.GetComponent<MeshRenderer>().material = cscrechitMaterial;
                        break;

                    case "MatchingCSCs_V1":
                        item.GetComponent<MeshRenderer>().material = matchingcscsMaterial;
                        break;

                    case "TrackingRecHits_V1":
                        item.GetComponent<MeshRenderer>().material = trackingrechitsMaterial;
                        break;

                    case "CaloTowers_V2":
                        item.GetComponent<MeshRenderer>().material = calotowerMaterial;
                        break;

                    default:
                        //print("No material assigned for object: " + objectName);
                        break;
                }
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
    void Update()
    {
        
    }
}
