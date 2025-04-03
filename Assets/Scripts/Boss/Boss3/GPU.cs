using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPU : MonoBehaviour
{
    public GameObject MotherRam;
    private List<GameObject> RamSticks = new List<GameObject>();
    public int currentRamStick;

    public void Start()
    {
        currentRamStick = GrabRam();
        Debug.Log("Ram Sticks: " + currentRamStick);

    }
    private void OnTriggerEnter(Collider collider )
    {
        GameObject thingHittingOurGPU = collider .gameObject;

        if (thingHittingOurGPU.CompareTag("tdEnemy"))
        {
            DamageCalculation(thingHittingOurGPU);
        }

    }



    private int GrabRam()
    {

        Debug.Assert(MotherRam != null, "MotherRam is null assign the gameobject containing all the ram sticks in the inspector");
        for(int i = 0; i < MotherRam.transform.childCount; i++)
        {
            RamSticks.Add(MotherRam.transform.GetChild(i).gameObject);
        }
        return RamSticks.Count;
    }

    public void DamageCalculation(GameObject thingHittingOurGPU)
    {
        Debug.Log("Some Fucker Hit Our GPU");
        var enemy = thingHittingOurGPU.GetComponent<AbstractEnemy>();
        if (currentRamStick > 0)
        {
            currentRamStick -= enemy.damage;
            Debug.Log("Ram Stick left: " + currentRamStick);
        }
        else
        {
            Debug.Log("No more ram");
        }
    }
}
