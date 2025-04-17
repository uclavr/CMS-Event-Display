/*
Author: Nathan Joshua
Last Updated: 11/15/2024
Note: Remember to setup the tags Destroyable and AngleUpdate in unity project if copying this code
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TMPro;
using System.Collections.Specialized;
using System.Diagnostics;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;
using System.Reflection.Emit;
using UnityEngine.EventSystems;
using Meta.Wit.LitJson;

public class LegoPlotter : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Toggle jetToggle;
    private bool isJetToggleOn = false;
    private bool jetFlag = false;
    public List<GameObject> legoPlotObjects;
    public JObject jsonFile;
    public TextAsset jsonText;
    public TextAsset hcalSegmentationData;
    private GameObject frame;
    private const float scalerX = 0.5f; // X = eta
    private const float scalerY = 0.5f;
    private const float scalerZ = 0.5f; // Z  phi
    private const float boxLengthX = 10.382f * scalerX; //5.338 // NEW
    private const float boxLengthY = 4f * scalerY;
    private const float boxLengthZ = 6.28318f * scalerZ;
    private const float axisOffset = 0.2f;
    bool[] isPlotGeneratedFor = new bool[3] { true, false, false };
    public GameObject cubeboi;
    public fileLoadMultiple loader;
    public int eventIndex;
    public static int eventFlag = 1; // start with 1 for this because we dont need to trigger the initial case

    // Start is called before the first frame update
    void Start()
    {
        GenerateLegoPlot(1, false);
    }

    void Update()
    {
        if (eventFlag == 0)
        {
            eventIndex = loader.getCurrentEvent();
            jsonFile = loader.getJson(eventIndex);
            string jsonString = jsonFile.ToString();
            jsonText = new TextAsset(jsonString);

            if (isPlotGeneratedFor[0] == true)
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(1, true);
                }
                else
                {
                    GenerateLegoPlot(1, false);
                }
            }
            else if (isPlotGeneratedFor[1] == true)
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(2, true);
                }
                else
                {
                    GenerateLegoPlot(2, false);
                }
            }
            else if (isPlotGeneratedFor[2] == true)
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(3, true);
                }
                else
                {
                    GenerateLegoPlot(3, false);
                }
            }
            eventFlag = 1;
        }

        // Making the axis labels and ticks look at you always
        if (GameObject.FindGameObjectsWithTag("AngleUpdate") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("AngleUpdate");
            foreach (GameObject go in gos)
            {
                go.transform.LookAt(Camera.main.transform);
                go.transform.Rotate(0, 180, 0);
            }
        }

        var activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();
        if (activeToggle != null)
        {
            if ((activeToggle.name == "Toggle1" && !isPlotGeneratedFor[0]) || (activeToggle.name == "Toggle1" && jetFlag == true))
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(1, true);
                    isPlotGeneratedFor[0] = true;
                    isPlotGeneratedFor[1] = false;
                    isPlotGeneratedFor[2] = false;
                    jetFlag = false;
                }
                else
                {
                    GenerateLegoPlot(1, false);
                    isPlotGeneratedFor[0] = true;
                    isPlotGeneratedFor[1] = false;
                    isPlotGeneratedFor[2] = false;
                    jetFlag = false;
                }
            }
            else if ((activeToggle.name == "Toggle2" && !isPlotGeneratedFor[1]) || (activeToggle.name == "Toggle2" && jetFlag == true))
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(2, true);
                    isPlotGeneratedFor[0] = false;
                    isPlotGeneratedFor[1] = true;
                    isPlotGeneratedFor[2] = false;
                    jetFlag = false;
                }
                else
                {
                    GenerateLegoPlot(2, false);
                    isPlotGeneratedFor[0] = false;
                    isPlotGeneratedFor[1] = true;
                    isPlotGeneratedFor[2] = false;
                    jetFlag = false;
                }

            }
            else if ((activeToggle.name == "Toggle3" && !isPlotGeneratedFor[2]) || (activeToggle.name == "Toggle3" && jetFlag == true))
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(3, true);
                    isPlotGeneratedFor[0] = false;
                    isPlotGeneratedFor[1] = false;
                    isPlotGeneratedFor[2] = true;
                    jetFlag = false;
                }
                else
                {
                    GenerateLegoPlot(3, false);
                    isPlotGeneratedFor[0] = false;
                    isPlotGeneratedFor[1] = false;
                    isPlotGeneratedFor[2] = true;
                    jetFlag = false;
                }
            }
        }
    }

    void Awake()
    {
        string jsonpath;
        if (jsonText != null) // for default events
        {
            jsonFile = (JObject)JToken.Parse(jsonText.text);
        }
        else if (UnityEngine.Application.isEditor == false)
        {
            jsonpath = @"/data/local/tmp/obj/totaldata.json"; //from headset
            print(jsonpath);
            using (StreamReader file = File.OpenText(jsonpath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonFile = (JObject)JToken.ReadFrom(reader);
                string jsonString = jsonFile.ToString();
                jsonText = new TextAsset(jsonString);
            }
        }
        else
        {
            //print("load no good :(");
            //jsonpath = @"C:\Users\uclav\Desktop\andrew\totaldata(masterclass139707779).json";
            //print(jsonpath);
            //using (StreamReader file = File.OpenText(jsonpath))
            //using (JsonTextReader reader = new JsonTextReader(file))
            //{
            //    jsonFile = (JObject)JToken.ReadFrom(reader);
            //    string jsonString = jsonFile.ToString();
            //    jsonText = new TextAsset(jsonString);
            //}
            eventIndex = loader.getCurrentEvent();
            jsonFile = loader.getJson(eventIndex);
            string jsonString = jsonFile.ToString();
            jsonText = new TextAsset(jsonString);
        }
    }

    public void jetToggleChanged()
    {
        if (jetToggle.isOn)
        {
            isJetToggleOn = true;
            jetFlag = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
        else if (!jetToggle.isOn)
        {
            isJetToggleOn = false;
            jetFlag = true;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }

    void onToggleChanged()
    {
        Toggle activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();

        if (activeToggle != null && activeToggle.isOn)
        {
            if (activeToggle.name == "Toggle1")
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(1, true); // All data case
                }
                else
                {
                    GenerateLegoPlot(1, false); // All data case
                }
            }
            if (activeToggle.name == "Toggle2")
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(2, true); // only hcal data
                }
                else
                {
                    GenerateLegoPlot(2, false); // only hcal data
                }
            }
            if (activeToggle.name == "Toggle3")
            {
                if (isJetToggleOn)
                {
                    GenerateLegoPlot(3, true); // only ecal data
                }
                else
                {
                    GenerateLegoPlot(3, false); // only ecal data
                }
            }
        }
    }

    void GenerateLegoPlot(int toggleNum, bool jetActive)
    {
        if (GameObject.FindGameObjectsWithTag("Destroyable2") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Destroyable2");
            foreach (GameObject go in gos)
                Destroy(go);
        }

        if (GameObject.FindGameObjectsWithTag("AngleUpdate") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("AngleUpdate");
            foreach (GameObject go in gos)
                Destroy(go);
        }

        JToken jsonDataHB;
        JToken jsonDataHO;
        JToken jsonDataHE;
        JToken jsonDataHF;
        JToken jsonDataEB;
        JToken jsonDataEE;
        JToken jsonDataPFJets;

        float maxEnergyHCAL = boxLengthY / 2;
        float maxEnergyECAL = boxLengthY / 2;
        //float maxEnergy = boxLengthY;
        float energyMultiplier = 1f;
        List<float> etaHCALlist = new List<float>();
        List<float> phiHCALlist = new List<float>();
        List<float> energyHCALlist = new List<float>();

        jsonDataHB = jsonFile["hbHitDatas"];
        jsonDataHO = jsonFile["hoHitDatas"];
        jsonDataHE = jsonFile["heHitDatas"];
        jsonDataHF = jsonFile["hfHitDatas"];
        jsonDataEB = jsonFile["ebHitDatas"];
        jsonDataEE = jsonFile["eeHitDatas"];
        jsonDataPFJets = jsonFile["jetDatas"];

        // Scaling and Positioning Cube in X and Y 

        Transform mainCube = GameObject.Find("CubeLEGO").GetComponent<Transform>();
        mainCube.localScale = new Vector3(scalerX, scalerY, scalerZ);
        Vector3 curPos = mainCube.localPosition;
        mainCube.localPosition = new Vector3(curPos.x, boxLengthY / 2, curPos.z);
        Vector3 framePos = mainCube.localPosition;
        LineRenderer verticalLine = GameObject.Find("CubeLEGO").GetComponentInChildren<LineRenderer>();
        verticalLine.positionCount = 2;
        Vector3[] linePoints = new Vector3[2]
        {
            new Vector3(framePos.x - boxLengthX / 2, framePos.y - boxLengthY / 2, framePos.z + boxLengthZ / 2),
            new Vector3(framePos.x - boxLengthX / 2, framePos.y + boxLengthY / 2, framePos.z + boxLengthZ / 2)
        };
        verticalLine.SetPositions(linePoints);

        if (toggleNum == 1)
        {
            // Figuring out how much to scale data in Y axis

            if (jsonDataHB != null)
            {
                foreach (JToken item in jsonDataHB)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHO != null)
            {
                foreach (JToken item in jsonDataHO)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHE != null)
            {
                foreach (JToken item in jsonDataHE)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHF != null)
            {
                foreach (JToken item in jsonDataHF)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataEB != null)
            {
                foreach (JToken item in jsonDataEB)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyECAL)
                    {
                        maxEnergyECAL = energy;
                    }
                }
            }

            if (jsonDataEE != null)
            {
                foreach (JToken item in jsonDataEE)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyECAL)
                    {
                        maxEnergyECAL = energy;
                    }
                }
            }

            float maxEnergy = maxEnergyHCAL + maxEnergyECAL;

            if (jetActive)
            {
                if (jsonDataPFJets != null) // Accounting for jets as well (Much bigger than ecal, hcal energies usually)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        if (et > maxEnergy)
                        {
                            maxEnergy = et;
                        }
                    }
                }
            }

            if (maxEnergy > boxLengthY)
            {
                energyMultiplier = boxLengthY / maxEnergy;
            }


            // Loading in HCAL Segmentation Data
            List<DataEntry> dataEntries = new List<DataEntry>();

            // Split the text asset's content into lines
            string[] lines = hcalSegmentationData.text.Split('\n');

            // Create list of data entires from hcal segmentation file
            foreach (var line in lines)
            {

                // Split each line into the values
                var parts = line.Split(' ');

                // Convert the parts to appropriate types
                int tower = int.Parse(parts[0]);
                float lowEta = float.Parse(parts[1]);
                float highEta = float.Parse(parts[2]);
                float deltaEta = float.Parse(parts[3]);
                float deltaPhi = float.Parse(parts[4]);

                // Create a new DataEntry object and add it to the list
                dataEntries.Add(new DataEntry(tower, lowEta, highEta, deltaEta, deltaPhi));
            }

            // Creating a map of the HCAL box positions to handle offsets
            List<float> hcalMap = new List<float>(); //can be done with vectors instead to include phi but phi seems to work fine for both hcal and ecal
            foreach (DataEntry data in dataEntries)
            {
                if (data.Tower != 92)
                {
                    if (data.Tower == 30)
                    {
                        hcalMap.Add(3.0515f * scalerX); //NEW
                        hcalMap.Add(-3.0515f * scalerX); //NEW
                    }
                    else
                    {
                        hcalMap.Add(((data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                        hcalMap.Add((-(data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                    }
                }
            }

            // Creating HB Lego Boxes

            if (jsonDataHB != null)
            {
                foreach (JToken item in jsonDataHB)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries); //acounting for depth segments
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);

                        }
                    }
                }
            }

            // Creating HO Lego Boxes

            if (jsonDataHO != null)
            {
                foreach (JToken item in jsonDataHO)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating HE Lego Boxes

            if (jsonDataHE != null)
            {
                foreach (JToken item in jsonDataHE)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    eta = mappedEtaValue(eta * scalerX, hcalMap); // NEW
                    eta = eta / scalerX; // Undo the scaling temporarily

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating HF Lego Boxes

            if (jsonDataHF != null)
            {
                foreach (JToken item in jsonDataHF)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    eta = mappedEtaValue(eta * scalerX, hcalMap); // NEW
                    eta = eta / scalerX; // Undo the scaling temporarily

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating EB Lego Boxes

            if (jsonDataEB != null)
            {
                foreach (JToken item in jsonDataEB)
                {
                    float delEta = 0.0175f;
                    float delPhi = 0.0175f;

                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    //eta = eta - (delEta / 2);  // Is this offset even right??
                    if (energy < Mathf.Epsilon * (float)Math.Pow(10, 36)) // preventing black squares for zero energies
                    {
                        energy = Mathf.Epsilon * (float)Math.Pow(10, 37);
                    }

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), Math.Abs(delEta), delPhi);
                    }
                    else
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), Math.Abs(delEta), delPhi);
                    }
                }
            }

            // Creating EE Lego Boxes

            if (jsonDataEE != null)
            {
                foreach (JToken item in jsonDataEE)
                {

                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();
                    float delEta = item["deltaEta"].Value<float>();
                    float delPhi = item["deltaPhi"].Value<float>();

                    //eta = eta - (delEta / 2);  // Is this offset even right??
                    if (energy < Mathf.Epsilon * (float)Math.Pow(10, 36)) // preventing black squares for zero energies
                    {
                        energy = Mathf.Epsilon * (float)Math.Pow(10, 37);
                    }

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), Math.Abs(delEta), delPhi);
                    }
                    else
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    }
                }
            }

            //Creating PFJet circles

            if (jsonDataPFJets != null)
            {
                if (jetActive)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        et = et * energyMultiplier;
                        float eta = item["eta"].Value<float>();
                        float phi = item["phi"].Value<float>();
                        float radius = 0.4f; // this is for akpf4 jets only as of now

                        int cutType = 1;
                        if (phi > (Mathf.PI - radius))
                        {
                            cutType = 2;
                        }
                        else if (phi < (-Mathf.PI + radius))
                        {
                            cutType = 3;
                        }

                        eta = eta * scalerX; // Scaling down in Horizontal Plane
                        phi = phi * scalerZ;
                        //radius = radius * scalerX;//Mathf.Sqrt(scalerX * scalerX + scalerZ * scalerZ);
                        InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi), radius);
                        if (cutType == 2)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi - (2 * Mathf.PI * scalerZ)), radius);
                        }
                        else if (cutType == 3)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi + (2 * Mathf.PI * scalerZ)), radius);
                        }
                    }
                }
            }
        }
        else if (toggleNum == 2)
        {
            // Figuring out how much to scale data in Y axis

            if (jsonDataHB != null)
            {
                foreach (JToken item in jsonDataHB)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHO != null)
            {
                foreach (JToken item in jsonDataHO)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHE != null)
            {
                foreach (JToken item in jsonDataHE)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            if (jsonDataHF != null)
            {
                foreach (JToken item in jsonDataHF)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyHCAL)
                    {
                        maxEnergyHCAL = energy;
                    }
                }
            }

            float maxEnergy = maxEnergyHCAL + maxEnergyECAL;

            if (jetActive)
            {
                if (jsonDataPFJets != null) // Accounting for jets as well (Much bigger than ecal, hcal energies usually)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        if (et > maxEnergy)
                        {
                            maxEnergy = et;
                        }
                    }
                }
            }

            if (maxEnergy > boxLengthY)
            {
                energyMultiplier = boxLengthY / maxEnergy;
            }

            // Loading in HCAL Segmentation Data

            List<DataEntry> dataEntries = new List<DataEntry>();

            // Split the text asset's content into lines
            string[] lines = hcalSegmentationData.text.Split('\n');

            foreach (var line in lines)
            {

                // Split each line into the values
                var parts = line.Split(' ');

                // Convert the parts to appropriate types
                int tower = int.Parse(parts[0]);
                float lowEta = float.Parse(parts[1]);
                float highEta = float.Parse(parts[2]);
                float deltaEta = float.Parse(parts[3]);
                float deltaPhi = float.Parse(parts[4]);

                // Create a new DataEntry object and add it to the list
                dataEntries.Add(new DataEntry(tower, lowEta, highEta, deltaEta, deltaPhi));
            }

            // Creating a map of the HCAL box positions to handle offsets

            List<float> hcalMap = new List<float>(); //can be done with vectors instead to include phi but phi seems to work fine for both hcal and ecal
            foreach (DataEntry data in dataEntries)
            {
                if (data.Tower != 92)
                {
                    if (data.Tower == 30)
                    {
                        hcalMap.Add(3.0515f * scalerX); //NEW
                        hcalMap.Add(-3.0515f * scalerX); //NEW
                    }
                    else
                    {
                        hcalMap.Add(((data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                        hcalMap.Add((-(data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                    }
                }
            }

            // Creating HB Lego Boxes

            if (jsonDataHB != null)
            {
                foreach (JToken item in jsonDataHB)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries); //acounting for depth segments
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating HO Lego Boxes

            if (jsonDataHO != null)
            {
                foreach (JToken item in jsonDataHO)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating HE Lego Boxes

            if (jsonDataHE != null)
            {
                foreach (JToken item in jsonDataHE)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    eta = mappedEtaValue(eta * scalerX, hcalMap); // NEW
                    eta = eta / scalerX; // Undo the scaling temporarily

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }

            // Creating HF Lego Boxes

            if (jsonDataHF != null)
            {
                foreach (JToken item in jsonDataHF)
                {
                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    eta = mappedEtaValue(eta * scalerX, hcalMap); // NEW
                    eta = eta / scalerX; // Undo the scaling temporarily

                    (float, float) hcalSeg = returnHCALsegmentation(eta, dataEntries);
                    float delEta = hcalSeg.Item1;
                    float delPhi = hcalSeg.Item2;

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    /*
                    InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    etaHCALlist.Add(eta);
                    phiHCALlist.Add(phi);
                    energyHCALlist.Add(energy);
                    */

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy + hcalHeight);
                        }
                    }
                    else
                    {
                        InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                        if (energy > 0) // ensure no stacking on negative energies
                        {
                            etaHCALlist.Add(eta);
                            phiHCALlist.Add(phi);
                            energyHCALlist.Add(energy);
                        }
                    }
                }
            }
            if (jsonDataPFJets != null)
            {
                if (jetActive)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        et = et * energyMultiplier;
                        float eta = item["eta"].Value<float>();
                        float phi = item["phi"].Value<float>();
                        float radius = 0.4f; // this is for akpf4 jets only as of now

                        int cutType = 1;
                        if (phi > (Mathf.PI - radius))
                        {
                            cutType = 2;
                        }
                        else if (phi < (-Mathf.PI + radius))
                        {
                            cutType = 3;
                        }

                        eta = eta * scalerX; // Scaling down in Horizontal Plane
                        phi = phi * scalerZ;
                        //radius = radius * scalerX;//Mathf.Sqrt(scalerX * scalerX + scalerZ * scalerZ);
                        InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi), radius);
                        if (cutType == 2)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi - (2 * Mathf.PI * scalerZ)), radius);
                        }
                        else if (cutType == 3)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi + (2 * Mathf.PI * scalerZ)), radius);
                        }
                    }
                }
            }
        }
        else
        {
            // Figuring out how much to scale data in Y axis

            if (jsonDataEB != null)
            {
                foreach (JToken item in jsonDataEB)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyECAL)
                    {
                        maxEnergyECAL = energy;
                    }
                }
            }

            if (jsonDataEE != null)
            {
                foreach (JToken item in jsonDataEE)
                {
                    float energy = item["energy"].Value<float>();
                    if (energy > maxEnergyECAL)
                    {
                        maxEnergyECAL = energy;
                    }
                }
            }

            float maxEnergy = maxEnergyHCAL + maxEnergyECAL;

            if (jetActive)
            {
                if (jsonDataPFJets != null) // Accounting for jets as well (Much bigger than ecal, hcal energies usually)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        if (et > maxEnergy)
                        {
                            maxEnergy = et;
                        }
                    }
                }
            }

            if (maxEnergy > boxLengthY)
            {
                energyMultiplier = boxLengthY / maxEnergy;
            }

            // Loading in HCAL Segmentation Data

            List<DataEntry> dataEntries = new List<DataEntry>();

            // Split the text asset's content into lines
            string[] lines = hcalSegmentationData.text.Split('\n');

            foreach (var line in lines)
            {

                // Split each line into the values
                var parts = line.Split(' ');

                // Convert the parts to appropriate types
                int tower = int.Parse(parts[0]);
                float lowEta = float.Parse(parts[1]);
                float highEta = float.Parse(parts[2]);
                float deltaEta = float.Parse(parts[3]);
                float deltaPhi = float.Parse(parts[4]);

                // Create a new DataEntry object and add it to the list
                dataEntries.Add(new DataEntry(tower, lowEta, highEta, deltaEta, deltaPhi));
            }

            // Creating a map of the HCAL box positions to handle offsets

            List<float> hcalMap = new List<float>(); //can be done with vectors instead to include phi but phi seems to work fine for both hcal and ecal
            foreach (DataEntry data in dataEntries)
            {
                if (data.Tower != 92)
                {
                    if (data.Tower == 30)
                    {
                        hcalMap.Add(3.0515f * scalerX); //NEW
                        hcalMap.Add(-3.0515f * scalerX); //NEW
                    }
                    else
                    {
                        hcalMap.Add(((data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                        hcalMap.Add((-(data.HighEta + data.LowEta) / 2) * scalerX); //NEW
                    }
                }
            }

            // Creating EB Lego Boxes

            if (jsonDataEB != null)
            {
                foreach (JToken item in jsonDataEB)
                {
                    float delEta = 0.0175f;
                    float delPhi = 0.0175f;

                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();

                    //eta = eta - (delEta / 2);  // Is this offset even right??
                    if (energy < Mathf.Epsilon * (float)Math.Pow(10, 36)) // preventing black squares for zero energies
                    {
                        energy = Mathf.Epsilon * (float)Math.Pow(10, 37);
                    }

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), Math.Abs(delEta), delPhi);
                    }
                    else
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), Math.Abs(delEta), delPhi);
                    }
                }
            }

            // Creating EE Lego Boxes

            if (jsonDataEE != null)
            {
                foreach (JToken item in jsonDataEE)
                {

                    float energy = item["energy"].Value<float>();
                    energy = energy * energyMultiplier;
                    float eta = item["eta"].Value<float>();
                    float phi = item["phi"].Value<float>();
                    float delEta = item["deltaEta"].Value<float>(); ;
                    float delPhi = item["deltaPhi"].Value<float>(); ;

                    //eta = eta - (delEta / 2);  // Is this offset even right??
                    if (energy < Mathf.Epsilon * (float)Math.Pow(10, 36)) // preventing black squares for zero energies
                    {
                        energy = Mathf.Epsilon * (float)Math.Pow(10, 37);
                    }

                    eta = eta * scalerX; // Scaling down in Horizontal Plane //NEW
                    phi = phi * scalerZ;
                    delEta = delEta * scalerX;
                    delPhi = delPhi * scalerZ;

                    float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist, dataEntries);
                    if (hcalHeight != 10000)
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), Math.Abs(delEta), delPhi);
                    }
                    else
                    {
                        InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), delEta, delPhi);
                    }
                }
            }
            if (jsonDataPFJets != null)
            {
                if (jetActive)
                {
                    foreach (JToken item in jsonDataPFJets)
                    {
                        float et = item["et"].Value<float>();
                        et = et * energyMultiplier;
                        float eta = item["eta"].Value<float>();
                        float phi = item["phi"].Value<float>();
                        float radius = 0.4f; // this is for akpf4 jets only as of now

                        int cutType = 1;
                        if (phi > (Mathf.PI - radius))
                        {
                            cutType = 2;
                        }
                        else if (phi < (-Mathf.PI + radius))
                        {
                            cutType = 3;
                        }

                        eta = eta * scalerX; // Scaling down in Horizontal Plane
                        phi = phi * scalerZ;
                        //radius = radius * scalerX;//Mathf.Sqrt(scalerX * scalerX + scalerZ * scalerZ);
                        InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi), radius);
                        if (cutType == 2)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi - (2 * Mathf.PI * scalerZ)), radius);
                        }
                        else if (cutType == 3)
                        {
                            InstantiatePFJetCylinder(et, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, et / 2, phi + (2 * Mathf.PI * scalerZ)), radius);
                        }
                    }
                }
            }
        }

        // Creating Axis Labels and Ticks

        string etaSymbol = "\u03B7";
        string phiSymbol = "\u03A6";
        CreateLabel(framePos + new Vector3(0, -boxLengthY / 2, -boxLengthZ / 2 - (axisOffset * 2)), etaSymbol); // eta label
        for (float j = 0; j < 8 + 1; j++) // eta ticks
        {
            float labelValue = j / (8 / boxLengthX) - boxLengthX / 2;
            CreateLabel(framePos + new Vector3(j / (8 / boxLengthX) - boxLengthX / 2, -boxLengthY / 2, -boxLengthZ / 2 - axisOffset), (Math.Round(labelValue / scalerX, 2)).ToString()); //eta
        }
        CreateLabel(framePos + new Vector3(-boxLengthX / 2, boxLengthY / 2 + (axisOffset * 2), boxLengthZ / 2 + (axisOffset * 2)), "E (GeV)"); // energy label
        for (float j = 0; j < 5 + 1; j++) // energy ticks
        {
            float labelValue = j / (5 / boxLengthY);
            CreateLabel(framePos + new Vector3(-boxLengthX / 2 - axisOffset, labelValue - boxLengthY / 2, boxLengthZ / 2 + axisOffset), Math.Round((labelValue / energyMultiplier), 2).ToString()); //energy
            createFrameLines(framePos.y + labelValue - boxLengthY / 2, framePos);
        }
        CreateLabel(framePos + new Vector3(-boxLengthX / 2 - (axisOffset * 2), -boxLengthY / 2, 0), phiSymbol); // phi label
        for (float j = 0; j < 5 + 1; j++) // phi ticks
        {
            float labelValue = j / (5 / boxLengthZ) - boxLengthZ / 2;
            CreateLabel(framePos + new Vector3(-boxLengthX / 2 - axisOffset, -boxLengthY / 2, j / (5 / boxLengthZ) - boxLengthZ / 2), Math.Round((labelValue / scalerZ), 2).ToString()); //phi
            //float labelValue = j / (5 / boxLengthZ);
            //CreateLabel(framePos + new Vector3(-boxLengthX / 2 - 0.2f, -boxLengthY / 2, labelValue - boxLengthZ / 2 - 0.2f), (labelValue).ToString()); //phi
        }
    }

    void CreateLabel(Vector3 position, string text)
    {
        GameObject label = Instantiate(legoPlotObjects[1], position, Quaternion.identity);
        label.tag = "AngleUpdate";
        TextMeshPro labelTMP = label.GetComponent<TextMeshPro>();
        labelTMP.text = text;
        labelTMP.color = Color.white;
        label.transform.localPosition = position; // Ensure the local position is correct relative to plotGenerator
        label.transform.LookAt(Camera.main.transform);
        label.transform.Rotate(0, 180, 0);
        label.transform.SetParent(cubeboi.transform);
    }

    void InstantiateECALBox(float height, Vector3 position, double delta_eta, float delta_phi)
    {
        GameObject box = Instantiate(legoPlotObjects[0]);
        if (height < 0.0f)
        {
            box.transform.GetComponent<Renderer>().material.color = new Color32(0, 255, 255, 131); // for negative energies use turquoise
        }
        box.transform.position = position;
        Vector3 currentScale = box.transform.localScale;
        //box.transform.localScale = new Vector3(currentScale.x, height, currentScale.z);
        float delEta = (float)delta_eta;
        box.transform.localScale = new Vector3(delEta, height, delta_phi);
        box.tag = "Destroyable2";
        box.transform.SetParent(cubeboi.transform);
    }

    void InstantiateHCALBox(float height, Vector3 position, float delta_eta, float delta_phi)
    {
        //float segmentationVal = 0.087f;
        GameObject box = Instantiate(legoPlotObjects[0]);
        box.transform.GetComponent<Renderer>().material.color = new Color32(255, 0, 0, 220);
        if (height < 0.0f)
        {
            box.transform.GetComponent<Renderer>().material.color = new Color32(255, 100, 0, 220); // for negative energies use orange
        }
        box.transform.position = position;
        //Vector3 currentScale = box.transform.localScale;
        box.transform.localScale = new Vector3(delta_eta, height, delta_phi);
        box.tag = "Destroyable2";
        box.transform.SetParent(cubeboi.transform);
    }

    void InstantiatePFJetCylinder(float height, Vector3 position, float radius) //annoyingly, unity's cylinder scales twice as fast as the cube
    {
        GameObject cylinder = Instantiate(legoPlotObjects[2]);
        cylinder.transform.position = position; //center + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(0, height/2, 0);
        cylinder.transform.localScale = new Vector3(radius, height / 2, radius);
        cylinder.tag = "Destroyable2";
        cylinder.transform.SetParent(cubeboi.transform);
    }

    float checkInsideHCAL(float etaECAL, float phiECAL, List<float> etaHCAL, List<float> phiHCAL, List<float> energyHCAL, List<DataEntry> dataEntries) // checks if ECAL box is inside HCAL box and returns height
    {
        //float hcalBoxGranularity = 0.087f;
        (float, float) hcalSeg;
        float hcalBoxGranularityEta;
        float hcalBoxGranularityPhi;
        for (int i = etaHCAL.Count - 1; i >= 0; i--)
        {
            hcalSeg = returnHCALsegmentation(etaHCAL[i] / scalerX, dataEntries);
            hcalBoxGranularityEta = hcalSeg.Item1 * scalerX; // scales the segmentation
            hcalBoxGranularityPhi = hcalSeg.Item2 * scalerZ;
            if (etaECAL >= (etaHCAL[i] - hcalBoxGranularityEta / 2) && etaECAL <= (etaHCAL[i] + hcalBoxGranularityEta / 2))
            {
                if (phiECAL >= (phiHCAL[i] - hcalBoxGranularityPhi / 2) && phiECAL <= (phiHCAL[i] + hcalBoxGranularityPhi / 2))
                {
                    return energyHCAL[i];
                }
            }
        }
        return 10000;
    }

    float mappedEtaValue(float eta, List<float> map)
    {
        float min_eta_diff = 100;
        float mapped_eta = eta;
        foreach (float mapElem in map)
        {
            if (Math.Abs(eta - mapElem) < min_eta_diff)
            {
                min_eta_diff = Math.Abs(eta - mapElem);
                mapped_eta = mapElem;
            }
        }
        return mapped_eta;
    }
    (float, float) returnHCALsegmentation(float etaVal, List<DataEntry> dataEntries)
    {
        if (etaVal < 0)
        {
            etaVal = -etaVal;  //Handling negative eta values
        }
        foreach (DataEntry data in dataEntries)
        {
            if (etaVal > 2.868f && etaVal < 2.964f)
            {
                float deltaEta = 0.096f;
                float deltaPhi = 0.1745329252f; // 10 degrees
                return (deltaEta, deltaPhi);
            }
            else
            {
                if (etaVal > data.LowEta && etaVal < data.HighEta)
                {
                    float deltaEta = data.DeltaEta;
                    float deltaPhi = data.DeltaPhi * ((float)Math.PI / 180f);
                    return (deltaEta, deltaPhi);
                }
            }
        }
        return (0, 0); // should never reach this stage
    }
    void createFrameLines(float height, Vector3 boxPosition)
    {
        GameObject lineObject = new GameObject("LineObject_" + height);
        lineObject.tag = "Destroyable2";
        Material lineMaterial = Resources.Load<Material>("legoFrameLines");
        LineRenderer lineRenderer = lineObject.AddComponent<LineRenderer>();
        lineRenderer.material.color = Color.white;
        lineRenderer.positionCount = 3;
        Vector3[] points = new Vector3[3]
        {
            new Vector3(boxPosition.x - boxLengthX / 2, height + boxPosition.y - boxLengthY / 2, boxPosition.z - boxLengthZ / 2),
            new Vector3(boxPosition.x - boxLengthX / 2, height + boxPosition.y - boxLengthY / 2, boxPosition.z + boxLengthZ / 2),
            new Vector3(boxPosition.x + boxLengthX / 2, height + boxPosition.y - boxLengthY / 2, boxPosition.z + boxLengthZ / 2)
        };
        lineRenderer.SetPositions(points);
        lineRenderer.material = lineMaterial;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
        lineObject.transform.SetParent(cubeboi.transform);
    }
}


