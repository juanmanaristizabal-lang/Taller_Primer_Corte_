using UnityEngine;

[System.Serializable] 
public class Misiones
{
    public string id;
    public string titulo;
    public string descripcion;

    public string Id { get => id; set => id = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }

    public Misiones(string id, string titulo, string descripcion)
    {
        this.id = id;
        this.titulo = titulo;
        this.descripcion = descripcion;
    }
}
