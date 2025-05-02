using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTask : AbstractTask
{
    // Start is called before the first frame update
    [SerializeField] UpdateGame updateGameWindow;
    public UpdatePanel updatePannel;

    public Northstar northstar;

    public new void Start()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.NotWorking);
        northstar = FindObjectOfType<Northstar>();
    }
    public override void startTask()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.NotWorkingInteractable);
        northstar.WriteHint("Update the pc playa...", Northstar.Style.cold, true);
        northstar.StartHintCoroutine("Use the system window boss", 18f);
    }

    public override void checkHazards()
    {
        //
    }

    public override void startHazards()
    {
        //
    }

    public override void stopHazards()
    {
        //
    }

    void OnEnable()
    {
        UpdateGame.updateGameStartNotify += HandleMinigameStarted;
        UpdateGame.updateGameEndWinNotify  += HandleMinigameEnded;
    }

    // Removing message handler?
    void OnDisable()
    {
        UpdateGame.updateGameStartNotify -= HandleMinigameStarted;
        UpdateGame.updateGameEndWinNotify  -= HandleMinigameEnded;
    }

    void HandleMinigameStarted()
    {
        northstar.InterruptHintCoroutine();
        startHazards();
        //northstar.WriteHint("OH SHIT WE GOTTA PUT THE RAM IN THE RAM SLOTS", Northstar.Style.warm);
    }

    void HandleMinigameEnded()
    {
        CompleteTask();
    }

    public override void CompleteTask()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.Working);
        //northstar.WriteHint("We did the ram!!!!!", Northstar.Style.warm);
        stopHazards();
        base.CompleteTask();
    }
}
