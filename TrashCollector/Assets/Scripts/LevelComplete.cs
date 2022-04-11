using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public int garbageTarget = 5;
    [HideInInspector]
    public int totalGarbage;
    bool isBoatDocked = true;
    GarbageCollector garbageLevel;
    public GameObject boat;

    // Start is called before the first frame update
    void Start()
    {
        garbageLevel = boat.GetComponent<GarbageCollector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isBoatDocked = true;
            totalGarbageCounter();

            if (totalGarbage < garbageTarget)
            {
                Debug.Log("Garbage Target : " + totalGarbage + "/" + garbageTarget);
                return;
            }
            else
            {
                Debug.Log("Garbage Target achived : " + totalGarbage + "/" + garbageTarget);
                
                
            }
            
        }
        else
            isBoatDocked = false;
        

    }

    void totalGarbageCounter()
    {
        if(isBoatDocked == true)
        {
            totalGarbage = totalGarbage + garbageLevel.garbage;
            //reset boat garbage level
            garbageLevel.garbage = 0;
        }
    }
}
