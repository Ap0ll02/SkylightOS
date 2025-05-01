using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class Boss3MusicManager : MonoBehaviour
{
    public AudioClip[] musicArray;
    public AudioClip nyanCatBossMusic;
    public AudioClip dogeBossMusic;
    public AudioSource currentMusic;
    public int currentMusicIndex = 0;
    public bool playMusic = true;
    public Coroutine musicCoroutine;
    // Start is called before the first frame update

    public void Start()
    {
        currentMusic = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        currentMusic.loop = false;
        if (musicCoroutine == null)
            musicCoroutine = StartCoroutine(Music());
    }

    public void StopMusic()
    {
        currentMusic.loop = false;
        currentMusic.Stop();
        if (musicCoroutine != null)
            StopCoroutine(musicCoroutine);
    }

    public void NyanCatMusic()
    {
        StopMusic();
        currentMusic.clip = nyanCatBossMusic;
        currentMusic.Play();
        currentMusic.loop = true;
    }

    public void DogeBossMusic()
    {
        StopMusic();
        currentMusic.clip = dogeBossMusic;
        currentMusic.Play();
        currentMusic.loop = true;
    }

    public IEnumerator Music()
    {
        while (true)
        {
            if (!currentMusic.isPlaying)
            {
                PlayNextTrack();
            }
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void PlayNextTrack()
    {
        if (musicArray.Length > 0 && musicArray != null)
        {
            // we increment the music index to be at the next track
            currentMusicIndex++;
            // Make sure it is with in the length of the array
            currentMusicIndex %= musicArray.Length;
            // Then change the track
            currentMusic.clip = musicArray[currentMusicIndex];
            currentMusic.Play();
        }
    }

}
