using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro; // Para usar TMP_Dropdown

[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("List of prefabs available for placement.")]
    List<GameObject> prefabs;

    [SerializeField]
    [Tooltip("List of custom labels corresponding to the prefabs.")]
    List<string> prefabLabels;

    [SerializeField]
    [Tooltip("TMP Dropdown to select the prefab to place.")]
    TMP_Dropdown prefabSelector;

    GameObject spawnedObject;
    ARRaycastManager aRRaycastManager;
    List<ARRaycastHit> hits = new List<ARRaycastHit>();
    int selectedPrefabIndex = 0;

    void Awake()
    {
        if (ARManager.instance.IsScenarioSaved())
        {
            ARManager.instance.placedObjects.Clear();
        }
        aRRaycastManager = GetComponent<ARRaycastManager>();

        // Configurar el TMP_Dropdown
        if (prefabSelector != null)
        {
            prefabSelector.ClearOptions();

            // Validar que las listas tengan el mismo tamaño
            if (prefabs.Count != prefabLabels.Count)
            {
                Debug.LogError("La cantidad de prefabs y etiquetas no coincide. Por favor, corrige las listas en el inspector.");
                return;
            }

            // Agregar las etiquetas personalizadas al TMP_Dropdown
            prefabSelector.AddOptions(prefabLabels);

            // Escuchar cambios en el TMP_Dropdown
            prefabSelector.onValueChanged.AddListener(OnPrefabSelected);
        }
    }

    void OnPrefabSelected(int index)
    {
        // Actualizar el índice del prefab seleccionado
        selectedPrefabIndex = index;
    }

    void Update()
    {
        bool isTouchInput = Input.touchCount > 0;
        bool isMouseInput = Input.GetMouseButtonDown(0);

        Vector2 inputPosition = Vector2.zero;

        if (isTouchInput)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began)
                return;

            inputPosition = touch.position;

            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                return;
        }
        else if (isMouseInput && Application.isEditor)
        {
            inputPosition = Input.mousePosition;

            if (EventSystem.current.IsPointerOverGameObject())
                return;
        }
        else
        {
            return;
        }

        if (aRRaycastManager.Raycast(inputPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            // Obtener el prefab seleccionado
            GameObject placedPrefab = prefabs[selectedPrefabIndex];

            // Usar rotación fija con x = -90 y el resto igual al prefab
            Quaternion fixedRotation = Quaternion.Euler(-90, 0, 0);

            spawnedObject = Instantiate(placedPrefab, hitPose.position, fixedRotation);

            ARManager.instance.SaveObjectData(hitPose.position, spawnedObject.transform.rotation, placedPrefab.name);
        }
    }
}
