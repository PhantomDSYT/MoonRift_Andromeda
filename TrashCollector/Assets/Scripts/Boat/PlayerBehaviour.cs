using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    private int garbage = 0;

    public string path = "neutral";
    public int pathLevel = 0;

    int playerHealth = 100;
    int healthMax;
    public float healthMulti { get; set;}
    public float baseHealth { get; set; }

    float collectionArea;
    public float collectionAreaMulti { get; set; }
    public float baseCollectionArea { get; set; }

    public float speedMulti;
    public float baseSpeed;

    float time;
    public int regenRate = 5;
    public int regenAmount = 1;

    [SerializeField]PlayerMovement playerMovement;

    //how to play tutorial
    public bool firstHit = false;



    private void Start()
    {
        // Setting Regen Rate to Timer
        time = regenRate;

        // Setting all health variables for upgrades and evolutions
        healthMulti = 1f;
        baseHealth = 100;
        healthMax = (int)(baseHealth * healthMulti);
        playerHealth = healthMax;
        UIManager.instance.healthSlider.maxValue = healthMax;
        UIManager.instance.healthSlider.value = playerHealth;

        baseCollectionArea = 0.25f;
        collectionAreaMulti = 1f;
        collectionArea = baseCollectionArea * collectionAreaMulti;
        GetComponent<CircleCollider2D>().radius = collectionArea;

        speedMulti = 1.0f;
        baseSpeed = playerMovement.maxSpeed;
        playerMovement.maxSpeed = baseSpeed * speedMulti;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("hazard"))
        {
            firstHit = true;
            playerHealth -= 10;
            UIManager.instance.healthSlider.value = playerHealth;

            if (playerHealth <= 0)
            {
                PlayerPrefs.SetInt("highscore", UIManager.instance.score);
                SceneManager.LoadScene("GameOver");
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("trash"))
        {
            
            UIManager.instance.AddPoint(collision.gameObject.GetComponent<TrashProperties>().worth);
            UIManager.instance.AddTrash(collision.gameObject.GetComponent<TrashProperties>().worth);
            UIManager.instance.AddXP(collision.gameObject.GetComponent<TrashProperties>().worth);
            Destroy(collision.gameObject);
            garbage++;
            Debug.Log("Garbage count : " + garbage);

        }
    }

    private void Healthregen()
    {
        playerHealth += regenAmount;
        UIManager.instance.healthSlider.value = playerHealth;
    }


    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0 && playerHealth < healthMax)
        {
            Healthregen();
            time = regenRate;
        }

        if (UIManager.instance.xp >= UIManager.instance.xpCap)
        {
            UIManager.instance.LevelUp();
        }

    }

    public void ApplyUpgrades()
    {
        healthMax = (int)(baseHealth * healthMulti);
        UIManager.instance.healthSlider.maxValue = healthMax;

        collectionArea = baseCollectionArea * collectionAreaMulti;
        GetComponent<CircleCollider2D>().radius = collectionArea;

        playerMovement.maxSpeed = baseSpeed * speedMulti;
    }
}
