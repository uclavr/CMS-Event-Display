using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutFeatureBase;

namespace CutFeatureObjects
{
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
            UnityEngine.Debug.Log(gameObjects.Count);
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

}