public class FileRecoveryTask : AbstractTask
{
    public FileSystemWindow status_text;
    public FileRecoMazeMiniGame frS;

    void Awake()
    {
        status_text = FindObjectOfType<FileSystemWindow>();
    }
    new void Start()
    {
        // Broken, Non-Interactable State
        status_text.UpdateStatus("EXT6 File System Corrupted.\n Please Recover File System", false);
    }

    public override void startTask(){
        // Interactable, Broken State
        status_text.UpdateStatus("EXT6 File System Corrupted.\n Please Recover File System", true);
    }

    void OnEnable()
    {
        frS.FileMazeOver += CompleteTask;
    }

    void OnDisable()
    {
        frS.FileMazeOver -= CompleteTask;
    }

    public override void CompleteTask()
    {
        // Fixed State
        stopHazards();
        base.CompleteTask();
        status_text.UpdateStatus("File System Recovered", false);
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