public class DataEntry
{
    public int Tower { get; private set; }
    public float LowEta { get; private set; }
    public float HighEta { get; private set; }
    public float DeltaEta { get; private set; }
    public float DeltaPhi { get; private set; }

    public DataEntry(int index, float value1, float value2, float value3, float value4)
    {
        Tower = index;
        LowEta = value1;
        HighEta = value2;
        DeltaEta = value3;
        DeltaPhi = value4;
    }

    public override string ToString()
    {
        return $"{Tower}: ({LowEta} - {HighEta}), {DeltaEta}, {DeltaPhi}";
    }
}



/* Some useful old code

1. Old math to compute HCAL segmentation (only delta_eta works)

foreach (JToken item in jsonDataHB)
        {
            // (THE OLD MATH TO COMPUTE DELTA_ETA)

            Vector3 front_1 = new Vector3(item["front_1"][0].Value<float>(), item["front_1"][1].Value<float>(), item["front_1"][2].Value<float>());
            Vector3 front_2 = new Vector3(item["front_2"][0].Value<float>(), item["front_2"][1].Value<float>(), item["front_2"][2].Value<float>());
            Vector3 front_3 = new Vector3(item["front_3"][0].Value<float>(), item["front_3"][1].Value<float>(), item["front_3"][2].Value<float>());
            Vector3 front_4 = new Vector3(item["front_4"][0].Value<float>(), item["front_4"][1].Value<float>(), item["front_4"][2].Value<float>());

            double sin_theta1 = Math.Sqrt(Math.Pow(front_1.x, 2) + Math.Pow(front_1.y, 2)) / front_1.magnitude;
            double cos_theta1 = front_1.z / front_1.magnitude;
            double sin_theta4 = Math.Sqrt(Math.Pow(front_4.x, 2) + Math.Pow(front_4.y, 2)) / front_4.magnitude;
            double cos_theta4 = front_4.z / front_4.magnitude;

            double delEta = Math.Log(((1 - cos_theta4) / (1 - cos_theta1)) * (sin_theta1 / sin_theta4));
            float delPhi = 0.087f;

            //UnityEngine.Debug.Log($"{delta_eta} vs {item["deltaEta"]}");
            //double delta_phi = Math.Acos(DotProduct(front_1, front_2) / (front_1.magnitude * front_2.magnitude));
            //UnityEngine.Debug.Log($"{delta_phi} delta_phi by dotprod");
            //UnityEngine.Debug.Log($"{delta_phi} delta_phi by dotprod");

            //

            float energy = item["energy"].Value<float>();
            energy = energy * energyMultiplier;
            float eta = item["eta"].Value<float>();
            float phi = item["phi"].Value<float>();

            InstantiateHCALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), Math.Abs(delEta), delPhi);
            etaHCALlist.Add(eta);
            phiHCALlist.Add(phi);
            energyHCALlist.Add(energy);
        }

2. Old math to compute ECAL segmentation

foreach (JToken item in jsonDataEB)
        {
            //
            var front_1 = new Vector3(item["front_1"][0].Value<float>(), item["front_1"][1].Value<float>(), item["front_1"][2].Value<float>());
            var front_2 = new Vector3(item["front_2"][0].Value<float>(), item["front_2"][1].Value<float>(), item["front_2"][2].Value<float>());
            var front_3 = new Vector3(item["front_3"][0].Value<float>(), item["front_3"][1].Value<float>(), item["front_3"][2].Value<float>());
            var front_4 = new Vector3(item["front_4"][0].Value<float>(), item["front_4"][1].Value<float>(), item["front_4"][2].Value<float>());

            double sin_theta1 = Math.Sqrt(Math.Pow(front_1.x, 2) + Math.Pow(front_1.y, 2)) / front_1.magnitude;
            double cos_theta1 = front_1.z / front_1.magnitude;
            double sin_theta2 = Math.Sqrt(Math.Pow(front_2.x, 2) + Math.Pow(front_2.y, 2)) / front_2.magnitude;
            double cos_theta2 = front_2.z / front_2.magnitude;

            double delEta = Math.Log(((1 - cos_theta2) / (1 - cos_theta1)) * (sin_theta1 / sin_theta2));
            float delPhi = 0.0175f;

            //UnityEngine.Debug.Log($"{delta_eta} vs {item["deltaEta"]}");
            //

            float energy = item["energy"].Value<float>();
            energy = energy * energyMultiplier;
            float eta = item["eta"].Value<float>();
            float phi = item["phi"].Value<float>();

            float hcalHeight = checkInsideHCAL(eta, phi, etaHCALlist, phiHCALlist, energyHCALlist);
            if (hcalHeight != 10000)
            {
                InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi) + new Vector3(0, hcalHeight, 0), Math.Abs(delEta), delPhi);
            }
            else
            {
                InstantiateECALBox(energy, framePos + new Vector3(0, -boxLengthY / 2, 0) + new Vector3(eta, energy / 2, phi), Math.Abs(delEta), delPhi);
            }
        }
*/