using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class ControllerGame : MonoBehaviour
{
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
    //public List<Misiones> listaMisiones = new List<Misiones>(); // CAMBIAR A PILA
    public TextMeshProUGUI textoColeccionables; 

    void Start()
    {
        CargarDatos();
        MostrarColeccionables();
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
}

