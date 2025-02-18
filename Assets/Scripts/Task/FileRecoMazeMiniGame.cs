using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileRecoMazeMiniGame : AbstractMinigame
{
    public int mazeWidth = 10;
    public int mazeHeight = 10;
    public GameObject wallPrefab;
    public GameObject pathPrefab;
    public GameObject parentMaze;
    public List<GameObject> mazePieces;
    public GameObject filePrefab;
    public List<GameObject> filePiece; 

    void Awake()
    {
        StartGame();
    }

    public override void StartGame()
    {
        CreateMaze();
    }


    // Github COPilot helped with automatic maze generation and camera setup
    // 3d Maze
    public int evIndex = 0;
    void CreateMaze() {
        System.Random rand = new();
        int PathOrWall = rand.Next(0,2);

        for (int i = 0; i < 840; i++){
            if(evIndex == 0 || (i - mazePieces.IndexOf(filePiece[evIndex]) > rand.Next(90, 150) && evIndex < 5)){
                    filePiece.Add(Instantiate(filePrefab, parent: parentMaze.GetComponent<RectTransform>()));
                    mazePieces.Add(filePiece[evIndex]);
                    evIndex++;
                    continue;
            }

            switch(PathOrWall){
                case 0:
                    mazePieces.Add(Instantiate(wallPrefab, parent: parentMaze.GetComponent<RectTransform>()));
                    break;
                case 1:
                    mazePieces.Add(Instantiate(pathPrefab, parent: parentMaze.GetComponent<RectTransform>()));
                    break;
                default:
                    break;
            }

            PathOrWall = rand.Next(0, 2);
        }
    }
}
