using System.Collections.Generic;
using UnityEngine;

public class HitCollision : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    [SerializeField] private List<AudioClip> bubbleExplosionClips;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            MyAudioManager.Instance?.PlayClip(bubbleExplosionClips);
            Instantiate(enemy.deathEffect, enemy.transform.position, Quaternion.identity);
            enemy.DestroyEnemy();
            bubble.DestroyBubble();
        }
    }
}
