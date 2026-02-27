
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerGame : MonoBehaviour
{
    // =========================
    // DATOS
    // =========================
    public List<Coleccionables> listaColeccionables = new List<Coleccionables>();
    public Stack<Misiones> pilaMisiones = new Stack<Misiones>();
    public Stack<Misiones> pilaCompletadas = new Stack<Misiones>();

    // =========================
    // UI MISIONES
    // =========================
    public TextMeshProUGUI textoMisionActual;
    public RawImage imagenMision;
    public TextMeshProUGUI misionesCompletadas;
    public Image menuColores;

    // =========================
    // ITEMS COLECCIONABLES (6 MANUALES)
    // =========================
    [Header("Items UI")]
    public ColeccionableUI item1;
    public ColeccionableUI item2;
    public ColeccionableUI item3;
    public ColeccionableUI item4;
    public ColeccionableUI item5;
    public ColeccionableUI item6;

    // =========================
    // PANEL INFO
    // =========================
    [Header("Panel Info Coleccionable")]
    public GameObject panelInfoColeccionable;
    public Image infoImagen;
    public TextMeshProUGUI infoNombre;
    public TextMeshProUGUI infoRareza;
    public TextMeshProUGUI infoValor;

    [Header("Imagenes Misiones")]
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

    // =========================
    // PANELES
    // =========================
    public GameObject panelMenu;
    public GameObject panelMisiones;
    public GameObject panelColeccionables;
    public GameObject panelAviso;

    private int ultimaAccion = 0;
    private Misiones ultimaMisionRemovida;

    // =====================================================
    // START
    // =====================================================
    void Start()
    {
        CargarDatos();
        ConfigurarColeccionablesUI();
        MostrarMisionActual();

        panelInfoColeccionable.SetActive(false);
        panelMenu.SetActive(true);
        panelMisiones.SetActive(false);
        panelColeccionables.SetActive(false);
    }

    // =====================================================
    // CARGAR JSON
    // =====================================================
    void CargarDatos()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Misiones_Coleccionables.json");

        if (!File.Exists(path))
        {
            Debug.LogError("No se encontró el JSON");
            return;
        }

        string json = File.ReadAllText(path);
        Misiones_Coleccionables data = JsonUtility.FromJson<Misiones_Coleccionables>(json);

        listaColeccionables = data.coleccionables;

        for (int i = data.misiones.Count - 1; i >= 0; i--)
            pilaMisiones.Push(data.misiones[i]);
    }

    // =====================================================
    // CONFIGURAR ITEMS (6 FIJOS)
    // =====================================================
    void ConfigurarColeccionablesUI()
    {
        if (listaColeccionables.Count < 6)
        {
            Debug.LogError("El JSON necesita mínimo 6 coleccionables");
            return;
        }

        item1.Setup(listaColeccionables[0], this);
        item2.Setup(listaColeccionables[1], this);
        item3.Setup(listaColeccionables[2], this);
        item4.Setup(listaColeccionables[3], this);
        item5.Setup(listaColeccionables[4], this);
        item6.Setup(listaColeccionables[5], this);
    }

    // =====================================================
    // MOSTRAR INFO COLECCIONABLE
    // =====================================================
    public void MostrarInfoColeccionable(Coleccionables c, Sprite img)
    {
        panelInfoColeccionable.SetActive(true);

        infoImagen.sprite = img;
        infoNombre.text = c.Nombre;
        infoRareza.text = "Rareza: " + c.Rareza;
        infoValor.text = "Valor: " + c.Valor;
    }

    public void CerrarInfoColeccionable()
    {
        panelInfoColeccionable.SetActive(false);
    }

    // =====================================================
    // MISIONES
    // =====================================================
    public void MostrarMisionActual()
    {
        if (pilaMisiones.Count == 0) return;

        Misiones m = pilaMisiones.Peek();

        textoMisionActual.text =
            $"Misión Actual:\n{m.Titulo}\n{m.Descripcion}";

        // ===== IMAGEN SEGUN ID =====
        Sprite spriteActual = null;

        switch (m.id)
        {
            case 1: spriteActual = imagen1; break;
            case 2: spriteActual = imagen2; break;
            case 3: spriteActual = imagen3; break;
            case 4: spriteActual = imagen4; break;
            case 5: spriteActual = imagen5; break;
            case 6: spriteActual = imagen6; break;
            case 7: spriteActual = imagen7; break;
            case 8: spriteActual = imagen8; break;
            case 9: spriteActual = imagen9; break;
            case 10: spriteActual = imagen10; break;
            case 11: spriteActual = imagen11; break;
            case 12: spriteActual = imagen12; break;
            case 13: spriteActual = imagen13; break;
            case 14: spriteActual = imagen14; break;
            case 15: spriteActual = imagen15; break;
            case 16: spriteActual = imagen16; break;
            case 17: spriteActual = imagen17; break;
        }

        if (spriteActual != null)
            imagenMision.texture = spriteActual.texture;

        menuColores.color = m.Completada ? Color.green : Color.red;

        MostrarMisionesCompletadas();
    }
    public void CompletarMision()
    {
        if (pilaMisiones.Count == 0) return;

        Misiones m = pilaMisiones.Peek();

        if (m.Completada) return;

        m.Completada = true;
        pilaCompletadas.Push(m);

        ultimaAccion = 1;
        MostrarMisionActual();
    }

    public void CambiarMision()
    {
        if (pilaMisiones.Count == 0) return;

        ultimaMisionRemovida = pilaMisiones.Pop();
        ultimaAccion = 2;

        MostrarMisionActual();
    }

    public void undo()
    {
        if (ultimaAccion == 0)
        {
            panelAviso.SetActive(true);
            return;
        }

        if (ultimaAccion == 1 && pilaCompletadas.Count > 0)
        {
            Misiones m = pilaCompletadas.Pop();
            m.Completada = false;
        }
        else if (ultimaAccion == 2 && ultimaMisionRemovida != null)
        {
            pilaMisiones.Push(ultimaMisionRemovida);
            ultimaMisionRemovida = null;
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
            misionesCompletadas.text += m.Titulo + "\n";
    }

    // =====================================================
    // NAVEGACIÓN
    // =====================================================
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

    public void EsconderAviso()
    {
        panelAviso.SetActive(false);
    }
}