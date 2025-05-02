using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Jack Ratermann
// Pipe game for the GPU
// Depends on: BasicWindow.cs
public class PipeGame : AbstractMinigame
{
    // MARK: - Globals
    public List<GameObject> PipePrefab;
    public GameObject lastPipe;
    public List<GameObject> SpawnedPipes;
    public GameObject PipeLayout;
    public bool gameRunning = false;

    public List<GameObject> ConnectedPath;
    public event Action GameStartEvent;
    public event Action GameOverEvent;
    public Coroutine GameUpdateCR;
    public Coroutine HeatUpCR;
    public TMP_Text temp;
    public int temp_val;

    private BasicWindow window;
    private bool firstTry = true; // stupid variable but i dont wanna do the smart fix

    public void Start()
    {
        window = GetComponent<BasicWindow>();
        window.ForceCloseWindow();
    }

    // MARK: - Initializations
    public override void StartGame()
    {
        if(firstTry)
            GameStartEvent?.Invoke();
        window.isClosable = false;
        try
        {
            StopCoroutine(GameUpdateCR);
        }
        catch (Exception e)
        {
            Debug.Log("early catch, ignore." + e);
        }
        GetComponent<BasicWindow>().OpenWindow();
        System.Random rnd = new();
        temp_val = 30;
        // Initialize the first and ending pipe
        int pipezero = rnd.Next(0, 3);
        switch (pipezero)
        {
            case 0:
                SpawnedPipes.Add(
                    Instantiate(PipePrefab[0], parent: PipeLayout.GetComponent<RectTransform>())
                );
                SpawnedPipes[0].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                    new Vector3(0, 0, 90)
                );
                SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
                break;
            case 1:
                SpawnedPipes.Add(
                    Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>())
                );
                SpawnedPipes[0].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                    new Vector3(0, 0, 180)
                );
                SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
                break;
            case 2:
                SpawnedPipes.Add(
                    Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>())
                );
                SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;
                break;
            default:
                Debug.Log("Accidental 3+");
                break;
        }

        for (int i = 1; i < 99; i++)
        {
            // Take care of ending connection pipe
            if (i == 87)
            {
                int pipelast = rnd.Next(0, 3);
                switch (pipelast)
                {
                    case 0:
                        SpawnedPipes.Add(
                            Instantiate(
                                PipePrefab[1],
                                parent: PipeLayout.GetComponent<RectTransform>()
                            )
                        );
                        SpawnedPipes[87].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                            new Vector3(0, 0, 270)
                        );
                        SpawnedPipes[87].GetComponent<pipe>().PipeStyle =
                            pipe.PipeType.StraightLaying;
                        break;
                    case 1:
                        SpawnedPipes.Add(
                            Instantiate(
                                PipePrefab[1],
                                parent: PipeLayout.GetComponent<RectTransform>()
                            )
                        );
                        SpawnedPipes[87].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
                        break;
                    case 2:
                        SpawnedPipes.Add(
                            Instantiate(
                                PipePrefab[2],
                                parent: PipeLayout.GetComponent<RectTransform>()
                            )
                        );
                        SpawnedPipes[87].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;
                        break;
                    default:
                        Debug.Log("Accidental 3+");
                        break;
                }
                continue;
            }

            int pipeType = rnd.Next(0, 7);
            switch (pipeType)
            {
                case 0: // Vertical Pipe
                    SpawnedPipes.Add(
                        Instantiate(
                            PipePrefab[pipeType],
                            parent: PipeLayout.GetComponent<RectTransform>()
                        )
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
                    break;
                case 1: // Horizontal Pipe
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[0], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                        new Vector3(0, 0, 90)
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
                    break;
                case 2: // Top right pipe
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                        new Vector3(0, 0, 180)
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopRight;
                    break;
                // Top Left
                case 3:
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                        new Vector3(0, 0, 270)
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopLeft;
                    break;
                // Bottom Left
                case 4:
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomLeft;
                    break;
                // Bottom Right
                case 5:
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(
                        new Vector3(0, 0, 90)
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomRight;
                    break;
                case 6:
                    SpawnedPipes.Add(
                        Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>())
                    );
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;
                    break;
                default:
                    Debug.Log("Accidental 7+");
                    break;
            }
            foreach (var pipe in SpawnedPipes)
            {
                pipe.GetComponent<RawImage>().color = Color.white;
            }
        }
        temp.text = "Temp: " + "<bounce a=1 f=3>" + temp_val + " </bounce>";
        lastPipe = SpawnedPipes[0];
        ConnectedPath.Add(SpawnedPipes[0]);
        gameRunning = true;
        GameUpdateCR = StartCoroutine(GameUpdate());
        HeatUpCR = StartCoroutine(HeatUpGPU());
    }

    // MARK: - Game Update Loops
    // Update is called once per frame
    public IEnumerator GameUpdate()
    {
        while (gameRunning)
        {
            yield return null;
            if (CheckPath(0, 87) && temp_val < 200)
            {
                GameOver();
            }
            else if (temp_val >= 200)
            {
                gameRunning = false;
                ResetGame();
            }
        }
    }

    public float initial = 0;
    public float final = 1;
    public float a_amp = 0;

    public IEnumerator HeatUpGPU()
    {
        while (temp_val < 200)
        {
            temp_val++;
            temp.text = "Temp: " + "<bounce a=" + a_amp + "f=3>" + temp_val + " </bounce>";
            a_amp = Mathf.Lerp(initial, final, 0.1f);
            yield return new WaitForSeconds(0.7f);
        }
    }

    // MARK: - Pipe Path Checking
    // Check if there is a path from start to end
    public bool CheckPath(int start, int end)
    {
        HashSet<int> visited = new();
        List<int> path = new();
        bool result = DFS(start, end, visited, path);
        if (result)
        {
            ConnectedPath.Clear();
            foreach (int index in path)
            {
                ConnectedPath.Add(SpawnedPipes[index]);
            }
        }
        return result;
    }

    // Depth First Search to find a path from start to end, and add to the connectedpath
    private bool DFS(int current, int end, HashSet<int> visited, List<int> path)
    {
        if (current == end)
        {
            path.Add(current);
            return true;
        }
        visited.Add(current);
        int[] directions = new int[] { -11, 11, -1, 1 }; // Up, Down, Left, Right
        foreach (var direction in directions)
        {
            int neighbor = current + direction;
            if (neighbor >= 0 && neighbor < SpawnedPipes.Count && !visited.Contains(neighbor))
            {
                if (PipesConnected(current, neighbor) && DFS(neighbor, end, visited, path))
                {
                    path.Add(current);
                    return true;
                }
            }
        }
        return false;
    }

    // Check if two pipes are connected
    private bool PipesConnected(int current, int neighbor)
    {
        // Check if the current or neighbor pipe is null
        if (SpawnedPipes[current] == null || SpawnedPipes[neighbor] == null)
        {
            return false;
        }

        var currentPipe = SpawnedPipes[current].GetComponent<pipe>();
        var neighborPipe = SpawnedPipes[neighbor].GetComponent<pipe>();

        // Ensure the components are not null
        if (currentPipe == null || neighborPipe == null)
        {
            return false;
        }

        int direction = neighbor - current;
        return direction switch
        {
            -11 => currentPipe.top_connectables.Contains(neighborPipe.PipeStyle)
                && neighborPipe.bottom_connectables.Contains(currentPipe.PipeStyle),
            11 => currentPipe.bottom_connectables.Contains(neighborPipe.PipeStyle)
                && neighborPipe.top_connectables.Contains(currentPipe.PipeStyle),
            -1 => currentPipe.left_connectables.Contains(neighborPipe.PipeStyle)
                && neighborPipe.right_connectables.Contains(currentPipe.PipeStyle),
            1 => currentPipe.right_connectables.Contains(neighborPipe.PipeStyle)
                && neighborPipe.left_connectables.Contains(currentPipe.PipeStyle),
            _ => false,
        };
    }

    public void GameOver()
    {
        GameOverEvent?.Invoke();
        if (GameUpdateCR != null)
            StopCoroutine(GameUpdateCR);
        if (HeatUpCR != null)
            StopCoroutine(HeatUpCR);
        gameRunning = false;
        Debug.Log("Game Over");
        foreach (var pipe in ConnectedPath)
        {
            pipe.GetComponent<ParticleSystem>().Play();
            pipe.GetComponent<RawImage>().color = Color.blue;
        }
        StartCoroutine(DestroyAfterSeconds(5));
    }

    public IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<BasicWindow>().ForceCloseWindow();
        foreach (var pipe in SpawnedPipes)
        {
            if (pipe != null)
            {
                pipe.GetComponent<ParticleSystem>().Stop();
                Destroy(pipe);
            }
        }
        SpawnedPipes.Clear(); // Clear the list to remove null references
    }

    public void ResetGame()
    {
        StopCoroutine(GameUpdateCR);
        foreach (var pipe in SpawnedPipes)
        {
            if (pipe != null)
            {
                pipe.GetComponent<ParticleSystem>().Stop();
                Destroy(pipe);
            }
        }
        SpawnedPipes.Clear(); // Clear the list to remove null references  
        ConnectedPath.Clear(); // Clear the connected path  
        initial = 0;
        final = 0;
        a_amp = 0;
        temp_val = 30; // Reset temperature value  
        temp.text = "Temp: " + "<bounce a=1 f=3>" + temp_val + " </bounce>";
        gameRunning = false;
        firstTry = false;
        window.ForceCloseWindow(); // Close the window  
        StartGame(); // Restart the game  
    }
}
