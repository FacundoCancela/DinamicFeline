using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelButton : MonoBehaviour
{

    private void Start()
    {
        // Agregar un Listener al bot�n para que cargue la escena al hacer clic.
        Button endButton = GetComponent<Button>();
        if (endButton != null)
        {
            endButton.onClick.AddListener(LoadGameScene);
        }
    }

    public void LoadGameScene()
    {
        // Cargar la escena especificada al hacer clic en el bot�n "Play".
        FindObjectOfType<SceneChanger>().LoadScene("ScoreMenu");
    }
}
