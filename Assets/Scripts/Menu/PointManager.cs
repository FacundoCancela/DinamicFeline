using UnityEngine;
using System.Collections.Generic;

public class PointManager : MonoBehaviour
{
    // Variable para mantener un registro de las puntuaciones anteriores.
    private Stack<int> scoreHistory = new Stack<int>();

    // Variable para almacenar la puntuaci�n actual.
    private int currentScore = 0;

    // Variable est�tica para mantener una �nica instancia del PointManager.
    private static PointManager instance;

    // Propiedad est�tica para acceder a la instancia del PointManager.
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
            // Establece esta instancia como la instancia �nica.
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Funci�n para sumar puntos.
    public void AddPoints(int pointsToAdd)
    {
        // Actualiza la puntuaci�n actual.
        currentScore += pointsToAdd;

        // Aqu� puedes realizar cualquier l�gica adicional, como actualizar la interfaz de usuario, etc.
    }

    // Funci�n para obtener la puntuaci�n actual.
    public int GetScore()
    {
        return currentScore;
    }

    public void ResetScore()
    {
        currentScore = 0;
    }

    // Funci�n para obtener el registro de puntuaciones anteriores.
    public Stack<int> GetScoreHistory()
    {
        return scoreHistory;
    }

    // Funci�n para guardar el puntaje final en la pila.
    public void SaveFinalScore()
    {
        scoreHistory.Push(currentScore);
    }
}
