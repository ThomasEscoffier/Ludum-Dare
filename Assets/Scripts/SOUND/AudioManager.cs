using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager instance;
    public float soundDecrease = 0.1f;
    public float SceneNumber = 0;
    public float MusicLevel = 0;
    public string musicPlayingName;

    private void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            
        }
    }
    private void Start()
    {
        if (SceneNumber == 0)
        {
            Play("Michel");
        }
    }

    private void Update()
    {
        SceneNumber = SceneManager.GetActiveScene().buildIndex;


        #region debug

        //if (scenenumber == 1)
        //{
        //    startcoroutine(fadeout("musicmenu"));

        //    if (musiclevel == 0)
        //    { 
        //    play("music1");
        //        musiclevel = 1;
        //    }

        //}
        //else if (scenenumber == 4)
        //{
        //    startcoroutine(fadeout("music1"));


        //    if (musiclevel == 1)
        //    {
        //        play("music2");
        //        musiclevel = 2;
        //    }
        //}
        //else if (scenenumber == 5)
        //{
        //    startcoroutine(fadeout("music2"));

        //    if (musiclevel == 2)
        //    {
        //        play("musicend");
        //        musiclevel = 3;
        //    }

        //}

        //Debug.Log(musicPlayingName);

        //if (Input.GetKeyDown(KeyCode.E))
        //{


        //    if(musicPlayingName == "Patrick")
        //    {
        //        StartCoroutine(Crossfade("Michel"));

        //    }
        //    else if(musicPlayingName == "Michel" || musicPlayingName == null)
        //    {
        //        if (musicPlayingName != null)
        //            StartCoroutine(Crossfade("Patrick"));
        //    }
        //}

        #endregion
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();

        if(s.CouldCrossfade)
            musicPlayingName = name;
    }

    public IEnumerator FadeOut(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        while (s.source.volume > 0)
        {
            s.source.volume -= soundDecrease;
            yield return new WaitForSeconds(0.1f);
        }
    }


    public IEnumerator Crossfade(string newMusic)
    {
        Sound sOld = Array.Find(sounds, sound => sound.name == musicPlayingName);
        Sound sNew = Array.Find(sounds, sound => sound.name == newMusic);

        sNew.source.volume = 0;
        sNew.source.Play();
        sNew.source.time = sOld.source.time;

        while (sOld.source.volume > 0)
        {
            sNew.source.volume += soundDecrease;
            sOld.source.volume -= soundDecrease;
            yield return new WaitForSeconds(0.1f);
        }

        if (sNew.CouldCrossfade)
            musicPlayingName = newMusic;

        sOld.source.Stop();
        sOld.source.volume = 1;
       
    }



}
