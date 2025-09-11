using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_AutoController : MonoBehaviour
{
    //ÌØÐ§×Ô»Ù
    [SerializeField] private bool autoSelfDestroy = true;
    [SerializeField] private float destoryDelay = 1;
    [Space]
    [SerializeField] private bool randomOffset =true;

    [Header("Random Position")]
    [SerializeField] private float xMinOffset = -.3f;
    [SerializeField] private float xMaxOffset = .3f;
    [Space]
    [SerializeField] private float yMinOffset = -.3f;
    [SerializeField] private float yMaxOffset = .3f;

    private void Start()
    {
        ApplyRandomOffset();
        ApplyRandomRotation();

        if (autoSelfDestroy)
            Destroy(gameObject, destoryDelay);

    }

    private void ApplyRandomOffset()
    {
        if (randomOffset == false)
            return;

        float xOffset = Random.Range(xMinOffset, xMaxOffset);
        float yOffset = Random.Range(yMinOffset, yMaxOffset);

        transform.position += new Vector3(xOffset, yOffset);
    }

    private void ApplyRandomRotation()
    {
        if (randomOffset == false)
            return;

        float zRotation = Random.Range(0, 360);

        transform.Rotate(0, 0, zRotation);
    }
}
