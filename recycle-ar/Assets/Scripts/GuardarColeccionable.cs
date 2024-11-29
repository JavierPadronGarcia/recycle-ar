using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuardarColeccionable : MonoBehaviour
{
    public string nombreColeccionable;
    public TextMeshProUGUI textoEstado;
    public Button botonRecolectar;

    private ColeccionableManager manager;

    private void Start()
    {
        manager = FindObjectOfType<ColeccionableManager>();

        if (manager == null)
        {
            Debug.LogError("No se encontró el ColeccionableManager en la escena.");
            return;
        }

        ActualizarUI();
    }

    public void Guardar()
    {
        if (manager != null)
        {
            manager.Recolectar(nombreColeccionable);
            Debug.Log($"Coleccionable '{nombreColeccionable}' guardado.");
            ActualizarUI();
        }
    }

    private void ActualizarUI()
    {
        if (manager.EsRecolectado(nombreColeccionable))
        {
            textoEstado.gameObject.SetActive(true);
            botonRecolectar.gameObject.SetActive(false);
        }
        else
        {
            textoEstado.gameObject.SetActive(false);
            botonRecolectar.gameObject.SetActive(true);
        }
    }
}

