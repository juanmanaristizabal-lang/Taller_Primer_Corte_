using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MissionItemUI : MonoBehaviour
{
    public TMP_Text titulo;
    public TMP_Text descripcion;
    public Image fondo;
    public Image icono;

    public void Setup(Misiones mision)
    {
        titulo.text = mision.Titulo;
        descripcion.text = mision.Descripcion;

        if (mision.Completada)
            fondo.color = Color.green;
        else
            fondo.color = Color.red;
    }
}