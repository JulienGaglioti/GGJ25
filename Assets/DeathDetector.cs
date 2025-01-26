using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    BubbleManager _bubbleManager;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private List<AudioClip> deathClips;
    private void Awake()
    {
        _bubbleManager = GetComponent<BubbleManager>();
    }

    void Update()
    {
        if (_bubbleManager.GetCurrentBubble() == null)
        {
            gameObject.SetActive(false);
            MyAudioManager.Instance?.PlayClip(deathClips);
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            GetComponent<WheelAnimator>().StartStaticAnimation();
            Destroy(this);
        }
    }
}
