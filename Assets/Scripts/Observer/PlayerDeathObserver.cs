using UnityEngine;

public class PlayerDeathObserver : MonoBehaviour, IPlayerDeathObserver
{
    public void OnPlayerDeath()
    {
        endLevel();
    }

    private void endLevel()
    {
        FindObjectOfType<PointManager>().SaveFinalScore();
        FindObjectOfType<SceneChanger>().LoadScene("ScoreMenu");
    }
}
