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

    // This method will call the Next stage and start the boss fight
    public void StartBossFight()
    {

        if (!GameOver)
        {
            this.bossStagePrefabs[currentBossStageIndex].SetActive(true);
            this.bossStagePrefabs[currentBossStageIndex].GetComponent<AbstractBossStage>().BossStartStage();
            currentMusic = GetComponent<AudioSource>();
            currentMusic.clip = musicArray[currentMusicIndex];
            currentMusic.Play();
            northstar = GameObject.Find("Northstar").GetComponent<Northstar>();
        }
    }

    private void Start()
    {
        StartBossFight();
        // this.bossStagePrefabs[currentBossStageIndex].SetActive(true);
        // this.bossStagePrefabs[currentBossStageIndex].GetComponent<AbstractBossStage>().BossStartStage();
        // currentMusic = GetComponent<AudioSource>();
        // currentMusic.clip = musicArray[currentMusicIndex];
        // currentMusic.Play();
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
    }
}
