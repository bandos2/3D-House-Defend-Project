using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Range(1,10)]
    public int WeaponDamage = 1;

    bool Hited = false;

    Animator Anim;

    private void Start()
    {
        Anim = GetComponentInParent<Animator>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy" && Hited == false)//was bug with one shoot kills (because on 1 animation was more than one collision)
        {
            if (Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking") && !Hited)//this way after hitting one enemy weapon till the end of animation will no more deal damage
            {
                Hited = true;
            }
            Debug.Log("Enemy hitted");
            this.GetComponentInParent<PlayerAttacks>().DeliverDamage(collision.gameObject);
        }
    }


    private void Update()
    {
        if(!Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attacking") && Hited)
        {
            Hited = false;
        }

    }

}
