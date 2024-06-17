using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{   
    public float startSpeed = 3f;
    [SerializeField] private Image healthBar;
    [HideInInspector]
    public float speed;
    public float health = 100; 
    public float startHealth = 100;  
    [SerializeField] int value = 50;
    [SerializeField] GameObject deathEffect;
    private bool isDead = false;
    

    void Start()
    {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
   
        healthBar.fillAmount = health/startHealth;
        if(health <= 0)
        {
            if(!isDead)
            {
                isDead = true;
                Die();
            }     
        }
    }

    void Die()
    {
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        PlayerStats.Money += value;
        Destroy(gameObject);
        WaveSpawner.EnemiesAlive--;
    }

    public void Slow (float pct)
    {
        speed = (1-pct) * startSpeed;
    }
}
