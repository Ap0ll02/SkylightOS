public class OverheatGPUTask : AbstractTask
{
    public SystemResourcesWindow SystemStatus;

    void Awake()
    {
        SystemStatus = GetComponent<SystemResourcesWindow>();
    }
    // Start is called before the first frame update
    new void Start()
    {
        SystemStatus.SetSystemResources(SystemResourcesWindow.GPUStatus.WARNING, SystemStatus.currentRAMStatus);
    }

    public override void startTask()
    {
        SystemStatus.SetSystemResources(SystemResourcesWindow.GPUStatus.CRITICAL, SystemStatus.currentRAMStatus);
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
}
