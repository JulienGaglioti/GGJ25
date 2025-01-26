using System;
using System.Collections.Generic;
using UnityEngine;

public class DeathDetector : MonoBehaviour
{
    BubbleManager _bubbleManager;
    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private List<AudioClip> deathClips;
    [SerializeField] private BubbleStation _bubbleStation;
    private void Start()
    {
        _bubbleManager = GetComponent<BubbleManager>();
        _bubbleStation = _bubbleManager.GetCurrentBubble().GetComponent<BubbleStation>();
    }

    void Update()
    {
        if (_bubbleManager.GetCurrentBubble() == null)
        {
            Death();
        }
    }

    private void Death()
    {
        if (_bubbleStation != null) _bubbleStation.CanShoot = false;
        gameObject.SetActive(false);
        Player.Instance.GameOverText.SetActive(true);
        MyAudioManager.Instance?.PlayClip(deathClips);
        Instantiate(_deathEffect, transform.position, Quaternion.identity);
        GetComponent<WheelAnimator>().StartStaticAnimation();
        Destroy(this);
    }
}
