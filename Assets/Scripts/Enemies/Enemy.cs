using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Base Stats")]
    public int enemyHealth;
    public int enemySpeed;
    public int enemyDamage;
    public int attackRange;

    // Enemy Target
    // Player class


    public int GetEnemyHealth() { return enemyHealth; }

    public void TakeDamage(int damage)
    {
        enemyHealth -= damage;
        CheckIfIsDead();
    }

    private void CheckIfIsDead()
    {
        if (enemyHealth <= 0)
        {

        }
    }
}
