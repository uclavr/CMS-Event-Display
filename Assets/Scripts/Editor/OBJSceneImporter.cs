using UnityEditor;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using Dummiesman;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

[CustomEditor(typeof(DefaultDataLoad))]
public class OBJSceneImporter : Editor
{
    public override void OnInspectorGUI()
    {
        string[] paths = {"DTRecHits_V1.obj", "DTRecSegment4D_V1.obj","CSCRecHit2Ds_V2.obj","CaloTowers_V2", "MatchingCSCs_V1.obj","CSCSegments_V2.obj","PFJets.obj","PFJets_V2.obj","TrackDets_V1.obj","TrackingRecHits_V1.obj","SiPixelClusters_V1.obj","SiStripClusters_V1.obj","EBRecHits_V2.obj","EERecHits_V2.obj", "ESRecHits_V2.obj", "RPCRecHits_V1.obj","CSCSegments_V1.obj",
                "GsfElectrons_V1.obj","GsfElectrons_V2.obj","GsfElectrons_V3.obj","HBRecHits_V2.obj","HERecHits_V2.obj","HFRecHits_V2.obj",
                "HORecHits_V2.obj","MuonChambers_V1.obj","TrackerMuons_V1.obj","TrackerMuons_V2.obj","GlobalMuons_V1.obj", "GlobalMuons_V2.obj",
                "StandaloneMuons_V1.obj","StandaloneMuons_V2.obj","Photons_V1.obj","Tracks_V1.obj","Tracks_V2.obj","Tracks_V3.obj","Tracks_V4.obj","SuperClusters_V1.obj","Vertices_V1.obj","PrimaryVertices_V1.obj","SecondaryVertices_V1.obj"};
        DrawDefaultInspector();

        if (GUILayout.Button("Import OBJs/Json from Scene Folder"))
        {
            DefaultDataLoad loader = (DefaultDataLoad)target;
            loader.METPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Models/METArrow.obj");
            if (loader.METPrefab == null)
            {
                Debug.LogError("Failed to load prefab from path: Assets/Models/METArrow.obj");
            }
            else
            {
                Debug.Log("Successfully loaded prefab: " + loader.METPrefab.name);
            }
            string scenePath = UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().path;
            string sceneDirectory = Path.GetDirectoryName(scenePath);
            string jsonFilePath = Path.Combine(sceneDirectory, "totaldata.json");
            Debug.Log("jsonFilePath: "+jsonFilePath);
            if (File.Exists(jsonFilePath))
            {
                try
                {
                    string json = File.ReadAllText(jsonFilePath);
                    TextAsset jsonText = new TextAsset(json);
                    loader.jsonText = jsonText;
                    loader.totalJson = JObject.Parse(jsonText.text);
                    LegoPlotter legoPlotter = Resources.FindObjectsOfTypeAll<LegoPlotter>()
                    .FirstOrDefault(lp => lp.gameObject.name == "CubeLEGO"); 
                    if (legoPlotter != null)
                    {
                   
                        legoPlotter.jsonText = jsonText;
                        EditorUtility.SetDirty(legoPlotter);
                    }
                    else
                    {
                        Debug.Log("legoplotter not found");
                    }
                        Debug.Log("JSON loaded successfully!");
                }
                catch (System.Exception e)
                {
                    Debug.LogError($"Failed to load JSON: {e.Message}");
                }
            }
            else
            {
                Debug.LogError("totaldata.json not found in scene folder!");
            }

            if (loader.eventObject != null && loader.eventObject.transform.childCount > 0 || loader.dataObjects.Count > 0)
            {
                Debug.LogWarning("OBJ files already imported. Skipping import.");
                return;
            }

            string[] allDirectories = Directory.GetDirectories(sceneDirectory, "*", SearchOption.AllDirectories);
            string objpath = "";
            foreach (string folder in allDirectories)
            {
                string[] objFiles = Directory.GetFiles(folder, "*.obj", SearchOption.TopDirectoryOnly);

                if (objFiles.Length > 0)
                {
                    Debug.Log($"OBJ files found in folder: {folder}");
                    objpath = folder + "\\";
                }
                else
                {
                    Debug.Log("No OBJ files found");
                }
            }


            foreach (var path in paths)
            {
                try
                {
                    FileInfo f = new FileInfo($@"{objpath}{path}");
                    if ((f.Length == 0) || f == null)
                    {
                        continue;
                    }

                    GameObject loadedObject = new OBJLoader().Load($"{objpath}{path}");
                    loader.ProcessOBJ(loadedObject, path);
                }
                catch (Exception e)
                {
                    continue;
                }
            }

            DefaultSelection selection = GameObject.FindObjectOfType<DefaultSelection>();
            if (selection != null)
            {
                selection.sceneObjects = loader.dataObjects;
                EditorUtility.SetDirty(selection); // mark scene dirty if in edit mode
            }

            EditorUtility.SetDirty(loader); 
            Debug.Log("OBJ import complete.");
        }
    }
}
