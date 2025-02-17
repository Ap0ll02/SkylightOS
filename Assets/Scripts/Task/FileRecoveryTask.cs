using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileRecoveryTask : AbstractTask
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

        public override void startTask()
    {
        
    }

    public override void CompleteTask()
    {
        stopHazards();
        base.CompleteTask();
    }
    // Update is called once per frame
    public override void checkHazards(){

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

}
