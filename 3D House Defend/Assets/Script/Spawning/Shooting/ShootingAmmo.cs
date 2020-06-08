using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShootingAmmo 
{
    public GameObject ShootingAmmoType;

    public string name;

    public bool HaveBulletsMaxCount;

    [Range(1f, 100f)]
    public int MaxBulletsCount;

    [Range(0.1f, 5f)]
    public float ReloadTime;
    [Range(100f, 10000f)]
    public float ShootingSpeed;
    [Range(0.1f, 5f)]
    public float TimeBetweenShoots;
    [Range(0.1f, 3f)]
    public float BulletLifeTime;

}
