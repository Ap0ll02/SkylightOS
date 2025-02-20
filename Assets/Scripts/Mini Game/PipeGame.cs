using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Jack Ratermann
// Pipe game for the GPU
// Depends on: BasicWindow.cs
public class PipeGame : AbstractMinigame
{
    public List<GameObject> PipePrefab;
    public GameObject lastPipe;
    public List<GameObject> SpawnedPipes;
    public GameObject PipeLayout;
    public bool gameRunning = false;

    public List<GameObject> ConnectedPath;
    public event Action GameOverEvent;

    public void Start(){
        GetComponent<BasicWindow>().CloseWindow();
    }

   public override void StartGame() { 
    GetComponent<BasicWindow>().OpenWindow();
    System.Random rnd = new();
    // Initialize the first and ending pipe
    int pipezero = rnd.Next(0, 3);
    switch(pipezero){
        case 0:
            SpawnedPipes.Add(Instantiate(PipePrefab[0], parent: PipeLayout.GetComponent<RectTransform>()));
            SpawnedPipes[0].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 90)); 
            SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
            break;
        case 1:
            SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
            SpawnedPipes[0].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
            break;
        case 2:
            SpawnedPipes.Add(Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>()));
            SpawnedPipes[0].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;
            break;
        default:
            Debug.Log("Accidental 3+");
            break;
    }

    for(int i = 1; i < 99; i++){
        // Take care of ending connection pipe
        if(i == 87) {
            int pipelast = rnd.Next(0, 3);
            switch(pipelast){
                case 0:
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[87].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 270)); 
                    SpawnedPipes[87].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
                    break;
                case 1:
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[87].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
                    break;
                case 2:
                    SpawnedPipes.Add(Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>()));
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
                SpawnedPipes.Add(Instantiate(PipePrefab[pipeType], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightUp;
                break;
            case 1: // Horizontal Pipe
                SpawnedPipes.Add(Instantiate(PipePrefab[0], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 90)); 
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
                break;
            case 2: // Top right pipe
                SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 180));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopRight;
                break;
            // Top Left
            case 3:
                SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 270));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopLeft;
                break;
            // Bottom Left
            case 4:
                SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomLeft;
                break;
            // Bottom Right
            case 5:
                SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<RectTransform>().rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomRight;
                break;
            case 6:
                SpawnedPipes.Add(Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>()));
                SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;
                break;
            default:
                Debug.Log("Accidental 7+");
                break;
        }
        foreach(var pipe in SpawnedPipes){
            pipe.GetComponent<RawImage>().color = Color.white;
        }
    } 
    lastPipe = SpawnedPipes[0];
    ConnectedPath.Add(SpawnedPipes[0]);
    gameRunning = true;
 }

    // Update is called once per frame
    void Update()
    {
        if(!gameRunning) return;
        if(CheckPath(0, 87)){
            GameOver();
        }
    }

    // Check if there is a path from start to end
    public bool CheckPath(int start, int end) {
        HashSet<int> visited = new();
        List<int> path = new();
        bool result = DFS(start, end, visited, path);
        if(result){
            ConnectedPath.Clear();
            foreach(int index in path){
                ConnectedPath.Add(SpawnedPipes[index]);
            }
        }
        return result;
    }

    // Depth First Search to find a path from start to end, and add to the connectedpath
    private bool DFS(int current, int end, HashSet<int> visited, List<int> path){
        if(current == end){
            path.Add(current);
            return true;
        }
        visited.Add(current);
        int[] directions = new int[]{-11, 11, -1, 1}; // Up, Down, Left, Right
        foreach(var direction in directions){
            int neighbor = current + direction;
            if(neighbor >= 0 && neighbor < SpawnedPipes.Count && !visited.Contains(neighbor)){
                if(PipesConnected(current, neighbor) && DFS(neighbor, end, visited, path)){
                    path.Add(current);
                    return true;
                }
            }
        }
        return false;
    }

    // Check if two pipes are connected
    private bool PipesConnected(int current, int neighbor){
        var currentPipe = SpawnedPipes[current].GetComponent<pipe>();
        var neighborPipe = SpawnedPipes[neighbor].GetComponent<pipe>();

        int direction = neighbor - current;
        return direction switch
        {
            -11 => currentPipe.top_connectables.Contains(neighborPipe.PipeStyle) &&
                    neighborPipe.bottom_connectables.Contains(currentPipe.PipeStyle),
            11 => currentPipe.bottom_connectables.Contains(neighborPipe.PipeStyle) &&
                    neighborPipe.top_connectables.Contains(currentPipe.PipeStyle),
            -1 => currentPipe.left_connectables.Contains(neighborPipe.PipeStyle) &&
                    neighborPipe.right_connectables.Contains(currentPipe.PipeStyle),
            1 => currentPipe.right_connectables.Contains(neighborPipe.PipeStyle) &&
                    neighborPipe.left_connectables.Contains(currentPipe.PipeStyle),
            _ => false,
        };
    }

    public void GameOver() {
        GameOverEvent?.Invoke();
        gameRunning = false;
        Debug.Log("Game Over");
        foreach(var pipe in ConnectedPath){
            pipe.GetComponent<ParticleSystem>().Play();
            pipe.GetComponent<RawImage>().color = Color.blue;
        }
        StartCoroutine(DestroyAfterSeconds(5));
    }

    public IEnumerator DestroyAfterSeconds(float seconds){
        yield return new WaitForSeconds(seconds);
        gameObject.GetComponent<BasicWindow>().CloseWindow();
        foreach(var pipe in SpawnedPipes){
            pipe.GetComponent<ParticleSystem>().Stop();
            Destroy(pipe);
        }
    }
}
