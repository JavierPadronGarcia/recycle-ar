using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ARGameManager : MonoBehaviour
{
    public ARPlaneManager planeManager;
    public Transform parentTransform;

    void Start()
    {
        if (ARManager.instance != null && ARManager.instance.IsScenarioSaved())
        {
            List<Vector3> positions = ARManager.instance.prefabPositions;
            List<Quaternion> rotations = ARManager.instance.prefabRotations;
            List<GameObject> prefabs = ARManager.instance.prefabList;

            for (int i = 0; i < positions.Count; i++)
            {
                if (i < prefabs.Count)
                {
                    GameObject obj = Instantiate(prefabs[i], positions[i], rotations[i]);

                    if (parentTransform != null)
                    {
                        obj.transform.SetParent(parentTransform);
                    }
                }
            }


            // Desactivar la detección de planos
            if (planeManager != null)
            {
                planeManager.enabled = false;

                // Opcional: desactivar todos los planos existentes
                foreach (var plane in planeManager.trackables)
                {
                    plane.gameObject.SetActive(false);
                }
            }
        }
    }
}
