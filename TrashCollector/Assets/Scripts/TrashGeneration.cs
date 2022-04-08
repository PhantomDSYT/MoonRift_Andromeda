using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGeneration : MonoBehaviour
{
    private int areaHeight;
    private int areaWidth;
    private int areaMinHeight;
    private int areaMinWidth;

    public int trashOnBoard;
    public int maxTrash;
    public List<Vector2> trashLocations;
    public GameObject trash;
    // Start is called before the first frame update
    void Start()
    {
        areaHeight = 30;
        areaWidth = 30;
        areaMinHeight = -30;
        areaMinWidth = -30;
        trashOnBoard = (areaHeight+areaWidth)/3;
        maxTrash = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(trashOnBoard>0)
        {
            GenerateTrash();
        }

        checkTrashOut();
    }

    void checkTrashOut()
    {
        GameObject[] trashes = GameObject.FindGameObjectsWithTag("trash");
        if(trashes.Length-1<5)
        {
            GenerateTrash();
        }
    }

    void GenerateTrash()
    {
        System.Random rnd = new System.Random();
        GameObject spawnedTrash = Instantiate(trash);
        spawnedTrash.tag = "trash";

        spawnedTrash.transform.position = new Vector2(rnd.Next(areaMinHeight, areaHeight), rnd.Next(areaMinWidth, areaWidth));
        while (trashLocations.Contains(spawnedTrash.transform.position))
        {
            spawnedTrash.transform.position = new Vector2(rnd.Next(areaMinHeight, areaHeight), rnd.Next(areaMinWidth, areaWidth));
        }
        trashLocations.Add(spawnedTrash.transform.position);
        trashOnBoard--;
    }
}
