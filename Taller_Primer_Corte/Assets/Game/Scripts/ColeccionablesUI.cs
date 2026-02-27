using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ColeccionableItemUI : MonoBehaviour
{
    public Image icono;
    public TMP_Text nombreText;
    public TMP_Text rarezaText;
    public TMP_Text valorText;
    public Image fondo;

    public void Setup(Coleccionables data)
    {
        nombreText.text = data.Nombre;
        rarezaText.text = data.Rareza;
        valorText.text = "Valor: " + data.Valor;

        fondo.color = ObtenerColorRareza(data.Rareza);
    }

    Color ObtenerColorRareza(string rareza)
    {
        switch (rareza.ToLower())
        {
            case "comun": return Color.white;
            case "poco comun": return Color.green;
            case "raro": return Color.blue;
            case "epico": return new Color(0.6f, 0f, 1f);
            case "legendario": return Color.yellow;
        }
        return Color.gray;
    }
}