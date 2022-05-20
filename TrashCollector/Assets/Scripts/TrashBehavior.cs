using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBehavior : MonoBehaviour
{
    private bool lr;

    private float x;
    private float y;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        x = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Wiggle()
    {
        if (lr) //left
        {
            Debug.Log("left");
            if (transform.position.x < x - .002)
            {
                Debug.Log("switch");
                lr = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x - .2f, transform.position.y), .005f);
            }
        }
        else
        {
            Debug.Log("right");
            if (transform.position.x > x + .002)
            {
                Debug.Log("switch");
                lr = true;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x + .2f, transform.position.y), .005f);
            }
        }
    }

    public void FixedUpdate()
    {
        Wiggle();
    }
}
