using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
    Enemy enemy => GetComponent<Enemy>();

    //判断：敌人受伤时，如果不处于战斗状态，进入战斗状态
    //同时传送player的transform
    public override void TakeDamage(float damage, Transform damageDealer)
    {
        base.TakeDamage(damage, damageDealer);

        if (isDead)
            return;

        //if(damageDealer.CompareTag("Player"))
        if (damageDealer.gameObject.GetComponent<Player>() != null)
            enemy.EnterBattleState(damageDealer);

        
    }
}
