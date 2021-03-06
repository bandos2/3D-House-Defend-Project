﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Range(50f, 500f)]
    public int MaxHealth;
    [Range(100f, 1000f)]
    public int MaxEnergy;

    [Range(1f, 100f)]
    public int MelleAttackPower;
    [Range(1f, 100f)]
    public int RangeAttackPower;

    [Range(0f, 50f)]
    public int Armor;

    public GameObject CurrentWeapon;

    public int CurrentHealth;
    public int CurrentArmor;
    public int CurrentEnergy;

    public int Realdamage;

    int WeaponDamage;

    void Start()
    {
        CurrentHealth = MaxHealth;
        CurrentEnergy = MaxEnergy;
        CurrentArmor = Armor;
    }


    void Update()
    {
        WeaponDamage = CurrentWeapon.GetComponentInChildren<Weapon>().WeaponDamage;
        Realdamage = MelleAttackPower + WeaponDamage;
    }

    public void ResiveDamage(int Amount)
    {
        int ActualDamageRecive = Amount - Armor;
        if (ActualDamageRecive > 0)
        {
            CurrentHealth -= ActualDamageRecive;
        }
        else
            return;
    }

    public void RestoreHealth(int Amount)
    {
        if (CurrentHealth < MaxHealth)
        {
            CurrentHealth += Amount;
        }
        else
            return;
    }

    public void RestoreEnergy(int Amount)
    {
        if (CurrentEnergy < MaxEnergy)
        {
            CurrentEnergy += Amount;
        }
        else
            return;
    }

    public void ChangeWeapon()
    {

    }


}
