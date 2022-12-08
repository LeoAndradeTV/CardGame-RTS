using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public TMP_Text healthText;
    public Image fill;
    public int maxHealth = 10000;
    public int currentHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        fill.color = Color.green;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        healthText.text = $"{health}/10000";
        if (health < 4000 && health > 1000)
        {
            fill.color = Color.yellow;
            return;
        }
        if (health < 1000)
        {
            fill.color = Color.red;
        }
    }
}
