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
    public GameObject PanelMisionCompletada;
    public GameObject PanelMisionEliminada;

    [Header("Scroll View")]
    public Transform contentMisiones;
    public GameObject missionPrefab;

    [Header("Paneles")]
    public GameObject panelAviso;

<<<<<<< HEAD
    [Header("Scroll Coleccionables")]
    public Transform contentColeccionables;
    public GameObject prefabColeccionableItem;


=======
>>>>>>> parent of d40fb7e (hola pausa aktiva)
    void Start()
        {
            CargarDatos();
            MostrarColeccionables();
            MostrarMisionActual();
        }

<<<<<<< HEAD
        void OnEnable()
        {
            CargarDatos();
            MostrarColeccionables();
            MostrarMisionActual();
=======
        CargarDatos();
        MostrarColeccionables();
        MostrarMisionActual();
        CrearListaMisionesUI();
>>>>>>> parent of d40fb7e (hola pausa aktiva)
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
<<<<<<< HEAD
   public void MostrarColeccionables()
=======

    void CrearListaMisionesUI()//como esto tiene que ver con el prefab lo hace fifo pero a mi me parece codigo innesesario
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
>>>>>>> parent of d40fb7e (hola pausa aktiva)
    {
        textoColeccionables.text = "";

        foreach (Coleccionables c in listaColeccionables)
        {
            string color = ObtenerColorRareza(c.Rareza);
            textoColeccionables.text +=
                $"<color={color}>{c.Nombre}</color>\n\n";
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
        textoMisionActual.text =
            $"Misión Actual:\n{m.Titulo}\n{m.Descripcion}";
    }
    public void CompletarMision()
    {
        if (pilaMisiones.Count == 0) return;

        Misiones m = pilaMisiones.Pop();
        ultimaMision = m;
        m.Completada = true;

        MostrarMisionActual();
        PanelMisionCompletada.SetActive(true);
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
            if (c.Nombre.Equals( textoBusqueda))
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
            panelAviso.SetActive(true);
            return;
        }

        pilaMisiones.Push(ultimaMision);
        ultimaMision = null;

        MostrarMisionActual();
        PanelMisionEliminada.SetActive(true);
    }
<<<<<<< HEAD

    public void EsconderPanelCompletado()
    {
        PanelMisionCompletada.SetActive(false);
    }

    public void EsconderPanelTerminada()
    {
        PanelMisionEliminada.SetActive(false);
    }

=======
    public void EsconderAviso()
    { 
        panelAviso.SetActive(false); 
    }
>>>>>>> parent of d40fb7e (hola pausa aktiva)
}