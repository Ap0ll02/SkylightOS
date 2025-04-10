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

    public VisualElement groupLogin;
    public VisualElement groupSaveLoadSelect;
    public VisualElement groupDifficulty;
    public VisualElement groupLevel;
    public VisualElement groupBack;
    public VisualElement root;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        root = uiDocument.rootVisualElement;

        groupLogin = root.Q<VisualElement>("LoginContainer");
        groupSaveLoadSelect = root.Q<VisualElement>("SaveLoadSelect");
        groupDifficulty = root.Q<VisualElement>("Difficulty");
        groupLevel = root.Q<VisualElement>("Level");
        groupBack = root.Q<VisualElement>("Back");

        var buttonLogin = groupLogin.Q<Button>("Login");
        var buttonBack = groupBack.Q<Button>("Back");
        var buttonLevel1 = groupLevel.Q<Button>("Level1");
        var buttonLevel2 = groupLevel.Q<Button>("Level2");
        var buttonLevel3 = groupLevel.Q<Button>("Level3");
        var buttonQuit = root.Q<Button>("Quit");
        var buttonSetting = root.Q<Button>("Settings");
        var buttonMusic = root.Q<Button>("Music");
        var buttonMusicOff = root.Q<Button>("MusicOff");
        var buttonCredits = root.Q<Button>("Credits");
        var buttonNewGame = groupSaveLoadSelect.Q<Button>("NewGame");
        var buttonLoadGame = groupSaveLoadSelect.Q<Button>("LoadGame");
        var buttonEasy = groupDifficulty.Q<Button>("Easy");
        var buttonMedium = groupDifficulty.Q<Button>("Medium");
        var buttonHard = groupDifficulty.Q<Button>("Hard");
        var buttonLevelSelect = groupSaveLoadSelect.Q<Button>("LevelSelect");

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
        buttonNewGame.clicked += NewGameButtonClicked;
        buttonLoadGame.clicked += LoadGameButtonClicked;
        buttonEasy.clicked += EasyButtonClicked;
        buttonMedium.clicked += MediumButtonClicked;
        buttonHard.clicked += HardButtonClicked;
        buttonLevelSelect.clicked += LevelSelectButtonClicked;

        groupBack.style.display = DisplayStyle.None;
        groupLevel.style.display = DisplayStyle.None;
        groupSaveLoadSelect.style.display = DisplayStyle.None;
        groupDifficulty.style.display = DisplayStyle.None;
    }

    private void LoginButtonClicked()
    {
        groupBack.style.display = DisplayStyle.Flex;
        groupLogin.style.display = DisplayStyle.None;
        groupSaveLoadSelect.style.display = DisplayStyle.Flex;
    }

    private void BackButtonClicked()
    {
        groupBack.style.display = DisplayStyle.None;
        groupLogin.style.display = DisplayStyle.Flex;
        groupLevel.style.display = DisplayStyle.None;
        groupSaveLoadSelect.style.display = DisplayStyle.None;
        groupDifficulty.style.display = DisplayStyle.None;
    }

    private void NewGameButtonClicked()
    {
        groupSaveLoadSelect.style.display = DisplayStyle.None;
        groupDifficulty.style.display = DisplayStyle.Flex;
    }

    private void LoadGameButtonClicked()
    {
        // Load the saved game level from PlayerPrefs
        SaveLoad.Level savedLevel = SaveLoad.GameLevel;

        // Load the corresponding scene based on the saved level
        switch (savedLevel)
        {
            case SaveLoad.Level.Level1:
                SceneManager.LoadScene("IntroCutscene");
                break;
            case SaveLoad.Level.Level2:
                SceneManager.LoadScene("1To2Cutscene");
                break;
            case SaveLoad.Level.Level3:
                SceneManager.LoadScene("2To3Cutscene");
                break;
            default:
                Debug.LogError("Unknown level saved in PlayerPrefs");
                break;
        }
    }

    private void EasyButtonClicked()
    {
        SaveLoad.GameDifficulty = SaveLoad.Difficulty.Easy;
        SceneManager.LoadScene("IntroCutscene");
    }

    private void MediumButtonClicked()
    {
        SaveLoad.GameDifficulty = SaveLoad.Difficulty.Medium;
        SceneManager.LoadScene("IntroCutscene");
    }

    private void HardButtonClicked()
    {
        SaveLoad.GameDifficulty = SaveLoad.Difficulty.Hard;
        SceneManager.LoadScene("IntroCutscene");
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
        var buttonLogin = groupLogin.Q<Button>("Login");
        var buttonBack = groupBack.Q<Button>("Back");
        var buttonLevel1 = groupLevel.Q<Button>("Level1");
        var buttonLevel2 = groupLevel.Q<Button>("Level2");
        var buttonLevel3 = groupLevel.Q<Button>("Level3");
        var buttonQuit = root.Q<Button>("Quit");
        var buttonSetting = root.Q<Button>("Settings");
        var buttonMusic = root.Q<Button>("Music");
        var buttonMusicOff = root.Q<Button>("MusicOff");
        var buttonCredits = root.Q<Button>("Credits");
        var buttonNewGame = groupSaveLoadSelect.Q<Button>("NewGame");
        var buttonLoadGame = groupSaveLoadSelect.Q<Button>("LoadGame");
        var buttonEasy = groupDifficulty.Q<Button>("Easy");
        var buttonMedium = groupDifficulty.Q<Button>("Medium");
        var buttonHard = groupDifficulty.Q<Button>("Hard");
        var buttonLevelSelect = groupSaveLoadSelect.Q<Button>("LevelSelect");

        buttonLogin.clicked -= LoginButtonClicked;
        buttonLevel1.clicked -= Level1ButtonClicked;
        buttonLevel2.clicked -= Level2ButtonClicked;
        buttonLevel3.clicked -= Level3ButtonClicked;
        buttonQuit.clicked -= QuitButtonClicked;
        buttonSetting.clicked -= SettingButtonClicked;
        buttonMusic.clicked -= MusicButtonClicked;
        buttonCredits.clicked -= CreditsButtonClicked;
        buttonBack.clicked -= BackButtonClicked;
        buttonNewGame.clicked -= NewGameButtonClicked;
        buttonLoadGame.clicked -= LoadGameButtonClicked;
        buttonEasy.clicked -= EasyButtonClicked;
        buttonMedium.clicked -= MediumButtonClicked;
        buttonHard.clicked -= HardButtonClicked;
        buttonLevelSelect.clicked -= LevelSelectButtonClicked;
    }

    private void LevelSelectButtonClicked()
    {
        groupBack.style.display = DisplayStyle.Flex;
        groupLogin.style.display = DisplayStyle.None;
        groupLevel.style.display = DisplayStyle.Flex;
        groupSaveLoadSelect.style.display = DisplayStyle.None;
        groupDifficulty.style.display = DisplayStyle.None;
    }
}
