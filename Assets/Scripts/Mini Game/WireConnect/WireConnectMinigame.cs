using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
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

    // Reference to the window
    public BasicWindow window;

    public int numRounds = 2;
    public int currentRound;

    private void Awake()
    {
        window = GetComponent<BasicWindow>();
    }
     
    // Start is called before the first frame update
    void Start()
    {
        currentRound = 1;
        numRounds = 2;
        window.CloseWindow();
    }

    public void TryStartGame()
    {
        if (!isStarted)
        {
            StartGame();
        }
        else
        {
            Debug.Log("Game already started!");
        }
    }

    public override void StartGame()
    {
        isStarted = true;
        window.isClosable = false;
        window.OpenWindow();

        StartCoroutine(CheckWinCoroutine());
        SpawnWiresAndSlots();
  
    }

    public void FinishGame()
    {
        Debug.Log("Wire Minigame Completed! ");
        isStarted = false;
        isComplete = true;
        window.isClosable = true;
        StartCoroutine(FinishGameCoroutine());
    }

    public IEnumerator FinishGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        window.CloseWindow();
    }

    public IEnumerator CheckWinCoroutine()
    {
        while (isStarted)
        {
            yield return new WaitForSeconds(0.5f);
            // Check for win condition
            if (CheckWinCondition())
            {
                Debug.Log("Round " + currentRound + " completed!" );
                FinishMinigameRound();
                break;
            }
        }
    }

    private void FinishMinigameRound()
    {
        StopAllCoroutines();
        // Clean up the minigame
        foreach (var wire in wires)
        {
            Destroy(wire.gameObject);
        }
        foreach (var slot in slots)
        {
            Destroy(slot.gameObject);
        }

        currentRound++;

        if (currentRound <= numRounds)
        {
            // Start the next round
            SpawnWiresAndSlots();
            StartCoroutine(CheckWinCoroutine());
        }
        else
        {
            FinishGame();
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
