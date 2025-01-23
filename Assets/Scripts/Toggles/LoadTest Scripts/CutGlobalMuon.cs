using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutFeatureBase;

namespace CutFeatureObjects
{
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

}