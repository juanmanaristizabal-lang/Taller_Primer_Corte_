using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColeccionableUI : MonoBehaviour
{
    public Image imagen;                 // Imagen asignada manualmente
    public TextMeshProUGUI nombre;

    private Coleccionables data;
    private ControllerGame controller;

    // Se llama desde ControllerGame
    public void Setup(Coleccionables c, ControllerGame ctrl)
    {
        data = c;
        controller = ctrl;

        // Solo mostramos texto (la imagen ya está puesta en Unity)
        nombre.text = data.Nombre;
    }

    // Botón del item
    public void OnClick()
    {
        if (controller == null || data == null) return;

        // Enviamos también la imagen manual
        controller.MostrarInfoColeccionable(data, imagen.sprite);
    }
}