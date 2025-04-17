using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutFeatureBase;

namespace CutFeatureObjects
{
    public class CutElectron : CutTrackObject
    {
        protected override void Start()
        {
            if (loader.GetComponent<fileLoad>() != null) gameObjects = loader.GetComponent<fileLoad>().electronObjects;
            else if (loader.GetComponent<fileLoadMultiple>() != null) gameObjects = loader.GetComponent<fileLoadMultiple>().electronObjects;
            else if (loader.GetComponent<DefaultDataLoad>() != null) gameObjects = loader.GetComponent<DefaultDataLoad>().electronObjects;
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

}