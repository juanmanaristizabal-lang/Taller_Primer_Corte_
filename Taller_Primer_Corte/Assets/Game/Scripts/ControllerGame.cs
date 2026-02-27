
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ControllerGame : MonoBehaviour
{
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
    public Stack<Misiones> pilaMisiones = new Stack<Misiones>();
    public Stack<Misiones> pilaCompletadas = new Stack<Misiones>();

    public TextMeshProUGUI textoColeccionables;
    public TextMeshProUGUI textoMisionActual;
    public TMP_InputField textobuscar;
    private int ultimaAccion = 0;
    private Misiones ultimaMisionRemovida;
    public RawImage imagenMision;
    public TextMeshProUGUI misionesCompletadas;

    [Header("Paneles")]
    public GameObject panelMenu;
    public GameObject panelMisiones;
    public GameObject panelColeccionables;
    public GameObject panelAviso;

    [Header("Misiones")]
    public Sprite imagen1;
    public Sprite imagen2;
    public Sprite imagen3;
    public Sprite imagen4;
    public Sprite imagen5;
    public Sprite imagen6;
    public Sprite imagen7;
    public Sprite imagen8;
    public Sprite imagen9;
    public Sprite imagen10;
    public Sprite imagen11;
    public Sprite imagen12;
    public Sprite imagen13;
    public Sprite imagen14;
    public Sprite imagen15;
    public Sprite imagen16;
    public Sprite imagen17;
    public Image menuColores;

    void Start()
    {

        CargarDatos();
        MostrarColeccionables();
        MostrarMisionActual();
        panelMenu.SetActive(true);
        panelMisiones.SetActive(false);
        panelColeccionables.SetActive(false);
    }

    void CargarDatos()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Misiones_Coleccionables.json");

        if (!File.Exists(path))
        {
            return;
        }

        string json = File.ReadAllText(path);
        Misiones_Coleccionables data = JsonUtility.FromJson<Misiones_Coleccionables>(json);

        listaColeccionables = data.coleccionables;

        for (int i = data.misiones.Count - 1; i >= 0; i--)
            pilaMisiones.Push(data.misiones[i]);
    }



    void MostrarColeccionables()
    {
        textoColeccionables.text = "";

        foreach (Coleccionables c in listaColeccionables)
        {
            string color = ObtenerColorRareza(c.Rareza);

            textoColeccionables.text +=
                $"<color={color}>Nombre: {c.Nombre}\nRareza: {c.Rareza}\nValor: {c.Valor}</color>\n\n";
        }
    }

    string ObtenerColorRareza(string rareza)
    {
        switch (rareza.ToLower())
        {
            case "comun": return "#CDCDCD";
            case "poco comun": return "green";
            case "raro": return "blue";
            case "epico": return "#FF00FF";
            case "legendario": return "yellow";
            default: return "white";
        }
    }

    public void MostrarMisionActual()
    {
        if (pilaMisiones.Count == 0)
        {
            return;
        }
        else
        {

            Misiones m = pilaMisiones.Peek();

            textoMisionActual.text = $"Misión Actual:\n{m.Titulo}\n{m.Descripcion}";

            switch (m.id)
            {
                case 1: imagenMision.texture = imagen1.texture; break;
                case 2: imagenMision.texture = imagen2.texture; break;
                case 3: imagenMision.texture = imagen3.texture; break;
                case 4: imagenMision.texture = imagen4.texture; break;
                case 5: imagenMision.texture = imagen5.texture; break;
                case 6: imagenMision.texture = imagen6.texture; break;
                case 7: imagenMision.texture = imagen7.texture; break;
                case 8: imagenMision.texture = imagen8.texture; break;
                case 9: imagenMision.texture = imagen9.texture; break;
                case 10: imagenMision.texture = imagen10.texture; break;
                case 11: imagenMision.texture = imagen11.texture; break;
                case 12: imagenMision.texture = imagen12.texture; break;
                case 13: imagenMision.texture = imagen13.texture; break;
                case 14: imagenMision.texture = imagen14.texture; break;
                case 15: imagenMision.texture = imagen15.texture; break;
                case 16: imagenMision.texture = imagen16.texture; break;
                case 17: imagenMision.texture = imagen17.texture; break;
            }
            if (m.Completada)
                menuColores.color = Color.green;
            else
                menuColores.color = Color.red;
            MostrarMisionesCompletadas();
        }
    }
    public void CompletarMision()
    {
        if (pilaMisiones.Count == 0) return;

        Misiones m = pilaMisiones.Peek();

        if (m.Completada)
        {
            return;
        }

        m.Completada = true;
        pilaCompletadas.Push(m);

        ultimaAccion = 1;

        MostrarMisionActual();
    }
    public void buscar()
    {

        string textoBusqueda = textobuscar.text;
        if (string.IsNullOrEmpty(textoBusqueda))
        {
            textoColeccionables.text = "Escribe algo para buscar...";
            return;
        }

        textoColeccionables.text = "";

        bool encontrado = false;


        foreach (Coleccionables c in listaColeccionables)
        {
            if (c.Nombre.Equals(textoBusqueda))
            {
                string color = ObtenerColorRareza(c.Rareza);

                textoColeccionables.text +=
                    $"<color={color}>Nombre: {c.Nombre}\n" +
                    $"Rareza: {c.Rareza}\n" +
                    $"Valor: {c.Valor}</color>\n\n";

                encontrado = true;
            }
        }

        if (!encontrado)
        {
            textoColeccionables.text = "No se encontro ningun objeto.";
        }
    }
    public void CambiarMision()
    {
        if (pilaMisiones.Count == 0) return;

        ultimaMisionRemovida = pilaMisiones.Pop();

        MostrarMisionActual();
        ultimaAccion = 2;
    }
    public void undo()
    {
        if (ultimaAccion == 0)
        {
            panelAviso.SetActive(true);
            return;
        }
        if (ultimaAccion == 1)
        {
            if (pilaCompletadas.Count > 0)
            {
                Misiones m = pilaCompletadas.Pop();
                m.Completada = false;
            }
        }
        else if (ultimaAccion == 2)
        {
            if (ultimaMisionRemovida != null)
            {
                pilaMisiones.Push(ultimaMisionRemovida);
                ultimaMisionRemovida = null;
            }
        }

        ultimaAccion = 0;
        MostrarMisionActual();
    }
    void MostrarMisionesCompletadas()
    {
        misionesCompletadas.text = "";

        if (pilaCompletadas.Count == 0)
        {
            misionesCompletadas.text = "No hay misiones completadas";
            return;
        }

        foreach (Misiones m in pilaCompletadas)
        {
            misionesCompletadas.text += m.Titulo + "\n";
        }
    }

    public void EsconderAviso()
    {
        panelAviso.SetActive(false);
    }
    public void AbrirMisiones()
    {
        panelMisiones.SetActive(true);
        panelColeccionables.SetActive(false);
        panelMenu.SetActive(false);
    }

    public void AbrirColeccionables()
    {
        panelColeccionables.SetActive(true);
        panelMisiones.SetActive(false);
        panelMenu.SetActive(false);
    }

    public void RegresarMenu()
    {
        panelMenu.SetActive(true);
        panelMisiones.SetActive(false);
        panelColeccionables.SetActive(false);
    }


}