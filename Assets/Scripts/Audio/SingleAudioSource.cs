using System.Collections;
using UnityEngine;

public class SingleAudioSource : MonoBehaviour
{
    private AudioSource _audioSource;
    private float _fadeDuration;
    private float _targetVolume;

    private void Awake() 
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetToTargetVolume(float targetVolume, float time)
    {
        _targetVolume = targetVolume;
        _fadeDuration = time;
        StartCoroutine(SetTargetVolume());
    }

    IEnumerator SetTargetVolume()
    {
        float startVolume = _audioSource.volume;  // Get the current volume
        float timeElapsed = 0f;

        // Gradually change the volume to the target value
        while (timeElapsed < _fadeDuration)
        {
            _audioSource.volume = Mathf.Lerp(startVolume, _targetVolume, timeElapsed / _fadeDuration);
            timeElapsed += Time.deltaTime;
            yield return null;  // Wait until the next frame
        }

        // Ensure the volume is exactly set to the target value after the fade is complete
        _audioSource.volume = _targetVolume;
    }
}
