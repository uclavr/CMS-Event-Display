using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetricManager : MonoBehaviour
{
    public List<GameObject> jetObjects;
    public List<GameObject> standaloneMuonObjects;
    public List<GameObject> globalMuonObjects;

    void Start()
    {
        GameObject loader = GameObject.Find("Loader");

        jetObjects = loader.GetComponent<fileLoad>().jetObjects;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
