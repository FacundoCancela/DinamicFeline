using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // Cargar la escena con el nombre especificado.
        SceneManager.LoadScene(sceneName);

    }

    public void LoadNextScene()
    {
        // Cargar la siguiente escena en la secuencia.
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
