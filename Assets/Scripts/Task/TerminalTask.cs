using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

/**
 * @Author Jack Ratermann
 * @Date 11/03/2024
 * @Version 0.1
 * @Brief This is the terminal task class that will be used to create tasks for the terminal to execute.
 * @Note If this breaks, the likely cause is "TerminalWindow" in the scene wasn't renamed and kept its' original "TerminalWindow Variant" name.
 */

public class TerminalTask : AbstractTask 
{
    /// @var State Enum that has on and off state for the task.
    private enum State
    {
        On,
        Off,
        ArrowGameOn,
        ArrowGameOff,
        MazeGameOn,
        MazeGameOff,
        StageTwo
    }

    /// @var Terminal Variables that get references in awake() to the scene's terminal
    public GameObject terminal;
    [SerializeField] GameObject dw;
    private List<bool> tasksDone = new List<bool>();
    public TMP_Text terminalText;
    public GameObject lsBtn;
    public GameObject hGroup;
    public GameObject avBtn;
    public GameObject nmapBtn;
    private State termState;
    public GameObject terminalPanel;

    // References to the arrow game to control its start and stop from the task
    public GameObject arrowGame;
    public Arrowgame ag;

    // Draw the maze for the LS Maze Game
    public GameObject drawMaze;
    public DrawMaze dm;

    public InputSystem_Actions agInput;

    // Audio source reference on TerminalWindow to play music on games.
    public AudioSource musicAG;
    public bool iTimer = false;

    /// @brief Assign all of the terminal objects in the scene.
    public void Awake() {
        taskTitle = "Download and Install Antivirus";
        taskDescription = "Click to download the Antivirus Toolkit, and then access the files to install it.";
        arrowGame = FindObjectOfType<Arrowgame>().gameObject;
        terminal = FindObjectOfType<Terminal>().gameObject.GetComponentInParent<BasicWindow>().gameObject;
        terminalPanel = FindObjectOfType<Terminal>().gameObject;
        terminalText = terminalPanel.GetComponentInChildren<TMP_Text>();
        hGroup = FindObjectOfType<Terminal>().gameObject.GetComponentInChildren<HorizontalLayoutGroup>().gameObject;
        // lsBtn = GameObject.Find("LSBtn");
        // avBtn = GameObject.Find("antiviralBtn");
        // nmapBtn = GameObject.Find("nmapBtn");
        termState = State.Off;
        ag = arrowGame.GetComponent<Arrowgame>();
        drawMaze = FindObjectOfType<DrawMaze>().gameObject;
        dm = drawMaze.GetComponent<DrawMaze>();
        musicAG = FindObjectOfType<Terminal>().GetComponentInParent<AudioSource>();
        tasksDone.Add(false);
        tasksDone.Add(false);
        agInput = ag.pInput;
    }

    public new void Start() {
        base.Start();
        gameObject.SetActive(false);
    }

    /// @brief Subscription handling to all 3 button events from the terminal.
    public void OnEnable()
    {
        Arrowgame.OnArrowPress += checkHazards;
        Arrowgame.OnGameEnd += GameEnd;
        DrawMaze.OnGameEnd += GameEnd;
        Terminal.OnAVPressed += AVTask;
        Terminal.OnLSPressed += LSTask;
        Terminal.OnNMAPPressed += NMAPTask;
        PerformanceThiefManager.PThiefStarted += TimerOn;
        PerformanceThiefManager.PThiefIDisable += StopInput;
    }

    public void OnDisable()
    {
        Arrowgame.OnArrowPress -= checkHazards;
        Arrowgame.OnGameEnd -= GameEnd;
        DrawMaze.OnGameEnd -= GameEnd;
        Terminal.OnAVPressed -= AVTask;
        Terminal.OnLSPressed -= LSTask;
        Terminal.OnNMAPPressed -= NMAPTask;
        PerformanceThiefManager.PThiefStarted -= TimerOn;
        PerformanceThiefManager.PThiefIDisable -= StopInput;
    }

    void TimerOn() {
        iTimer = false;
    }

    public override void checkHazards()
    {
        foreach (var hazardManager in hazardManagers)
        {
            if (!hazardManager.CanProgress())
            {
                ag.CanContinue = false;
                break;
            }
            else
            {
                ag.CanContinue = true;
            }
        }
    }

    void StopInput() {
        if(iTimer == false) {
            iTimer = true;
            StartCoroutine(StopInputTimer());
        }
    }

