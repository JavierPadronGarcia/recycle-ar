using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ColeccionableManager : MonoBehaviour
{
    private string filePath; // Ruta del archivo donde se guardarán los datos
    public Dictionary<string, bool> coleccionables = new Dictionary<string, bool>();

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "coleccionables.json");
        CargarDatos();
    }

    // Guardar los datos en un archivo JSON
    public void GuardarDatos()
    {
        string json = JsonUtility.ToJson(new Serialization<string, bool>(coleccionables), true);
        File.WriteAllText(filePath, json);
        Debug.Log("Datos guardados en " + filePath);
    }

    // Cargar los datos desde el archivo JSON
    public void CargarDatos()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            coleccionables = JsonUtility.FromJson<Serialization<string, bool>>(json).ToDictionary();
            Debug.Log("Datos cargados desde " + filePath);
        }
        else
        {
            Debug.Log("No se encontró el archivo. Creando datos iniciales.");
            // Crear datos iniciales
            coleccionables.Add("lata", false);
            GuardarDatos();
        }
    }

    // Marcar un coleccionable como recolectado
    public void Recolectar(string nombre)
    {
        if (coleccionables.ContainsKey(nombre))
        {
            coleccionables[nombre] = true;
            Debug.Log($"Coleccionable '{nombre}' recolectado.");
            GuardarDatos();
        }
        else
        {
            Debug.Log($"Coleccionable '{nombre}' no encontrado.");
        }
    }

    // Verificar si un coleccionable ya fue recolectado
    public bool EsRecolectado(string nombre)
    {
        return coleccionables.ContainsKey(nombre) && coleccionables[nombre];
    }
}
