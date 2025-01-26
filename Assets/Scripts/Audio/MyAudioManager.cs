using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviourSingleton<MyAudioManager>
{
    public float DefaultFadeDuration;
    public List<SingleAudioSource> MusicSources;
    private int _currentMusicValue;

    public void SetMusic(int index, float targetVolume, float duration)
    {
        MusicSources[index].SetToTargetVolume(targetVolume, duration);
    }

    public void OnDynamicValueUpdate(float dynamicValue)
    {
        if(dynamicValue < 0.25f)
        {
            OnMusicValueUpdate(0);
        }
        else if(dynamicValue > 0.25f)
        {
            OnMusicValueUpdate(1);
        }
        else if(dynamicValue > 0.40f)
        {
            OnMusicValueUpdate(2);
        }
        else if(dynamicValue > 0.65f)
        {
            OnMusicValueUpdate(3);
        }
        else if(dynamicValue > 0.8f)
        {
            OnMusicValueUpdate(4);
        }
    }

    private void OnMusicValueUpdate(int newValue)
    {
        if(newValue == _currentMusicValue) return;

        _currentMusicValue = newValue;

        switch (_currentMusicValue)
        {
            case 0:
                print("value 0: é presente solo la _0");
                SetMusic(1, 1f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume > 0) SetMusic(2, 0f, DefaultFadeDuration);
                if(MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration);
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration);
                break;
            case 1:
                print("value 1: é presente solo la _25, sparisce la _0");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                SetMusic(2, 1f, DefaultFadeDuration);                
                if(MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration);
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration);
                break;
            case 2:
                print("value 2: sono presenti _25, _50");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration);                
                SetMusic(3, 1f, DefaultFadeDuration); 
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration);
                break;
            case 3:
                print("value 3: sono presenti _25, _50, _75");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration);                
                if(MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration); 
                SetMusic(4, 1f, DefaultFadeDuration);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration);
                break;
            case 4:
                print("value 4: sono presenti _25, _50, _75, _100");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration);                
                if(MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration); 
                if(MusicSources[4].Source.volume < 1) SetMusic(4, 1f, DefaultFadeDuration);
                SetMusic(5, 1f, DefaultFadeDuration);
                break;
        }
    }
}
