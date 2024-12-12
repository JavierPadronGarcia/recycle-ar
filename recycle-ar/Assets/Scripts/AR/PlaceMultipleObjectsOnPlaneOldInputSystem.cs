using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

/// <summary>
/// For tutorial video, see my YouTube channel: <seealso href="https://www.youtube.com/@xiennastudio">YouTube channel</seealso>
/// How to use this script:
/// - Add ARPlaneManager to XROrigin GameObject.
/// - Add ARRaycastManager to XROrigin GameObject.
/// - Attach this script to XROrigin GameObject.
/// - Add the prefab that will be spawned to the <see cref="placedPrefab"/>.
/// 
/// Touch to place the <see cref="placedPrefab"/> object on the touch position.
/// Will only placed the object if the touch position is on detected trackables.
/// Will spawn a new object on the touch position.
/// Using Unity old input system.
/// </summary>
[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    /// <summary>
    /// The prefab that will be instantiated on touch.
    /// </summary>
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;

    /// <summary>
    /// The instantiated object.
    /// </summary>
    GameObject spawnedObject;

    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Awake()
    {
        if (ARManager.instance.IsScenarioSaved())
        {
            ARManager.instance.placedObjects.Clear();
        }
        aRRaycastManager = GetComponent<ARRaycastManager>();
    }

    void Update()
    {

        // Verificar si hay entrada táctil o de ratón
        bool isTouchInput = Input.touchCount > 0;
        bool isMouseInput = Input.GetMouseButtonDown(0);

        Vector2 inputPosition = Vector2.zero;

        // Determinar la posición de la entrada
        if (isTouchInput)
        {
            // Obtener la posición táctil
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;

            inputPosition = touch.position;
        }
        else if (isMouseInput && Application.isEditor)
        {
            // Usar la posición del ratón para simular un toque en el editor
            inputPosition = Input.mousePosition;
        }
        else
        {
            return;
        }

        // Realizar el raycast para detectar planos
        if (aRRaycastManager.Raycast(inputPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            // Raycast hits están ordenados por distancia; el primero es el más cercano
            var hitPose = hits[0].pose;

            // Instanciar el prefab en la posición y rotación detectadas
            spawnedObject = Instantiate(placedPrefab, hitPose.position, hitPose.rotation);

            // Almacenar los datos del objeto instanciado
            ARManager.instance.SaveObjectData(hitPose.position, hitPose.rotation, placedPrefab.name);
        }
    }

}
