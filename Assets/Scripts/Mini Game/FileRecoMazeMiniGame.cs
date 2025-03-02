using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class FileRecoMazeMiniGame : AbstractMinigame
{
    // Maze Initials
    public GameObject player;
    public GameObject cameraPos;
    public GameObject filePiecePrefab;
    public GameObject SpawnContainer;
    public List<Transform> SpawnList;
    public List<GameObject> filePieces;
    int filePiecesCount = 5;
    public GameObject parentContainer;
    public GameObject ExitWall;

    // GamePlay Initials
    public Coroutine updateGame;
    public InputAction moveAction;
    public bool gameRunning = false;
    readonly float moveSpeed = 1.7f;
    public FileRecoPlayer rpScript;
    public ExitWall ewScript;

    Vector3 OriginalScale;

    // Get any non-inspector references here
    void Start()
    {
        StartGame();
    }

    // MARK: - Game Initialization
    public override void StartGame() {
        Canvas.ForceUpdateCanvases(); // Co-Pilot tip for updating UI before calculations
        GetComponent<BasicWindow>().OpenWindow();        
        HashSet<int> randomNumbers = new();
        gameRunning = true;
        SpawnList.AddRange(SpawnContainer.GetComponentsInChildren<Transform>());
        System.Random rand = new();
        while(randomNumbers.Count < 5){
            randomNumbers.Add(rand.Next(0, SpawnList.Count));
        }
        int i = 0;
        foreach(int num in randomNumbers){
            filePieces.Add(Instantiate(filePiecePrefab, parent: parentContainer.transform));
            filePieces[i].transform.position = SpawnList[num].position;
            i++;
        }
        foreach(Transform spawnBlock in SpawnList){
            Destroy(spawnBlock.gameObject);
        }
        new Vector3(player.transform.localScale.x, player.transform.localScale.y, player.transform.localScale.z);
        moveAction = InputSystem.actions.FindAction("Move"); 
        updateGame = StartCoroutine(GameUpdate());
    }
    public void OnEnable() {
        rpScript.FileRecovered += FileRecover;
        ewScript.GameOverEvent += GameOver;
    }

    public void OnDisable() {
        rpScript.FileRecovered -= FileRecover;
        ewScript.GameOverEvent -= GameOver;
    }

    public void GameOver() {
        gameRunning = false;
        StopCoroutine(updateGame);
        // Destroy all the file pieces, the list, and the player
        foreach(GameObject filePiece in filePieces){
            Destroy(filePiece);
        }
        filePieces.Clear();
        Destroy(player);

        GetComponent<BasicWindow>().CloseWindow();
    }
    // -2645 x, 1330 y, is the maximum right and bottom we allow the maze to move
    // MARK: - Game Update
    Vector3 velocity = Vector3.zero;
    public IEnumerator GameUpdate() {
        while (gameRunning) {
            // Read input, move player
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            player.transform.position += moveSpeed * Time.deltaTime * (Vector3)moveValue;
            cameraPos.transform.position = Vector3.SmoothDamp(cameraPos.transform.position, player.transform.position, ref velocity, 0.1f);
            // Rotate Player In Direction Of Movement
            // CoPilot Help To Write Every Case After The First
            var turnSpeed = 8f * Time.deltaTime;
            if(moveValue != Vector2.zero){
                var targetRotation = Mathf.Atan2(moveValue.y, moveValue.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, targetRotation), turnSpeed);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void ExitUnlock() {
        ExitWall.GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public void FileRecover(Collider2D filePiece) {
        Debug.Log("File Recovered");
        switch(filePiecesCount){
            case 1:
                ExitUnlock();
                filePiecesCount--;
                filePieces.Remove(filePiece.gameObject);
                Destroy(filePiece.gameObject);
                break;
            default:
                filePiecesCount--;
                filePieces.Remove(filePiece.gameObject);
                Destroy(filePiece.gameObject);
                break;
        }
    }
}
