using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ABBHighScores : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    private ABB abb;
    private PointManager pointManager;

    public class PlayerInfo
    {
        public string playerName;
        public int playerScore;
    }

    private void Start()
    {
        abb = new ABB();
        pointManager = PointManager.Instance;

        string puntajesTexto = "";
        List<PlayerInfo> jugadoresOrdenados = ObtenerJugadoresDesdePointManager();

        for (int i = 0; i < jugadoresOrdenados.Count; i++)
        {
            puntajesTexto += (i + 1) + "° Lugar " + jugadoresOrdenados[i].playerName + " - Puntaje: " + jugadoresOrdenados[i].playerScore + "\n";
            Debug.Log((i + 1) + "° Lugar " + jugadoresOrdenados[i].playerName + " - Puntaje: " + jugadoresOrdenados[i].playerScore);
        }

        highScore.text = puntajesTexto;
    }

    private List<PlayerInfo> ObtenerJugadoresDesdePointManager()
    {
        List<PlayerInfo> jugadoresOrdenados = new List<PlayerInfo>();
        Stack<int> scoreHistory = pointManager.GetScoreHistory();

        foreach (int score in scoreHistory)
        {
            PlayerInfo playerInfo = new PlayerInfo();
            playerInfo.playerName = "NombreDelJugador"; 
            playerInfo.playerScore = score;

            jugadoresOrdenados.Add(playerInfo);
        }

        jugadoresOrdenados.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));

        return jugadoresOrdenados;
    }
}
