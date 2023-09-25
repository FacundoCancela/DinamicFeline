using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa la referencia de TextMeshPro

public class ScoreScreen : MonoBehaviour
{
    public TextMeshProUGUI scoreHistoryText; // Asigna el objeto TextMeshPro en el Inspector para mostrar el historial

    void Start()
    {
        Stack<int> scoreHistory = PointManager.Instance.GetScoreHistory();

        // Muestra el historial de puntuaciones en el TextMeshPro
        if (scoreHistoryText != null)
        {
            string historyString = "Historial de Puntuaciones:\n";

            foreach (int score in scoreHistory)
            {
                historyString += "Puntaje: " + score.ToString() + "\n";
            }

            scoreHistoryText.text = historyString;
        }
    }
}
