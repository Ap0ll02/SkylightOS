using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;


public class GooseMove : MonoBehaviour
{

    public float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane; //this is so the mouse position is not outside of the camera folcrium
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = Vector3.MoveTowards(transform.position, worldPosition + new Vector3(0.6f,0,0),speed * Time.deltaTime);
    }
}
