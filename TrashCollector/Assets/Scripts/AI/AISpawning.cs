using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISpawning : MonoBehaviour
{
    public int maxEnemies;
    public GameObject enemyVeh;
    public int difficulty;
    public Sprite improvedSprite;
    private int uniqueID;

    public HealthBarBehaviour healthBarBehaviour;

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
        spawnedEnemy.tag = "enemy";
        spawnedEnemy.GetComponent<AIBehavior>().uniqueID = uniqueID;
        spawnedEnemy.GetComponent<AIBehavior>().healthBar = healthBarBehaviour;
        spawnedEnemy.GetComponent<SpriteRenderer>().sprite = improvedSprite;

        //CANNOT CHANGE TEXT HERE, CAUSES MULTI-SPAWN
        uniqueID++;
        System.Random rnd = new System.Random();
        int x = 0;
        int y = 0;
        for (int i = 0; i < uniqueID; i++)
        {
            x = rnd.Next(-9, 3);
            y = rnd.Next(-7, 3);
        }
        spawnedEnemy.transform.position = new Vector2(x, y);
        if (difficulty < 3)
        {
            enemyLevels.Add(0);
        }
    }
}
