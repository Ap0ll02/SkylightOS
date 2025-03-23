//Author Quinn Contaldi The Boss Manager
//1. All information that needs to persist between stages should be stored in the concrete implimentation of the Boss manager.
//2. You should invoke the NextStage() function at the end of each concrete BossEndStage() to iterate to the next stage.
//3. Make sure to use the BossStartStage() to instantiate any prefabs you will need for the boss
//4. Make sure to deactivate and clean up the stage in BossEndStage() BEFORE calling NextStage()
using UnityEngine;
public abstract class BossManager : MonoBehaviour
{
    // This is the current index for our game
    public int currentBossStageIndex = 0;
    // This is the array of all the boss stages
    public GameObject[] bossStagePrefabs;
    public bool bossIsDonee = false;

    // This will start the next stage of the boss fight
     public void NextStage()
     {
         if (currentBossStageIndex >= bossStagePrefabs.Length)
         {
             Debug.Log("Boss is done");
             bossIsDonee = true;
             return;
         }
         else if (bossStagePrefabs[currentBossStageIndex] == null || bossStagePrefabs.Length == 0)
         {
             Debug.Log("We are ethier empty or null");
             return;
         }
         else if(currentBossStageIndex < bossStagePrefabs.Length)
         {
             bossStagePrefabs[currentBossStageIndex].SetActive(true);
             var BossStage = bossStagePrefabs[currentBossStageIndex].GetComponent<AbstractBossStage>();
             // This invokes the start stage of the next Boss stage
             BossStage.BossStartStage();
             // We need to hit the next stage so we increment our index
             currentBossStageIndex++;
         }
         else
         {
             Debug.Log("something went wrong");
         }
     }


}
