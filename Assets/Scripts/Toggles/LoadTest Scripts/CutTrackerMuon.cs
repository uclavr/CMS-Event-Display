using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CutFeatureBase;

namespace CutFeatureObjects
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
}