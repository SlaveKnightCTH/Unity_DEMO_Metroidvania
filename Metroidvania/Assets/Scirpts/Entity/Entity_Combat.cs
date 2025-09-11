using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Entity_Combat : MonoBehaviour
{
    //������ײ��
    //ȡ�ñ����������Idamageable���
    //�������˺���
    public float damage = 10;
    private Entity_VFX entity_VFX;

    [Header("Traget Detail")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius=1;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        entity_VFX = GetComponent<Entity_VFX>();
    }

    public void PerformAttack()
    {
        foreach (var target in GetDetectedColliders())
        {
            Idamageable idamageable = target.GetComponent<Idamageable>();

            if (idamageable == null)
                continue;

            idamageable.TakeDamage(damage, this.transform);
            entity_VFX.CreateOnHitVFX(target.transform);
            //Entity_Health targetHealth = target.GetComponent<Entity_Health>();
            //targetHealth?.TakeDamage(damage,this.transform);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }


}
