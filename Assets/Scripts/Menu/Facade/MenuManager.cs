using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour, IMenuManager
{
    public void LoadScene(string sceneName)
    {
        // Cargar la escena con el nombre especificado.
        SceneManager.LoadScene(sceneName);
    }
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
