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
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        areaHeight = 38;
        areaWidth = 17;
        areaMinHeight = -33;
        areaMinWidth = -38;
        trashOnBoard = (areaHeight+areaWidth)/3;
        maxTrash = 5;
    }

    // Update is called once per frame
    void Update()
    {
        checkTrashOut();
        refreshList();
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
        spawnedTrash.GetComponent<SpriteRenderer>().sprite = sprites[rnd.Next(0, sprites.Length - 1)];

        
        spawnedTrash.GetComponent<SpriteRenderer>().size = spawnedTrash.GetComponent<SpriteRenderer>().size * 10;
        spawnedTrash.GetComponent<BoxCollider2D>().size = new Vector2(.15f, .15f);
        while (trashLocations.Contains(spawnedTrash.transform.position))
        {
            spawnedTrash.transform.position = new Vector2(rnd.Next(areaMinHeight, areaHeight), rnd.Next(areaMinWidth, areaWidth));
        }
        trashLocations.Add(spawnedTrash.transform.position);
        trashOnBoard--;
    }

    public void refreshList()
    {
        GameObject[] allTrash = GameObject.FindGameObjectsWithTag("trash");
        trashLocations.RemoveRange(0,trashLocations.Count);
        foreach (GameObject trash in allTrash)
        {
            trashLocations.Add(trash.transform.position);
        }
    }
}

