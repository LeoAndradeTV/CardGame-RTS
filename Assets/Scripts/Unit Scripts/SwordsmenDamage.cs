using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmenDamage : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
    }

    public void DealDamage()
    {
        if (healthBar != null)
        {
            healthBar.currentHealth -= damage;
            healthBar.SetHealth(healthBar.currentHealth);
        }
        
    }
    
}
