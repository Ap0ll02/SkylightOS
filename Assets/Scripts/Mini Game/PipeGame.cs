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
    public List<GameObject> SpawnedPipes;
    public GameObject PipeLayout;

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

                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.FourWay);
                   break;
                case 1: // Horizontal Pipe
                    SpawnedPipes.Add(Instantiate(PipePrefab[0], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<RectTransform>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90)); 
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.StraightLaying;
                    
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.FourWay);
                    break;
                case 2: // Top right pipe
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopRight;

                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.FourWay);
                    break;
                case 3:
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.TopLeft;

                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.FourWay);
                    break;
                case 4:
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomLeft;

                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.FourWay);
                    break;
                case 5:
                    SpawnedPipes.Add(Instantiate(PipePrefab[1], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.BottomRight;

                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.FourWay);
                    break;
                case 6:
                    SpawnedPipes.Add(Instantiate(PipePrefab[2], parent: PipeLayout.GetComponent<RectTransform>()));
                    SpawnedPipes[i].GetComponent<pipe>().PipeStyle = pipe.PipeType.FourWay;

                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.TopLeft);
                    SpawnedPipes[i].GetComponent<pipe>().left_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().top_connectables.Add(pipe.PipeType.FourWay);

                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.TopRight);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomLeft);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.StraightLaying);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.StraightUp);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.BottomRight);
                    SpawnedPipes[i].GetComponent<pipe>().right_connectables.Add(pipe.PipeType.FourWay);
                    SpawnedPipes[i].GetComponent<pipe>().bottom_connectables.Add(pipe.PipeType.FourWay);
                    break;
                default:
                    Debug.Log("Accidental 7+");
                    break;
            }
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
