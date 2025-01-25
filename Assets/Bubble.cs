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
    public float Value
    {
        get => value;
        set
        {
            this.value = value;
            transform.localScale = Vector3.one * (float)Math.Max(0, Math.Log(Math.Max(0, this.value/10) + 1) * multiplier);
            _meshRenderer.material.SetFloat(RadiusProperty, transform.localScale.x / 2);
        }
    }
    
    [SerializeField] protected float multiplier = 1f;
    private MeshRenderer _meshRenderer;
    private BubbleCollisionMerger _bubbleCollisionMerger;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _bubbleCollisionMerger = GetComponent<BubbleCollisionMerger>();
        Value = value;
    }
    
    public bool CanMerge()
    {
        return _bubbleCollisionMerger.enabled;
    }

    public void SetCanMerge(bool canMerge)
    {
        _bubbleCollisionMerger.enabled = canMerge;
    }

    public void MergeWith(Bubble otherBubble, MergePosition mergePosition = MergePosition.Midpoint, bool forced = false)
    {
        if ((!otherBubble.CanMerge() || !CanMerge()) && !forced) { return; }

        if (mergePosition == MergePosition.Midpoint)
        {
            transform.position = (otherBubble.transform.position + transform.position) / 2;
        }

        Value += otherBubble.Value;
        Destroy(otherBubble.gameObject);
    }
}
