using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;

public class GPU : MonoBehaviour
{
    // Reference to the parent game object that holds all the children ram sticks child 0 is the stick child 1 is the base
    public GameObject MotherRam;
    // Stores all the ram sticks we will be associating with our score
    private List<GameObject> RamSticks = new List<GameObject>();
    // This is the index the to ram array 1-24 of course we start at zero since it's an array. 0-23. so 0 is the first stick of ram that we are going to deactivate
    public int currentRamStick;
    // Ignore the ram count this is for debugging and what not
    public int RamStickCount;

    public void Start()
    {
        RamStickCount = GrabRam();
        // Debug.Log("Ram Sticks: " + RamStickCount);
        Debug.Assert(RamStickCount > 0, "No ram sticks in the gameobject");
        //StartCoroutine(RamStickDamage());
    }
    // We are sending the object that collided with our GPU, Hopefully a enemy...
    private void OnTriggerEnter(Collider collider )
    {
        // Debug.Log("GPU Hit");
        GameObject thingHittingOurGPU = collider.gameObject;
        // Nice! If it's an enemy we should call our DamageCalculation function
        if (thingHittingOurGPU.CompareTag("tdEnemy") || thingHittingOurGPU.CompareTag("StealthEnemyTD"))
        {
            Debug.Log("We Confirmed Its A Bug");
            DamageCalculation(thingHittingOurGPU);
        }

    }

    // public IEnumerator RamStickDamage()
    // {
    //     while (currentRamStick < RamSticks.Count)
    //     {
    //         var ramParent = RamSticks[currentRamStick];
    //         Debug.Log("ramParent: " + ramParent);
    //         Debug.Assert(ramParent != null,
    //             "ramParent is null assign the game object containing all the ram sticks in the inspector");
    //         var ram = ramParent.transform.GetChild(0).gameObject;
    //         Debug.Log("ram: " + ram);
    //         Debug.Assert(ram != null,
    //             "ram is null assign the game object containing all the ram sticks in the inspector");
    //         ram.SetActive(false);
    //         currentRamStick++;
    //         yield return new WaitForSeconds(1f);
    //     }
    // }

    // Grab all the ram sticks in the mother ram game object.
    private int GrabRam()
    {

        Debug.Assert(MotherRam != null, "MotherRam is null assign the gameobject containing all the ram sticks in the inspector");
        for(int i = 0; i < MotherRam.transform.childCount; i++)
        {
            RamSticks.Add(MotherRam.transform.GetChild(i).gameObject);
        }
        return RamSticks.Count;
    }

    // This function will take the object that collided with our GPU and calculate the damage it will do to our GPU
    public void DamageCalculation(GameObject thingHittingOurGPU)
    {
        // We cant directly apply the damage... our Ram sticks is technically our health. We also have to deactivate all the ram sticks... So we need a loop for this
        int damage = thingHittingOurGPU.GetComponent<AbstractEnemy>().damage;
        // We will loop for as many times as damage is applied. This will deactivate the equivalent amount of ram sticks
        while (damage > 0)
        {
            // We don't want to deactivate ram sticks if they don't exist. So we need to sanity check here
            if (currentRamStick < RamSticks.Count)
            {
                var ramParent = RamSticks[currentRamStick];
                Debug.Log("ramParent: " + ramParent);
                Debug.Assert(ramParent != null,
                    "ramParent is null assign the gameobject containing all the ram sticks in the inspector");
                var ram = ramParent.transform.GetChild(0).gameObject;
                Debug.Assert(ram != null,
                    "ram is null assign the gameobject containing all the ram sticks in the inspector");
                ram.SetActive(false);
                currentRamStick++;
                damage--;
            }
            // If we are out of ram end the game... maybe break the loop for good measure who the fuck knows
            else
            {
                Debug.Log("No more ram sticks");
                break;
            }
        }
        // This will get rid of the bug that hit our GPU we dont need him sticking around
        thingHittingOurGPU.GetComponent<AbstractEnemy>().Death();
    }
}
