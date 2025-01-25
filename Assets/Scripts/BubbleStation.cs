using System.Collections;
using UnityEngine;

public class BubbleStation : MonoBehaviour
{
    public float ShootForce;
    [SerializeField] private Bubble bubbleScript;
    [SerializeField] private Bubble bubblePrefab;

    private void Start() 
    {
        InputManager.Instance.AttackAction += ShootBubble;
    }

    private void OnDestroy() 
    {
        InputManager.Instance.AttackAction -= ShootBubble;
    }

    private void ShootBubble()
    {
        Bubble bubbleProjectile = Instantiate(bubblePrefab, transform.position, transform.rotation);
        Vector2 shootdirection;
        if(InputManager.Instance.ControlSchemeIsMouse())
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
            shootdirection = (mousePosition - transform.position).normalized;
        }
        else
        {
            shootdirection = InputManager.Instance.LookInput;
        }
        bubbleProjectile.AddForce(shootdirection * ShootForce);
    }
}
