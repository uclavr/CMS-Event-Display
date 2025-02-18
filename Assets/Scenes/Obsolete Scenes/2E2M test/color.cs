using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public GameObject tracks;
    public Material trackmaterial;
    void Start()
    {
        foreach (var track in AllChilds(tracks)) 
        {
            if (track.GetComponent<MeshRenderer>() == null) { continue; }
            track.GetComponent<MeshRenderer>().material = trackmaterial;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private List<GameObject> AllChilds(GameObject root)
    {
        List<GameObject> result = new List<GameObject>();
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(result, VARIABLE.gameObject);
            }
        }
        return result;
    }

    private void Searcher(List<GameObject> list, GameObject root)
    {
        list.Add(root);
        if (root.transform.childCount > 0)
        {
            foreach (Transform VARIABLE in root.transform)
            {
                Searcher(list, VARIABLE.gameObject);
            }
        }
    }
}
