using System;
using System.Collections;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public bool IsStationBubble;
    public bool IsPlayerBubble;
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
    private bool _isSpawnedBubble;
    private float _spawnedBubbleDecreaseRate;
    private float _minValue;
    private bool _canFade = false;

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
            StartCoroutine(DelayedInitialization());
        }
    }

    private void Update()
    {
        if (!_isSpawnedBubble) return;

        Oxygen -= _spawnedBubbleDecreaseRate * Time.deltaTime;

        if (_canFade && Oxygen < _minValue)
        {
            Destroy(gameObject);
        }
    }

    public void InitializeSpawnedBubble(float decreaseRate, float minValue)
    {
        _isSpawnedBubble = true;
        _spawnedBubbleDecreaseRate = decreaseRate;
        _minValue = minValue;
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
        otherBubble.DestroyBubble();
        if (!IsStationBubble && !IsPlayerBubble)
        {
            _rigidBody.linearVelocity = (_rigidBody.linearVelocity + otherBubble._rigidBody.linearVelocity) / 2;
        }
    }

    public void AddForce(Vector2 direction)
    {
        _rigidBody.AddForce(direction);
    }

    private IEnumerator DelayedInitialization()
    {
        yield return new WaitForSeconds(TimeBeforeMergeIsEnabled);
        CanMergeWithStationBubble = true;
        _canFade = true;
    }

    public void DestroyBubble()
    {
        if(IsStationBubble)
        {
            gameObject.SetActive(false);
            Oxygen = 0;
            var bubbleManager = Player.Instance.GetComponent<BubbleManager>();
            var currentBubble = bubbleManager.GetCurrentBubble();
            if (currentBubble == this)
            {
                bubbleManager.SetCurrentBubble(null);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
