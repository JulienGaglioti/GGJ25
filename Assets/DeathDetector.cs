using System;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    BubbleManager _bubbleManager;
    [SerializeField] private GameObject _deathEffect;
    private void Awake()
    {
        _bubbleManager = GetComponent<BubbleManager>();
    }

    void Update()
    {
        if (_bubbleManager.GetCurrentBubble() == null)
        {
            gameObject.SetActive(false);
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
        }
    }
}
