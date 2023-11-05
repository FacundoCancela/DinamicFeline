using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBar : MonoBehaviour
{
    public Slider slider;
    GameObject Player;
    CharacterStats characterStats;
    void Start()
    {
        characterStats = Player.GetComponent<CharacterStats>();
        slider.maxValue = characterStats.CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = characterStats.CurrentHealth;
    }
}
