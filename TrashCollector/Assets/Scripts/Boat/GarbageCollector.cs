using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public int garbage = 0;
    public int garbageLimit;

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if(collision.gameObject.CompareTag("Garbage"))
    //    {
    //        if (garbage < garbageLimit)
    //        {
    //            Destroy(collision.gameObject);
    //            garbage++;
    //            Debug.Log("Garbage count : " + garbage);
    //        }
    //        else
    //        {
    //            Debug.Log("Garbage Full");
    //        }
            
    //    }
        
    //}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            if (garbage < garbageLimit)
            {
                Destroy(collision.gameObject);
                garbage++;
                Debug.Log("Garbage count : " + garbage);
            }
            else
            {
                Debug.Log("Garbage Full");
            }

        }
    }
}
