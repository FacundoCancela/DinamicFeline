using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreScreen : MonoBehaviour
{
    void Start()
    {
        int finalScore = PointManager.Instance.GetFinalScore();
        Debug.Log(finalScore);
    }
}
