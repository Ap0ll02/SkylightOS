using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeBossFightManager : BossManager
{
    public AudioClip[] musicArray;
    public AudioClip bossMusic;
    public AudioSource currentMusic;
    public int currentMusicIndex = 0;
    public bool isPlaying = false;
    private Northstar northstar;
    public GameObject[] objectsToActivate;
    public BasicWindow window;

    // This method will call the Next stage and start the boss fight
    public void Start()
    {
        window = gameObject.transform.parent.GetComponent<BasicWindow>();
        window.ForceCloseWindow();
    }

    public void StartBossFight()
    {
        if (!isPlaying)
        {
            window.OpenWindow();
            isPlaying = true;
            //StartCoroutine(BossMusic());
            this.NextStage();
            currentMusic = GetComponent<AudioSource>();
            currentMusic.clip = musicArray[currentMusicIndex];
            currentMusic.Play();
        }
    }

    public void LevelMusic()
    {
        if (currentMusic.isPlaying == false)
        {
            Debug.Log("Not Playing Music");
            currentMusicIndex++;
            if (currentMusicIndex >= musicArray.Length)
            {
                currentMusicIndex = 0;
            }
            currentMusic.clip = musicArray[currentMusicIndex];
            currentMusic.Play();
        }

    }

    public void StopLevelMusic()
    {
        currentMusic.Stop();
    }

    public void PlayBossMusic()
    {
        currentMusic.clip = bossMusic;
        currentMusic.Play();
    }

    public void StopBossMusic()
    {
        currentMusic.Stop();
    }

    void  OnEnable()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            gameObject.SetActive(true);
        }
    }

    IEnumerator BossMusic()
    {
        while (isPlaying == true)
        {
            LevelMusic();
        }
        yield return null;
    }
}
