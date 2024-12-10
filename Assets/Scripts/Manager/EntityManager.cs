using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Garrett Sharp
/// Entity manager
/// manages the entities
/// Coded to be specific to the Nyan Kitten (even though it shouldnt)
/// Sue me
/// </summary>
public class EntityManager : AbstractManager
{
    [SerializeField] private GameObject nyanKittenPrefab;
    private List<NyanKitten> nyanKittens = new List<NyanKitten>();
    public float stateChangeInterval = 5f;
    public float timeSinceLastStateChange = 0f;
    public float spawnChanceAfterFlee = 0.5f; // 50% chance to spawn another Nyan Kitten
    public float randomValue;
    private bool isHazardActive = true; // Flag to control state updates

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isHazardActive)
        { 
            SetNyanKittenState(NyanKitten.NyanKittenState.Flee);// Skip updating states if hazard is not active
            return;
        }

        timeSinceLastStateChange += Time.deltaTime;
        if (timeSinceLastStateChange >= stateChangeInterval)
        {
            SetRandomNyanKittenState();
            timeSinceLastStateChange = 0f;
        }
    }

    public void SpawnNyanKitten()
    {
        Vector3 spawnPosition = new Vector3(Random.Range(-10f, 10f), Random.Range(-6f, 6f), 0);
        GameObject nyanKittenObject = Instantiate(nyanKittenPrefab, spawnPosition, Quaternion.identity);
        NyanKitten nyanKitten = nyanKittenObject.GetComponent<NyanKitten>();
        nyanKittens.Add(nyanKitten);
    }

    public void SetNyanKittenState(NyanKitten.NyanKittenState state)
    {
        foreach (NyanKitten nyanKitten in nyanKittens)
        {
            nyanKitten.currentState = state;
            if (state == NyanKitten.NyanKittenState.Flee && isHazardActive)
            {
                TrySpawnAnotherNyanKitten();
            }
        }
    }

    private void SetRandomNyanKittenState()
    {
        foreach (NyanKitten nyanKitten in nyanKittens)
        {
            randomValue = Random.value;
            if (nyanKitten.currentState == NyanKitten.NyanKittenState.Flee)
            {
                TrySpawnAnotherNyanKitten();
            }
            else if (randomValue > 0.3f)
            {
                nyanKitten.currentState = NyanKitten.NyanKittenState.Attack;
            }
            else if (randomValue > 0.2f)
            {
                nyanKitten.currentState = NyanKitten.NyanKittenState.Roam;
            }
            else
            {
                nyanKitten.currentState = NyanKitten.NyanKittenState.Flee;
            }
        }
    }

    private void TrySpawnAnotherNyanKitten()
    {
        if (Random.value < spawnChanceAfterFlee)
        {
            SpawnNyanKitten();
        }
    }

    public override void StartHazard()
    {
        isHazardActive = true; // Activate hazard
        SpawnNyanKitten();
        SetNyanKittenState(NyanKitten.NyanKittenState.Roam);
    }

    public override void StopHazard()
    {
        isHazardActive = false; // Deactivate hazard
        SetNyanKittenState(NyanKitten.NyanKittenState.Flee);
    }

    public override bool CanProgress()
    {
        return true;
    }
}

