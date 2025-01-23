using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;

namespace CutFeatureBase
{
    public abstract class CutObject : MonoBehaviour
    {
        // Object data
        public GameObject loader;
        protected List<GameObject> gameObjects;
        protected Dictionary<GameObject, List<bool>> objFlags;
        protected Dictionary<GameObject, List<double>> objData;

        // UI stuff
        protected List<double?> minValues;
        protected List<double?> maxValues;
        protected List<bool> toggles;
        protected int numObjParam = 4;

        protected virtual void Awake()
        {
            objFlags = new Dictionary<GameObject, List<bool>>();
            objData = new Dictionary<GameObject, List<double>>();

            minValues = new List<double?>();
            maxValues = new List<double?>();
            toggles = new List<bool>();
            for (int i = 0; i < numObjParam; i++)
            {
                minValues.Add(null);
                maxValues.Add(null);
                toggles.Add(false);
            }
        }

        protected abstract void Start();

        // Hides objects that are less than or greater than a given value
        protected void toggleFlags(int toggleIndex)
        {
            foreach (var gameObject in gameObjects)
            {
                if ((minValues[toggleIndex] != null && objData[gameObject][toggleIndex] < minValues[toggleIndex]) ||
                    (maxValues[toggleIndex] != null && objData[gameObject][toggleIndex] > maxValues[toggleIndex]))
                {
                    objFlags[gameObject][toggleIndex] = !objFlags[gameObject][toggleIndex];
                }
            }
        }

        protected void activateToggle()
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

        protected void toggleFeature(int toggleIndex)
        {
            toggles[toggleIndex] = !toggles[toggleIndex];
            toggleFlags(toggleIndex);
            activateToggle();
        }

        protected void updateValue(double? newVal, int index)
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
        // assumes valid number as a string
        protected double? stringToDouble(string str)
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

    public abstract class CutTrackObject : CutObject
    {
        protected override void Awake()
        {
            numObjParam = 4;
            base.Awake();
        }
        protected const int momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3;
        public void toggleMomentum() { toggleFeature(momentumIndex); }
        public void toggleEta() { toggleFeature(etaIndex); }
        public void togglePhi() { toggleFeature(phiIndex); }
        public void toggleCharge() { toggleFeature(chargeIndex); }

        // get min values
        public void getMinPTValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, momentumIndex);
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

        public void getMinChargeValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, chargeIndex);
        }

        // get max values
        public void getMaxPTValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, momentumIndex);
        }

    }

    public abstract class CutMuonObject : CutObject
    {
        protected const int momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3, caloEnergyIndex = 4;

        public void toggleMomentum() { toggleFeature(momentumIndex); }
        public void toggleEta() { toggleFeature(etaIndex); }
        public void togglePhi() { toggleFeature(phiIndex); }
        public void toggleCharge() { toggleFeature(chargeIndex); }
        public void toggleCaloEnergy() { toggleFeature(caloEnergyIndex); }

        public void getMinPTValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, momentumIndex);
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

        public void getMinChargeValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, chargeIndex);
        }

        public void getMinCaloEnergyValue(TMP_InputField inputField)
        {
            double? x = stringToDouble(inputField.text);
            updateValue(x, caloEnergyIndex);
        }
    }
}

