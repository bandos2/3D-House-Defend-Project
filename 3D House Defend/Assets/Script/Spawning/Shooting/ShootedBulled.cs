using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootedBulled : MonoBehaviour // Attached to every shouted bullet and sends info about last hitted obj
{
    public string wasShootFrom;
    private void OnCollisionEnter(Collision collision)
    {
        GameObject.Find(wasShootFrom).GetComponentInChildren<ShootingManager>().LastHitInfo = collision.gameObject.name; 
        //FindObjectOfType<ShootingManager>().LastHitInfo = collision.gameObject.name;     
    }
}
