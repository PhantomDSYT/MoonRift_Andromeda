using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AISpawning : MonoBehaviour
{
    public int maxEnemies;
    public GameObject enemyVeh;
    public int difficulty;
    private int uniqueID;

    public int enemiesSpawned;
    private List<int> enemyLevels;

    private AIBehavior ai;
    // Start is called before the first frame update
    void Start()
    {
        difficulty = 0;
        uniqueID = 1;
        enemyLevels = new List<int>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesSpawned < maxEnemies)
        {
            SpawnEnemy();
            enemiesSpawned++;
        }
    }

    public void SpawnEnemy()
    {
        GameObject spawnedEnemy = Instantiate(enemyVeh);
        spawnedEnemy.GetComponent<AIBehavior>().nameTags = spawnedEnemy.GetComponentInChildren<TextMeshPro>();
        spawnedEnemy.tag = "boat";
        spawnedEnemy.GetComponent<AIBehavior>().uniqueID = uniqueID;

        //CANNOT CHANGE TEXT HERE, CAUSES MULTI-SPAWN
        uniqueID++;
        System.Random rnd = new System.Random();
        int x = 0;
        int y = 0;
        for (int i=0; i<uniqueID; i++)
        {
            x = rnd.Next(-20, 20);
            y = rnd.Next(-20, 20);
        }
        spawnedEnemy.transform.position = new Vector2(x, y);
        if (difficulty < 3)
        {
            enemyLevels.Add(0);
        }
    }
}
