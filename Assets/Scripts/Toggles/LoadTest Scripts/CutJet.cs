using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using TMPro;
using CutFeatureBase;

namespace CutFeatureObjects
{
    public class CutJet : CutObject
    {
        private const int energyIndex = 0, etaIndex = 1, phiIndex = 2;
        protected override void Awake()
        {
            numObjParam = 3;
            base.Awake();
        }

        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().jetObjects;
            else if (loader.GetComponent<DefaultDataLoad>() != null) gameObjects = loader.GetComponent<DefaultDataLoad>().jetObjects;
            else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().jetObjects;
            else if (loader.GetComponent<TwoETwoMDataLoad>() != null) gameObjects = loader.GetComponent<TwoETwoMDataLoad>().jetObjects;
            else if (loader.GetComponent<METSceneDataLoad>() != null) gameObjects = loader.GetComponent<METSceneDataLoad>().jetObjects;
            else if (loader.GetComponent<MinimumBiasDataLoad>() != null) gameObjects = loader.GetComponent<MinimumBiasDataLoad>().jetObjects;

            foreach (var gameObject in gameObjects)
            {
                JetComponent objComp = gameObject.GetComponent<JetComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3;
                objData[gameObject] = new List<double>() { objComp.getET(), objComp.getEta(), objComp.getPhi() };
            }
        }
        public void toggleEnergy() { toggleFeature(energyIndex); }
        public void toggleEta() { toggleFeature(etaIndex); }
        public void togglePhi() { toggleFeature(phiIndex); }

        public void getMinETValue(TMP_InputField inputField)
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
    }
}
