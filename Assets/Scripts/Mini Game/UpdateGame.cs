using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateGame : MonoBehaviour
{
    public GameObject[] mail;
    public BasicWindow window;
    public Rect windowRect;
    public GameObject character;
    public Camera mainCamera; // Reference to the camera in your scene
    public Rect windowBounds = new Rect(-2166, -1500, 4332, 3000);
    public Transform spawnPoint; // The location where the mail will spawn
    public float spawnInterval = 0.5f; // Time interval between spawns (in
    //Vector3 characterPosition;

    void Awake()
    {
        window = GetComponent<BasicWindow>();
    }
    // Start is called before the first frame update
    void Start()
    {
        //characterPosition = character.transform.position;
        window.isClosable = false;
        window.CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
        StartCoroutine(SpawnMail());
    }

    void MoveCharacter()
    {
            Vector3 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Since the mouseWorldPosition includes depth (Z axis), we need to adjust it for 2D
            mouseWorldPosition.z = transform.position.z; // Keep character's z position unchanged
            //Vector3 clampedPosition = new Vector3
            character.transform.position = mouseWorldPosition;

    }

    void SpawnPackets()
    {

    }

    void CheckTask()
    {

    }

    private IEnumerator SpawnMail()
    {
        while (true) // Infinite loop to keep spawning mail
        {
            int index = Random.Range(0, mail.Length);
            // Loop through the mail array
            // Instantiate the mail object at the spawn point's position and rotation
            Instantiate(mail[index], spawnPoint.position, spawnPoint.rotation);

            // Wait for the specified interval before spawning the next object
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
