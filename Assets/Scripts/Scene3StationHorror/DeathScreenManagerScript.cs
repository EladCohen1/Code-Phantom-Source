using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreenManagerScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject enemy;
    [SerializeField] Canvas deathScreen;
    [SerializeField] GameObject endBackgroundMusic;
    public void tryAgain()
    {
        Destroy(endBackgroundMusic);
        SceneManager.LoadScene(4);
        //tryAgainFromPositions();
    }

    private void tryAgainFromPositions()
    {
        returnLocations();
        StartCoroutine(returnVariables());
        returnAnimations();
        Cursor.lockState = CursorLockMode.Locked;
        deathScreen.enabled = false;
    }

    private void returnLocations()
    {
        player.transform.position = new Vector3(3.82f, 0, -4.20f);
        enemy.transform.position = new Vector3(-0.531f, 0, -9.962f);
    }

    private IEnumerator returnVariables()
    {
        yield return new WaitForFixedUpdate();
        player.GetComponent<PlayerScript>().isControlled = true;
        enemy.GetComponent<ZlorpNavMeshScript>().isPatroling = true;
        enemy.GetComponent<ZlorpFOVScript>().isLooking = true;
        enemy.GetComponent<ZlorpFOVScript>().ZlorpScreamAudio.enabled = false;
    }

    private void returnAnimations()
    {
        enemy.GetComponent<Animator>().SetBool("IsScreaming", false);
        player.GetComponent<Animator>().SetBool("IsFalling", false);
    }
}
