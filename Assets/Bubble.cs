using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private static readonly int RadiusProperty = Shader.PropertyToID("_Radius");
    [SerializeField] protected float value = 1f;
    [SerializeField] protected float multiplier = 1f;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        transform.localScale = Vector3.one * (float)Math.Max(0, Math.Log(Math.Max(0, value) + 1) * multiplier);
        _meshRenderer.material.SetFloat(RadiusProperty, transform.localScale.x / 2);
    }
}
