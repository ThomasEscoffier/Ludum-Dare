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
            Play("MusicMenu");
        }
    }

    private void Update()
    {
        SceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (SceneNumber == 1)
        {
            StartCoroutine(FadeOut("MusicMenu"));

            if (MusicLevel == 0)
            { 
            Play("Music1");
                MusicLevel = 1;
            }

        }
        else if (SceneNumber == 4)
        {
            StartCoroutine(FadeOut("Music1"));
            

            if (MusicLevel == 1)
            {
                Play("Music2");
                MusicLevel = 2;
            }
        }
        else if (SceneNumber == 5)
        {
            StartCoroutine(FadeOut("Music2"));

            if (MusicLevel == 2)
            {
                Play("MusicEnd");
                MusicLevel = 3;
            }
            
        }
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



}
