using UnityEngine;

public class FixedScale : MonoBehaviour
{
    [SerializeField] private Vector3 desiredScale = Vector3.one;

    void Update()
    {
        Transform parent = transform;
        Vector3 scale = desiredScale;
        while ((parent = parent.parent) != null)
        {
            scale = new Vector3(scale.x / parent.localScale.x, scale.y / parent.localScale.y, scale.z / parent.localScale.z);
        }
        transform.localScale = scale;
    }
}
