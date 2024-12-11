using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARGameManager : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Transform parentTransform;
    public List<GameObject> prefabs;

    void Start()
    {
        if (ARManager.instance != null && ARManager.instance.IsScenarioSaved())
        {
            foreach (var objectData in ARManager.instance.placedObjects)
            {
                // Buscar el prefab correspondiente por nombre
                GameObject prefab = prefabs.Find(p => p.name == objectData.prefabName);
                if (prefab != null)
                {
                    Instantiate(prefab, objectData.position, objectData.rotation);
                }
                else
                {
                    Debug.LogWarning($"Prefab no encontrado: {objectData.prefabName}");
                }
            }
        }
    }
}
