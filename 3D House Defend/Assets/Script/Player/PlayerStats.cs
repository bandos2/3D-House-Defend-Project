using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(50f, 500f)]
    public int MaxHealth;
    [Range(100f, 1000f)]
    public int MaxEnergy;

    public int CurrentHealth;
    public int CurrentEnergy;

    [Range(1f, 100f)]
    public int MelleAttackPower;
    [Range(1f, 100f)]
    public int RangeAttackPower;

    void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentEnergy = MaxEnergy;
    }


    void Update()
    {
        
    }
}
