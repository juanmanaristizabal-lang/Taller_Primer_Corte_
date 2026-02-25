using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
    public Stack<Misiones> pilaMisiones = new Stack<Misiones>();

    public TextMeshProUGUI textoColeccionables;
    public TextMeshProUGUI textoMisionActual;

    [Header("Scroll View")]
    public Transform contentMisiones;
    public GameObject missionPrefab;

    void Start()
    {
        CargarDatos();
        MostrarColeccionables();
        MostrarMisionActual();
        CrearListaMisionesUI();
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
            case "comun": return "white";
            case "poco comun": return "green";
            case "raro": return "blue";
            case "epico": return "magenta";
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
        m.Completada = true;

        MostrarMisionActual();
        CrearListaMisionesUI();
    }
}