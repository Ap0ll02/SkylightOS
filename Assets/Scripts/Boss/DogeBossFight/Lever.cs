using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    AlpinePlayer player;
    public Sprite onSprite;
    public Sprite offSprite;
    public bool isOn = false;  // The current state of the lever (On or Off)
    public GameObject leverHandle;
    public GameObject thingToActivate;// Reference to the lever handle (you can animate or change sprite when toggling)
    public AudioClip leverSound;      // Reference to the sound to play when the lever is turned on
    private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the lever's trigger zone
        if (other.CompareTag("Player") && !isOn)
        {
            TurnLeverOn();
        }
    }

    // Toggle the lever's state between On and Off
    void TurnLeverOn()
    {
        isOn = true;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (audioSource != null && leverSound != null)
        {
            audioSource.PlayOneShot(leverSound);
        }
        // Update the lever's state (e.g., changing its visual appearance)
        if (leverHandle != null)
        {
            if (thingToActivate != null)
            {
                Debug.Log("Lever Activated");
                thingToActivate.GetComponent<AbstractBossStage>().BossEndStage();
                leverHandle.GetComponent<SpriteRenderer>().sprite = onSprite;
            }
        }
    }
}
