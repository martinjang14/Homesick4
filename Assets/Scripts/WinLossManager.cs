using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinLossManager : MonoBehaviour
{
    public GameObject bossPrefab;
    public GameObject bossSpawnLocation;

    public void spawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab, bossSpawnLocation.transform);
    }

    public static void win()
    {
        SceneManager.LoadScene("FinalCutscene");
    }

    public static void lose()
    {
        SceneManager.LoadScene("Loss Screen");
    }
}
