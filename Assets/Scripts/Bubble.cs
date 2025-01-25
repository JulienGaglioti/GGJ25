using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Bubble : MonoBehaviour
{
    public bool IsStationBubble;
    public bool CanMergeWithStationBubble;
    public int priority = 1;
    public float TimeBeforeMergeIsEnabled;
    public enum MergePosition
    {
        Midpoint,
        SelfOrigin
    }
    
    private static readonly int RadiusProperty = Shader.PropertyToID("_Radius");
    private static readonly int BorderColor = Shader.PropertyToID("_RingColor");

    [SerializeField] private float value;
    public float Oxygen
    {
        get => value;
        set
        {
            this.value = value;
            transform.localScale = Vector3.one * ((float)Math.Max(0, Math.Log(Math.Max(0, this.value/10f) + 1) * multiplier) + minSize);
            _meshRenderer.material.SetFloat(RadiusProperty, transform.localScale.x / 2);
        }
    }

    private float _borderAlpha = 0;

    public float BorderAlpha
    {
        get => _borderAlpha;
        set
        {
            _borderAlpha = value;
            Color color = _meshRenderer.material.GetColor(BorderColor);
            color.a = _borderAlpha;
            _meshRenderer.material.SetColor(BorderColor, color);
            _borderAlpha = value;
        }
    }

    [SerializeField] protected float multiplier = 1f;
    [SerializeField] protected float minSize = 0f;
    private Rigidbody2D _rigidBody;
    private MeshRenderer _meshRenderer;
    private BubbleCollisionMerger _bubbleCollisionMerger;

    private void Awake()
    {
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        _bubbleCollisionMerger = GetComponent<BubbleCollisionMerger>();
        _rigidBody = GetComponent<Rigidbody2D>();
        Oxygen = value;

        if(!IsStationBubble)
        {
            StartCoroutine(EnablemergeDelayed());
        }
    }
    
    public bool CanMerge(Bubble askingBubble)
    {
        if(IsStationBubble)
        {
            return _bubbleCollisionMerger.enabled && askingBubble.CanMergeWithStationBubble;
        }
        else
        {
            return _bubbleCollisionMerger.enabled;
        }
        // return _bubbleCollisionMerger.enabled;
    }

    public void SetCanMerge(bool canMerge)
    {
        _bubbleCollisionMerger.enabled = canMerge;
    }

    public void MergeWith(Bubble otherBubble, MergePosition mergePosition = MergePosition.Midpoint, bool forced = false)
    {
        if ((!otherBubble.CanMerge(this) || !CanMerge(this)) && !forced) { return; }

        if (mergePosition == MergePosition.Midpoint)
        {
            transform.position = (otherBubble.transform.position + transform.position) / 2;
        }

        Oxygen += otherBubble.Oxygen;
        Destroy(otherBubble.gameObject);
    }

    public void AddForce(Vector2 direction)
    {
        _rigidBody.AddForce(direction);
    }

    private IEnumerator EnablemergeDelayed()
    {
        yield return new WaitForSeconds(TimeBeforeMergeIsEnabled);
        CanMergeWithStationBubble = true;
    }
}
