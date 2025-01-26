using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    public GameObject station;
    private bool _shooting = true;
    [SerializeField] private float ejectForce = 6;
    [SerializeField] private List<AudioClip> enterBubbleAudioClips;
    [SerializeField] private List<AudioClip> exitBubbleAudioClips;

    void Start()
    {
        InputManager.Instance.SwitchAction += Dismount;
    }

    public void Dismount()
    {
        if (_shooting)
        {
            var transformPosition = GameObject.FindGameObjectWithTag("Cannon")?.transform.position ?? transform.position;
            transform.position = new Vector3(transformPosition.x, transformPosition.y, transform.position.z);
            var bubbleManager = GetComponent<BubbleManager>();
            var newBubbleObj = Instantiate(bubblePrefab, transform);
            var newBubble = newBubbleObj.GetComponent<Bubble>();
            newBubble.IsPlayerBubble = true;
            var newBubbleCollisionMerger = newBubbleObj.GetComponent<BubbleCollisionMerger>();
            newBubbleCollisionMerger.mergePosition = Bubble.MergePosition.SelfOrigin;
            newBubble.minValueForFade = 1;
            newBubble.scaleOffset = 0.15f;
            newBubble.multiplier = 0.5f;
            var baseBubble = BubbleStation.Instance.GetComponent<Bubble>();
            bubbleManager.SetCurrentBubble(newBubble);
            newBubbleObj.transform.localPosition = new Vector3(0, 0, 1);
            Destroy(newBubbleObj.GetComponent<Rigidbody2D>());
            newBubbleObj.layer = LayerMask.NameToLayer("PlayerBubble");
            newBubble.Oxygen = Mathf.Min(Player.Instance.leaveBubbleValue, baseBubble.Oxygen);
            newBubble.priority = 2;
            baseBubble.Oxygen -= Player.Instance.leaveBubbleValue;
            if (baseBubble.Oxygen <= 0)
            {
                baseBubble.Oxygen = 0;
                baseBubble.DestroyBubble();
            }
            _shooting = false;
            var movement2D = GetComponent<Movement2D>();
            movement2D.enabled = !_shooting;
            movement2D.SetTargetVelocity(BubbleStation.Instance.GetShootDirection() * ejectForce);
            movement2D.SetVelocity(BubbleStation.Instance.GetShootDirection() * ejectForce);
            MyAudioManager.Instance?.PlayClip(exitBubbleAudioClips);
            Player.Instance?.StartSwimAnimation();
        }
    }

    public void Mount()
    {
        if (!_shooting)
        {
            _shooting = true;
            BubbleStation.Instance.gameObject.SetActive(true);
            var movement2D = GetComponent<Movement2D>();
            movement2D.Reset();
            movement2D.enabled = !_shooting;
            transform.position = new Vector3(station.transform.position.x, station.transform.position.y, transform.position.z);
            var bubbleManager = GetComponent<BubbleManager>();
            var otherBubble = bubbleManager.GetCurrentBubble();
            var baseBubble = BubbleStation.Instance.GetComponent<Bubble>();
            if (otherBubble == baseBubble) return;
            bubbleManager.SetCurrentBubble(baseBubble);
            Destroy(otherBubble.gameObject);
            baseBubble.Oxygen += otherBubble.Oxygen;
            Player.Instance?.StartSteerAnimation();
            MyAudioManager.Instance?.PlayClip(enterBubbleAudioClips);
        }
    }

    public bool IsShooting()
    {
        return _shooting;
    }
}
