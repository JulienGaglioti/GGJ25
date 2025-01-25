using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject bubblePrefab;
    public GameObject station;
    private bool _shooting = true;
    [SerializeField] private float ejectForce = 6;

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
            _shooting = false;
            var movement2D = GetComponent<Movement2D>();
            movement2D.enabled = !_shooting;
            movement2D.SetTargetVelocity(BubbleStation.Instance.GetShootDirection() * ejectForce);
            movement2D.SetVelocity(BubbleStation.Instance.GetShootDirection() * ejectForce);
        }
    }

    public void Mount()
    {
        if (!_shooting)
        {
            _shooting = true;
            var movement2D = GetComponent<Movement2D>();
            movement2D.Reset();
            movement2D.enabled = !_shooting;
            transform.position = new Vector3(station.transform.position.x, station.transform.position.y, transform.position.z);
        }
    }

    public bool IsShooting()
    {
        return _shooting;
    }
}
