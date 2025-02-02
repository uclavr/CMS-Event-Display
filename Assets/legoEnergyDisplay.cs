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
    private XRSimpleInteractable grabInteractable;
    private Transform objectTransform;
    public static ToggleGroup toggleGroup;
    private int toggleVal = 1;
    private static int flag1 = 0;
    private static int flag2 = 0;

    void Start()
    {
        // Code that does the actual hovering stuff
        //toggleGroup = GameObject.Find("ToggleGroupContainer").GetComponent<ToggleGroup>();
        grabInteractable = GetComponent<XRSimpleInteractable>();
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
                toggleGroup = GameObject.Find("CubeLEGO").GetComponent<LegoPlotter>().toggleGroup;
                flag1 = 1;
            }
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
        float maxEnergyHCAL = boxLengthY / 2;
        float maxEnergyECAL = boxLengthY / 2;
        float energyMultiplier = 1f;
        //jsonFile = GameObject.Find("DataInteractionManager").GetComponent<LegoPlotScript>().jsonFile;  // IN THE FINAL VERSION USE THIS
        if(flag2 == 0)
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
        scaleText.text = $"{Math.Round(objectTransform.localScale.y * scalerY / energyMultiplier, 2)} GeV";

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