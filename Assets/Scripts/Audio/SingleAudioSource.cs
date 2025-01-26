using System.Collections;
using UnityEngine;

public class SingleAudioSource : MonoBehaviour
{
    public AudioSource Source;
    private float _fadeDuration;
    private float _targetVolume;
    private float _startingVolume;
    private float _startingPitch;

    private void Awake() 
    {
        Source = GetComponent<AudioSource>();
        _startingVolume = Source.volume;
        _startingPitch = Source.pitch;
    }

    public void SetToTargetVolume(float targetVolume, float time)
    {
        _targetVolume = targetVolume;
        if(Source.volume > targetVolume)
        {
            _fadeDuration = MyAudioManager.Instance.DefaultFadeDuration;
        }
        else
        {
            _fadeDuration = time;
        }
        
        StartCoroutine(SetTargetVolume());
    }

    IEnumerator SetTargetVolume()
    {
        float startVolume = Source.volume;  // Get the current volume
        float timeElapsed = 0f;

        // Gradually change the volume to the target value
        while (timeElapsed < _fadeDuration)
        {
            Source.volume = Mathf.Lerp(startVolume, _targetVolume, timeElapsed / _fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the volume is exactly set to the target value after the fade is complete
        Source.volume = _targetVolume;
    }

    public void PlayClip(AudioClip clip)
    {
        Source.volume = Random.Range(_startingVolume - 0.1f, _startingVolume + 0.1f);
        Source.pitch = Random.Range(_startingPitch - 0.15f, _startingPitch + 0.15f);
        Source.PlayOneShot(clip);
    }
}
