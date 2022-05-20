 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatInputHandler : MonoBehaviour
{
    //Component
    PlayerMovement playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputVector = Vector2.zero;

        inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        //Update input vectors of PlayerMovement --> SetInputVector
        playerMovement.SetInputVector(inputVector);

        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        if (Input.GetKeyDown(KeyCode.M))
        {
            foreach (AudioSource audio in sources)
            {
                if (audio.mute)
                {
                    audio.mute = false;
                }
                else { audio.mute = true; }
            }
        }
    }
}
