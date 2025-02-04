using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGame : MonoBehaviour
{
    public GameObject[] mail;
    public BasicWindow window;
    public Camera mainCamera; // Reference to the camera in your scene
    public Rect windowBounds = new Rect(-2166, -3000, 4332, 6000);
    public RectTransform playerRectTransform;
    public UpdateGameScoreManager scoreManager;
    public UpdatePanel updatePanel;
    public int winScore = 1000;
    public int loseScore = -100;
    public bool isGameOver;
    public float spawnInterval = 1.5f; // Time interval between spawns (in


    void Awake()
    {
        window = GetComponent<BasicWindow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        scoreManager = FindObjectOfType<UpdateGameScoreManager>();
        //characterPosition = character.transform.position;
        window.isClosable = false;
        window.CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        scoreCheck();
    }

    public void SpawnPackets()
    {
        isGameOver = false;
        StartCoroutine(SpawnMail());
    }

    public void MoveCharacter()
    {

        // Convert mouse position to anchored position
        // Anchored Position is the position in reference to the parent object so, the local cordinate system
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 targetAnchoredPosition = (Vector2)playerRectTransform.parent.InverseTransformPoint(mouseWorldPosition);

        // Clamping X  positions based on parent window bounds
        targetAnchoredPosition.x = Mathf.Clamp(targetAnchoredPosition.x, windowBounds.xMin, windowBounds.xMax);
        // We want to ensure the Y position does not move
        targetAnchoredPosition.y = -1881;
        // Apply the constrained anchored position to the player. We want to move our character so....
        playerRectTransform.anchoredPosition = targetAnchoredPosition;

    }



    private IEnumerator SpawnMail()
    {
        while (isGameOver == false) // Infinite loop to keep spawning mail
        {
            GameObject newMail = Instantiate(mail[Random.Range(0, mail.Length)], playerRectTransform.parent);
            RectTransform rectTransform = newMail.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(Random.Range(windowBounds.xMin, windowBounds.xMax), 1881 );
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void scoreCheck()
    {
        if (scoreManager.score >= winScore)
        {
            isGameOver = true;
            updatePanel.ChangeState(UpdatePanel.UpdateState.Working);
        }
        else if (scoreManager.score <= loseScore)
        {
            isGameOver = true;
            updatePanel.ChangeState(UpdatePanel.UpdateState.NotWorkingInteractable);
        }
    }
}
