using UnityEngine;

public class Collectionable : MonoBehaviour
{
    public string nombreColeccionable;  // Nombre único del coleccionable
    public Material materialBlanco;     // Material si no se ha recolectado
    public Material materialObtenido;   // Material si ya fue recolectado

    public Renderer objetoRenderer;     // Atributo público del tipo Renderer

    void Start()
    {
        // Si el Renderer no se asignó en el inspector, intenta obtenerlo automáticamente
        if (objetoRenderer == null)
        {
            objetoRenderer = GetComponent<Renderer>();
        }

        // Verificar si el coleccionable ya ha sido recolectado
        ColeccionableManager manager = FindObjectOfType<ColeccionableManager>();
        if (manager != null)
        {
            bool obtenido = manager.EsRecolectado(nombreColeccionable);
            CambiarMaterial(obtenido);
        }
        else
        {
            Debug.LogError("No se encontró el ColeccionablesManager en la escena.");
        }
    }

    // Cambiar el material según el estado del coleccionable
    private void CambiarMaterial(bool obtenido)
    {
        if (objetoRenderer != null)
        {
            if (obtenido)
            {
                objetoRenderer.material = materialObtenido;
            }
            else
            {
                objetoRenderer.material = materialBlanco;
            }
        }
        else
        {
            Debug.LogError("No se encontró un Renderer asociado al objeto.");
        }
    }
}
