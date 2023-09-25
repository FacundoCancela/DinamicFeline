using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    private IMenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();

        // Agregar un Listener al bot�n para que regrese al men� al hacer clic.
        Button mainMenuButton = GetComponent<Button>();
        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(ReturnToMainMenu);
        }
    }

    private void ReturnToMainMenu()
    {
        menuManager.LoadScene("MenuInicial");
    }
}
