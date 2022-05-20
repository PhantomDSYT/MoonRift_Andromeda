using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIOverhaul : MonoBehaviour
{
    public bool pause;

    //How much boart should drift
    private float driftFactor = 0.65f;
    //How fast does accelarate
    private float accelerationFactor = .05f;
    //How fast does turn
    private float steeringFactor = 6;
    private float maxSpeed = .9f;
    private float reversePresentage = 0.05f;
    private float rotationSpeed = .2f;
    private bool isMoving = true;

    //-1,1
    float accelerationInput = 0;
    float steeringInput = 0;
    
    float rotationAngle = 0;
    float velocityVsUp = 0;

    float forwardChanged;
    float steeringChanged;
    float lastInHitBox;
    int trashPickedup;

    //all enemies
    //public HealthBarBehaviour healthBar;
    //private List<string> usedNames = new List<string>();
    //public string[] collectiveNames = { "MrKrabs", "bubbles", "floaty", "Duck", "Lazlo", "ARGGGGGG", "SoulFate", "ectoPlasmic", "lil", "Phantom", "Player_0485", "sanic" };
    //private TextMeshProUGUI nametags;
    private int hitPoints = 50;

    //seekers only
    bool seekingTrash;
    GameObject[] trashPieces;
    GameObject trash;
    GameObject[] allTrash;
    float distance = 10;
    Vector3 trashLocation = new Vector2(0, 0);

    //0 - inactive
    //1 - left
    //2 - right
    int chooseOne;

    Rigidbody2D boatRigidbody2D;
    RaycastHit2D hit;

    public int uniqueID;
    //0 - Random
    //1 - Trash Seeker
    //3 - Bully
    public int type;

    // Start is called before the first frame update
    private void Awake()
    {
        //get the compenet of the rigidbody
        boatRigidbody2D = GetComponent<Rigidbody2D>();
        forwardChanged = Time.realtimeSinceStartup;
        steeringChanged = Time.realtimeSinceStartup;
        hit = Physics2D.Raycast(new Vector2(transform.position.x,transform.position.y+.18f), transform.TransformDirection(Vector2.up),1f);
        //allTrash = GameObject.FindGameObjectsWithTag("trash");
        trashPickedup = 0;
    }

    void Start()
    {
        //nametags = gameObject.GetComponentInChildren<Canvas>().GetComponentInChildren<TextMeshProUGUI>();
        //nametags.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        //if (nametags.text == "New Text")
        //{
        //    nametags.text = setName();
        //}
        //nametags.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        if (!pause)
        {
            if (type == 0)
            {
                AimlessProtocol();
            }
            else if (type == 1)
            {
                //SeekerProtocol();
            }
            else
            {
                HunterProtocol();
            }
        }
    }

    void FixedUpdate()
    {

    }

    void ApplyEngineForce()
    {
        //Calulate how much "Forward" are we going in terms of the direction of velocity
        velocityVsUp = Vector2.Dot(transform.up, boatRigidbody2D.velocity);

        //limit max speed in the forward direction
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //limit reverse direction with 30% of max speed
        if (velocityVsUp < -maxSpeed * reversePresentage && accelerationInput < 0)
        {
            return;
        }

        //limit speed in diagonal directions.
        if (boatRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //Apply drag if there is no accelarationInput
        if (accelerationInput == 0)
        {
            isMoving = false;
            boatRigidbody2D.drag = Mathf.Lerp(boatRigidbody2D.drag, 3f, Time.fixedDeltaTime * 3);
        }
        else
        {
            boatRigidbody2D.drag = 0;
            isMoving = true;
        }

        //create the force
        // transform.up = forward (Green arrow)
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply the force to push the boat forward
        boatRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }
    void ApplySteering()
    {
        //update rotation angle based on input
        rotationAngle -= steeringInput * steeringFactor * rotationSpeed;

        //apply steering by rotating the boat object
        boatRigidbody2D.MoveRotation(rotationAngle);
    }

    //Remove part of the side forces as we turn
    void KillOrthogonalVelocity()
    {
        //calculate the forward velocity of the boat
        Vector2 forwardVelocity = transform.up * Vector2.Dot(boatRigidbody2D.velocity, transform.up);
        //calculate the right right velocity of the boat (how much its goin sideways)
        Vector2 rightVelocity = transform.right * Vector2.Dot(boatRigidbody2D.velocity, transform.right);


        //remove right velocity based on the driftFactor
        //If driftFactor is 0 it will not drift at all
        boatRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetFB(float velocityItem)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < uniqueID; i++)
        {
            rnd.Next(0, 3);
        }
        int tempRND = rnd.Next(0, 100);

        //stall
        if (tempRND % 13 == 0)
        {
            //Debug.Log("STALL");
            if (velocityItem < 0)
            {
                velocityItem += .1f;
            }
            if (velocityItem > 0)
            {
                velocityItem -= .1f;
            }
        }
        //back
        else if (tempRND % 10 == 0)
        {
            //Debug.Log("B");
            if (velocityItem > -.5)
            {
                velocityItem -= .1f;
            }
        }
        else //forward
        {
            //Debug.Log("F");
            if (velocityItem < maxSpeed)
            {
                //Debug.Log("velocityF " + velocityItem+ " of "+uniqueID);
                velocityItem += .1f;
            }
        }

        return velocityItem;
    }

    public float GetSteer(float velocityItem)
    {
        System.Random rnd = new System.Random();
        for (int i = 0; i < uniqueID; i++)
        {
            rnd.Next(0, 3);
        }
        int tempRND = rnd.Next(0, 100);

        //left
        if (tempRND % 3 == 0)
        {
            //Debug.Log("L");
            if (velocityItem < .5)
            {
                velocityItem += .01f;
            }
        }
        //right
        else if (tempRND % 4 == 0)
        {
            //Debug.Log("R");
            if (velocityItem > -.5)
            {
                velocityItem -= .01f;
            }
        }
        else //straight
        {
            //Debug.Log("HOLD");
            if (velocityItem < 0)
            {
                velocityItem += .1f;
            }
            if (velocityItem > 0)
            {
                velocityItem -= .1f;
            }
        }

        return velocityItem;
    }

    public float oneDirection(float rotator, int direction)
    {
        if(direction==1&&rotator>-.5f)
        {
            rotator -= .25f;
        }
        else
        {
            rotator += .25f;
        }

        return rotator;
    }

    public float straightAway(float turnInput)
    {
        if (turnInput < 0)
        {
            turnInput += .2f;
        }
        if (turnInput > 0)
        {
            turnInput -= .2f;
        }

        return turnInput;
    }

    public float reverse(float accInput)
    {
        if(accInput>.5f)
        {
            accInput -= .5f;
        }
        return accInput;
    }

    //public string setName()
    //{
    //    int i = uniqueID;
    //    System.Random rnd = new System.Random();
    //    string ret = "";
    //    for (int x = 0; x < i; x++)
    //    {
    //        rnd.Next(0, 11);
    //    }
    //    //ret = collectiveNames[rnd.Next(0, 11)];
    //    //usedNames.Clear();
    //    //TextMeshProUGUI[] texts = FindObjectsOfType<TextMeshProUGUI>();
    //    //foreach (TextMeshProUGUI tmp in texts)
    //    //{
    //    //    usedNames.Add(tmp.text);
    //    //}
    //    //if (!usedNames.Contains(ret))
    //    //{
    //    //    usedNames.Add(ret);
    //    //}
    //    //else
    //    //{
    //    //    while (usedNames.Contains(ret))
    //    //    {
    //    //        ret = collectiveNames[rnd.Next(0, 11)];
    //    //    }
    //    //}
    //    return ret;
    //}

    public void AimlessProtocol()
    {
        //obstacle found
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up) * 2f, Color.red);
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up), 2f);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.name);
            if(hit.collider.tag!="trash"&&hit.collider.tag!="boat")
            {
                System.Random rnd = new System.Random();
                if (chooseOne == 0)
                {
                    for (int x = 0; x < uniqueID; x++)
                    {
                        rnd.Next(1, 3);
                    }
                    chooseOne = rnd.Next(1, 3);
                }
                steeringInput = oneDirection(steeringInput, chooseOne);
                lastInHitBox = Time.realtimeSinceStartup;
            }
        }
        else
        {
            chooseOne = 0;
            if (Time.realtimeSinceStartup - forwardChanged > 5)
            {
                accelerationInput = GetFB(accelerationInput);
                forwardChanged = Time.realtimeSinceStartup;
            }
            if (Time.realtimeSinceStartup - steeringChanged > 3)
            {
                steeringInput = GetSteer(steeringInput);
                steeringChanged = Time.realtimeSinceStartup;
            }
        }

        if (Time.realtimeSinceStartup - lastInHitBox <= 2 && chooseOne == 0)
        {
            //Debug.Log("straight");
            steeringInput = straightAway(steeringInput);
        }

        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    //public void SeekerProtocol()
    //{
    //    Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up) * 3f, Color.red);
    //    hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up), 3f);
    //    if(hit.collider!=null&&hit.collider.tag=="trash")
    //    {
    //        steeringInput = straightAway(steeringInput);
    //        seekingTrash = true;
    //        Debug.Log("seeking");
    //    }
    //    else
    //    {
    //        if(Time.realtimeSinceStartup-steeringChanged>5)
    //        {
    //            seekingTrash = false;
    //            steeringInput = GetSteer(steeringInput);
    //            steeringChanged = Time.realtimeSinceStartup;
    //        }
    //    }

    //    if (seekingTrash&&accelerationInput<.5f)
    //    {
    //        accelerationInput += .1f;
    //    }

    //    ApplyEngineForce();
    //    KillOrthogonalVelocity();
    //    ApplySteering();
    //}

    public void HunterProtocol()
    {
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up) * 2f, Color.red);
        hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y), transform.TransformDirection(Vector2.up), 2f);
        if (hit.collider != null)
        {
            Debug.Log(hit.collider.name + " in my path");
            if(hit.collider.tag=="player"||hit.collider.tag=="enemy")
            {
                Debug.Log(hit.collider.tag + " SPOTTED");
                steeringInput = straightAway(steeringInput);

                accelerationInput = GetFB(accelerationInput);

                ApplyEngineForce();
                KillOrthogonalVelocity();
                ApplySteering();
            }
            else
            {
                System.Random rnd = new System.Random();
                if (chooseOne == 0)
                {
                    for (int x = 0; x < uniqueID; x++)
                    {
                        rnd.Next(1, 3);
                    }
                    chooseOne = rnd.Next(1, 3);
                }
                steeringInput = oneDirection(steeringInput, chooseOne);
                lastInHitBox = Time.realtimeSinceStartup;
            }
        }
        else
        {
            AimlessProtocol();
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="trash")
        {
            seekingTrash = false;
            Destroy(collision.gameObject);
            trashPickedup++;
        }
        else
        {
            //hitPoints -= 10;
            //healthBar.SetHealth(hitPoints, 50);
            accelerationInput = reverse(accelerationInput);
        }
        //splashSource.Play();
    }
}
