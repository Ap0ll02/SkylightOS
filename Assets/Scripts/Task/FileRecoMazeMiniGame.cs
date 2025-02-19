using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FileRecoMazeMiniGame : AbstractMinigame
{
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

    public override void StartGame() {
        
    }

    
}
