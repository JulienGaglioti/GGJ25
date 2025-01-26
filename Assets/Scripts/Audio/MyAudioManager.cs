using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviourSingleton<MyAudioManager>
{
    public List<SingleAudioSource> MusicSources;

    public void SetMusic(int index, float targetVolume, float duration)
    {
        print(targetVolume);
        MusicSources[index].SetToTargetVolume(targetVolume, duration);
    }
}
