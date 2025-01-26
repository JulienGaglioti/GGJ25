using UnityEngine;

public class HitCollision : MonoBehaviour
{
    [SerializeField] private Bubble bubble;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            enemy.DestroyEnemy();
            bubble.DestroyBubble();
        }
    }
}
