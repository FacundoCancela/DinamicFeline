using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuickSortHighScores : MonoBehaviour
{
    public TextMeshProUGUI globalHighScore; // Donde se muestran los jugadores
    public int cantidadJugadores = 5; // Número de jugadores.
    private string nombreDelJugador;

    private PointManager pointManager;
    private List<Puntajes> puntajes;

    private void Start()
    {
        pointManager = PointManager.Instance;
        nombreDelJugador = "NombreDelJugador";
        GenerarPuntajesAleatorios();
    }

    private void GenerarPuntajesAleatorios()
    {
        puntajes = new List<Puntajes>();

        for (int i = 1; i <= cantidadJugadores; i++)
        {
            puntajes.Add(new Puntajes("Jugador " + i, Random.Range(10, 200)));
        }

        Stack<int> scoreHistory = pointManager.GetScoreHistory();
        foreach (int score in scoreHistory)
        {
            puntajes.Add(new Puntajes(nombreDelJugador, score));
        }

        QuickSort.Sort(puntajes); // Utiliza el algoritmo QuickSort para ordenar los puntajes.
        MostrarPuntajes();
    }

    private void OrdenarPuntajes()
    {
        QuickSort.Sort(puntajes);
    }

    private void MostrarPuntajes()
    {
        string puntajesTexto = "";

        for (int i = 0; i < puntajes.Count; i++)
        {
            puntajesTexto += (i + 1) + "°Lugar " + puntajes[i].Jugador + " - Puntaje: " + puntajes[i].Puntaje + "\n";
            Debug.Log((i + 1) + "°Lugar " + puntajes[i].Jugador + " - Puntaje: " + puntajes[i].Puntaje);
        }

        globalHighScore.text = puntajesTexto;
    }
}
