using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIBehavior : MonoBehaviour
{
    public Vector2[] docks;
    //needed for the enemies, basic/simple default
    private float speed = .5f;
    private int HP = 10;
    private float timePassedSincePickup;
    public int trashPickedUp;
    private string username;
    public int uniqueID;

    //camRadius used if need to spawn off camera
    private int camRadius;
    public string[] collectiveNames = {"MrKrabs","bubbles","floaty","Duck","Lazlo", "ARGGGGGG","SoulFate","ectoPlasmic","lil","Phantom","Player_0485","sanic"};
    public TextMeshPro nameTags;

    private int xDest;
    private int yDest;
    private bool destSet;
    Rigidbody2D rgb;
    private bool headedtoTrash;
    private bool checkingIn;

    private bool waiting;

    private bool dead;

    AudioSource splashSource;
    // Start is called before the first frame update
    void Start()
    {
        headedtoTrash = false;
        destSet = false;
        checkingIn = false;
        dead = false;
        trashPickedUp = 0;
        splashSource = gameObject.GetComponent<AudioSource>();
        timePassedSincePickup = Time.realtimeSinceStartup;
        rgb = gameObject.GetComponent<Rigidbody2D>();

        //camRadius = (int)FindObjectOfType<Camera>().fieldOfView;
    }
    private void Awake()
    {
        ///has to be included or the boats don't appear
        transform.right = new Vector3(xDest, yDest) - transform.position;
        
    }

    private void FixedUpdate()
    {
        ////NEEDS TO BE FIXED
        if(!checkingIn&&!dead)
        {
            if (!TimeCheck(Time.realtimeSinceStartup))
            {
                //HP--;
                //Debug.Log(HP);
            }

            if (HP < 0)
            {
                System.Random rnd = new System.Random();
                gameObject.transform.position = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
                username = collectiveNames[rnd.Next(0, 11)];
                HP = 10;
            }
            //else
            //{
            //    dead = true;
            //    gameObject.GetComponent<MeshRenderer>().enabled = false;
            //    gameObject.transform.position = new Vector2(-40, -50);
            //    Invoke("death", 10);
            //}


            if (trashPickedUp % 5 == 0 && trashPickedUp != 0)
            {
                checkIn();
                checkingIn = true;
            }
        }
        else { checkIn(); }
    }

    //Update is called once per frame
    void Update()
    {
        if(nameTags.text=="New Text")
        {
            nameTags.text=setName();
        }
        targetTrash();
        boundsCheck();
       if(!checkingIn&&!dead)
        {
            if (!headedtoTrash)
            {
                System.Random rnd = new System.Random();
                int re_roll = rnd.Next(0, 101);
                re_roll = (int)re_roll * uniqueID;
                for (int i = 0; i < re_roll; i++)
                {
                    rnd.Next(0, 101);
                }
                if (rnd.Next(0, 101) * Time.deltaTime % 2 == 0)
                {
                    moveForward();
                }
                else if (rnd.Next(0, 101) % 10 == 0)
                { moveBack(); }
            }
            else
            {
                Vector2 destination = new Vector2(xDest, yDest);
                gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, destination, .005f);
            }
        }
    }

    //fdt=fixedDeltaTime
    public bool TimeCheck(float fdt)
    {
        //Debug.Log(fdt + " <fdt  tpsp> " + timePassedSincePickup);
        if (timePassedSincePickup * 10 > fdt)
        {
            timePassedSincePickup = Time.realtimeSinceStartup;
            return false;
        }
        else return true;
    }

    //targets trash and sends ememy toward it
    public void targetTrash()
    {
        GameObject[] trashPieces = GameObject.FindGameObjectsWithTag("trash");

        float distance = 0.0f;
        if(trashPieces.Length>0)
        {
            float mindistance = Vector2.Distance(gameObject.transform.position, trashPieces[0].transform.position);
            int placer = 0;
            for (int i = 0; i < trashPieces.Length - 1; i++)
            {
                distance = Vector2.Distance(gameObject.transform.position, trashPieces[i].transform.position);
                if (distance < mindistance)
                {
                    mindistance = distance;
                    placer = i;
                }
            }
            if (mindistance < 10)
            {
                xDest = (int)trashPieces[placer].transform.position.x;
                yDest = (int)trashPieces[placer].transform.position.y;
                headedtoTrash = true;
            }
            else { headedtoTrash = false; }
        }
    }

    void moveForward()
    {
        if(gameObject.transform.position.x<20)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x + .15f*speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =
       new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x - .15f * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        System.Random rnd = new System.Random();
        int re_roll = rnd.Next(0, 101);
        re_roll = (int)re_roll * uniqueID;
        if (rnd.Next(0, 31) * Time.deltaTime % 3 == 0)
        {
            if (gameObject.transform.position.y < 20)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
          new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else if (rnd.Next(0, 61) * Time.deltaTime % 6 == 0)
        {
            if (gameObject.transform.position.y > -20)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y - .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void moveBack()
    {
        if (gameObject.transform.position.x > -20)
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x - .15f*speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            gameObject.GetComponent<Rigidbody2D>().velocity =
            new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x + .15f * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y);
        }
        System.Random rnd = new System.Random();
        int re_roll = rnd.Next(0, 101);
        re_roll = (int)re_roll * uniqueID;
        if (rnd.Next(0, 31) * Time.deltaTime % 3 == 0)
        {
            if (gameObject.transform.position.y < 20)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y - .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
        else if (rnd.Next(0, 61) * Time.deltaTime % 6 == 0)
        {
            if (gameObject.transform.position.y > -20)
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y - .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().velocity =
           new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, gameObject.GetComponent<Rigidbody2D>().velocity.y + .015f * speed);
                Vector2 v = rgb.velocity;
                float angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    void boundsCheck()
    {
        if(gameObject.transform.position.x>35)
        {
            gameObject.transform.position = new Vector2(34, gameObject.transform.position.y);
        }
        else if(gameObject.transform.position.x<-40)
        {
            gameObject.transform.position = new Vector2(-39, gameObject.transform.position.y);
        }

        if (gameObject.transform.position.y > 17)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 16);
        }
        else if (gameObject.transform.position.y < -35)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -34);
        }
    }

    void checkIn()
    {
        Debug.Log("checkin "+uniqueID);
        System.Random rnd = new System.Random();
        gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, docks[uniqueID], speed);

        if ((Vector2)gameObject.transform.position == docks[uniqueID])
        {
            Debug.Log("checkout " + uniqueID);
            Invoke("checkOut", 10);
        }
    }

    void checkOut()
    {
        checkingIn = false;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        splashSource.Play();

        int leanX=(int)gameObject.transform.position.x;
        int leanY = (int)gameObject.transform.position.y;
        if(collision.gameObject.tag!="boat")
        {
            if (leanX < 0)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y);
            }
            else if (leanX > 0)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y);
            }

            if (leanY < 0)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1);
            }
            else { gameObject.transform.position = new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y - 1); }

        }
        if (collision.gameObject.tag=="trash")
        {
            Destroy(collision.gameObject);
            trashPickedUp++;
        }
    }

    void death()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        dead = false;

        System.Random rnd = new System.Random();
        for (int i = 0; i < uniqueID; i++)
        {
            gameObject.transform.position = new Vector2(rnd.Next(-35, 40), rnd.Next(-35, 17));
            nameTags.text = collectiveNames[rnd.Next(0, 11)];
        }
    }

    public string setName()
    {
        int i = gameObject.GetComponent<AIBehavior>().uniqueID;
        System.Random rnd = new System.Random();
        string ret="";
        for(int x=0;x<i;x++)
        {
            rnd.Next(0, 11);
        }
        ret = collectiveNames[rnd.Next(0, 11)];
        return ret;
    }
}

