using UnityEngine;

[System.Serializable] 
public class Coleccionables
{
    public string nombre;
    public string rareza;
    public int valor;

    public string Nombre { get => nombre; set => nombre = value; }
    public string Rareza { get => rareza; set => rareza = value; }
    public int Valor { get => valor; set => valor = value; }

    public Coleccionables(string nombre, string rareza, int valor)
    {
        this.nombre = nombre;
        this.rareza = rareza;
        this.valor = valor;
    }
}
