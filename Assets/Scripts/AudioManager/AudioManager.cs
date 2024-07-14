using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance{get;private set;}

    private bool volumeON=true;
    private float tempovolumen=0f;

    public AudioMixerGroup Music_mixer;
    public AudioMixerGroup Fx_mixer;
    public Sound[] songs;
    public Sound[] Effects;
    public string currentSong=null;
    public string songTochage=null;
    
    void Awake()
    { 
        if (Instance==null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound effect in Effects)
        {
            effect.source = gameObject.AddComponent<AudioSource>();
            effect.source.outputAudioMixerGroup = Fx_mixer;
            effect.source.clip = effect.clip;
            effect.source.volume = effect.volume;
            effect.source.pitch = effect.pitch;
            effect.source.loop = effect.loop;
        }
        foreach (Sound song in songs)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.outputAudioMixerGroup = Music_mixer;
            song.source.clip = song.clip;
            song.source.volume = song.volume;
            song.source.pitch = song.pitch;
            song.source.loop = song.loop;
        }
    }
    public void ChangeSong(string name)
    {
        StartCoroutine("SlowChangeSongOff");
        songTochage=name;
    }
    void LateUpdate()
    {
        ChangeVolumen(volumeON);
    }
    void ChangeVolumen(bool status)
    {
        Sound s = Array.Find(songs, song => song.name == currentSong);
            if (s == null)
            {
            Debug.LogWarning("No currentSong Yet - Playing FirstTime");
            return;
            }

        if (volumeON)
        {
            tempovolumen = Mathf.Lerp(tempovolumen,1,0.1f);
        }
        if (!volumeON)
        {
            tempovolumen = Mathf.Lerp(tempovolumen,0,0.1f);
        }
        s.source.volume = tempovolumen;
    }
    IEnumerator SlowChangeSongOff()
    {
        volumeON=false;
        yield return new WaitForSeconds(3);
        PlaySong(songTochage);
        StopCoroutine("SlowChangeSongOff");
    }
    public void PlaySong(string name)
    {
        StopSong(currentSong);
        Sound s = Array.Find(songs, song => song.name == name);
        currentSong = name;
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " Doesnt Exist");
            return;
        }
        s.source.Play();
        volumeON=true;
    }
    public void StopSong(string name)
    {
       // Debug.Log("stopping "+name);
        Sound s = Array.Find(songs, song => song.name == name);
  
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " Doesnt Exist");
            return;
        }
        s.source.Stop();
    }
    //--------------Effects
    public void PlayEffect(string name)
    {
        Sound s = Array.Find(Effects, effect => effect.name == name);
  
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " Doesnt Exist");
            return;
        }
        s.source.Play();
    }
    public void StopEffect(string name)
    {
        Sound s = Array.Find(Effects, effect => effect.name == name);
  
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " Doesnt Exist");
            return;
        }
        s.source.Stop();
    }
    public void ChangePitch(string name,float pitcht)
    {
        Sound s = Array.Find(Effects, effect => effect.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound" + name + " Doesnt Exist");
            return;
        }
        s.source.pitch=pitcht;
    }
}
