using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[HelpURL("https://youtu.be/HkNVp04GOEI")]
[RequireComponent(typeof(ARRaycastManager))]
public class PlaceMultipleObjectsOnPlaneOldInputSystem : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Instantiates this prefab on a plane at the touch location.")]
    GameObject placedPrefab;

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
        }
        else
        {
            return;
        }

        if (aRRaycastManager.Raycast(inputPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;

            // Usar rotación fija con x = -90 y el resto igual al prefab
            Quaternion fixedRotation = Quaternion.Euler(-90, 0, 0);

            spawnedObject = Instantiate(placedPrefab, hitPose.position, fixedRotation);

            ARManager.instance.SaveObjectData(hitPose.position, spawnedObject.transform.rotation, placedPrefab.name);
        }
    }
}
