using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashGeneration : MonoBehaviour
{
    private float areaHeight;
    private float areaWidth;
    private float areaMinHeight;
    private float areaMinWidth;

    public int trashWorth;

    public int trashOnBoard;
    public int maxTrash;
    public List<Vector2> trashLocations;
    public GameObject trash;
    public Sprite[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        //top left; -30.2, 21.75
        //bottom right; 10.5, -9.3
        areaHeight = 21;
        areaWidth = 10;
        areaMinHeight = -9;
        areaMinWidth = -29;
        //trashOnBoard = (areaHeight + areaWidth) / 3;
        //maxTrash = 5;
        trashWorth = 1;
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
        if (trashes.Length < maxTrash && !UIManager.instance.endgame)
        {
            GenerateTrash();
        }
    }

    void GenerateTrash()
    {
        System.Random rnd = new System.Random();
        GameObject spawnedTrash = Instantiate(trash);
        spawnedTrash.tag = "trash";
        spawnedTrash.transform.position = new Vector2(Random.Range(areaMinWidth, areaWidth), Random.Range(areaMinHeight, areaHeight));
        int x = rnd.Next(0, sprites.Length);
        spawnedTrash.GetComponent<SpriteRenderer>().sprite = sprites[x];
        if(x<=2)
            trashWorth = 1;

        else if(x==3||x==4)
            trashWorth = 3;
        
        else 
            trashWorth = 5; 

        spawnedTrash.GetComponent<TrashProperties>().worth = trashWorth;


        spawnedTrash.GetComponent<SpriteRenderer>().size = spawnedTrash.GetComponent<SpriteRenderer>().size * 10;
        spawnedTrash.GetComponent<BoxCollider2D>().size = new Vector2(.15f, .15f);
        while (trashLocations.Contains(spawnedTrash.transform.position))
        {
            spawnedTrash.transform.position = new Vector2(Random.Range(areaMinWidth, areaWidth), Random.Range(areaMinHeight, areaHeight));
        }
        trashLocations.Add(spawnedTrash.transform.position);
        trashOnBoard--;
    }

    public void refreshList()
    {
        GameObject[] allTrash = GameObject.FindGameObjectsWithTag("trash");
        trashLocations.RemoveRange(0, trashLocations.Count);
        foreach (GameObject trash in allTrash)
        {
            trashLocations.Add(trash.transform.position);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.name=="IslandAvoidance")
        {
            Destroy(this);
            trashOnBoard++;
            GenerateTrash();
        }
    }
}
