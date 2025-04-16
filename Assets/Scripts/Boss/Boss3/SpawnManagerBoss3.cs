using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManagerBoss3 : MonoBehaviour
{
    public List<GameObject> enemies;
    public GameObject enemyContainer;
    public float defaultSpawnRate = 0.5f;

    public IEnumerator spawnAmount(int index, int amount, float rate = 0.0f)
    {
        for (int i = 0; i < amount; i++)
        {
           var enemyReference = Instantiate(enemies[index],enemyContainer.transform );
           if (rate == 0.0f)
           {
               yield return coolDown(defaultSpawnRate);
           }
           else
           {
               yield return coolDown(rate);
           }
        }
        yield return null;
    }


    public IEnumerator SpawnRandom(int maxPoint, int minIndex = 0, int maxIndex = 0, float rate = 0.0f)
    {
        var currentPoint = maxPoint;
        while (currentPoint > 0)
        {
            var index = Random.Range(minIndex,maxIndex);
            currentPoint = (currentPoint - index);
            if (rate == 0.0f)
            {
                Debug.Log("Default Cooling down time: " + defaultSpawnRate);
                var enemyReference = Instantiate(enemies[index],enemyContainer.transform);
                yield return coolDown(defaultSpawnRate);
            }
            else if (currentPoint <= maxIndex)
            {
                currentPoint = 0;
                yield return null;
            }
            else
            {
                Debug.Log("Cooling down time: " + rate);
                var enemyReference = Instantiate(enemies[index],enemyContainer.transform);
                yield return coolDown(rate);
            }
        }
        yield return null;
    }

    IEnumerator coolDown(float time)
    {
        yield return new WaitForSeconds(time);
    }


}
