using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popups_Factory : Factory
{
    Hazard hazard;
    public GameObject Prefab;
    public void CreatePopup()
    {
        hazard = new Popup1();
        GameObject popup = Instantiate(Prefab);
        popup.transform.position = new Vector3(Random.Range(-8, 8), Random.Range(-4, 4), 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
