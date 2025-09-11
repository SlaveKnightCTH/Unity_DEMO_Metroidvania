using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_VFX : MonoBehaviour
{
    private SpriteRenderer sr;

    // ‹…À
    [Header("On Taking Damage VFX")]
    [SerializeField] private Material onDamageMaterial;
    [SerializeField] private float onDamageDuration=0.1f;
    private Material originalMaterial;

    //π•ª˜Ãÿ–ß
    [Header("On Hit VFX")]
    [SerializeField] private GameObject onHitVFX;
    [SerializeField] private Color onHitVFXColor = Color.white;

    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private Coroutine currentCo;

    public void CreateOnHitVFX(Transform target)
    {
        GameObject vfx= Instantiate(onHitVFX, target.position, Quaternion.identity);
        vfx.GetComponentInChildren<SpriteRenderer>().color = onHitVFXColor;

    }

    public void PlayDamageVFX()
    {
        if (currentCo != null)
        {
            StopCoroutine(currentCo);
        }

        currentCo = StartCoroutine(OnDamageVfxCo());
    }


    private IEnumerator OnDamageVfxCo()
    {
        sr.material = onDamageMaterial;

        yield return new WaitForSeconds(onDamageDuration);

        sr.material = originalMaterial;
    }
}
