using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ABBHighScores : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    private ABB abb;
    private PointManager pointManager;

    private string nombreDelJugador;

    private void Start()
    {
        abb = new ABB();
        pointManager = PointManager.Instance;
        nombreDelJugador = "NombreDelJugador";

        // Obtener jugadores desde PointManager y agregarlos al ABB
        Stack<int> scoreHistory = pointManager.GetScoreHistory();
        foreach (int score in scoreHistory)
        {
            Jugador jugador = new Jugador(nombreDelJugador, score);
            abb.AgregarJugador(jugador);
        }

        // Obtener jugadores ordenados desde el ABB
        List<Jugador> jugadoresOrdenados = abb.ObtenerJugadoresEnOrden();

        // Mostrar los jugadores ordenados
        string puntajesTexto = "";
        for (int i = 0; i < jugadoresOrdenados.Count; i++)
        {
            puntajesTexto += (i + 1) + "° Lugar " + jugadoresOrdenados[i].Nombre + " - Puntaje: " + jugadoresOrdenados[i].Puntaje + "\n";
            Debug.Log((i + 1) + "° Lugar " + jugadoresOrdenados[i].Nombre + " - Puntaje: " + jugadoresOrdenados[i].Puntaje);
        }

        highScore.text = puntajesTexto;
    }
}