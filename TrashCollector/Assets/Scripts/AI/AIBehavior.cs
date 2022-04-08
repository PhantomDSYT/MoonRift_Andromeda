using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBehavior : MonoBehaviour
{
    //needed for the enemies, basic/simple default
    private float speed = .5f;
    private int HP = 10;
    private float timePassedSincePickup;
    private string username;
    public int uniqueID;

    //camRadius used if need to spawn off camera
    private int camRadius;
    private string[] collectiveNames;
    [SerializeField] private TextMesh nameTags;

    private int xDest;
    private int yDest;
    private bool destSet;
    Rigidbody2D rgb;
    private bool headedtoTrash;

    AudioSource splashSource;
    // Start is called before the first frame update
    void Start()
    {
        headedtoTrash = false;
        destSet = false;
        splashSource = gameObject.GetComponent<AudioSource>();
        timePassedSincePickup = Time.realtimeSinceStartup;
        rgb = gameObject.GetComponent<Rigidbody2D>();
        //camRadius = (int)FindObjectOfType<Camera>().fieldOfView;
        collectiveNames = new string[] { "MrKrabs", "bubbles","floaty","Duck","Lazlo","ARGGGGGG","SoulFate","ectoPlasmic","lil","Phantom","Player_0485" };
    }
    private void Awake()
    {
        ///has to be included or the boats don't appear
        transform.right = new Vector3(xDest, yDest) - transform.position;
    }

    private void FixedUpdate()
    {
        ////NEEDS TO BE FIXED
        if (!TimeCheck(Time.realtimeSinceStartup))
        {
            //HP--;
            //Debug.Log(HP);
        }

        if (HP <= 0)
        {
            System.Random rnd = new System.Random();
            gameObject.transform.position = new Vector2(rnd.Next(-10, 10), rnd.Next(-10, 10));
            username = collectiveNames[rnd.Next(0, 11)];
            HP = 10;
        }
    }

    //Update is called once per frame
    void Update()
    {
        targetTrash();
        boundsCheck();
        if (!headedtoTrash)
        {
            System.Random rnd = new System.Random();
            int re_roll = rnd.Next(0, 101);
            re_roll = (int)re_roll * uniqueID;
            for (int i = 0; i < re_roll; i++)
            {
                rnd.Next(0, 101);
            }
            if (rnd.Next(0, 101)*Time.deltaTime % 2 == 0)
            {
                moveForward();
            }
            else if (rnd.Next(0, 101) % 10 == 0)
            { moveBack(); }
        }
        else
        {
            Vector2 destination = new Vector2(xDest,yDest);
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, destination, .005f);
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
        float mindistance= Vector2.Distance(gameObject.transform.position, trashPieces[0].transform.position);
        int placer = 0;
        for(int i=0; i<trashPieces.Length-1;i++)
        {
            distance = Vector2.Distance(gameObject.transform.position, trashPieces[i].transform.position);
            if(distance<mindistance)
            {
                mindistance = distance;
                placer = i;
            }
        }
        if(mindistance<10)
        {
            xDest = (int)trashPieces[placer].transform.position.x;
            yDest = (int)trashPieces[placer].transform.position.y;
            headedtoTrash = true;
        }
        else { headedtoTrash = false; }
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
        else if(gameObject.transform.position.x<-35)
        {
            gameObject.transform.position = new Vector2(-34, gameObject.transform.position.y);
        }

        if (gameObject.transform.position.y > 35)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, 34);
        }
        else if (gameObject.transform.position.y < -35)
        {
            gameObject.transform.position = new Vector2(gameObject.transform.position.x, -34);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
        splashSource.Play();

        int leanX=(int)gameObject.transform.position.x;
        int leanY = (int)gameObject.transform.position.y;
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
        
        if(collision.gameObject.tag=="trash")
        {
            //Debug.Log("trash");
            Destroy(collision.gameObject);
        }
    }
}

