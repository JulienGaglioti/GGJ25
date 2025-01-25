using System;
using System.Collections;
using UnityEngine;

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
    public float minValueForFade = 0.0f;
    public float multiplier = 1f;
    public float scaleOffset = 0f;

    private Rigidbody2D _rigidBody;
    private MeshRenderer _meshRenderer;
    private BubbleCollisionMerger _bubbleCollisionMerger;

    public float Oxygen
    {
        get => value;
        set
        {
            this.value = value;
            UpdateScale();
            UpdateFade();
        }
    }

    private float _borderAlpha = 1f;
    public float BorderAlpha
    {
        get => _borderAlpha;
        private set
        {
            _borderAlpha = Mathf.Clamp01(value);

            Color color = _meshRenderer.material.GetColor(BorderColor);
            color.a = _borderAlpha;
            _meshRenderer.material.SetColor(BorderColor, color);
        }
    }

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _bubbleCollisionMerger = GetComponent<BubbleCollisionMerger>();

        Oxygen = value;

        if (!IsStationBubble)
        {
            StartCoroutine(EnablemergeDelayed());
        }
    }

    private void UpdateScale()
    {
        float scaleValue = 0f;

        if (value > 0)
        {
            scaleValue = (float)Math.Log(Math.Max(0, this.value / 10f) + 1) * multiplier;
        }

        transform.localScale = Vector3.one * (Mathf.Max(0, scaleValue) + scaleOffset);
        _meshRenderer.material.SetFloat(RadiusProperty, transform.localScale.x / 2);
    }

    private void UpdateFade()
    {
        if (value <= minValueForFade)
        {
            BorderAlpha = value / minValueForFade;
        }
        else
        {
            BorderAlpha = 1f;
        }
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
        if ((!otherBubble.CanMerge() || !CanMerge()) && !forced) 
            return;

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
