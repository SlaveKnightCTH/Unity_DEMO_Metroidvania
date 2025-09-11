using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour,Idamageable
{
    Animator animator => GetComponentInChildren<Animator>();
    public void TakeDamage(float damage, Transform damageDealer)
    {
        animator.SetBool("open", true);
    }

}
