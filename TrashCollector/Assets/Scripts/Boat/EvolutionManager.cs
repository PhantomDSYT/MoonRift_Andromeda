using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EvolutionManager : MonoBehaviour
{
    public static EvolutionManager instance;

    [SerializeField] GameObject MainUI;
    [SerializeField] GameObject EvoUI; 
    [SerializeField] GameObject playerSprite;
    [SerializeField] GameObject player;
    [SerializeField] PlayerBehaviour playerBehaviour;

    public Sprite[] speedPath;
    public Sprite[] tankPath;


    private void Awake()
    {
        instance = this;
    }


    public void Evolve()
    {
        
        string path = playerBehaviour.path;
        int section = playerBehaviour.pathLevel;
        switch (path)
        {
            case "neutral":
                {
                    MainUI.SetActive(false);
                    EvoUI.SetActive(true);
                    break;
                }
            case "speed":
                {
                    playerSprite.GetComponent<SpriteRenderer>().sprite = speedPath[section-1];
                    var points = player.GetComponent<PolygonCollider2D>().points;
                    UIManager.instance.endgameRequired++;
                    if (section == 1)
                    {
                        playerBehaviour.baseHealth = 75;
                        playerBehaviour.baseSpeed = 1.2f;
                        playerBehaviour.baseCollectionArea = 0.3f;
                        playerBehaviour.regenRate = 4;

                        points[0].x = 0.1434617f;
                        points[0].y = -0.005469799f;

                        points[1].x = 0.0004481673f;
                        points[1].y = 0.2989724f;

                        points[2].x = -0.1464462f;
                        points[2].y = -0.0006049871f;

                        points[3].x = 0.001012444f;
                        points[3].y = -0.2972848f;

                        player.GetComponent<PolygonCollider2D>().points = points;
                    }
                    else if (section == 2)
                    {
                        playerBehaviour.baseHealth = 60;
                        playerBehaviour.baseSpeed = 1.5f;
                        playerBehaviour.baseCollectionArea = 0.35f;
                        playerBehaviour.regenRate = 3;

                        points[0].x = 0.1434617f;
                        points[0].y = -0.005469799f;

                        points[1].x = 0.0004481673f;
                        points[1].y = 0.2989724f;

                        points[2].x = -0.1464462f;
                        points[2].y = -0.0006049871f;

                        points[3].x = 0.001012444f;
                        points[3].y = -0.2972848f;

                        player.GetComponent<PolygonCollider2D>().points = points;
                    }
                    playerBehaviour.pathLevel++;
                    break;
                }

            case "tank":
                {
                    playerSprite.GetComponent<SpriteRenderer>().sprite = tankPath[section-1];
                    var points = player.GetComponent<PolygonCollider2D>().points;
                    UIManager.instance.endgameRequired++;
                    if (section == 1)
                    {
                        playerBehaviour.baseHealth = 125;
                        playerBehaviour.baseSpeed = 0.8f;
                        playerBehaviour.baseCollectionArea = 0.4f;
                        playerBehaviour.regenRate = 4;

                        points[0].x = 0.2711265f;
                        points[0].y = 0.1294613f;

                        points[1].x = -0.001973927f;
                        points[1].y = 0.3295129f;

                        points[2].x = -0.2477951f;
                        points[2].y = 0.1305524f;

                        points[3].x = -0.00231114f;
                        points[3].y = -0.3286927f;

                        player.GetComponent<PolygonCollider2D>().points = points;
                    }
                    else if (section == 2)
                    {
                        playerBehaviour.baseHealth = 150;
                        playerBehaviour.baseSpeed = 0.6f;
                        playerBehaviour.baseCollectionArea = 0.45f;
                        playerBehaviour.regenRate = 3;

                        points[0].x = 0.1194155f;
                        points[0].y = -0.01082348f;

                        points[1].x = -0.002501704f;
                        points[1].y = 0.2427326f;

                        points[2].x = -0.1277401f;
                        points[2].y = -0.01129377f;

                        points[3].x = 0.001426985f;
                        points[3].y = -0.2783592f;

                        player.GetComponent<PolygonCollider2D>().points = points;
                    }
                    playerBehaviour.pathLevel++;
                    break;
                }
        }
        UIManager.instance.evolve = false;
        playerBehaviour.ApplyUpgrades();
    }

    public void SetPath()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        var playerSelection = buttonRef.GetComponent<PlayerPath>().path;

        playerBehaviour.path = playerSelection;
        playerBehaviour.pathLevel++;
        EvoUI.SetActive(false);
        MainUI.SetActive(true);
        Evolve();
    }
}
