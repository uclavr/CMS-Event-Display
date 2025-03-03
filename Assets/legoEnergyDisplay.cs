using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Diagnostics;
using System.Collections.Generic;
using Meta.Wit.LitJson;
using System.IO;

public class legoEnergyDisplay : MonoBehaviour
{
    private const float scalerY = 0.5f;
    private const float boxLengthY = 4f * scalerY;
    public static JObject jsonFile;
    public static TextAsset jsonText;
    public GameObject scaleTextPrefab; // Reference to the Text prefab
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable grabInteractable;
    private Transform objectTransform;
    public static GameObject cubeLego;
    public static ToggleGroup toggleGroup;
    public static Toggle jetToggle;
    private bool jetActive;
    private int toggleVal = 1;
    public static int flag1 = 0;
    public static int flag2 = 0;


    // IMPORTANT NOTE: STATIC VARIABLES ARE RESET IN RESETSTATICVARIABLES.CS SCRIPT TO ACCOMODATE SCENE MANAGEMENT
    void Start()
    {
        // Code that does the actual hovering stuff
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();
        objectTransform = transform;
        grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        grabInteractable.hoverExited.AddListener(OnHoverExited);
    }

    private void Update()
    {
        if (flag1 == 0)
        {
            if (GameObject.Find("CubeLEGO") != null)
            {
                cubeLego = GameObject.Find("CubeLEGO");
                toggleGroup = cubeLego.GetComponent<LegoPlotter>().toggleGroup;
                jetToggle = cubeLego.GetComponent<LegoPlotter>().jetToggle;
                //toggleGroup = GameObject.Find("CubeLEGO").GetComponent<LegoPlotter>().toggleGroup;

                flag1 = 1;
            }
        }
        if (jetToggle != null)
        {
            jetActive = jetToggle.isOn;
        }
        if (toggleGroup != null)
        {
            var activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();
            if (activeToggle != null)
            {
                if (activeToggle.name == "Toggle1")
                {
                    toggleVal = 1;
                }
                else if (activeToggle.name == "Toggle2")
                {
                    toggleVal = 2;
                }
                else
                {
                    toggleVal = 3;
                }
            }
        }
    }

    void Awake()
    {
        //jsonFile = (JObject)JToken.Parse(jsonText.text);
        //JToken jsonDataHB;
        //JToken jsonDataEB;
    }

    float CalculateEnergyMultiplier() // Code to figure out the height scaling
    {
        JToken jsonDataHB;
        JToken jsonDataHO;
        JToken jsonDataHE;
        JToken jsonDataHF;
        JToken jsonDataEB;
        JToken jsonDataEE;
        JToken jsonDataPFJets;
        float maxEnergyHCAL = boxLengthY / 2;
        float maxEnergyECAL = boxLengthY / 2;
        float energyMultiplier = 1f;
        float delta_height = Mathf.Epsilon * (float)Math.Pow(10, 37);
        //jsonFile = cubeLego.GetComponent<LegoPlotter>().jsonFile;
        //jsonFile = GameObject.Find("DataInteractionManager").GetComponent<LegoPlotScript>().jsonFile;  // IN THE FINAL VERSION USE THIS
        if (flag2 == 0)
        {
            if (GameObject.Find("CubeLEGO") != null)
            {
                jsonText = GameObject.Find("CubeLEGO").GetComponent<LegoPlotter>().jsonText;
                jsonFile = JObject.Parse(jsonText.text);
                flag2 = 1;
            }
        }
        jsonDataHB = jsonFile["hbHitDatas"];
        jsonDataHO = jsonFile["hoHitDatas"];
        jsonDataHE = jsonFile["heHitDatas"];
        jsonDataHF = jsonFile["hfHitDatas"];
        jsonDataEB = jsonFile["ebHitDatas"];
        jsonDataEE = jsonFile["eeHitDatas"];
        jsonDataPFJets = jsonFile["jetDatas"];

        if (toggleVal==1)
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
            return energyMultiplier;
        }

        else if (toggleVal==2)
        {
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
            return energyMultiplier;
        }
        else
        {
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
            return energyMultiplier;
        }
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        float energyMultiplier = CalculateEnergyMultiplier();
        GameObject instantiatedText = Instantiate(scaleTextPrefab, transform.position, Quaternion.identity);
        instantiatedText.tag = "Destroyable";
        TextMeshPro scaleText = instantiatedText.GetComponent<TextMeshPro>();
        if (objectTransform.gameObject.name == "legoJetPrefab(Clone)")
        {
            scaleText.text = $"{Math.Round(objectTransform.localScale.y * scalerY * 2/ energyMultiplier, 2)} GeV";
        }
        else
        {
            scaleText.text = $"{Math.Round(objectTransform.localScale.y * scalerY / energyMultiplier, 2)} GeV";
        }        

        // Position the text above the object
        //Vector3 worldPosition = objectTransform.position + Vector3.up * (objectTransform.localScale.y + 0.5f);
        scaleText.color = Color.white;
        instantiatedText.transform.position = objectTransform.position + Vector3.up * (objectTransform.localScale.y / 2 + 0.3f);
        instantiatedText.transform.LookAt(Camera.main.transform);
        instantiatedText.transform.Rotate(0, 180, 0);
        instantiatedText.SetActive(true);
    }


    void OnHoverExited(HoverExitEventArgs args)
    {
        if (GameObject.FindGameObjectsWithTag("Destroyable") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Destroyable");
            foreach (GameObject go in gos)
                Destroy(go);
        }
    }
}