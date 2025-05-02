// Jack Ratermann
// Overehating task for the GPU
// Depends on: SystemResourcesWindow.cs, PipeGame.cs
public class OverheatGPUTask : AbstractTask
{
    public SystemResourcesWindow SystemStatus;
    public PipeGame pg;

    public Northstar northstar;

    void Awake()
    {
        SystemStatus = FindObjectOfType<SystemResourcesWindow>();
        pg = FindObjectOfType<PipeGame>();
        northstar = FindObjectOfType<Northstar>();
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
        northstar.WriteHint("Looks like the GPU is overheating...", Northstar.Style.cold, true);
        northstar.StartHintCoroutine("Check out the resources panel!",10f);

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
        northstar.InterruptHintCoroutine();
        foreach (var hazardManager in hazardManagers) {
            hazardManager.StartHazard();
        }
    }

    private void OnEnable()
    {
        pg.GameStartEvent += startHazards;
        pg.GameOverEvent += CompleteTask;        
    }

    private void OnDisable()
    {
        pg.GameStartEvent -= startHazards;
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
