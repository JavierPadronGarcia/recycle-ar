using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Main");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Creditos");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Collection()
    {
        SceneManager.LoadScene("Collection");
    }

    public void AR()
    {
        if (ARManager.instance && ARManager.instance.IsScenarioSaved())
        {
            SceneManager.LoadScene("ArGame");
        }
        else
        {
            SceneManager.LoadScene("Ar");
        }
    }

    public void ARModify()
    {
        SceneManager.LoadScene("Ar");
    }

    public void ConfirmScenario()
    {
        PlaceMultipleObjectsOnPlaneOldInputSystem objectsScript = GameObject.FindGameObjectWithTag("xrorigin").GetComponent<PlaceMultipleObjectsOnPlaneOldInputSystem>();
        ARManager.instance.SaveScenario(objectsScript.prefabPositions, objectsScript.prefabRotations, objectsScript.prefabList);
        SceneManager.LoadScene("ArGame");
    }
}
