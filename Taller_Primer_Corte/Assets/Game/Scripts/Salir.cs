using UnityEngine;

public class SalirJuego : MonoBehaviour
{
    public void Salir()
    {
        Debug.Log("Cerrando juego...");

        Application.Quit();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}