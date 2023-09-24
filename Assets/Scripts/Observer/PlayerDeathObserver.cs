using UnityEngine;

public class PlayerDeathObserver : MonoBehaviour, IPlayerDeathObserver
{
    public EndLevelButton endLevelButton;

    public void OnPlayerDeath()
    {
        endLevel();
    }

    private void endLevel()
    {
        FindObjectOfType<SceneChanger>().LoadScene("ScoreMenu");
    }
}
