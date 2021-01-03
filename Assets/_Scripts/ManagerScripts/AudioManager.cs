using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    void Awake()
    {
        foreach (Sound S in Sounds)
        {
            S.Source = gameObject.AddComponent<AudioSource>();

            S.Source.clip = S.Clip;
            S.Source.volume = S.Volume;
            S.Source.pitch = S.Pitch;
            S.Source.loop = S.Loop;
            S.Source.playOnAwake = S.PlayOnAwake;
        }
    }

    public void Play(string name)
    {
        Sound S = Array.Find(Sounds, Sound => Sound.Name == name);
        bool SoundClassIsEqualToNull = (S == null);
        if (SoundClassIsEqualToNull)
            return;

        bool audioManagerIsActive = (gameObject.activeSelf == true);
        if (audioManagerIsActive)
        {
            S.Source.Play();
        }
    }
}
