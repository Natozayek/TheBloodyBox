using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class SoundManager : MonoBehaviour
{
    private List<AudioSource> audioSources;
    private List<AudioClip> audioClips;

    void Awake()
    {
        audioSources = GetComponents<AudioSource>().ToList();
        audioClips = new List<AudioClip>();

        InitializeSoundFX();

    }

    private void InitializeSoundFX()
    {
        audioClips.Add(Resources.Load<AudioClip>("Audio/jump"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/hurt"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/death"));
        audioClips.Add(Resources.Load<AudioClip>("Audio/bgm"));
    }

    public void PlaySoundFX(SoundFX sound, SoundChannel channel)
    {
        audioSources[(int)channel].clip = audioClips[(int)sound];
        audioSources[(int)channel].Play();
    }

    public void PlayBGMMusic()
    {
        audioSources[(int)SoundChannel.MUSIC].clip = audioClips[(int)SoundFX.MUSIC];
        audioSources[(int)SoundChannel.MUSIC].volume = 0.3f;
        audioSources[(int)SoundChannel.MUSIC].loop = true;
        audioSources[(int)SoundChannel.MUSIC].Play();

    }
}
