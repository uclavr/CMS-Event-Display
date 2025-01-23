using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;

public class DisplaySuperCluster : MonoBehaviour
{
    // Object data
    public GameObject loader;
    private List<GameObject> gameObjects;
    private Dictionary<GameObject, List<bool>> objFlags;
    private Dictionary<GameObject, List<double>> objData;

    // UI stuff
    private List<double?> minValues;
    private List<bool> toggles;
    private const int energyIndex = 0, etaIndex = 1, phiIndex = 2, etaWidthIndex = 3, phiWidthIndex = 4, rawEnergyIndex = 5, preshowerEnergyIndex = 6;
    private int numObjParam = 7; // *** hardcoded for jets


    void Awake()
    {
        objFlags = new Dictionary<GameObject, List<bool>>();
        objData = new Dictionary<GameObject, List<double>>();

        minValues = new List<double?>();
        toggles = new List<bool>();
        for (int i = 0; i < numObjParam; i++)
        {
            minValues.Add(null);
            toggles.Add(false);
        }
    }

    void Start()
    {
        if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().superClusterObjects;
        // else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().superClusterObjects;
        
        foreach (var gameObject in gameObjects)
        {
            SuperClusterComponent objComp = gameObject.GetComponent<SuperClusterComponent>();
            objFlags[gameObject] = new List<bool>();
            for (int i = 0; i < numObjParam; i++)
            {
                objFlags[gameObject].Add(true);
            }
            // energyIndex = 0, etaIndex = 1, phiIndex = 2, etaWidthIndex = 3, phiWidthIndex = 4, rawEnergyIndex = 5, preshowerEnergyIndex = 6;
            objData[gameObject] = new List<double>() { objComp.getEnergy(), objComp.getEta(), objComp.getPhi(), objComp.getEtaWidth(), 
                                                        objComp.getPhiWidth(), objComp.getRawEnergy(), objComp.getPreshowerEnergy() };
        }
    }

    // Hides objects that are less than a given value
    public void toggleFlags(int toggleIndex)
    {
        foreach (var gameObject in gameObjects)
        {
            if (minValues[toggleIndex] != null && objData[gameObject][toggleIndex] < minValues[toggleIndex])
            {
                objFlags[gameObject][toggleIndex] = !objFlags[gameObject][toggleIndex];
            }
        }
    }

    public void activateToggle()
    {
        foreach (var gameObject in gameObjects)
        {
            int flagCount = 0;

            for (int i = 0; i < numObjParam; i++)
            {
                if (objFlags[gameObject][i] == false)
                {
                    flagCount++;
                }
            }

            if (flagCount == 0) gameObject.SetActive(true);
            else gameObject.SetActive(false);
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
            foreach (var gameObject in gameObjects)
            {
                if (newVal == null) objFlags[gameObject][index] = true;
                if (newVal != null)
                {
                    if (objData[gameObject][index] > newVal) objFlags[gameObject][index] = true;
                    if (objData[gameObject][index] <= newVal)
                    {
                        objFlags[gameObject][index] = !toggles[index];
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
    public void etaWidthToggleState() { toggleFeature(etaWidthIndex); }
    public void phiWidthToggleState() { toggleFeature(phiWidthIndex); }
    public void rawEnergyToggleState() { toggleFeature(rawEnergyIndex); }
    public void preshowerEnergyToggleState() { toggleFeature(preshowerEnergyIndex); }

    public void getMinEnergyValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, energyIndex);
    }

    public void getMinEtaValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, etaIndex);
    }

    public void getMinPhiValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, phiIndex);
    }

    public void getMinEtaWidthValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, etaWidthIndex);
    }

    public void getMinPhiWidthValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, phiWidthIndex);
    }

    public void getMinRawEnergyValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, rawEnergyIndex);
    }

    public void getMinPreshowerEnergyValue(TMP_InputField inputField)
    {
        double? x = stringToDouble(inputField.text);
        updateValue(x, preshowerEnergyIndex);
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
