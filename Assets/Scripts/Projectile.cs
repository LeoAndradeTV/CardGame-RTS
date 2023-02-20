using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField] private int damage;
    [SerializeField] private HealthBar healthBar;

    private Rigidbody rb;

    private void Start()
    {
        //healthBar = GameObject.Find("Health Bar").GetComponent<HealthBar>();

        rb = GetComponent<Rigidbody>();
    }

    public void DealDamage()
    {
        if (gameObject.CompareTag("Arrow"))
        {
            damage = PlayerStats.Instance.archersAttackStat;
        }
        else if (gameObject.CompareTag("Siege"))
        {
            damage = PlayerStats.Instance.siegeAttackStat;
        }

        //healthBar.currentHealth -= damage;
        //healthBar.SetHealth(healthBar.currentHealth);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //DealDamage
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (rb != null)
        {
            rb.angularVelocity = Vector3.zero;
            rb.velocity = Vector3.zero;
        }
    }
}
