using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Selection : MonoBehaviour
{
    public List<GameObject> sceneObjects;
    public TMP_Text textComponent;
    private int typeIndex;
    private int dataIndex;
    private bool controlState;
    private GameObject currentObject;
    private List<GameObject> subObjects;

    void Start()
    {
        controlState = false;
        typeIndex = 0;
        dataIndex = 0;
        textComponent.text = $"Selected Item: {sceneObjects[typeIndex].name}\nActive: True";
    }

    void Update()
    {

        OVRInput.Update();
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            //UnityEngine.Debug.Log(!sceneObjects[typeIndex].activeSelf);
            bool state = !sceneObjects[typeIndex].activeSelf;
            sceneObjects[typeIndex].SetActive(state);
            textComponent.text = $"Selected Item:{sceneObjects[typeIndex].name}\nActive: {state}\n";
        }
        if (controlState)
        {
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                dataIndex += 2;
                if (dataIndex == subObjects.Count)
                {
                    dataIndex = 0;
                }
                string dataString = GetDataFromPhysicsObject(currentObject.name, dataIndex);
                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[dataIndex].name}\n{dataString}";

            }

            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                dataIndex = 0;
                controlState = false;

                textComponent.text = $"Selected Item: {currentObject.name}\nActive: {currentObject.activeSelf}";
                currentObject = null;
                //subObjects.Clear();
                return;
            }

            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                dataIndex -= 2;
                if (dataIndex < 0)
                {
                    dataIndex = subObjects.Count - 2;
                }
                UnityEngine.Debug.Log(currentObject.name);
                string dataString = GetDataFromPhysicsObject(currentObject.name, dataIndex);

                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[dataIndex].name}\n{dataString}";
            }
        }
        else
        {
            if (OVRInput.GetDown(OVRInput.Button.Three))
            {
                controlState = true;
                currentObject = sceneObjects[typeIndex];
                subObjects = AllChilds(currentObject);

                string dataString = GetDataFromPhysicsObject(currentObject.name, dataIndex);

                textComponent.text = $"Selected Item:{currentObject.name}\nActive: {currentObject.activeSelf}\nCurrently selected subitem: {subObjects[dataIndex].name}\n{dataString}";
                return;
            }
            if (OVRInput.GetDown(OVRInput.RawButton.LIndexTrigger))
            {
                typeIndex++;
                if (typeIndex == sceneObjects.Count)
                {

                    typeIndex = 0;
                }
                textComponent.text = $"Selected Item: {sceneObjects[typeIndex].name}\nActive: {sceneObjects[typeIndex].activeSelf}";
            }
            if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger))
            {
                typeIndex -= 1;
                if (typeIndex < 0)
                {
                    typeIndex = sceneObjects.Count - 1;
                }

                textComponent.text = $"Selected Item: {sceneObjects[typeIndex].name}\nActive: {sceneObjects[typeIndex].activeSelf}";
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
    private string GetDataFromPhysicsObject(string name, int dataIndex)
    {
        string dataString;
        switch (currentObject.name)
        {
            case "PFMET":
                dataString = this.subObjects[dataIndex].GetComponent<METComponent>().GetData();
                break;
            case "PFJets":
                dataString = this.subObjects[dataIndex].GetComponent<JetComponent>().GetData();
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
                dataString = this.subObjects[dataIndex].GetComponent<ElectronComponent>().GetData();
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
                dataString = this.subObjects[dataIndex].GetComponent<TrackerMuonComponent>().GetData();
                break;
            case "globalMuons":
                dataString = this.subObjects[dataIndex].GetComponent<GlobalMuonComponent>().GetData();
                break;
            case "standaloneMuons":
                dataString = this.subObjects[dataIndex].GetComponent<StandaloneMuonComponent>().GetData();
                break;
            case "Photons": dataString = ""; break;
            case "Tracks":

                dataString = this.subObjects[dataIndex].GetComponent<TrackComponent>().GetData();
                break;
            case "Superclusters":
                dataString = this.subObjects[dataIndex].GetComponent<SuperClusterComponent>().GetData();
                break;

            default: dataString = ""; break;
        }
        return dataString;
    }
}
