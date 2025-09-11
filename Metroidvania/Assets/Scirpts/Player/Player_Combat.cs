using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : Entity_Combat
{
    [Header("Counter attack details")]
    [SerializeField] private float counterRecovery=.1f;
    
    //ִ�з���
    //��������Ƿ���icounterable
    //�ж����嵱ǰ�ܷ񷴻�
    //����ܣ�ִ�������handlecounter
    //����bool�Ƿ�ִ���˷���
    public bool CounterAttackPerformed()
    {
        bool hasPerformedCounter = false;

        foreach (var target in GetDetectedColliders())
        {
            ICounterable counterable = target.GetComponent<ICounterable>();

            if (counterable == null)
                continue;

            if (counterable.canBeCountered)
            {
                counterable.HandleCounter();
                hasPerformedCounter = true;
            }
        }

        return hasPerformedCounter;
    }

    public float GetCounterRecoveryDuration() => counterRecovery; 
}
