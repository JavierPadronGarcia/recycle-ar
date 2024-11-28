using UnityEngine;

public class GuardarColeccionable : MonoBehaviour
{
    public string nombreColeccionable;

    // Método que se llamará al presionar el botón
    public void Guardar()
    {
        ColeccionableManager manager = FindObjectOfType<ColeccionableManager>();
        if (manager != null)
        {
            manager.Recolectar(nombreColeccionable);
            Debug.Log($"Coleccionable '{nombreColeccionable}' guardado.");
        }
        else
        {
            Debug.LogError("No se encontró el ColeccionablesManager en la escena.");
        }
    }
}
