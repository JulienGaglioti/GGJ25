using System.Collections;
using UnityEngine;

public class BubbleStation : MonoBehaviour
{
    public float ShootForce;
    public float OxygenCostPerBubble;
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
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = transform.position.z;
            Vector3 directionToMouse = mouseWorldPosition - transform.position;

            shootdirection = directionToMouse.normalized;
        }
        else
        {
            shootdirection = InputManager.Instance.LookInput;
        }
        bubbleProjectile.AddForce(shootdirection * ShootForce);

        bubbleScript.Oxygen -= OxygenCostPerBubble;
    }
}
