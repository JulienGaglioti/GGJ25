using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviourSingleton<MyAudioManager>
{
    public float DefaultFadeDuration;
    public float MusicLevel0;
    public float MusicLevel1;
    public float MusicLevel2;
    public float MusicLevel3;
    public List<SingleAudioSource> MusicSources;
    private int _currentMusicValue;


    public void SetMusic(int index, float targetVolume, float duration)
    {
        MusicSources[index].SetToTargetVolume(targetVolume, duration);
    }

    public void OnDynamicValueUpdate(float dynamicValue)
    {
        if(dynamicValue <= MusicLevel0)
        {
            OnMusicValueUpdate(0);
        }
        
        if(dynamicValue > MusicLevel0 && dynamicValue < MusicLevel1)
        {
            OnMusicValueUpdate(1);
        }
        
        if(dynamicValue > MusicLevel1 && dynamicValue < MusicLevel2)
        {
            OnMusicValueUpdate(2);
        }
        
        if(dynamicValue > MusicLevel2 && dynamicValue < MusicLevel3)
        {
            OnMusicValueUpdate(3);
        }
        
        if(dynamicValue > MusicLevel3)
        {
            OnMusicValueUpdate(4);
        }
    }

    private void OnMusicValueUpdate(int newValue)
    {
        if(newValue == _currentMusicValue) return;
        // print("updated value " + newValue);

        _currentMusicValue = Mathf.Min(newValue, WaveManager.Instance.CurrentDifficultyValue);

        switch (_currentMusicValue)
        {
            case 0:
                print("value 0: é presente solo la _0");
                SetMusic(1, 1f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume > 0) SetMusic(2, 0f, DefaultFadeDuration * 2);
                if(MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration * 3);
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 4);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 5);
                break;
            case 1:
                print("value 1: é presente solo la _25, sparisce la _0");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                SetMusic(2, 1f, DefaultFadeDuration * 2);                
                if(MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration * 3);
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 4);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 5);
                break;
            case 2:
                print("value 2: sono presenti _25, _50");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 2);                
                SetMusic(3, 1f, DefaultFadeDuration * 3); 
                if(MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 4);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 5);
                break;
            case 3:
                print("value 3: sono presenti _25, _50, _75");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 2);                
                if(MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration * 3); 
                SetMusic(4, 1f, DefaultFadeDuration * 4);
                if(MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 5);
                break;
            case 4:
                print("value 4: sono presenti _25, _50, _75, _100");
                if(MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if(MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 2);                
                if(MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration * 3); 
                if(MusicSources[4].Source.volume < 1) SetMusic(4, 1f, DefaultFadeDuration * 4);
                SetMusic(5, 1f, DefaultFadeDuration * 5);
                break;
        }
    }
}
