using UnityEngine;

public class OneShotAnimation : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();

        if (_animator != null)
        {
            AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;

            if (clips.Length > 0)
            {
                Destroy(gameObject, clips[0].length);
            }
        }
        else
        {
            Debug.LogError("Animator not found on GameObject.");
            Destroy(gameObject);
        }
    }}
