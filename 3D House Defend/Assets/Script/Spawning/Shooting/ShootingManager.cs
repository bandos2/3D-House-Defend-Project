using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingManager : MonoBehaviour
{
    public   ShootingAmmo[] AllPosibleBulletsType;//work on statics variables
    public  ShootingAmmo CurrentTypeOfAmmo;//may be at all make other script and seperare all enemys attacs


    public int CurrentBulletsCount;
    public int MaxBullets;
    public  float ShootingSpeed;
    public float BulletLifeTime;

    public string LastHitInfo; 

    private  float NextTimeForShooting = 0f;

    private void Awake()
    {
        foreach (ShootingAmmo BuletType in AllPosibleBulletsType) 
        {
            BuletType.name = BuletType.ShootingAmmoType.name;         
        }
        CurrentTypeOfAmmo = AllPosibleBulletsType[0];
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= NextTimeForShooting)
        {
            NextTimeForShooting = Time.time + CurrentTypeOfAmmo.TimeBetweenShoots;
            Shoot();
        }
        if (Input.GetMouseButtonDown(1))
        {
            ReloadAmmo();
        }
        if (CurrentTypeOfAmmo.HaveBulletsMaxCount)
        {
            MaxBullets = CurrentTypeOfAmmo.MaxBulletsCount;
        }
        else { MaxBullets = 0; }
        ShootingSpeed = CurrentTypeOfAmmo.ShootingSpeed;
        BulletLifeTime = CurrentTypeOfAmmo.BulletLifeTime;
    }

    public void ShootAtTarget(Transform From,Transform Target)
    {
        if (Time.time < NextTimeForShooting)
            return;
        else
        {
            NextTimeForShooting = Time.time + CurrentTypeOfAmmo.TimeBetweenShoots;
            GameObject Bullet = Instantiate(CurrentTypeOfAmmo.ShootingAmmoType, From.position, Quaternion.identity);
            Bullet.GetComponentInChildren<Rigidbody>().AddForce(Target.position * ShootingSpeed);
        }
    }


    public void Shoot() //Object to shoot, 
    {
        Debug.Log(CurrentBulletsCount);
        if (CurrentBulletsCount > 0 || MaxBullets == 0 ) // MAx Bullets == 0 means that you have no limit in bullets
        {
            GameObject Bullet = Instantiate(CurrentTypeOfAmmo.ShootingAmmoType, this.transform.position, Quaternion.identity);
            Bullet.AddComponent<ShootedBulled>();//need to make it do callback for this ShootingManager
            Bullet.GetComponentInChildren<ShootedBulled>().wasShootFrom = this.name;
            Bullet.GetComponentInChildren<Rigidbody>().useGravity = false;
            Bullet.GetComponentInChildren<Rigidbody>().AddForce(this.transform.forward * ShootingSpeed);
            StartCoroutine("DestroyBullet", Bullet);
            if (CurrentBulletsCount > 0)
            {
                CurrentBulletsCount--;
            }
        }
        else
        {
            Debug.Log("you need to RELOAD your weapon");
        }
    }
    public void ReloadAmmo()
    {
        CurrentBulletsCount = 0;
        StartCoroutine("ReloadCoorutine");
    }
    public void ChangeTypeOfAmmo()
    {

    }
    IEnumerator ReloadCoorutine() 
    {
        yield return new WaitForSeconds(CurrentTypeOfAmmo.ReloadTime);
        CurrentBulletsCount = MaxBullets;
    }
    IEnumerator DestroyBullet(GameObject spawnedObject) //destroy after manually seted life time ends
    {
            yield return new WaitForSeconds(BulletLifeTime);
            Destroy(spawnedObject);
    }
}
