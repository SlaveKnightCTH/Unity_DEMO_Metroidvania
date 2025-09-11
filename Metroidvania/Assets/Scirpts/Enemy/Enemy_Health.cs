using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
    Enemy enemy => GetComponent<Enemy>();

    //�жϣ���������ʱ�����������ս��״̬������ս��״̬
    //ͬʱ����player��transform
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
