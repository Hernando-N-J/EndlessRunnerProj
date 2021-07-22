using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHolder : MonoBehaviour
{
  public AudioSource source;
  public static AudioHolder Instance{ get; private set; }

  private void Awake() => Instance = this;

  public void Play(AudioClip clip)
  {
    source.clip = clip;
    source.Play();
  }
  
  private void OnDestroy() => Instance = null;

  private void OnApplicationQuit() => OnDestroy();
}
