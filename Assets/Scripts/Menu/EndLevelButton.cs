using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelButton : MonoBehaviour
{
    public PlayerDeathObserver playerDeathObserver;
    private void Start()
    {
        // Agregar un Listener al botón para que cargue la escena al hacer clic.
        Button endButton = GetComponent<Button>();
        if (endButton != null)
        {
            endButton.onClick.AddListener(EndLevel);
        }
    }

    public void EndLevel()
    {
        playerDeathObserver.OnPlayerDeath();
    }
}
