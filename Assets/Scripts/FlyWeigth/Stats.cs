using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "Stats")]
public class Stats : ScriptableObject
{
    public int health = 100;
    public int damage = 1;
}
