using UnityEngine;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    // Variable para mantener un registro de las puntuaciones anteriores.
    private Stack<int> scoreHistory = new Stack<int>();

    // Variable para almacenar la puntuación actual.
    private int currentScore = 0;

    // Variable estática para mantener una única instancia del PointManager.
    private static PointManager instance;

    // Propiedad estática para acceder a la instancia del PointManager.
    public static PointManager Instance
    {
        get { return instance; }
    }

    // Awake se llama antes del Start y garantiza que solo haya una instancia del PointManager.
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            // Si ya existe una instancia, destruye este objeto.
            Destroy(this.gameObject);
            return;
        }
        else
        {
            // Establece esta instancia como la instancia única.
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Función para sumar puntos.
    public void AddPoints(int pointsToAdd)
    {
        // Actualiza la puntuación actual.
        currentScore += pointsToAdd;

        // Aquí puedes realizar cualquier lógica adicional, como actualizar la interfaz de usuario, etc.
    }

    // Función para obtener la puntuación actual.
    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    // Función para obtener el registro de puntuaciones anteriores.
    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    // Función para guardar el puntaje final en la pila.
    public void SaveFinalScore()
    {
        scoreHistory.Push(currentScore);
    }
}
