public class FileRecoveryTask : AbstractTask
{
    public FileSystemWindow fileSystemWindow;
    public FileRecoMazeMiniGame frS;

    void Awake()
    {
        fileSystemWindow = FindObjectOfType<FileSystemWindow>();
        taskTitle = "Recover Lost Files";
        taskDescription = "Recover the EXT6 File System by visiting the file recovery software.";
    }
    new void Start()
    {
        // Broken, Non-Interactable State
        fileSystemWindow.SetState(FileSystemWindow.WindowState.NotWorking);
    }

    public override void startTask(){
        // Interactable, Broken State
        fileSystemWindow.SetState(FileSystemWindow.WindowState.NotWorkingInteractable);
    }

    void OnEnable()
    {
        frS.FileMazeStarted += startHazards;
        frS.FileMazeOver += CompleteTask;
    }

    void OnDisable()
    {
        frS.FileMazeStarted -= startHazards;
        frS.FileMazeOver -= CompleteTask;
    }

    public override void CompleteTask()
    {
        // Fixed State
        stopHazards();
        base.CompleteTask();
        fileSystemWindow.SetState(FileSystemWindow.WindowState.Working);
    }
    // Update is called once per frame
    public override void checkHazards(){
        // Setup cancontinue for the maze game, when pop ups occur, no input allowed.
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
