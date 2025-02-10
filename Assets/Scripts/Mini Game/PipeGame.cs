using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;
// TODO 87 is ending pipe. 0 is first
public class PipeGame : AbstractMinigame
{
    public List<GameObject> PipePrefab;
    public GameObject lastPipe;
    public List<GameObject> SpawnedPipes;
    public GameObject PipeLayout;

    public List<GameObject> ConnectedPath;

    public void Start(){
        StartGame();
    }

   public override void StartGame()
    {
        System.Random rnd = new();
        for(int i = 0; i < 99; i++){
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
        } 
        lastPipe = SpawnedPipes[0];
        ConnectedPath.Add(SpawnedPipes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        int above = SpawnedPipes.IndexOf(lastPipe);
        int below = above;
        int left = above;
        int right = above;

        // Calculate all adjacent index numbers
        if(above < 11) {
            above = 666;
        }
        else {
            above -= 11;
        }

        if(below > 87) {
            below = 666;
        }
        else {
            below += 11;
        }

        if(left % 11 == 0) {
            left = 666;
        }
        else {
            left -= 1;
        }

        if(right+1 % 11 == 0) {
            right = 666;
        }
        else {
            right += 1;
        }
        // MISTAKING BOTTOM LEFT FOR TOP LEFT, MISTAKING AND VICE VERSA
        // Test if sides have any adjacent connections
        if(above != 666 && 
        lastPipe.GetComponent<pipe>().top_connectables
        .Contains(SpawnedPipes[above].GetComponent<pipe>().PipeStyle)) {
            Debug.Log("My " + SpawnedPipes[above].GetComponent<pipe>().PipeStyle + "connected to my " + lastPipe.GetComponent<pipe>().PipeStyle);
        }

        else if(below != 666 && 
        lastPipe.GetComponent<pipe>().bottom_connectables
        .Contains(SpawnedPipes[below].GetComponent<pipe>().PipeStyle)) {
            Debug.Log("My " + SpawnedPipes[below].GetComponent<pipe>().PipeStyle + "connected to my " + lastPipe.GetComponent<pipe>().PipeStyle);
        }

        else if(left != 666 && 
        lastPipe.GetComponent<pipe>().left_connectables
        .Contains(SpawnedPipes[left].GetComponent<pipe>().PipeStyle)) {
            Debug.Log("My " + SpawnedPipes[left].GetComponent<pipe>().PipeStyle + "connected to my " + lastPipe.GetComponent<pipe>().PipeStyle);
        }

        else if(right != 666 && 
        lastPipe.GetComponent<pipe>().right_connectables
        .Contains(SpawnedPipes[right].GetComponent<pipe>().PipeStyle)) {
            Debug.Log("My " + SpawnedPipes[right].GetComponent<pipe>().PipeStyle + "connected to my " + lastPipe.GetComponent<pipe>().PipeStyle);
        }
    }
}
