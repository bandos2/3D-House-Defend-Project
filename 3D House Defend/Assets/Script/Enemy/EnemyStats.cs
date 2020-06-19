using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Range(1, 10)]
    public int MaxHealth;

    public int CurrentHealth;

    public void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public void ResiveDamage(int Amount)
    {
        CurrentHealth -= Amount;
    }

    private void Update()
    {
        if(CurrentHealth <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        Destroy(this.gameObject);
    }

}
