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
    public float randomValue;
    private bool isHazardActive = true; // Flag to control state updates
    [SerializeField] private int maxKittens = 5;

    // Stars the hazard
    public override void StartHazard()
    {
        switch (difficulty)
        {
            case (OSManager.Difficulty.Easy):
                maxKittens = 1;
                break;
            case (OSManager.Difficulty.Medium):
                maxKittens = 3;
                break;
            case (OSManager.Difficulty.Hard):
                maxKittens = 5;
                break;
            default:
                maxKittens = 3;
                break;
        }

        isHazardActive = true; // Activate hazard
        SpawnNyanKitten(GetRandomState());
        StartCoroutine(UpdateNyanKittens());
    }

    // Stops the hazard
    public override void StopHazard()
    {
        isHazardActive = false; // Deactivate hazard
        StopAllCoroutines();
        SetAllNyanKittenState(NyanKitten.NyanKittenState.Flee);
    }

    // Spawns a nyan kitten
    public void SpawnNyanKitten(NyanKitten.NyanKittenState startState)
    {
        Vector3 spawnPosition = new Vector3(
            Random.Range(-20f, -10f) * (Random.value > 0.5f ? 1 : -1),
            Random.Range(-12f, -6f) * (Random.value > 0.5f ? 1 : -1),
            0
        );
        GameObject nyanKittenObject = Instantiate(nyanKittenPrefab, spawnPosition, Quaternion.identity);
        NyanKitten nyanKitten = nyanKittenObject.GetComponent<NyanKitten>();
        nyanKitten.currentState = startState;
        nyanKittens.Add(nyanKitten);
    }

    // Sets the states of all current nyan kittens
    public void SetAllNyanKittenState(NyanKitten.NyanKittenState state)
    {
        foreach (NyanKitten nyanKitten in nyanKittens)
        {
            nyanKitten.currentState = state;
        }
    }

    // Updates the nyan kittens, and can possibly spawn a new kitten
    IEnumerator UpdateNyanKittens()
    {
        while (isHazardActive)
        {
            UpdateNyanKittenStates();
            if(nyanKittens.Count < maxKittens && Random.Range(0,5) == 1)
            {
                SpawnNyanKitten(GetRandomState());
            }
            yield return new WaitForSeconds(3f);
        }
    }

    // Randomly changes the state of a nyan kitten
    private void UpdateNyanKittenStates()
    {
        NyanKitten currentKitten = nyanKittens[Random.Range(0, nyanKittens.Count)];
        randomValue = Random.Range(0,10);
        if (currentKitten.currentState == NyanKitten.NyanKittenState.Flee)
            return;
        if(randomValue > 8)
        {
            currentKitten.currentState = NyanKitten.NyanKittenState.Flee;
            return;
        }
        if (randomValue > 4 && randomValue <= 8)
        {
            currentKitten.currentState = NyanKitten.NyanKittenState.Attack;
        }
        else
        {
            currentKitten.currentState = NyanKitten.NyanKittenState.Roam;
        }
    }

    // Might update later idk
    public override bool CanProgress()
    {
        return true;
    }

    // Returns a random state
    public NyanKitten.NyanKittenState GetRandomState()
    {
        return (NyanKitten.NyanKittenState)Random.Range(0, 3);
    }
}

