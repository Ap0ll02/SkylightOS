// Jack Ratermann
// Overehating task for the GPU
// Depends on: SystemResourcesWindow.cs, PipeGame.cs
public class OverheatGPUTask : AbstractTask
{
    public SystemResourcesWindow SystemStatus;
    public PipeGame pg;

    void Awake()
    {
        SystemStatus = FindObjectOfType<SystemResourcesWindow>();
        pg = FindObjectOfType<PipeGame>();
    }

    new void Start()
    {
        SystemStatus.SetSystemResources(SystemResourcesWindow.GPUStatus.WARNING, SystemStatus.currentRAMStatus);
        SystemStatus.UpdateSystemResourcesText();
    }
    // Start is called before the first frame update
    public override void startTask()
    {
        SystemStatus.SetSystemResources(SystemResourcesWindow.GPUStatus.CRITICAL, SystemStatus.currentRAMStatus);
        SystemStatus.UpdateSystemResourcesText();
    }

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

    private void OEnable()
    {
        pg.GameOverEvent += CompleteTask;        
    }

    private void OnDisable()
    {
        pg.GameOverEvent -= CompleteTask;
    }

    public override void CompleteTask()
    {
        stopHazards();
        SystemStatus.SetSystemResources(SystemResourcesWindow.GPUStatus.OK, SystemStatus.currentRAMStatus);
        SystemStatus.UpdateSystemResourcesText();
        base.CompleteTask();
    }
}
