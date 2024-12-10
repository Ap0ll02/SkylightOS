// Quinn Contaldi 
// This is the script to control the MainMenu Functionality and handle the transition too the cutscene
// Future work. I need to be able to toggle the music button
// I need to create another transition for our saved files which will repersent users 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartScreenScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    // We want to be able to control our music right? this is the reference for it 
    private AudioSource audioSource;
    
    // This is getting references to all of our button components 
    public Button buttonLogin;
    public Button buttonQuit;
    public Button buttonSetting;
    public Button buttonMusic;
    public Button buttonMusicOff;
    public Button buttonCredits;

    // Start is called before the first frame update

    private void Start()
    {
        // Grab the audio source reference
        audioSource = GetComponent<AudioSource>();
        var root = uiDocument.rootVisualElement;
        // We need to find the buttons
        buttonLogin = root.Q<Button>("Login");
        buttonQuit = root.Q<Button>("Quit");
        buttonSetting = root.Q<Button>("Settings");
        buttonMusic = root.Q<Button>("Music");
        buttonMusicOff = root.Q<Button>("MusicOff");
        buttonCredits = root.Q<Button>("Credits");


        // UXML does not manage events. C# will through subscribing to events or registering callbacks. Let's subscribe to an event
        // Simple the event is the login button being clicked we will subscribe our LoginButtonClicked function to this event
        buttonLogin.clicked += LoginButtonClicked;
        buttonQuit.clicked += QuitButtonClicked;
        buttonSetting.clicked += SettingButtonClicked;
        buttonMusic.clicked += MusicButtonClicked;
        buttonMusicOff.clicked += MusicButtonClicked;
        buttonCredits.clicked += CreditsButtonClicked;

        //UpdateMusicButtonVisibility();
    }

    private void LoginButtonClicked()
    {
        SceneManager.LoadScene("IntroCutscene");
        audioSource.Stop();
    }

    private void QuitButtonClicked()
    {
        Application.Quit();
    }

    private void SettingButtonClicked()
    {
        Debug.Log("This is where the Settings will be");
    }

    // This function simply handles the toggle button for the audio button 
    private void MusicButtonClicked()
    {
        if (audioSource.isPlaying) 
            audioSource.Pause();
        else
            audioSource.Play();
    }

    private void CreditsButtonClicked()
    {
        Debug.Log("This is where the Credits will be");
    }

    private void OnDisable()
    {
        // This function just unsubscribes our function from the event. We want to avoid those pesky errors 
        buttonLogin.clicked -= LoginButtonClicked;
        buttonLogin.clicked -= QuitButtonClicked;
        buttonLogin.clicked -= SettingButtonClicked;
        buttonLogin.clicked -= MusicButtonClicked;
        buttonLogin.clicked -= CreditsButtonClicked;

    }

    // This is so I can eventually implement the toggle feature 
    // private void UpdateMusicButtonVisibility()
    // {
    //     if (audioSource.isPlaying)
    //     {
    //         buttonMusic.style.display = DisplayStyle.Flex;
    //         buttonMusicOff.style.display = DisplayStyle.None;
    //     }
    //     else
    //     {
    //         buttonMusic.style.display = DisplayStyle.None;
    //         buttonMusicOff.style.display = DisplayStyle.Flex;
    //     }
    // }

}
