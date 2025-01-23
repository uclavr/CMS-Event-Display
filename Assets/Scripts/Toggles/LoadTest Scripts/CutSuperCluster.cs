using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CutFeatureBase;

namespace CutFeatureObjects
{
    public class CutSuperCluster : CutObject
    {
        private const int energyIndex = 0, etaIndex = 1, phiIndex = 2, etaWidthIndex = 3, phiWidthIndex = 4, rawEnergyIndex = 5, preshowerEnergyIndex = 6;
        protected override void Awake()
        {
            numObjParam = 7;
            base.Awake();
        }

        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().superClusterObjects;

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

        public void toggleEnergy() { toggleFeature(energyIndex); }
        public void toggleEta() { toggleFeature(etaIndex); }
        public void togglePhi() { toggleFeature(phiIndex); }
        public void toggleEtaWidth() { toggleFeature(etaWidthIndex); }
        public void togglePhiWidth() { toggleFeature(phiWidthIndex); }
        public void toggleRawEnergy() { toggleFeature(rawEnergyIndex); }
        public void togglePreshowerEnergy() { toggleFeature(preshowerEnergyIndex); }

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
    }
}