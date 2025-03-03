// Quinn Contaldi 
// This is the script to control the MainMenu Functionality and handle the transition too the cutscene
// Future work. I need to be able to toggle the music button
// I need to create another transition for our saved files which will repersent users 
using System;
using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartScreenScript : MonoBehaviour
{
    [SerializeField] private UIDocument uiDocument;
    private AudioSource audioSource;

    public Button buttonLogin;
    public Button buttonBack;
    public Button buttonLevel1;
    public Button buttonLevel2;
    public Button buttonLevel3;
    public Button buttonQuit;
    public Button buttonSetting;
    public Button buttonMusic;
    public Button buttonMusicOff;
    public Button buttonCredits;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        var root = uiDocument.rootVisualElement;

        buttonLogin = root.Q<Button>("Login");
        buttonBack = root.Q<Button>("Back");
        buttonLevel1 = root.Q<Button>("Level1");
        buttonLevel2 = root.Q<Button>("Level2");
        buttonLevel3 = root.Q<Button>("Level3");
        buttonQuit = root.Q<Button>("Quit");
        buttonSetting = root.Q<Button>("Settings");
        buttonMusic = root.Q<Button>("Music");
        buttonMusicOff = root.Q<Button>("MusicOff");
        buttonCredits = root.Q<Button>("Credits");

        buttonLogin.clicked += LoginButtonClicked;
        buttonLevel1.clicked += Level1ButtonClicked;
        buttonLevel2.clicked += Level2ButtonClicked;
        buttonLevel3.clicked += Level3ButtonClicked;
        buttonQuit.clicked += QuitButtonClicked;
        buttonSetting.clicked += SettingButtonClicked;
        buttonMusic.clicked += MusicButtonClicked;
        buttonMusicOff.clicked += MusicButtonClicked;
        buttonCredits.clicked += CreditsButtonClicked;
        buttonBack.clicked += BackButtonClicked;

        buttonBack.style.display = DisplayStyle.None;
        buttonLevel1.style.display = DisplayStyle.None;
        buttonLevel2.style.display = DisplayStyle.None;
        buttonLevel3.style.display = DisplayStyle.None;
    }

    private void LoginButtonClicked()
    {
        buttonBack.style.display = DisplayStyle.Flex;
        buttonLogin.style.display = DisplayStyle.None;
        buttonLevel1.style.display = DisplayStyle.Flex;
        buttonLevel2.style.display = DisplayStyle.Flex;
        buttonLevel3.style.display = DisplayStyle.Flex;
    }

    private void BackButtonClicked()
    {
        buttonBack.style.display = DisplayStyle.None;
        buttonLogin.style.display = DisplayStyle.Flex;
        buttonLevel1.style.display = DisplayStyle.None;
        buttonLevel2.style.display = DisplayStyle.None;
        buttonLevel3.style.display = DisplayStyle.None;
    }

    private void Level1ButtonClicked()
    {
        SceneManager.LoadScene("IntroCutscene");
        audioSource.Stop();
    }

    private void Level2ButtonClicked()
    {
        SceneManager.LoadScene("Level2");
        audioSource.Stop();
    }

    private void Level3ButtonClicked()
    {
        SceneManager.LoadScene("Level3");
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
        buttonLogin.clicked -= LoginButtonClicked;
        buttonLevel1.clicked -= Level1ButtonClicked;
        buttonLevel2.clicked -= Level2ButtonClicked;
        buttonLevel3.clicked -= Level3ButtonClicked;
        buttonQuit.clicked -= QuitButtonClicked;
        buttonSetting.clicked -= SettingButtonClicked;
        buttonMusic.clicked -= MusicButtonClicked;
        buttonCredits.clicked -= CreditsButtonClicked;
        buttonBack.clicked -= BackButtonClicked;
    }
}
