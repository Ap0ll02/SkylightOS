using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireConnectMinigame : AbstractMinigame
{
    public List<MinigameWire> wires;
    public List<MinigameSlot> slots;
    public GameObject wirePrefab;
    public GameObject slotPrefab;
    public Transform wireLayoutGroup;
    public Transform slotLayoutGroup;
    public Color wireColor;
    public Color slotColor;
    public char wireCharColor = 'a';
    public char slotCharColor = 'a';

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for win condition
        if (CheckWinCondition())
        {
            Debug.Log("You win!");
            // Handle win condition (e.g., end the game, show a message, etc.)
        }
    }

    public override void StartGame()
    {
        isStarted = true;
        // Initialize wires and slots
        SpawnWiresAndSlots();
    }

    private void SpawnWiresAndSlots()
    {
        foreach (var wire in wires)
        {
            var wireInstance = Instantiate(wirePrefab, wireLayoutGroup);
            //wireInstance.GetComponent<Renderer>().material.color = wireColor;
            wireInstance.GetComponent<MinigameWire>().wireColor = wireCharColor;
        }

        foreach (var slot in slots)
        {
            var slotInstance = Instantiate(slotPrefab, slotLayoutGroup);
            //slotInstance.GetComponent<Renderer>().material.color = slotColor;
            slotInstance.GetComponent<MinigameSlot>().slotColor = slotCharColor;
        }
    }

    private bool CheckWinCondition()
    {
        foreach (var slot in slots)
        {
            // Check if each slot has the correct wire
            // Implement your logic here
        }
        return false;
    }
}
