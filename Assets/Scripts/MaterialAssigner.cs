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
using UnityEngine.XR.Interaction.Toolkit;

public class MaterialAssigner : MonoBehaviour
{
    public GameObject allObjectsInScene;

    void Start()
    {
        print("started materialassigner");
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
            foreach (var item in children)
            {
                item.AddComponent<MeshCollider>();
                item.AddComponent<hoverOBJ>();
                item.AddComponent<XRSimpleInteractable>();
                item.AddComponent<MeshRenderer>();
                switch (objectName)
                {
                    case "SecondaryVertices_V1":
                        item.GetComponent<MeshRenderer>().material = secondaryVertexMaterial;
                        break;

                    case "PrimaryVertices_V1":
                        item.GetComponent<MeshRenderer>().material = vertexMaterial;
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
