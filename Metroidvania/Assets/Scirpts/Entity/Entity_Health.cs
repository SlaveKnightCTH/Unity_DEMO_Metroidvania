using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEditor.SceneTemplate;
using UnityEngine;
using UnityEngine.UI;

public class Entity_Health : MonoBehaviour,Idamageable
{
    private Entity_VFX entityVfx;
    private Entity entity;
    private Slider healthBar;
    private Entity_Stats stats;

    [SerializeField] protected float currentHP;
    [SerializeField] protected bool isDead;

    [Header("On Damage KnockBack")]
    [SerializeField] private Vector2 knockBackSpeed=new Vector2(1.5f,2.5f);
    [SerializeField] private float knockBackDurantion=0.2f;
    [Header("Heavy Damage")]
    [SerializeField] private float heavyDamageThreshold = 0.3f;//>30%
    [SerializeField] private Vector2 heavyKnockBackSpeed = new Vector2(7, 7);
    [SerializeField] private float heavyKnockBackDuration = 0.5f;

    private void Awake()
    {
        entityVfx = GetComponent<Entity_VFX>();
        entity = GetComponent<Entity>();
        healthBar = GetComponentInChildren<Slider>();
        stats = GetComponent<Entity_Stats>();

        currentHP = stats.GetMaxHP();
        UpdateHealthBar();
         
    }

    public void UpdateHealthBar()
    {
        if (healthBar == null)
            return;
        healthBar.value = currentHP / stats.GetMaxHP();
    }



    //受伤
    public virtual void TakeDamage(float damage,Transform damageDealer)//伤害，攻击方的transform
    {
        if (isDead)
            return;

        //受伤时 entity被弹飞
        entity?.ReciveKnockBack(CalculateKnockBackSpeed(damage,damageDealer), CalculateKnockBackDuratin(damage));
        //调用VFX，将entity变白
        entityVfx?.PlayDamageVFX();
        ReduceHP(damage);
    }

    public void ReduceHP(float damage) 
    {
        currentHP -= damage;
        UpdateHealthBar();

        if (currentHP <= 0)
            Die();
    }

    private void Die()
    {
        isDead = true;
        entity.EntityDeath();
    }

    private Vector2 CalculateKnockBackSpeed(float damage,Transform damageDealer)
    {
        //攻击方在右边为-1
        int direction = damageDealer.transform.position.x < entity.transform.position.x ? 1 : -1;


        if (isHeavyDamage(damage))
            return heavyKnockBackSpeed * new Vector2(direction, 1);
        else
            return knockBackSpeed * new Vector2(direction, 1);
         
    }

    private float CalculateKnockBackDuratin(float damage) => isHeavyDamage(damage) ? heavyKnockBackDuration : knockBackDurantion;

    private bool isHeavyDamage(float damage) => damage / stats.GetMaxHP() >=heavyDamageThreshold;
}
