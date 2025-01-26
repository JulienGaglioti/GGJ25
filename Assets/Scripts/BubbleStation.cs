using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BubbleStation : MonoBehaviourSingleton<BubbleStation>
{
    public float ShootForce;
    public float OxygenCostPerBubble;
    [SerializeField] private Bubble bubbleScript;
    [SerializeField] private Bubble bubblePrefab;
    [SerializeField] private Transform cannonTransform;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private List<AudioClip> shootClips;


    private void Start() 
    {
        InputManager.Instance.AttackAction += ShootBubble;
    }

    private void OnDestroy() 
    {
        InputManager.Instance.AttackAction -= ShootBubble;
    }
    
    private void Update() 
    {
        if (!Player.Instance.GetComponent<ModeSwitcher>().IsShooting()) { return; }

        if(InputManager.Instance.ControlSchemeIsMouse())
        {
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPosition.z = transform.position.z;
            Vector3 directionToMouse = mouseWorldPosition - transform.position;

            Vector2 aimDirection = directionToMouse.normalized;

            if(aimDirection.sqrMagnitude > 0)
            {
                float angle = Mathf.Atan2(aimDirection.x, aimDirection.y) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, -90f, 90f);
                cannonTransform.localRotation = Quaternion.Euler(0, angle, 0);
            }  
        }
        else
        {
            if(InputManager.Instance.LookInput.sqrMagnitude > 0)
            {
                float angle = Mathf.Atan2(InputManager.Instance.LookInput.x, InputManager.Instance.LookInput.y) * Mathf.Rad2Deg;
                angle = Mathf.Clamp(angle, -90f, 90f);
                cannonTransform.localRotation = Quaternion.Euler(0, angle, 0);
            }            
        }
    }

    private void ShootBubble()
    {
        if (!Player.Instance.GetComponent<ModeSwitcher>().IsShooting()) { return; }
        Bubble bubbleProjectile = Instantiate(bubblePrefab, shootPoint.position, transform.rotation);
        bubbleProjectile.Oxygen = OxygenCostPerBubble;
        var shootdirection = GetShootDirection();
        bubbleProjectile.AddForce(shootdirection * ShootForce);
        bubbleScript.Oxygen -= OxygenCostPerBubble;

        MyAudioManager.Instance.PlayClip(shootClips);
    }

    public Vector2 GetShootDirection()
    {
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

        return shootdirection;
    }
}
