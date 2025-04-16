using UnityEngine;

public class RotateTrail : MonoBehaviour
{
    public GameObject parent;
    public GameObject tower;
    public float radius;
    public float speed = 360f;
    public GameObject tr;
    private float angle = 0f;
    public Vector3 center;

    // Update is called once per frame
    void Start()
    {
        center = transform.position;
    }

    void Update()
    {
        angle += speed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(rad), 0, Mathf.Sin(rad)) * radius;
        transform.position = center + offset;
    }
}
