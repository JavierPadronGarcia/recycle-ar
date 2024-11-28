using UnityEngine;

public class GuardarColeccionable : MonoBehaviour
{
    public string nombreColeccionable;

    // M�todo que se llamar� al presionar el bot�n
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
            Debug.LogError("No se encontr� el ColeccionablesManager en la escena.");
        }
    }
}
