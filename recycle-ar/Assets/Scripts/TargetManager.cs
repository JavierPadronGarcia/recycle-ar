using TMPro;
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
    public TextMeshProUGUI feetbackText;
    public GameObject papeleras;

    public PointsManager points;

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
            if (checkName(hit.collider.gameObject.name) == target)
            {
                feetbackText.SetText("Its Ok");
                papeleras.SetActive(false);
                points.points = points.points + 3;
            }
            else
            {
                feetbackText.SetText("Mistake");
                papeleras.SetActive(false);
            }
        }
    }

    private Target checkName(string gameObjectName)
    {
        switch (gameObjectName)
        {
            case "paeleraAmarilla":
                return Target.plastico;
            case "paeleraVerde":
                return Target.cristal;
            case "paeleraAzul":
                return Target.papel;
            case "paeleraGris":
                return Target.organico;
            default:
                return Target.plastico;
        }
    }   
}
