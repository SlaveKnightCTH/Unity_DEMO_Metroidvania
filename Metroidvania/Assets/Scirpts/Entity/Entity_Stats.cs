using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Stats : MonoBehaviour
{
    //属性系统
    //属性，以及计算

    public Stat maxHP ;//=100;
    public Stat_MajorGroup major;
    public Stat_OffenseGroup offense;
    public Stat_DefenseGroup defense;

    
    public float GetMaxHP()
    {
        float baseHP = maxHP.GetValue();
        float bonusHP = major.vitality.GetValue() * 5;
        return baseHP + bonusHP;
    }
}
