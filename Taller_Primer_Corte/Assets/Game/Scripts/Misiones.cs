[System.Serializable]
public class Misiones
{
    public int id;
    public string titulo;
    public string descripcion;
    public bool completada = false;

    public int Id { get => id; set => id = value; }
    public string Titulo { get => titulo; set => titulo = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public bool Completada { get => completada; set => completada = value; }

    public Misiones(int id, string titulo, string descripcion)
    {
        this.id = id;
        this.titulo = titulo;
        this.descripcion = descripcion;
    }
}