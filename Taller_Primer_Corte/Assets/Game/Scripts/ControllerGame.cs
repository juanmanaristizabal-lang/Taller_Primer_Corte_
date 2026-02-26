using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Resources;

public class ControllerGame : MonoBehaviour
{
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
    public Stack<Misiones> pilaMisiones = new Stack<Misiones>();

    public TextMeshProUGUI textoColeccionables;
    public TextMeshProUGUI textoMisionActual;
    public TMP_InputField textobuscar;
    public Misiones ultimaMision = null;

    [Header("Scroll View")]
    public Transform contentMisiones;
    public GameObject missionPrefab;

    [Header("Paneles")]
    public GameObject panelAviso;

    [Header("Inventario UI")]
    public Transform contentColeccionables;
    public GameObject coleccionablePrefab;

    [Header("Detalle UI")]
    public TMP_Text nombreDetalle;
    public TMP_Text rarezaDetalle;
    public TMP_Text valorDetalle;

    void Start()
    {
        ultimaMision = null;

        CargarDatos();
        MostrarColeccionables();
        MostrarMisionActual();
        CrearListaMisionesUI();
        CrearInventarioUI();
    }

    void CargarDatos()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Misiones_Coleccionables.json");

        if (!File.Exists(path))
        {
            Debug.LogError("No se encontró JSON");
            return;
        }

        string json = File.ReadAllText(path);
        Misiones_Coleccionables data = JsonUtility.FromJson<Misiones_Coleccionables>(json);

        listaColeccionables = data.coleccionables;

        for (int i = data.misiones.Count - 1; i >= 0; i--)
            pilaMisiones.Push(data.misiones[i]);
    }

    void CrearListaMisionesUI()
    {
        foreach (Transform child in contentMisiones)
            Destroy(child.gameObject);

        foreach (Misiones m in pilaMisiones)
        {
            GameObject obj = Instantiate(missionPrefab, contentMisiones);

            MissionItemUI ui = obj.GetComponent<MissionItemUI>();
            ui.Setup(m);
        }
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
            textoMisionActual.text = "No hay misiones";
            return;
        }

        Misiones m = pilaMisiones.Peek();
        textoMisionActual.text = $"Misión Actual:\n{m.Titulo}\n{m.Descripcion}";
    }

    public void CompletarMision()
    {
        if (pilaMisiones.Count == 0) return;

        Misiones m = pilaMisiones.Pop();
        ultimaMision = m;
        m.Completada = true;

        MostrarMisionActual();
        CrearListaMisionesUI();
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

    public void undo()
    {
        Debug.Log("SE UNDIO");

        if (ultimaMision == null)
        {
            Debug.Log("No se puede hacer undo porque no se ha completado ninguna misión.");
            panelAviso.SetActive(true);
            return;
        }

        pilaMisiones.Push(ultimaMision);
        Debug.Log("la ultima mision es :" + ultimaMision.Titulo + ".");

        ultimaMision = null;

        MostrarMisionActual();
        CrearListaMisionesUI();
    }

    public void EsconderAviso()
    {
        panelAviso.SetActive(false);
    }

    void CrearInventarioUI()
    {
        foreach (Transform child in contentColeccionables)
            Destroy(child.gameObject);

        foreach (Coleccionables c in listaColeccionables)
        {
            GameObject obj = Instantiate(coleccionablePrefab, contentColeccionables);

            ColeccionableItemUI ui = obj.GetComponent<ColeccionableItemUI>();
            ui.Setup(c, this);
        }
    }

    public void MostrarDetalle(Coleccionables c)
    {
        nombreDetalle.text = "Nombre: " + c.Nombre;
        rarezaDetalle.text = "Rareza: " + c.Rareza;
        valorDetalle.text = "Valor: " + c.Valor;
    }
}