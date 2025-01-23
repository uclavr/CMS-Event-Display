using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using CutFeatureBase;

namespace CutFeature
{
    public class CutTrackerMuon : CutTrackObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().trackerMuonObjects;
            else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().trackerMuonObjects;
            else if (loader.GetComponent<FourMDataLoad>() != null) gameObjects = loader.GetComponent<FourMDataLoad>().trackerMuonObjects;
            else if (loader.GetComponent<TwoETwoMDataLoad>() != null) gameObjects = loader.GetComponent<TwoETwoMDataLoad>().trackerMuonObjects;
            else if (loader.GetComponent<METSceneDataLoad>() != null) gameObjects = loader.GetComponent<METSceneDataLoad>().trackerMuonObjects;
            else if (loader.GetComponent<MinimumBiasDataLoad>() != null) gameObjects = loader.GetComponent<MinimumBiasDataLoad>().trackerMuonObjects;

            foreach (var gameObject in gameObjects)
            {
                TrackerMuonComponent objComp = gameObject.GetComponent<TrackerMuonComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3;
                objData[gameObject] = new List<double>() { objComp.getPT(), objComp.getEta(), objComp.getPhi(), objComp.getCharge() };
            }
        }
    }

    public class CutElectron : CutTrackObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().electronObjects;
            else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().electronObjects;
            else if (loader.GetComponent<FourMDataLoad>() != null) gameObjects = loader.GetComponent<FourMDataLoad>().electronObjects;
            else if (loader.GetComponent<TwoETwoMDataLoad>() != null) gameObjects = loader.GetComponent<TwoETwoMDataLoad>().electronObjects;
            else if (loader.GetComponent<METSceneDataLoad>() != null) gameObjects = loader.GetComponent<METSceneDataLoad>().electronObjects;
            else if (loader.GetComponent<MinimumBiasDataLoad>() != null) gameObjects = loader.GetComponent<MinimumBiasDataLoad>().electronObjects;

            foreach (var gameObject in gameObjects)
            {
                ElectronComponent objComp = gameObject.GetComponent<ElectronComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3;
                objData[gameObject] = new List<double>() { objComp.getPT(), objComp.getEta(), objComp.getPhi(), objComp.getCharge() };

            }
        }
    }

    public class CutTrack : CutTrackObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().trackObjects;
            else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().trackObjects;
            else if (loader.GetComponent<FourMDataLoad>() != null) gameObjects = loader.GetComponent<FourMDataLoad>().trackObjects;
            else if (loader.GetComponent<TwoETwoMDataLoad>() != null) gameObjects = loader.GetComponent<TwoETwoMDataLoad>().trackObjects;
            else if (loader.GetComponent<METSceneDataLoad>() != null) gameObjects = loader.GetComponent<METSceneDataLoad>().trackObjects;
            else if (loader.GetComponent<MinimumBiasDataLoad>() != null) gameObjects = loader.GetComponent<MinimumBiasDataLoad>().trackObjects;

            foreach (var gameObject in gameObjects)
            {
                TrackComponent objComp = gameObject.GetComponent<TrackComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3;
                objData[gameObject] = new List<double>() { objComp.getPT(), objComp.getEta(), objComp.getPhi(), objComp.getCharge() };
            }
        }
    }



    public class CutGlobalMuon : CutMuonObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().globalMuonObjects;
            else if (loader.GetComponent<BJetDataLoad>() != null) gameObjects = loader.GetComponent<BJetDataLoad>().globalMuonObjects;
            else if (loader.GetComponent<FourMDataLoad>() != null) gameObjects = loader.GetComponent<FourMDataLoad>().globalMuonObjects;
            else if (loader.GetComponent<TwoETwoMDataLoad>() != null) gameObjects = loader.GetComponent<TwoETwoMDataLoad>().globalMuonObjects;
            else if (loader.GetComponent<METSceneDataLoad>() != null) gameObjects = loader.GetComponent<METSceneDataLoad>().globalMuonObjects;
            else if (loader.GetComponent<MinimumBiasDataLoad>() != null) gameObjects = loader.GetComponent<MinimumBiasDataLoad>().globalMuonObjects;

            foreach (var gameObject in gameObjects)
            {
                GlobalMuonComponent objComp = gameObject.GetComponent<GlobalMuonComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3, caloEnergyIndex = 4;
                objData[gameObject] = new List<double>() { objComp.getPT(), objComp.getEta(), objComp.getPhi(), objComp.getCharge(), objComp.getCaloEnergy() };
            }
        }
    }

    public class CutStandaloneMuon : CutMuonObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().standaloneMuonObjects;

            foreach (var gameObject in gameObjects)
            {
                StandaloneMuonComponent objComp = gameObject.GetComponent<StandaloneMuonComponent>();
                objFlags[gameObject] = new List<bool>();
                for (int i = 0; i < numObjParam; i++)
                {
                    objFlags[gameObject].Add(true);
                }
                // momentumIndex = 0, etaIndex = 1, phiIndex = 2, chargeIndex = 3, caloEnergyIndex = 4;
                objData[gameObject] = new List<double>() { objComp.getPT(), objComp.getEta(), objComp.getPhi(), objComp.getCharge(), objComp.getCaloEnergy() };
            }
        }
    }



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
