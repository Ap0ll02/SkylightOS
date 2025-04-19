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
    bool GameOver = false;
    private Northstar northstar;
    public GameObject[] objectsToActivate;

    // This method will call the Next stage and start the boss fight
    public void StartBossFight()
    {
        if (!GameOver)
        {
            this.NextStage();
            currentMusic = GetComponent<AudioSource>();
            currentMusic.clip = musicArray[currentMusicIndex];
            currentMusic.Play();
        }
    }

    private void Start()
    {
        StartBossFight();
    }
    private void Update()
    {
        LevelMusic();
    }

    #region Music
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
    #endregion Music

    void  OnEnable()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            gameObject.SetActive(true);
        }
    }
}
