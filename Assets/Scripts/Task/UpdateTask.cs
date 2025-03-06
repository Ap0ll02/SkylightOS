using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTask : AbstractTask
{
    // Start is called before the first frame update
    [SerializeField] UpdateGame updateGameWindow;
    public UpdatePanel updatePannel;

    public void Awake()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.NotWorking);
    }
    public override void startTask()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.NotWorkingInteractable);
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