    /// @brief Changes terminal information prompt and terminal state.
    public override void startTask()
    {
        // Terminal Task Start, Prompts User To Use The AntiVirus installation tool. Changes terminal state to On.
        string termText = "Welcome To The Console, Let's get you started downloading some software\n"
            + "The AntiVirus toolkit is a helpful addition for getting rid of pesky malware!\n" + "Click On The Anti-Virus Download to start.";
        termState = State.On;
        terminalText.text = termText;
    }

    public override void stopHazards()
    {
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StopHazard();
        }
    }

    public override void startHazards()
    {
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StartHazard();
        }
    }

    /// @brief 3 Functions To Handle When The Terminal Buttons Are Pressed.
    public void AVTask()
    {
        /// @var termLoadBar will be used for arrow game, how the loading progresses. 
        /// Different process and visual than the prefab loading bar.
        if (termState == State.On)
        {
            terminalText.text = "";
            termState = State.ArrowGameOn;
            arrowGame.SetActive(true);
            startHazards();
            //hGroup.SetActive(false);
            ag.StartGame();
            agInput = ag.pInput;
            if(!musicAG.isPlaying) {
                musicAG.Play();
            }
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
        else if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
    }

    // Used for when the games are done
    public void GameEnd() {
        Debug.Log("I heard you bitch");
        if(termState == State.ArrowGameOn) {
            termState = State.ArrowGameOff;
            arrowGame.SetActive(false);
            hGroup.SetActive(true);
            StartCoroutine(FadeOut(5));
            terminalText.text = "Download Complete. Please Locate The Installer In The Filesystem.\n";
            terminalText.text += "Try our new file browser with the button below!\n";
        }
        else if(termState == State.MazeGameOn) {
            termState = State.MazeGameOff;
            hGroup.SetActive(true);
            dm.gameObject.SetActive(false);
            terminalText.text = "Installing File, Congratulations!";

            GameObject termLoad = Instantiate(dw, FindObjectOfType<Terminal>().GetComponentInParent<BasicWindow>().gameObject.transform);
            termLoad.SetActive(true);
            termLoad.GetComponentInChildren<LoadingScript>().StartLoading();
        }
        stopHazards();

        static int getInd(State t) =>
            t switch
            {
                State.MazeGameOff => 1,
                State.ArrowGameOff => 0,
                _ => -1
            };

        tasksDone[getInd(termState)] = true;
    }

    // Start of the maze game task with state checking so the LS can be used in multiple ways, further
    // decoupling the task from the OS element to enforce reusability & individuality of tasks and components
    public void LSTask()
    {
        Debug.Log(termState);
        if(termState == State.ArrowGameOff)
        {
            dm.gameObject.SetActive(true);
            termState = State.MazeGameOn;
            terminalText.text = "Scanning Files: Maze Game Initiate!";
            terminalText.text += "Click A, B, C, or D for your options.";
            StartCoroutine(Timer(7f));
            terminalText.text = "---| A |---| B |---| C |---|D|---\n";
            terminalText.text += "Example Stage, The Paths Correspond To A, B, C & D.\n";
            terminalText.text += "Please Press (A, B, C, or D) To Continue Into The File System\n";
            terminalText.text += "-----------------------------------\n";
            hGroup.SetActive(false);
            dm.StartGame();
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
        else if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
    }

    void NMAPTask()
    {
        if (termState == State.MazeGameOn) {
            terminalText.text = terminalText.text;
        }
        else if (termState == State.ArrowGameOn) {
            terminalText.text = "";
        }
    }

    public new void CompleteTask() {
        termState = State.Off;
        base.CompleteTask();
    }

    // Variable second timer for allowing text to read, or whichever your heart desires
    private IEnumerator Timer(float x) {
        while(true) yield return new WaitForSeconds(x);
    }

    // Fades out music, passed in parameter is number of seconds to fadeout
    private IEnumerator FadeOut(float x) {
        while(musicAG.volume > 0) {
            musicAG.volume -= 1/(x*10);
            yield return new WaitForSeconds(0.1f);
        }
        if(musicAG.volume < 0) musicAG.Stop();  
    }

    public IEnumerator StopInputTimer() {
        Debug.Log("Input Disabled");
        agInput.User.Arrows.Disable();
        yield return new WaitForSeconds(4.5f);
        agInput.User.Arrows.Enable();
        Debug.Log("Input Re-Enabled");
        iTimer = false;
    }
}