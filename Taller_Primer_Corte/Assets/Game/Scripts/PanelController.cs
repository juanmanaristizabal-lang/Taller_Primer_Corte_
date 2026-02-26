using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    public GameObject prefabMisiones;
    public GameObject prefabColeccionables;

    public void AbrirMisiones()
    {
        prefabMisiones.SetActive(true);
        prefabColeccionables.SetActive(false);

    }

    public void AbrirColeccionables()
    {
        prefabColeccionables.SetActive(true);
        prefabMisiones.SetActive(false);
    }

    public void volverMenu()
    {         prefabMisiones.SetActive(false);
        prefabColeccionables.SetActive(false);
    }
}
