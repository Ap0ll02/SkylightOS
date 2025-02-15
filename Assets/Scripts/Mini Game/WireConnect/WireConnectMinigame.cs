using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class WireConnectMinigame : AbstractMinigame
{
    public List<MinigameWire> wires;
    public List<MinigameSlot> slots;
    public GameObject wirePrefab;
    public GameObject slotPrefab;
    public Transform wireLayoutGroup;
    public Transform slotLayoutGroup;
    public MinigameWire.WireColors wireColor;
    public MinigameSlot.SlotColors slotColor;
    public RectTransform dragAreaRectTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartGame();
    }

    public override void StartGame()
    {
        isStarted = true;
        // Initialize wires and slots
        SpawnWiresAndSlots();
        StartCoroutine(CheckWinCoroutine());
    }

    public IEnumerator CheckWinCoroutine()
    {
        while (isStarted)
        {
            yield return new WaitForSeconds(0.5f);
            // Check for win condition
            if (CheckWinCondition())
            {
                Debug.Log("You win!");
                // Handle win condition (e.g., end the game, show a message, etc.)
                break;
            }
        }
    }

    private void SpawnWiresAndSlots()
    {
        // Shuffle the colors
        var wireColors = new List<MinigameWire.WireColors> { MinigameWire.WireColors.Red, MinigameWire.WireColors.Green, MinigameWire.WireColors.Blue, MinigameWire.WireColors.Yellow };
        var slotColors = new List<MinigameSlot.SlotColors> { MinigameSlot.SlotColors.Red, MinigameSlot.SlotColors.Green, MinigameSlot.SlotColors.Blue, MinigameSlot.SlotColors.Yellow };
        wireColors = wireColors.OrderBy(a => System.Guid.NewGuid()).ToList();
        slotColors = slotColors.OrderBy(a => System.Guid.NewGuid()).ToList();

        for (int i = 0; i < slots.Count; i++)
        {
            var slotInstance = Instantiate(slotPrefab, slotLayoutGroup);
            slotInstance.GetComponent<MinigameSlot>().slotColor = slotColors[i];
            slots[i] = slotInstance.GetComponent<MinigameSlot>();
        }

        for (int i = 0; i < wires.Count; i++)
        {
            var wireInstance = Instantiate(wirePrefab, wireLayoutGroup);
            wireInstance.GetComponent<MinigameWire>().wireColor = wireColors[i];
            wireInstance.GetComponent<MinigameWire>().dragAreaRectTransform = dragAreaRectTransform;
            wires[i] = wireInstance.GetComponent<MinigameWire>();
        }
    }

    private bool CheckWinCondition()
    {
        foreach (var slot in slots)
        {
            if (!slot.isConnected)
                return false;
        }
        return true;
    }
}
