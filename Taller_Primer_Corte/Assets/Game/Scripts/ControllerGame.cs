using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ControllerGame : MonoBehaviour
{
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
   
    public Stack<Misiones> pilaMisiones = new Stack<Misiones>();   

    public TextMeshProUGUI textoColeccionables;
    public TextMeshProUGUI textoMisionActual;
   

    void Start()
    {
        CargarDatos();
        MostrarColeccionables();
        MostrarMisionActual();
    }

    void Update()
    {

    }

    void CargarDatos()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Misiones_Coleccionables.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            Misiones_Coleccionables data = JsonUtility.FromJson<Misiones_Coleccionables>(json);

            listaColeccionables = data.coleccionables;
            //listaMisiones = data.misiones;

            for (int i = data.misiones.Count - 1; i >= 0; i--)
            {
                pilaMisiones.Push(data.misiones[i]);
            }

            Debug.Log("Datos cargados correctamente");
            Debug.Log("Coleccionables: " + listaColeccionables.Count);
            //Debug.Log("Misiones: " + listaMisiones.Count);
        }
        else
        {
            Debug.LogError("No se encontró Misiones_Coleccionables.json");
        }
    }

    public Coleccionables BuscarColeccionablePorNombre(string nombre)
    {
        return listaColeccionables.Find(c =>
            c.Nombre.Equals(nombre, System.StringComparison.OrdinalIgnoreCase));
    }
    void MostrarColeccionables()
    {
        if (textoColeccionables == null) return;

        textoColeccionables.text = ""; 

        foreach (Coleccionables c in listaColeccionables)
        {
            textoColeccionables.text += $"Nombre: {c.Nombre}\nRareza: {c.Rareza}\nValor: {c.Valor}\n\n";
        }
    }

    public void MostrarMisionActual()
    {
        if (textoMisionActual is null) return;

        if (pilaMisiones.Count.Equals(0))
        {
            textoMisionActual.text = "No hay misiones activas en el momento";
            return;
        }

        Misiones m = pilaMisiones.Peek();
        textoMisionActual.text = $"Misión Actual:\n{m.Titulo}\n{m.Descripcion}";
    }

    public void CompletarMision()
    {
        if (pilaMisiones.Count.Equals(0))
        {
            Debug.Log("No hay misiones para completar");
            return;
        }
        pilaMisiones.Pop();
        MostrarMisionActual();
    }

    public void Deshacer()
    {
        if(pilaMisiones.Count.Equals(0))
        {
            Debug.Log("No hay misiones para deshacer");
            return;
        }
        pilaMisiones.Pop();
        MostrarMisionActual();
    }
}

