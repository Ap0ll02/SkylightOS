using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
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
    public GameObject finalFilePiece; // New final file piece  

    // GamePlay Initials  
    public Coroutine updateGame;
    public InputAction moveAction;
    public bool gameRunning = false;
    readonly float moveSpeed = 1.7f;
    public FileRecoPlayer rpScript;
    public ExitWall ewScript;

    public event Action FileMazeStarted;
    public event Action FileMazeOver;

    // Reference to the 'Files Recovered' UI element  
    public TMP_Text filesRecoveredText;

    public BasicWindow evidence;

    // Audio source for movement  
    public AudioSource movementAudioSource;
    public AudioSource pickupSource;

    // Get any non-inspector references here  
    void Awake()
    {
        gameObject.SetActive(true);
    }
    void Start()
    {
        GetComponent<BasicWindow>().CloseWindow();
        // StartGame();  
    }

    // MARK: - Game Initialization  
    public override void StartGame()
    {
        FileMazeStarted?.Invoke();
        filePieces = new List<GameObject>();
        GetComponent<BasicWindow>().OpenWindow();
        HashSet<int> randomNumbers = new();
        gameRunning = true;
        SpawnList.AddRange(SpawnContainer.GetComponentsInChildren<Transform>());
        System.Random rand = new();
        while (randomNumbers.Count < 5)
        {
            randomNumbers.Add(rand.Next(0, SpawnList.Count));
        }
        int i = 0;
        foreach (int num in randomNumbers)
        {
            filePieces.Add(Instantiate(filePiecePrefab, parent: parentContainer.transform));
            filePieces[i].transform.position = SpawnList[num].position;
            i++;
        }
        foreach (Transform spawnBlock in SpawnList)
        {
            spawnBlock.gameObject.SetActive(false);
        }
        moveAction = InputSystem.actions.FindAction("Move");
        updateGame = StartCoroutine(GameUpdate());

        // Ensure the final file piece is disabled initially  
        if (finalFilePiece != null)
        {
            finalFilePiece.SetActive(false);
        }

        // Ensure the movement audio source is set to loop and start playing  
        if (movementAudioSource != null)
        {
            movementAudioSource.loop = true;
            movementAudioSource.Play();
        }
    }

    public void OnEnable()
    {
        rpScript.FileRecovered += FileRecover;
        ewScript.GameOverEvent += GameOver;
    }

    public void OnDisable()
    {
        rpScript.FileRecovered -= FileRecover;
        ewScript.GameOverEvent -= GameOver;
    }

    public void GameOver()
    {
        gameRunning = false;
        isStarted = false;
        StopCoroutine(updateGame);
        foreach (GameObject filePiece in filePieces)
        {
            Destroy(filePiece);
        }
        filePieces.Clear();
        Destroy(player);
        FileMazeOver?.Invoke();
        GetComponent<BasicWindow>().CloseWindow();

        // Stop movement audio when the game is over  
        if (movementAudioSource != null && movementAudioSource.isPlaying)
        {
            movementAudioSource.Stop();
        }
    }

    Vector3 velocity = Vector3.zero;
    public IEnumerator GameUpdate()
    {
        while (gameRunning)
        {
            Vector2 moveValue = moveAction.ReadValue<Vector2>();
            player.transform.position += moveSpeed * Time.deltaTime * (Vector3)moveValue;
            cameraPos.transform.position = Vector3.SmoothDamp(cameraPos.transform.position, player.transform.position, ref velocity, 0.1f);
            var turnSpeed = 8f * Time.deltaTime;
            if (moveValue != Vector2.zero)
            {
                var targetRotation = Mathf.Atan2(moveValue.y, moveValue.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, targetRotation), turnSpeed);

                // Adjust pitch based on cardinal direction
                if (movementAudioSource != null)
                {
                    if (Mathf.Abs(moveValue.x) > Mathf.Abs(moveValue.y))
                    {
                        // Horizontal movement
                        movementAudioSource.pitch = moveValue.x > 0 ? 1.2f : 0.8f; // Right: higher pitch, Left: lower pitch
                    }
                    else
                    {
                        // Vertical movement
                        movementAudioSource.pitch = moveValue.y > 0 ? 1.5f : 0.6f; // Up: highest pitch, Down: lowest pitch
                    }
                }
            }
            else
            {
                // Reset pitch when not moving
                if (movementAudioSource != null)
                {
                    movementAudioSource.pitch = 1.0f;
                }
            }
            yield return new WaitForFixedUpdate();
        }
    }

    public void ExitUnlock()
    {
        ExitWall.GetComponent<BoxCollider2D>().isTrigger = true;
        ExitWall.GetComponent<MeshRenderer>().enabled = false;
        // Enable the final file piece when the exit is unlocked  
        if (finalFilePiece != null)
        {
            finalFilePiece.SetActive(true);
        }
    }

    public void FileRecover(Collider2D filePiece)
    {
        switch (filePiecesCount)
        {
            case 1:
                ExitUnlock();
                if (filePieces.Contains(filePiece.gameObject))
                {
                    filePieces.Remove(filePiece.gameObject);
                    Destroy(filePiece.gameObject);
                }
                filePiecesCount--;
                break;
            case 0:
                if (filePiece == finalFilePiece.GetComponent<Collider2D>())
                {
                    finalFilePiece.SetActive(false);
                    evidence.OpenWindow();
                }
                break;
            default:
                if (filePieces.Contains(filePiece.gameObject))
                {
                    filePieces.Remove(filePiece.gameObject);
                    Destroy(filePiece.gameObject);
                }
                filePiecesCount--;
                break;
        }
        pickupSource.Play();
        UpdateCounter();
    }

    public void UpdateCounter()
    {
        filesRecoveredText.text = "Files Recovered: " + (5 - filePiecesCount) + "/5";
    }
}
