    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTrigger :Entity_AnimationTrigger
{
    private Enemy enemy;
    private Enemy_VFX enemy_VFX;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        enemy_VFX = GetComponentInParent<Enemy_VFX>();
    }

    private void EnableCounterWindow()
    {
        enemy_VFX.EnableCounterAttackAlert(true);
        enemy.EnableCounterWindow(true);
    }

    private void DisableCounterWindow()
    {
        enemy_VFX.EnableCounterAttackAlert(false);
        enemy.EnableCounterWindow(false);
    }
}
