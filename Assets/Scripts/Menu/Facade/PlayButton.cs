using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    public string sceneToLoad; // Nombre de la escena a la que deseas cambiar.
    private IMenuManager menuManager;
    private PointManager pointManager;
    AudioSource audioSource;
    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
        pointManager = FindObjectOfType<PointManager>();

        // Agregar un Listener al botón para que cargue la escena al hacer clic.
        Button playButton = GetComponent<Button>();
        if (playButton != null)
        {
            playButton.onClick.AddListener(LoadGameScene);
            
        }
    }

    private void LoadGameScene()
    {
        pointManager.ResetScore();
        menuManager.LoadScene(sceneToLoad);
    }
}
