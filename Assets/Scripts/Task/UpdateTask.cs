using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateTask : AbstractTask
{
    // Start is called before the first frame update
    public GameObject UpdatePannelObject;
    public UpdatePanel updatePannel;

    public override void startTask()
    {
        updatePannel.ChangeState(UpdatePanel.UpdateState.NotWorkingInteractable);
    }

    public override void checkHazards()
    {

    }

    public override void startHazards()
    {

    }

    public override void stopHazards()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }
}
