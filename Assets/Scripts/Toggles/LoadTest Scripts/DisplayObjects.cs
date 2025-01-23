using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;

// currently for jetObjects only
public class DisplayObjects : MonoBehaviour
{
    // Object data
    public GameObject loader;
    private List<GameObject> jetObjects;
    private Dictionary<GameObject, List<bool>> jetFlags;
    private Dictionary<GameObject, List<double>> jetData;

    // UI stuff
    private List<double?> minValues;
    private List<bool> toggles;
    // private bool energyButton, etaButton;
    private const int energyIndex = 0, etaIndex = 1, thetaIndex = 2, phiIndex = 3;
    private int numJetParam = 4; // *** hardcoded for jets


    void Awake()
    {
        jetFlags = new Dictionary<GameObject, List<bool>>();
        jetData = new Dictionary<GameObject, List<double>>();

        minValues = new List<double?>() { null, null, null, null }; // *** values and number of parameters hardcoded at the moment
        toggles = new List<bool>() { false, false, false, false };  // *** number of toggles hardcoded atm
                                                                    // energyButton = false;
    }

    void Start()
    {
        if(loader.GetComponent<fileLoad>() != null) jetObjects = loader.GetComponent<fileLoad>().jetObjects;
        else if(loader.GetComponent< BJetDataLoad>() != null) jetObjects = loader.GetComponent<BJetDataLoad>().jetObjects;
        else if (loader.GetComponent<TwoETwoMDataLoad>() != null) jetObjects = loader.GetComponent<TwoETwoMDataLoad>().jetObjects;
        else if (loader.GetComponent<METSceneDataLoad>() != null) jetObjects = loader.GetComponent<METSceneDataLoad>().jetObjects;
        else if (loader.GetComponent<MinimumBiasDataLoad>() != null) jetObjects = loader.GetComponent<MinimumBiasDataLoad>().jetObjects;



        foreach (var jetObject in jetObjects)
        {
            JetComponent jetComp = jetObject.GetComponent<JetComponent>();
            // *** 5 is hardcoded as the number of data parameters for jets
            jetFlags[jetObject] = new List<bool>();
            for (int i = 0; i < numJetParam; i++)
            {
                jetFlags[jetObject].Add(true);
            }
            jetData[jetObject] = new List<double>() { jetComp.getET(), jetComp.getEta(), jetComp.getTheta(), jetComp.getPhi() };
        }
    }

    void Update()
    {
        // toggleFeature();
    }

    //public void toggleFeature()
    //{
    //    for (int i = 0; i < numJetParam; i++)
    //    {
    //        if (toggles[i] == true)
    //        {
    //            toggles[i] = false;
    //            foreach (var jetObject in jetObjects)
    //            {
    //                if (minValues[i] != null && jetData[jetObject][i] < minValues[i])
    //                {
    //                    jetFlags[jetObject][i] = !jetFlags[jetObject][i];
    //                }
    //            }
    //        }
    //    }
    //    UnityEngine.Debug.Log(toggles[etaIndex]);
    //    foreach (var jetObject in jetObjects)
    //    {
    //        int flagCount = 0;

    //        for (int i = 0; i < numJetParam; i++)
    //        {
    //            if (jetFlags[jetObject][i] == false)
    //            {
    //                flagCount++;
    //            }
    //        }

    //        if (flagCount == 0) jetObject.SetActive(true);
    //        else jetObject.SetActive(false);
    //    }
    //    // UnityEngine.Debug.Log(toggles[etaIndex]);
    //}
    
    // Hides objects that are less than a given value
    public void toggleFlags(int toggleIndex)
    {
        foreach (var jetObject in jetObjects)
        {
            if (minValues[toggleIndex] != null && jetData[jetObject][toggleIndex] < minValues[toggleIndex])
            {
                jetFlags[jetObject][toggleIndex] = !jetFlags[jetObject][toggleIndex];
            }
        }
    }

    public void activateToggle()
    {
        foreach (var jetObject in jetObjects)
        {
            int flagCount = 0;

            for (int i = 0; i < numJetParam; i++)
            {
                if (jetFlags[jetObject][i] == false)
                {
                    flagCount++;
                }
            }

            if (flagCount == 0) jetObject.SetActive(true);
            else jetObject.SetActive(false);
        }
    }

    public void toggleFeature(int toggleIndex)
    {
        toggles[toggleIndex] = !toggles[toggleIndex];
        toggleFlags(toggleIndex);
        activateToggle();
    }

    public void updateValue(double? newVal, int index)
    {
        if (minValues[index] != null || (minValues[index] == null && toggles[index] == true))
        {
            foreach (var jetObject in jetObjects)
            {
                if (newVal == null) jetFlags[jetObject][index] = true;
                if (newVal != null)
                {
                    if (jetData[jetObject][index] > newVal) jetFlags[jetObject][index] = true;
                    if (jetData[jetObject][index] <= newVal)
                    {
                        jetFlags[jetObject][index] = !toggles[index];
                    }
                }
            }
        }
        minValues[index] = newVal;
        activateToggle();
    }

    public void energyToggleState() { toggleFeature(energyIndex); }
    public void etaToggleState() { toggleFeature(etaIndex); }
    public void phiToggleState() { toggleFeature(phiIndex); }

    public void getMinETValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, energyIndex);
    }

    public void getMinEtaValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, etaIndex);

        //if (minValues[etaIndex] != null)
        //{
        //    foreach (var jetObject in jetObjects)
        //    {
        //        if (x == null) jetFlags[jetObject][etaIndex] = true;
        //        if (x != null)
        //        {
        //            if (jetData[jetObject][etaIndex] > x) jetFlags[jetObject][etaIndex] = true;
        //            if (jetData[jetObject][etaIndex] <= x)
        //            {
        //                jetFlags[jetObject][etaIndex] = toggles[etaIndex];
        //            }
        //        }
        //    }
        //}

        //if (minValues[etaIndex] == null && toggles[etaIndex] == true)
        //{
        //    foreach(var jetObject in jetObjects)
        //    {
        //        if (x == null) jetFlags[jetObject][etaIndex] = true;
        //        if(x != null)
        //        {
        //            if (jetData[jetObject][etaIndex] > x) jetFlags[jetObject][etaIndex] = true;
        //            if (jetData[jetObject][etaIndex] <= x)
        //            {
        //                jetFlags[jetObject][etaIndex] = false;
        //            }
        //        }
        //    }
        //}
        
    }

    public void getMinPhiValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);

        updateValue(x, phiIndex);
    }

    // assumes valid number as a string
    private double? stringToDouble(string str)
    {
        if (str == "" || str == "-") return null;
        double x = 0;
        int decimalIndex = str.IndexOf('.');
        int sign = (str[0] == '-') ? -1 : 1; //*** check that this parsing works!

        for (int i = 0; i < str.Length; i++)
        {
            if (str[i] == '-') continue;
            if (i == decimalIndex)
            {
                break;
            }
            int y = str[i] - '0';
            x = x * 10 + y;
        }

        if (decimalIndex != -1)
        {
            int deciPower = -1;
            for (int i = decimalIndex + 1; i < str.Length; i++)
            {
                int y = str[i] - '0';
                x = x + y * Math.Pow(10.0, deciPower);
                deciPower--;
            }
        }
        x = x * sign;
        // UnityEngine.Debug.Log(x);
        return x;
    }
}
