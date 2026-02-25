using UnityEngine;

[System.Serializable]
public class Coleccionables
{
    public string nombre;
    public string rareza;
    public int valor;

    public string Nombre => nombre;
    public string Rareza => rareza;
    public int Valor => valor;

    public Coleccionables() { }
}