using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public int Score = 0;
    public int TrashCollected = 0;
    public AudioSource SplashSource;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision");
        SplashSource.Play();

        int leanX = (int)gameObject.transform.position.x;
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

        if (collision.gameObject.CompareTag("trash"))
        {
            //Debug.Log("trash");
            ScoreManager.instance.AddPoint();
            Score++;
            TrashCollected++;
            Destroy(collision.gameObject);
        }
    }
}
