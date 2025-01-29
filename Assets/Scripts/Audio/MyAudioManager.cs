using System.Collections.Generic;
using UnityEngine;

public class MyAudioManager : MonoBehaviourSingleton<MyAudioManager>
{
    public float DefaultFadeDuration;
    public float MusicLevel0;
    public float MusicLevel1;
    public float MusicLevel2;
    public float MusicLevel3;
    public float TopMusicLevel;
    public List<SingleAudioSource> MusicSources;
    public List<SingleAudioSource> AudioSources;
    public AudioSource EndWaveSource;
    private int _currentMusicValue;

    private void Start()
    {
        SetMusic(0, TopMusicLevel, 0);
    }

    public void PlayClip(List<AudioClip> clips)
    {
        AudioClip clip = clips[Random.Range(0, clips.Count)];

        bool allSourcesOccupied = true;
        foreach (var source in AudioSources)
        {
            if (!source.Source.isPlaying)
            {
                source.PlayClip(clip);
                allSourcesOccupied = false;
                // print(AudioSources.IndexOf(source));
                return;
            }
        }

        if (allSourcesOccupied)
        {
            // print("all sources occupied");
            AudioSources[0].PlayClip(clip);
        }
    }

    public void PlayEndWave()
    {
        EndWaveSource.Play();
    }

    public void SetMusic(int index, float targetVolume, float duration)
    {
        MusicSources[index].SetToTargetVolume(targetVolume, duration);
    }

    public void OnDynamicValueUpdate(float dynamicValue)
    {
        if (dynamicValue <= MusicLevel0)
        {
            OnMusicValueUpdate(0);
        }

        if (dynamicValue > MusicLevel0 && dynamicValue < MusicLevel1)
        {
            OnMusicValueUpdate(1);
        }

        if (dynamicValue > MusicLevel1 && dynamicValue < MusicLevel2)
        {
            OnMusicValueUpdate(2);
        }

        if (dynamicValue > MusicLevel2 && dynamicValue < MusicLevel3)
        {
            OnMusicValueUpdate(3);
        }

        if (dynamicValue > MusicLevel3)
        {
            OnMusicValueUpdate(4);
        }
    }

    private void OnMusicValueUpdate(int newValue)
    {
        if (newValue == _currentMusicValue) return;
        // print("updated value " + newValue);

        _currentMusicValue = Mathf.Min(newValue, WaveManager.Instance.CurrentDifficultyValue);

        switch (_currentMusicValue)
        {
            case 0:
                // print("value 0: é presente solo la _0");
                SetMusic(1, TopMusicLevel, DefaultFadeDuration);
                if (MusicSources[2].Source.volume > 0) SetMusic(2, 0f, DefaultFadeDuration * 1.5f);
                if (MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration * 2f);
                if (MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 2.5f);
                if (MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 3f);
                break;
            case 1:
                // print("value 1: é presente solo la _25, sparisce la _0");
                if (MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                SetMusic(2, TopMusicLevel, DefaultFadeDuration * 1.5f);
                if (MusicSources[3].Source.volume > 0) SetMusic(3, 0f, DefaultFadeDuration * 2f);
                if (MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 2.5f);
                if (MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 3f);
                break;
            case 2:
                // print("value 2: sono presenti _25, _50");
                if (MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if (MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 1.5f);
                SetMusic(3, TopMusicLevel, DefaultFadeDuration * 2f);
                if (MusicSources[4].Source.volume > 0) SetMusic(4, 0f, DefaultFadeDuration * 2.5f);
                if (MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 3f);
                break;
            case 3:
                // print("value 3: sono presenti _25, _50, _75");
                if (MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if (MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 1.5f);
                if (MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration * 2f);
                SetMusic(4, TopMusicLevel, DefaultFadeDuration * 2.5f);
                if (MusicSources[5].Source.volume > 0) SetMusic(5, 0f, DefaultFadeDuration * 3f);
                break;
            case 4:
                // print("value 4: sono presenti _25, _50, _75, _100");
                if (MusicSources[1].Source.volume > 0) SetMusic(1, 0f, DefaultFadeDuration);
                if (MusicSources[2].Source.volume < 1) SetMusic(2, 1f, DefaultFadeDuration * 1.5f);
                if (MusicSources[3].Source.volume < 1) SetMusic(3, 1f, DefaultFadeDuration * 2f);
                if (MusicSources[4].Source.volume < 1) SetMusic(4, 1f, DefaultFadeDuration * 2.5f);
                SetMusic(5, TopMusicLevel, DefaultFadeDuration * 3f);
                break;
        }
    }
}
