using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();
    }

    public void DealDamage()
    {
        healthBar.currentHealth -= damage;
        healthBar.SetHealth(healthBar.currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        DealDamage();
        Destroy(gameObject);
    }

}
