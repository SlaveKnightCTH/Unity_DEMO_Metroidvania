using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_VFX : Entity_VFX
{
    //������������
    [Header("Counter Attack Details")]
    [SerializeField] private GameObject attackAlert;

    public void EnableCounterAttackAlert(bool enable)=>attackAlert.SetActive(enable);
}
