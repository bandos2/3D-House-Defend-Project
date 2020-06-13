using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
      public int ThisBulletDamage;


    public void SetBulletDamage(int Amount)
    {
        ThisBulletDamage = Amount;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponentInChildren<PlayerStats>().ResiveDamage(ThisBulletDamage);
        }
    }
}
