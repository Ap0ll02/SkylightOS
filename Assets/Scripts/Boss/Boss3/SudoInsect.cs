using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SudoInsect : AbstractEnemy
{
    public bool isAlive = true;
    public GameObject SpawnManager;
    List<GameObject> Bugs = new List<GameObject>();
    void Start()
    {
        animator = GetComponent<Animator>();
        if(navi == null)
            navi = GameObject.Find("NavigationManager").GetComponent<NavigationManager>();
        if(SpawnManager == null)
            SpawnManager = GameObject.Find("SpawnManagerBoss3");

        Debug.Assert(animator != null, "Animator is null");
        Debug.Assert(navi != null, "Navigation Manager is null");
        Debug.Assert(SpawnManager != null, "Spawn Manager is null");

        GetNewWaypoint();

        speed = Random.Range(50f, 60f);
        maxHealth = 5;
        currentHealth = maxHealth;
        pointValue = 100;
        damage = 2;
        reward = 30;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            // Debug.Log(this.transform.GetChild(i).gameObject.name);
            Bugs.Add(this.transform.GetChild(i).gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        if(isAlive == false)
            Death();
    }

    public override void Death()
    {
        // When the sudo insect dies it has a list of all the child bugs it will spawn
        foreach(GameObject bug in Bugs)
        {
            // for each bug we should set active since they start inactive
            Instantiate(bug,
                this.transform.position,
                this.transform.rotation);
            bug.transform.SetParent(SpawnManager.transform);
            bug.SetActive(true);
            // and set the next waypoint to the current waypoint of the parent
            var bugClass = bug.GetComponent<AbstractEnemy>();
            bugClass.currentPosition = currentPosition;
        }
        // kill our sudo insect
        Destroy(gameObject);
    }

}
