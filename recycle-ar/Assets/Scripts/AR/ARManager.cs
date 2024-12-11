using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    public static ARManager instance;

    [System.Serializable]
    public class ObjectData
    {
        public Vector3 position;
        public Quaternion rotation;
        public string prefabName;
    }

    public List<ObjectData> placedObjects = new List<ObjectData>();

    private void Awake()
    {

        if (instance == null) instance = this;
        else { Destroy(gameObject); return; }

        DontDestroyOnLoad(gameObject);
    }

    public bool IsScenarioSaved() { return placedObjects.Count != 0; }

    public void SaveObjectData(Vector3 position, Quaternion rotation, string prefabName)
    {
        placedObjects.Add(new ObjectData { position = position, rotation = rotation, prefabName = prefabName });
    }
}
