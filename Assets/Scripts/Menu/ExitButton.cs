using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Salir de la aplicaci�n (funciona en plataformas compatibles).
        Application.Quit();

        // Si est�s en el editor de Unity, det�n la reproducci�n en el editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
