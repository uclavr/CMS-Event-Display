using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCutMenuManager : MonoBehaviour
{
    public GameObject Tracks;
    public GameObject Jets;
    public GameObject GlobalMuons;
    public GameObject TrackerMuons;
    public GameObject Electrons;
    // Start is called before the first frame update

    public void TracksButton()
    {
        if (!Tracks.activeSelf)
        {
            Tracks.SetActive(true);
            Jets.SetActive(false);
            GlobalMuons.SetActive(false);
            TrackerMuons.SetActive(false);
            Electrons.SetActive(false);
        }
    }
    public void JetsButton()
    {
        if (!Jets.activeSelf)
        {
            Tracks.SetActive(false);
            Jets.SetActive(true);
            GlobalMuons.SetActive(false);
            TrackerMuons.SetActive(false);
            Electrons.SetActive(false);
        }
    }

    public void GlobalMuonsButton()
    {
        if (!GlobalMuons.activeSelf)
        {
            Tracks.SetActive(false);
            Jets.SetActive(false);
            GlobalMuons.SetActive(true);
            TrackerMuons.SetActive(false);
            Electrons.SetActive(false);
        }
    }

    public void TrackerMuonsButton()
    {
        if (!TrackerMuons.activeSelf)
        {
            Tracks.SetActive(false);
            Jets.SetActive(false);
            GlobalMuons.SetActive(false);
            TrackerMuons.SetActive(true);
            Electrons.SetActive(false);
        }
    }
    public void ElectronsButton()
    {
        if (!Electrons.activeSelf)
        {
            Tracks.SetActive(false);
            Jets.SetActive(false);
            GlobalMuons.SetActive(false);
            TrackerMuons.SetActive(false);
            Electrons.SetActive(true);
        }
    }
}
