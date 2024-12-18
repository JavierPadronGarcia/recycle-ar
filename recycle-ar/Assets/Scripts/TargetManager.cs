using System;
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
    public GameObject thisObject;

    public PointsManager points;
    public AudioSource audioSource;
    public AudioClip successClip;
    public AudioClip mistakeClip;

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
            string mainParentNameHit = hit.collider.gameObject.transform.parent.transform.parent.name;
            string mainParentName = this.name;
            if (mainParentNameHit.Equals(mainParentName))
            {
                if (checkName(hit.collider.gameObject.name) == target)
                {
                    feetbackText.SetText("Its Ok");
                    papeleras.SetActive(false);
                    points.points = points.points + 3;

                    if (audioSource != null && successClip != null)
                    {
                        audioSource.PlayOneShot(successClip);
                    }
                }
                else
                {
                    feetbackText.SetText("Mistake");
                    papeleras.SetActive(false);

                    if (audioSource != null && mistakeClip != null)
                    {
                        audioSource.PlayOneShot(mistakeClip);
                    }
                }
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
