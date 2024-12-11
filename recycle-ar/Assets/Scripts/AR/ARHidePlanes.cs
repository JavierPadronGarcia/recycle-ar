using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARHidePlanes : MonoBehaviour
{
    void Start()
    {
        var planeVisualizers = FindObjectsOfType<ARPlane>();
        foreach (var plane in planeVisualizers)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
