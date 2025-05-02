using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance;

    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(MusicManager).Name);
                    instance = singleton.AddComponent<MusicManager>();
                    DontDestroyOnLoad(singleton);
                }
            }
            return instance;
        }
    }

    public AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        //DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update  
    void Start()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
    }

    // Update is called once per frame  
    void Update()
    {

    }

    public void ResumeMusic()
    {
        if (audioSource != null && !audioSource.isPlaying)
        {
            StartCoroutine(FadeInMusic(3f)); // Adjust fade duration as needed
        }
    }

    private IEnumerator FadeInMusic(float duration)
    {
        audioSource.volume = 0f;
        audioSource.Play();

        float startVolume = 0f;
        float targetVolume = 1f; // Assuming full volume is 1
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / duration);
            yield return null;
        }

        audioSource.volume = targetVolume;
    }

    public void StopMusic()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
