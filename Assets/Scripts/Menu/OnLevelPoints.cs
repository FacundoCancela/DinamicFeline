using UnityEngine;
using TMPro;

public class OnLevelPoints : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Referencia al componente TextMeshPro en la interfaz de usuario.
    private void Update()
    {
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText != null)
        {
            // Obtén la puntuación actual del PointManager y muestra "Points: X".
            int currentScore = PointManager.Instance.GetScore();
            scoreText.text = "SCORE - " + currentScore.ToString();
        }
    }
}
