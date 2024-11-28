using UnityEngine;

public class Collectionable : MonoBehaviour
{
    public string nombreColeccionable;  // Nombre �nico del coleccionable
    public Material materialBlanco;     // Material si no se ha recolectado
    public Material materialObtenido;   // Material si ya fue recolectado

    public Renderer objetoRenderer;     // Atributo p�blico del tipo Renderer

    void Start()
    {
        // Si el Renderer no se asign� en el inspector, intenta obtenerlo autom�ticamente
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
            Debug.LogError("No se encontr� el ColeccionablesManager en la escena.");
        }
    }

    // Cambiar el material seg�n el estado del coleccionable
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
            Debug.LogError("No se encontr� un Renderer asociado al objeto.");
        }
    }
}
