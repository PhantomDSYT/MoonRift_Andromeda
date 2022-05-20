using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{
    private float areaHeight;
    private float areaWidth;
    private float areaMinHeight;
    private float areaMinWidth;

    public int obstaclesOnBoard;
    public int maxObstacles;
    public List<Vector2> obsLocations;
    public GameObject[] prefabObs;
    // Start is called before the first frame update
    void Start()
    {
        areaHeight = 21;
        areaWidth = 10;
        areaMinHeight = -9;
        areaMinWidth = -29;
    }

    // Update is called once per frame
    void Update()
    {
        if(obstaclesOnBoard<maxObstacles)
        {
            GenerateTerrain();
        }
    }

    void GenerateTerrain()
    {
        System.Random rnd = new System.Random();

        GameObject spawnedObs = Instantiate(prefabObs[rnd.Next(0, prefabObs.Length)]);
        spawnedObs.tag = "hazard";
        spawnedObs.transform.position = new Vector2(Random.Range(areaMinWidth, areaWidth), Random.Range(areaMinHeight, areaHeight));

        bool tooClose = true;
        while (tooClose)
        {
            if(obsLocations.Contains(spawnedObs.transform.position))
            {
                tooClose = true;
            }
            else { tooClose = false; }

            int proxAlert = 0;
            foreach(Vector2 v2 in obsLocations)
            {
                float distance = Vector2.Distance(spawnedObs.transform.position, v2);
                if(distance<=4)
                {
                    proxAlert++;
                }
            }

            if(proxAlert>0)
            {
                tooClose = true;
            }
            else
            {
                tooClose = false;
            }

            spawnedObs.transform.position = new Vector2(Random.Range(areaMinWidth, areaWidth), Random.Range(areaMinHeight, areaHeight));
        }
        obsLocations.Add(spawnedObs.transform.position);
        obstaclesOnBoard++;
    }
}
