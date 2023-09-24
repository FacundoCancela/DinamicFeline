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
        // Salir de la aplicación (funciona en plataformas compatibles).
        Application.Quit();

        // Si estás en el editor de Unity, detén la reproducción en el editor.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
