using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bubble : MonoBehaviour
{
    public int priority = 1;
    public enum MergePosition
    {
        Midpoint,
        SelfOrigin
    }
    
    private static readonly int RadiusProperty = Shader.PropertyToID("_Radius");

    [SerializeField] private float value;
    protected float Value
    {
        get => value;
        private set
        {
            this.value = value;
            transform.localScale = Vector3.one * (float)Math.Max(0, Math.Log(Math.Max(0, this.value) + 1) * multiplier);
            _meshRenderer.material.SetFloat(RadiusProperty, transform.localScale.x / 2);
        }
    }
    
    [SerializeField] protected float multiplier = 1f;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        Value = value;
    }

    public void MergeWith(Bubble bubble, MergePosition mergePosition = MergePosition.Midpoint)
    {
        if (mergePosition == MergePosition.Midpoint)
        {
            transform.position = (bubble.transform.position + transform.position) / 2;
        }

        Value += bubble.Value;
        Destroy(bubble.gameObject);
    }
}
