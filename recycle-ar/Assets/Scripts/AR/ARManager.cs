using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    public static ARManager instance;

    public List<Vector3> prefabPositions = new List<Vector3>();
    public List<Quaternion> prefabRotations = new List<Quaternion>();
    public List<GameObject> prefabList = new List<GameObject>();

    bool scenarioSaved = false;

    private void Awake()
    {

        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
    }

    public bool IsScenarioSaved() { return scenarioSaved; }

    public void SaveScenario(List<Vector3> positions, List<Quaternion> rotations, List<GameObject> objects)
    {
        prefabPositions.Clear();
        prefabRotations.Clear();
        prefabList.Clear();

        prefabPositions.AddRange(positions);
        prefabRotations.AddRange(rotations);
        prefabList.AddRange(objects);
        scenarioSaved = true;
    }
}
