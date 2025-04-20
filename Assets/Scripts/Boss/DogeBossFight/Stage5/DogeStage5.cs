using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogeStage5 : AbstractBossStage
{
    public GameObject Doge;
    public BasicWindow window;
    public Camera camera;
    public AlpinePlayer player;
    public override void BossStartStage()
    {
        Doge.SetActive(true);
        var doge = Doge.GetComponent<Doge>();
        CameraShake();
        //doge.StartDoge();
        //window.ForceCloseWindow();
        player.moveSpeed = 0;
        player.jumpSpeed = 0;
    }

    public override void BossEndStage()
    {
        Debug.Log("Stage 5 End");
        bossManager.NextStage();
    }

    public void CameraShake()
    {
        StartCoroutine(Shake(4f, 0.6f));
    }


    private IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = camera.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            camera.transform.position = new Vector3(
                originalPosition.x + x,
                originalPosition.y + y,
                originalPosition.z
            );

            elapsed += Time.deltaTime;
            yield return null;
        }

        camera.transform.position = originalPosition;
    }

}
