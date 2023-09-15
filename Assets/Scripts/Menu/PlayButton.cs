using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que deseas cambiar.

    private void Start()
    {
        // Agregar un Listener al botón para que cargue la escena al hacer clic.
        Button playButton = GetComponent<Button>();
        if (playButton != null)
        {
            playButton.onClick.AddListener(LoadGameScene);
        }
    }

    private void LoadGameScene()
    {
        // Cargar la escena especificada al hacer clic en el botón "Play".
        FindObjectOfType<SceneChanger>().LoadScene(sceneToLoad);
    }
}
