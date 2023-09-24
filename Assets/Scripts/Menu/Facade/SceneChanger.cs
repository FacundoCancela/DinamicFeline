using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    private IMenuManager menuManager;

    private void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }
    public void LoadScene(string sceneName)
    {
        menuManager.LoadScene(sceneName);
    }
}
