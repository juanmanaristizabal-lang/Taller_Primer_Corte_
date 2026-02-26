using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColeccionableItemUI : MonoBehaviour
{
    public TMP_Text nombreText;
    private Coleccionables data;
    private ControllerGame controller;

    public void Setup(Coleccionables c, ControllerGame ctrl)
    {
        data = c;
        controller = ctrl;

        nombreText.text = c.Nombre;

        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        controller.MostrarDetalle(data);
    }
}