using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stat //ÊôÐÔ
{
    [SerializeField] private float baseValue;

    public float GetValue() => baseValue;
}
