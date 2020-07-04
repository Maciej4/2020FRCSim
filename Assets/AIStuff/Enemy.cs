using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int startingHealth = 100;
    public Vector3 StartPosition;

    private int currentHealth = 100;

    void Start()
    {
        currentHealth = startingHealth;
        this.transform.position = StartPosition;
    }

    public void GetShot(int damage, ShootingAgent shooter)
    {
        ApplyDamage(damage, shooter);
    }

    private void ApplyDamage(int damage, ShootingAgent shooter)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die(shooter);
        }
    }

    private void Die(ShootingAgent shooter)
    {
        Debug.Log("I died!");
        shooter.registerKill();
        Respawn();
    }

    private void Respawn()
    {
        currentHealth = startingHealth;
        this.transform.position = StartPosition;
    }

    void Update()
    {
        
    }
}
