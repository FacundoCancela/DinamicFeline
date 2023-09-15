using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        // Salir de la aplicación (funciona en plataformas compatibles).
        Application.Quit();

        // Si estás en el editor de Unity, detén la reproducción en el editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
