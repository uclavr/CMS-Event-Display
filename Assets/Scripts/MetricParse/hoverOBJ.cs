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
using Oculus.Interaction;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UIElements;
using static System.Net.Mime.MediaTypeNames;

public class hoverOBJ : MonoBehaviour
{
    public GameObject scaleTextPrefab; // Reference to the Text prefab
    private XRSimpleInteractable grabInteractable;
    private Transform objectTransform;
    public XRRayInteractor xrRayInteractor;
    public GameObject textPrefab;
    //public ToggleGroup toggleGroup;
    //private Toggle activeToggle;

    void Start()
    {
        // Dynamically find the Right Ray Interactor
        textPrefab = Resources.Load<GameObject>("legoTextPrefab");
        GameObject xrOriginObject = GameObject.Find("XR Origin");
        Transform cameraOffset = xrOriginObject.transform.Find("Camera Offset");
        xrRayInteractor = cameraOffset.Find("Right Ray Interactor").GetComponent<XRRayInteractor>();

        // Hover code
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
        // Get the raycast hit point from the XR Ray Interactor
        if (xrRayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            // Create the instantiated text object
            Vector3 hitPoint = hit.point;  // The exact position where the ray hit the object
            Vector3 offset = new Vector3(0f, 0.5f, 0f); // 1 unit offset upwards

            GameObject instantiatedText = Instantiate(textPrefab, hitPoint + offset, Quaternion.identity);
            instantiatedText.tag = "DestroyableOBJName";
            TextMeshPro labelTMP = instantiatedText.GetComponent<TextMeshPro>();
            labelTMP.text = $"{objectTransform.parent.name}"; // Display the object's parent name
            labelTMP.color = Color.white;
            // Make the text always face the camera (user's head position)
            instantiatedText.transform.LookAt(Camera.main.transform.position);  // Make it face the camera
            instantiatedText.transform.Rotate(0, 180f, 0);  
            instantiatedText.transform.localScale = new Vector3(1f, 1f, 1f);

            // Make the text active
            instantiatedText.SetActive(true);
        }
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