using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Xcy.Common;

public class TempAudioManager : MonoSingleton<TempAudioManager>
{
    private AudioSource _audioSource;
    public AudioClip[] musics;
    public override void Awake()
    {
        _destoryOnLoad = false;
        base.Awake();
    }
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusic(int index)
    {
        _audioSource.clip = musics[index];
        _audioSource.Play();
    }

}
