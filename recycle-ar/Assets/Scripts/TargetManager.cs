using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public enum Target
    {
        plastico,
        papel,
        cristal,
        organico
    }

    public Target target;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("found mouse input");
            DetectTouchOrClick(Input.mousePosition);
        }
        else if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DetectTouchOrClick(Input.GetTouch(0).position);
        }
    }

    void DetectTouchOrClick(Vector3 inputPosition)
    {
        Debug.Log("DetectTouchOrClick");

        Ray ray = Camera.main.ScreenPointToRay(inputPosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hiciste clic o tocaste en: " + hit.collider.gameObject.name);

            GameObject clickedObject = hit.collider.gameObject;
        }
    }
}
