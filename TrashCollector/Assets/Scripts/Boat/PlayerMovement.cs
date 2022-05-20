using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Boat Settings")]
    //How much boart should drift
    public float driftFactor = 0.65f;
    //How fast does car will accelarate
    public float accelerationFactor = 5f;
    //How fast does car will turn
    public float steertingFactor = 3.5f;
    public float maxSpeed;
    public float reversePresentage = 0.3f;
    public float rotationSpeed;
    public bool isMoving = true;

    //Local variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;

    Rigidbody2D boatRigidbody2D;

    // Start is called before the first frame update
    private void Awake()
    {
        //get the compenet of the rigidbody
        boatRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce()
    {
        
        //Calulate how much "Forward" are we going in terms of the direction of velocity
        velocityVsUp = Vector2.Dot(transform.up, boatRigidbody2D.velocity);

        //limit max speed in the forward direction
        if(velocityVsUp > maxSpeed && accelerationInput > 0)
        {
            return;
        }

        //limit reverse direction with 30% of max speed
        if(velocityVsUp < -maxSpeed * reversePresentage && accelerationInput < 0)
        {
            return;
        }

        //limit speed in diagonal directions.
        if(boatRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0)
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
        //Limit ability to turn when moving slowly
        //float minSpeedBeforeAllowTurnFactor = (boatRigidbody2D.velocity.magnitude / 8);
        //minSpeedBeforeAllowTurnFactor = Mathf.Clamp01(minSpeedBeforeAllowTurnFactor);

        //update rotation angle based on input
        rotationAngle -= steeringInput * steertingFactor * rotationSpeed;

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
}
