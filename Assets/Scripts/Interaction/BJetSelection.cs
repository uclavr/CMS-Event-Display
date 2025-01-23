using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class BJetSelection : MonoBehaviour
{
    public List<GameObject> sceneObjects;
    public TMP_Text textComponent;
    private int index1;
    private int index2;
    private bool controlState;
    private GameObject currentObject;
    private List<GameObject> subObjects;

    void Start()
    {
        controlState = false;
        index1 = 0;
        index2 = 0;
        textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: True";
    }

    void Update()
    {

        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //UnityEngine.Debug.Log(!sceneObjects[index1].activeSelf);
            bool state = !sceneObjects[index1].activeSelf;
            sceneObjects[index1].SetActive(state);
            textComponent.text = $"Selected Item:{sceneObjects[index1].name}\nActive: {state}\n";
        }
        if (controlState)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                index2 += 2;
                if (index2 == subObjects.Count)
                {
                    index2 = 0;
                }
                string dataString = GetDataFromPhysicsObject(currentObject.name, index2);
                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[index2].name}\n{dataString}";

            }

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                index2 = 0;
                controlState = false;
                textComponent.text = $"Selected Item: {currentObject.name}\nActive: {currentObject.activeSelf}";
                currentObject = null;
                //subObjects.Clear();
                return;
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                index2 -= 2;
                if (index2 < 0)
                {
                    index2 = subObjects.Count - 2;
                }
                UnityEngine.Debug.Log(currentObject.name);
                string dataString = GetDataFromPhysicsObject(currentObject.name, index2);

                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[index2].name}\n{dataString}";
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                controlState = true;
                currentObject = sceneObjects[index1];
                subObjects = AllChilds(currentObject);

                string dataString = GetDataFromPhysicsObject(currentObject.name, index2);

                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[index2].name}\n{dataString}";
                return;
            }
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                index1++;
                if (index1 == sceneObjects.Count)
                {

                    index1 = 0;
                }
                textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: {sceneObjects[index1].activeSelf}";
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                index1 -= 1;
                if (index1 < 0)
                {
                    index1 = sceneObjects.Count - 1;
                }

                textComponent.text = $"Selected Item: {sceneObjects[index1].name}\nActive: {sceneObjects[index1].activeSelf}";
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
            case "gsfElectrons":
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
                dataString = this.subObjects[index2].GetComponent<TrackerMuonComponent>().GetData();
                break;
            case "globalMuons":
                dataString = this.subObjects[index2].GetComponent<GlobalMuonComponent>().GetData();
                break;
            case "standaloneMuons":
                dataString = this.subObjects[index2].GetComponent<StandaloneMuonComponent>().GetData();
                break;
            case "Photons": dataString = ""; break;
            case "Tracks":

                dataString = this.subObjects[index2].GetComponent<TrackComponent>().GetData();
                break;
            case "Superclusters":
                dataString = this.subObjects[index2].GetComponent<SuperClusterComponent>().GetData();
                break;

            default: dataString = ""; break;
        }
        return dataString;
    }
}
