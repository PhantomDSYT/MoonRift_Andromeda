using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    //needed for the enemies, basic/simple default
    private float speed = .0015f;
    public float HP { get; private set; }
    public float maxHP { get; private set; }
    public bool pause;
    //private float timePassedSincePickup;
    public int trashPickedUp;
    //private string username;
    public int uniqueID;
    
    public HealthBarBehaviour healthBar;
    private List<string> usedNames=new List<string>();

    //camRadius used if need to spawn off camera
    public string[] collectiveNames = { "MrKrabs", "bubbles", "floaty", "Duck", "Lazlo", "ARGGGGGG", "SoulFate", "ectoPlasmic", "lil", "Phantom", "Player_0485", "sanic" };
    private TextMeshProUGUI nametags;

    private bool dead;

    AudioSource splashSource;
    // Start is called before the first frame update
    void Start()
    {
        dead = false;
        //trashPickedUp = 0;
        pause = false;
        splashSource = gameObject.GetComponent<AudioSource>();

        nametags = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        nametags.fontSize = 20;

        HP = 50f;
        maxHP = 50f;

        //front = gameObject.GetComponentInChildren<GameObject>();
        healthBar.SetHealth(HP, maxHP);
        //camRadius = (int)FindObjectOfType<Camera>().fieldOfView;
    }
    private void Awake()
    {
        HP = 50;
        maxHP = 50;
        healthBar.SetHealth(HP, maxHP);
    }

    private void FixedUpdate()
    {
        //if (!checkingIn && !dead && !pause)
        //{
        //    if (HP <= 0)
        //    {
        //        dead = true;

        //        gameObject.transform.position = new Vector2(-40, -50);
        //        Invoke("death", 10);
        //    }

        //    if (trashPickedUp % 5 == 0 && trashPickedUp != 0)
        //    {
        //        checkIn();
        //        checkingIn = true;
        //    }
        //}
        //else if (checkingIn) 
        //{ 
        //    checkIn(); 
        //}

        if (dead)
        {
            death();
        }
    }

    //Update is called once per frame
    void Update()
    {
        if (nametags.text == "New Text")
        {
            nametags.text = setName();
        }
        nametags.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        if (!pause)
        {
            //if (!headedtoTrash)
            //{
            //    targetTrash();
            //}
            //else
            //{
            //    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, trashAt, speed);
            //    //Vector3 target = trashAt;
            //    //transform.right = (target - transform.position).normalized;
            //    Quaternion toRotate = Quaternion.LookRotation(Vector3.forward, trashAt);
            //    gameObject.transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotate, .75f);
            //}

            //if ((Vector2)gameObject.transform.position == trashAt)
            //{
            //    headedtoTrash = false;
            //}
            ////boundsCheck();
            //if (!checkingIn && !dead)
            //{
            //    if (!headedtoTrash)
            //    {
            //        System.Random rnd = new System.Random();
            //        int re_roll = rnd.Next(0, 101);
            //        re_roll = (int)re_roll * uniqueID;
            //        for (int i = 0; i < re_roll; i++)
            //        {
            //            rnd.Next(0, 101);
            //        }
            //    }
            //}
            if (dead)
            {
                death();
            }

            //if (enteredCollision && timeInCollision - Time.realtimeSinceStartup > 5)
            //{
            //    targetTrash();
            //}
        }
    }

    //targets trash and sends ememy toward it
    public void targetTrash()
    {
        //GameObject[] trashPieces = GameObject.FindGameObjectsWithTag("trash");

        //List<float> distances = new List<float>();
        //if (trashPieces.Length > 0)
        //{
        //    int rd = 0;
        //    System.Random rnd = new System.Random();
        //    for (int i = 0; i < uniqueID; i++)
        //    {
        //        rd = rnd.Next(0, trashPieces.Length - 1);
        //    }
        //    trashAt = trashPieces[rd].transform.position;
        //    gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, trashAt, speed);
        //    headedtoTrash = true;

        //}
        //else { headedtoTrash = false; }
    }

    //void boundsCheck()
    //{
    //    //top left; -9, 4
    //    //bottom right; 9, -7.5
    //    if (gameObject.transform.position.x > 9)
    //    {
    //        gameObject.transform.position = new Vector2(9, gameObject.transform.position.y);
    //    }
    //    else if (gameObject.transform.position.x < -9)
    //    {
    //        gameObject.transform.position = new Vector2(-9, gameObject.transform.position.y);
    //    }

    //    if (gameObject.transform.position.y > 4)
    //    {
    //        gameObject.transform.position = new Vector2(gameObject.transform.position.x, 4);
    //    }
    //    else if (gameObject.transform.position.y < -7.5)
    //    {
    //        gameObject.transform.position = new Vector2(gameObject.transform.position.x, -7);
    //    }
    //}

    //used to make the AI pause briefly for "upgrade"
    //void checkIn()
    //{
    //    Invoke("checkOut", 10);
    //}

    //void checkOut()
    //{
    //    checkingIn = false;
    //}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        splashSource.Play();

        if (collision.gameObject.tag == "trash")
        {
            Destroy(collision.gameObject);
            //headedtoTrash = false;
            trashPickedUp++;
        }
        //else if(collision.gameObject.name=="Boat")
        //{
        //    speed = .0025f;
        //}
        //else
        //{
        //    enteredCollision = true;
        //    timeInCollision = Time.realtimeSinceStartup;
        //}
        //Debug.Log("hit " + collision.gameObject.name);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "trash")
        {
            HP -= 10;
            healthBar.SetHealth(HP, maxHP);
            if (HP <= 0)
            {
                dead = true;
            }
        }
        //timeInCollision = 0;
        //enteredCollision = false;
        //speed = .0015f;
    }

    void death()
    {
        dead = false;
        Debug.Log("Dead");
        System.Random rnd = new System.Random();
        for (int i = 0; i < uniqueID; i++)
        {
            gameObject.transform.position = new Vector2(rnd.Next(-9, 9), rnd.Next(-7, 4));
            nametags.text = setName();
        }
        HP = 50;
        healthBar.SetHealth(HP, HP);
    }

    public string setName()
    {
        int i = gameObject.GetComponent<AIBehavior>().uniqueID;
        System.Random rnd = new System.Random();
        string ret = "";
        for (int x = 0; x < i; x++)
        {
            rnd.Next(0, 11);
        }
        ret = collectiveNames[rnd.Next(0, 11)];
        usedNames.Clear();
        TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
        foreach(TextMeshProUGUI tmp in texts)
        {
            usedNames.Add(tmp.text);
        }
        if (!usedNames.Contains(ret))
        {
            usedNames.Add(ret);
        }
        else
        {
            while (usedNames.Contains(ret))
            {
                ret = collectiveNames[rnd.Next(0, 11)];
            }
        }
        return ret;
    }
}

