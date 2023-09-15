using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que deseas cambiar.

    private void Start()
    {
        // Agregar un Listener al bot�n para que cargue la escena al hacer clic.
        Button playButton = GetComponent<Button>();
        if (playButton != null)
        {
            playButton.onClick.AddListener(LoadGameScene);
        }
    }

    private void LoadGameScene()
    {
        // Cargar la escena especificada al hacer clic en el bot�n "Play".
        FindObjectOfType<SceneChanger>().LoadScene(sceneToLoad);
    }
}
