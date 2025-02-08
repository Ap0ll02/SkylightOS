using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PipeGame : MonoBehaviour
{
    public List<GameObject> PipePrefab;
    public List<GameObject> SpawnedPipes;
    public GameObject PipeLayout;
    // Start is called before the first frame update
    void Start()
    {
        System.Random rnd = new();
        for(int i = 0; i < 99; i++){
            SpawnedPipes.Add(Instantiate(PipePrefab[rnd.Next(0, PipePrefab.Count)], parent: PipeLayout.GetComponent<RectTransform>()));
        } 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
