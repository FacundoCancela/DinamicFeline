using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelButton : MonoBehaviour
{

    private void Start()
    {
        // Agregar un Listener al botón para que cargue la escena al hacer clic.
        Button endButton = GetComponent<Button>();
        if (endButton != null)
        {
            endButton.onClick.AddListener(LoadGameScene);
        }
    }

    public void LoadGameScene()
    {
        // Cargar la escena especificada al hacer clic en el botón "Play".
        FindObjectOfType<SceneChanger>().LoadScene("ScoreMenu");
    }
}
