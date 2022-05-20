using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    float particleEmissionRate = 0;

    //componenets
    PlayerMovement playerMovement;

    ParticleSystem particleSystem;
    ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        playerMovement = GetComponentInParent<PlayerMovement>();

        particleSystem = GetComponent<ParticleSystem>();

        emissionModule = particleSystem.emission;

        //Set emmision to zero
        emissionModule.rateOverTime = 0;
    }
   

    // Update is called once per frame
    void Update()
    {
        //reduces particle overtime
        particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
        emissionModule.rateOverTime = particleEmissionRate;

        if (playerMovement.isMoving == true)
        {
            particleEmissionRate = 30;
            
        }
        else
        { 
            particleEmissionRate = 4;
            
        }
            
    }

    
}
