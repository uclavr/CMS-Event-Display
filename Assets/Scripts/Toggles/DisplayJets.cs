using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class DisplayJets : MonoBehaviour
{
    //public GameObject obj;
    public GameObject MetricManager;


    private JetDataParse loadJetScript;
    private List<JetData> jetData;
    private Dictionary<int, List<bool>> jetFlags;
    private Dictionary<int, List<double>> jetIdData;
    private Dictionary<string, GameObject> stringObjectPairs;

    private List<bool> paramToggles;
    private bool energyButton, thetaButton;
    // private double minEnergy;
    private const int energyIndex = 0, thetaIndex = 2;

    private List<double> minValues;

    void Awake()
    {
        stringObjectPairs = new Dictionary<string, GameObject>();
        jetFlags = new Dictionary<int, List<bool>>();
        jetIdData = new Dictionary<int, List<double>>();
        paramToggles = new List<bool>() { false, false, false, false };
        energyButton = thetaButton = false;
        // minEnergy = double.MaxValue;
        minValues = new List<double>() { 0, 0, 0, 0 };
    }

    // Start is called before the first frame update
    void Start()
    {
        loadJetScript = MetricManager.GetComponent<JetDataParse>();
        jetData = loadJetScript.jetDataList;
        //UnityEngine.Debug.Log("jetData size: ");
        //UnityEngine.Debug.Log(jetData.Count);
        foreach (var element in jetData)
        {
            jetFlags[element.getID()] = new List<bool>() { true, true, true, true };
            jetIdData[element.getID()] = new List<double>() { element.getET(), element.getEta(), element.getTheta(), element.getPhi() };
            // if (element.getET() < minEnergy) minEnergy = element.getET();
        }


        foreach (var element in FindObjectsOfType<GameObject>())
        {
            stringObjectPairs[element.name] = element;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UnityEngine.Debug.Log(jetData.Count);
        // List<double> minValues = new List<double>() { 100.0, 0, Math.PI / 2, 0 }; // ***hardcoded
        // const int energyIndex = 0;
        // const int thetaIndex = 2;

        //OVRInput.Update();

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) // change to the button input
        //{
        //    setEnergyToggle();
        //}

        if (energyButton == true) // maybe check out onClick later
        {
            energyButton = false;
            foreach (var element in jetData)
            {
                if (jetIdData[element.getID()][energyIndex] < minValues[energyIndex])
                {
                    jetFlags[element.getID()][energyIndex] = !jetFlags[element.getID()][energyIndex];
                }
            }
        }

        if (thetaButton == true) // maybe check out onClick later
        {
            thetaButton = false;
            foreach (var element in jetData)
            {
                if (jetIdData[element.getID()][thetaIndex] < minValues[thetaIndex])
                {
                    jetFlags[element.getID()][thetaIndex] = !jetFlags[element.getID()][thetaIndex];
                }
            }
        }

        foreach (var element in jetData)
        {
            int flagCount = 0;
            for (int i = 0; i < element.getNumParam(); i++)
            {
                if (jetFlags[element.getID()][i] == false)
                {
                    flagCount++;
                }
            }

            string jetName = "jet" + element.getID().ToString();
            GameObject obj = stringObjectPairs[jetName];
            if (flagCount == 0)
            {
                obj.SetActive(true);
            }
            else obj.SetActive(false);

        }
    }

    public void setEnergyToggle() { paramToggles[energyIndex] = !paramToggles[energyIndex]; }
    public void setThetaToggle() { paramToggles[thetaIndex] = !paramToggles[thetaIndex]; }

    public void energyButtonState() { energyButton = !energyButton; }
    public void thetaButtonState() { thetaButton = !thetaButton; }

    public void getminETValue(TMP_InputField inputField) { minValues[energyIndex] = stringToDouble(inputField.text); }
    public void getminThetaValue(TMP_InputField inputField) { minValues[thetaIndex] = stringToDouble(inputField.text); } 

    private double stringToDouble(string str)
    {
        double x = 0;
        int decimalIndex = str.IndexOf('.');
        int sign = (str[0] == '-') ? -1 : 1; //*** check that this parsing works!

        for(int i = 0; i < str.Length; i++)
        {
            if (str[i] == '-') continue;
            if(i == decimalIndex)
            {
                break;
            }
            int y = str[i] - '0';
            x = x * 10 + y;
        }

        if(decimalIndex != -1)
        {
            int deciPower = -1;
            for(int i = decimalIndex + 1; i < str.Length; i++)
            {
                int y = str[i] - '0';
                x = x + y * Math.Pow(10.0, deciPower);
                deciPower--;
            }
        }
        x = x * sign;
        UnityEngine.Debug.Log(x);
        return x;
    }
}
