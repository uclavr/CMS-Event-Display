using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Net;

public class CustomGeneral : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> sceneObjects;
    public TMP_Text textComponent;
    private bool state;
    private int index1;
    private int index2;
    private bool controlState;
    private GameObject currentObject;
    private List<GameObject> subObjects;
    public GameObject CutCanvas;
    //public GameObject Event;

    public bool secondaryHandState;
    public bool primaryHandState;
    public bool thumbState;
    public bool threeState;
    public bool fourState;
    public bool indexState;
    void Start()
    {
        secondaryHandState = false;
        primaryHandState = false;
        indexState = false;
        thumbState = false;
        threeState = false;
        fourState = false;
        state = true;
        index1 = 0;
        index2 = 0;
        sceneObjects = GameObject.Find("Loader").GetComponent<fileLoad>().allObjects;
        UnityEngine.Debug.Log(sceneObjects[index1].name);
        textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: {sceneObjects[index1].activeSelf}";
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
    private string GetDataFromPhysicsObject(string name, int index2)
    {
        string dataString;
        switch (currentObject.name)
        {
            case "PFMET":
                dataString = this.subObjects[index2].GetComponent<METComponent>().GetData();
                break;
            case "PFJets":
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
            case "GsfElectrons_V2":
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
            case "TrackerMuons":
            case "TrackerMuons_V2":
                dataString = this.subObjects[index2].GetComponent<TrackerMuonComponent>().GetData();
                break;
            case "globalMuons":
            case "GlobalMuons_V1":
            case "GlobalMuons_V2":
                dataString = this.subObjects[index2].GetComponent<GlobalMuonComponent>().GetData();
                break;
            case "standaloneMuons":
            case "StandaloneMuons_V2":
                dataString = this.subObjects[index2].GetComponent<StandaloneMuonComponent>().GetData();
                break;
            case "Photons": dataString = ""; break;
            case "Tracks":
            case "Tracks_V3":
                dataString = this.subObjects[index2].GetComponent<TrackComponent>().GetData();
                break;
            case "SuperClusters_V1":
                dataString = this.subObjects[index2].GetComponent<SuperClusterComponent>().GetData();
                break;

            default: dataString = ""; break;
        }
        return dataString;
    }
    // Update is called once per frame
    void Update()
    {
        
        OVRInput.Update();
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.1f, transform.position.z);
        }

        if (OVRInput.Get(OVRInput.Button.One))
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
        }
        if (OVRInput.Get(OVRInput.Button.Start))
        {
            SceneManager.LoadScene("Main menu");
        }
        if (OVRInput.Get(OVRInput.Button.PrimaryThumbstick)&&!thumbState)
        {
            bool currentState = CutCanvas.activeSelf;
            thumbState = true;
            //Flip it
            currentState = !currentState;

            //Set the current State to the flipped value
            CutCanvas.SetActive(currentState);
        } else if (!OVRInput.Get(OVRInput.Button.PrimaryThumbstick)) { thumbState = false; }
        if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)&&!secondaryHandState)
            {
            
            secondaryHandState = true;
                UnityEngine.Debug.Log("shit has been called");
                index2 = 0;
                index1 += 1; UnityEngine.Debug.Log(index1);
                if (index1 == sceneObjects.Count)
                {
                    index1 = 0;
                }
                currentObject = sceneObjects[index1];
                state = currentObject.activeSelf;
                UnityEngine.Debug.Log(state);
                textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: {state}\n";
                subObjects = AllChilds(currentObject);
            } else if (!OVRInput.Get(OVRInput.Button.SecondaryHandTrigger)){ secondaryHandState = false; }

            if (OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)&&!primaryHandState)
            {
            //if (primaryHandState == true) { goto startPoint; }
            primaryHandState = true;
                index2 = 0;
                index1 -= 1;
                if (index1 < 0)
                {
                    index1 = sceneObjects.Count - 1;
                }

                currentObject = sceneObjects[index1];
                state = currentObject.activeSelf;
                textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: {state}\n";

                subObjects = AllChilds(currentObject);
        }
        else if (!OVRInput.Get(OVRInput.Button.PrimaryHandTrigger)) { primaryHandState = false; }

            if (OVRInput.Get(OVRInput.Button.Four)&&!fourState)
            {
                index2 += 1;
                fourState = true;
                if (index2 == subObjects.Count)
                {
                    index2 = 0;
                }
                else if (subObjects.Count == 1)
                {
                    index2 = 0;
                }
                string dataString = GetDataFromPhysicsObject(currentObject.name, index2);
                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[index2].name}\n{dataString}";

            } else if (!OVRInput.Get(OVRInput.Button.Four)) { fourState = false; }

            if (OVRInput.Get(OVRInput.Button.Three)&&!threeState)
            {
            threeState = true;
                index2 -= 1;
                if (index2 < 0 && subObjects.Count >= 1)
                {
                    index2 = subObjects.Count - 1;
                }
                else if (subObjects.Count == 1)
                {
                    index2 = 0;
                }
                UnityEngine.Debug.Log(index2);
                string dataString = GetDataFromPhysicsObject(currentObject.name, index2);
                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[index2].name}\n{dataString}";

            }else if (!OVRInput.Get(OVRInput.Button.Three)) { threeState = false; }

            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)&&!indexState)
            {
            indexState = true;
                //UnityEngine.Debug.Log(!sceneObjects[index1].activeSelf);
                state = !sceneObjects[index1].activeSelf;
                sceneObjects[index1].SetActive(state);
                textComponent.text = $"Selected Item:{sceneObjects[index1].name}\nActive: {state}\n";
            }else if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger)) { indexState = false; }
        

        
    



    }

}
