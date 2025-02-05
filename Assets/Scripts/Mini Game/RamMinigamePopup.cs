using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RamMinigamePopup : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SetAsLastChildRoutine());
        Destroy(gameObject, Random.Range(1f, 4f));
    }

    // Coroutine to set the object as the last child every second
    IEnumerator SetAsLastChildRoutine()
    {
        while (true)
        {
            transform.SetAsLastSibling();
            yield return new WaitForSeconds(1f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
