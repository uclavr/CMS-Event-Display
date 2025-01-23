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
using UnityEditor;

public class hoverOBJ : MonoBehaviour
{
    public GameObject scaleTextPrefab; // Reference to the Text prefab
    private XRSimpleInteractable grabInteractable;
    private Transform objectTransform;
    //public ToggleGroup toggleGroup;
    //private Toggle activeToggle;

    void Start()
    {
        // Code that does the actual hovering stuff
        //GameObject toggleGroup = GameObject.Find("ToggleGroupContainer");
        grabInteractable = GetComponent<XRSimpleInteractable>();
        objectTransform = transform;
        grabInteractable.hoverEntered.AddListener(OnHoverEntered);
        grabInteractable.hoverExited.AddListener(OnHoverExited);
    }

    private void Update()
    {
        //activeToggle = toggleGroup.ActiveToggles().FirstOrDefault();
    }

    void Awake()
    {
        //jsonFile = (JObject)JToken.Parse(jsonText.text);
        //JToken jsonDataHB;
        //JToken jsonDataEB;
    }

    void OnHoverEntered(HoverEnterEventArgs args)
    {
        //GameObject instantiatedText = Instantiate(scaleTextPrefab, transform.position, Quaternion.identity);
        GameObject instantiatedText = new GameObject("DynamicText");
        instantiatedText.tag = "DestroyableOBJName";
        TextMeshPro scaleText = instantiatedText.AddComponent<TextMeshPro>();
        scaleText.fontSize = 6;
        scaleText.color = Color.white;
        scaleText.text = $"{objectTransform.parent.name}";

        // Position the text above the object
        instantiatedText.transform.position = objectTransform.parent.position + Vector3.up*0.2f;
        instantiatedText.transform.LookAt(Camera.main.transform);
        instantiatedText.transform.Rotate(0, 180, 0);
        instantiatedText.SetActive(true);
    }


    void OnHoverExited(HoverExitEventArgs args)
    {
        if (GameObject.FindGameObjectsWithTag("DestroyableOBJName") != null)
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("DestroyableOBJName");
            foreach (GameObject go in gos)
                Destroy(go);
        }
    }
}