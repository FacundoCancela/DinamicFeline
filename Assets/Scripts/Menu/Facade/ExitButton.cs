using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    private IMenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    public void ExitGame()
    {
        menuManager.ExitGame();
    }
}
