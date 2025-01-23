using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeometryToggle : MonoBehaviour
{
    public GameObject DetectorItem;
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(ChangeState);
    }
    void ChangeState()
    {
        DetectorItem.SetActive(!DetectorItem.activeSelf);
    }
}
