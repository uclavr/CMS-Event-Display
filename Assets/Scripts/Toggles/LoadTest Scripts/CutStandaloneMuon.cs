using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutFeatureBase;

namespace CutFeatureObjects
{
    public class CutStandaloneMuon : CutMuonObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().standaloneMuonObjects;
            else if (loader.GetComponent<fileLoadMultiple>() != null) gameObjects = loader.GetComponent<fileLoadMultiple>().standaloneMuonObjects;

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
}