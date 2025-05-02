using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Task for updating the peripherals
/// Garrett Shart
/// Lowkey got this task shit down real good fr
/// </summary>

public class PeripheralUpdateTask : AbstractTask
{
    // Reference to the bios window, and the peripheral window 
    // We change the states of the bios window but don't really mess with the peripheral window
    // (we receive messages from the peripheral window)
    public SkyBIOSWindow biosWindow;
    public PeripheralWindow peripheralWindow;

    // fuckass hentai character
    // "what model are you guys using for your ai assistant?" 
    // "shit fart 3.5"
    public Northstar northstar;

    private bool loadingBarStage;
    private bool wireConnectStage;

    public List<AbstractManager> loadingHazards;  

    // I have risen
    private void Awake()
    {
        biosWindow = FindObjectOfType<SkyBIOSWindow>();
        peripheralWindow = FindObjectOfType<PeripheralWindow>();
        northstar = FindObjectOfType<Northstar>();
        taskTitle = "Update Peripherals";
        taskDescription = "Your peripherals have stopped working and need a firmware update. Navigate to the SkyBIOS window to learn more";
    }

    // Start is theoretically called before the first update
    new void Start()
    {
        biosWindow.SetState(SkyBIOSWindow.PeripheralState.NotWorking);
    }

    // She enable on my disable until I long compiler time (c# quirk)
    private void OnEnable()
    {
        peripheralWindow.OnLoadingStart += HandlePeripheralLoadingStarted;
        peripheralWindow.OnLoadingComplete += HandlePeripheralLoadingEnded;
        peripheralWindow.OnConnectStart += HandleConnectMinigameStarted;
        peripheralWindow.OnConnectComplete += HandleConnectMinigameEnded;
    }

    private void OnDisable()
    {
        peripheralWindow.OnLoadingStart -= HandlePeripheralLoadingStarted;
        peripheralWindow.OnLoadingComplete -= HandlePeripheralLoadingEnded;
        peripheralWindow.OnConnectStart -= HandleConnectMinigameStarted;
        peripheralWindow.OnConnectComplete -= HandleConnectMinigameEnded;
    }

    // It will bother me until the end of time that 'startTask' is lowercase
    public override void startTask()
    {
        biosWindow.SetState(SkyBIOSWindow.PeripheralState.NotWorkingInteractable);
        northstar.WriteHint("Your peripherals (except for mouse and keyboard ig) have stopped working.", Northstar.Style.warm, true);
        northstar.StartHintCoroutine("Check the SKYBIOS window perhaps?",18f);
    }

    // CompleteTask is uppercase though (because I made it)
    public override void CompleteTask()
    {
        biosWindow.SetState(SkyBIOSWindow.PeripheralState.Working);
        northstar.WriteHint("Your peripherals are now working again.", Northstar.Style.warm, true);
        base.CompleteTask();
    }

    // Basically whenever the loading bar starts
    void HandlePeripheralLoadingStarted()
    {
        northstar.InterruptHintCoroutine();
        StartLoadingHazards();
        northstar.WriteHint("A loading bar, how original.", Northstar.Style.warm);

    }

    // Loading bar end or some shiet
    void HandlePeripheralLoadingEnded()
    {
        StopLoadingHazards();
        northstar.WriteHint("Mom i got the virus but my pants slipped", Northstar.Style.warm);
    }

    void HandleConnectMinigameStarted()
    {
        startHazards();
        northstar.WriteHint("Plug the wires in, but watch out for the imposter", Northstar.Style.warm, true);
        StartCoroutine(Sus());
    }

    void HandleConnectMinigameEnded()
    {
        //northstar.WriteHint("You did it! You plugged in the wires!", Northstar.Style.warm, true);
        stopHazards();
        CompleteTask();
    }

    private IEnumerator Sus()
    {
        yield return new WaitForSeconds(5);
        northstar.WriteHint("Sus", Northstar.Style.hot, true);
    }

    public override void startHazards()
    {
        foreach (AbstractManager hazard in hazardManagers)
        {
            hazard.StartHazard();
        }
    }

    public void StartLoadingHazards()
    {
        foreach (AbstractManager hazard in loadingHazards)
        {
            hazard.StartHazard();
        }
    }

    public void StopLoadingHazards()
    {
        foreach (AbstractManager hazard in loadingHazards)
        {
            hazard.StopHazard();
        }
    }

    public override void stopHazards()
    {
        foreach (AbstractManager hazard in hazardManagers)
        {
            hazard.StopHazard();
        }
    }

    public override void checkHazards()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
