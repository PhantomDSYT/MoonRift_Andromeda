using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrades : MonoBehaviour
{
    [SerializeField]GameObject player;

    public int itemId;
    private bool maxedOut = false;
    [SerializeField] private Text levelText;
    [SerializeField] private Text priceText;
    [SerializeField] private Button BuyButton;
    [SerializeField] private GameObject shopManager;

    public bool evolve = false;



    // Start is called before the first frame update
    void Start()
    {
        levelText.text = "Lv. 1";
        priceText.text = "(50)";

        UIManager.instance.AddTrash(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(maxedOut)
            BuyButton.interactable = false;
    }

    public void SetLevel(int level)
    {
        levelText.text = "Lv. " + level.ToString();
    }

    public void MaxLevel()
    {
        maxedOut = true;
        levelText.text = "Lv. MAX ";
        priceText.text = "(MAX)";
    }

    public void NewPrice(int price)
    {
        priceText.text = "(" + price.ToString() + ")";
    }

    public void Upgrade()
    {
        var health = shopManager.GetComponent<ShopManager>().shopItems[2, 0];
        var regen = shopManager.GetComponent<ShopManager>().shopItems[2, 1];
        var speed = shopManager.GetComponent<ShopManager>().shopItems[2, 2];
        var range = shopManager.GetComponent<ShopManager>().shopItems[2, 3];

        switch(health)
        {
            case 1:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 1.0f;
                    break;
                }

            case 2:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 1.2f;
                    break;
                }

            case 3:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 1.4f;
                    break;
                }

            case 4:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 1.6f;
                    break;
                }
            case 5:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 1.8f;
                    break;
                }
            case 6:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 2.0f;
                    break;
                }
            case 7:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 2.2f;
                    break;
                }
            case 8:
                {
                    player.GetComponent<PlayerBehaviour>().healthMulti = 2.4f;
                    break;
                }
        }

        switch (regen)
        {
            case 1:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 1;
                    break;
                }

            case 2:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 2;
                    break;
                }

            case 3:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 3;
                    break;
                }

            case 4:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 4;
                    break;
                }
            case 5:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 5;
                    break;
                }
            case 6:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 6;
                    break;
                }
            case 7:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 7;
                    break;
                }
            case 8:
                {
                    player.GetComponent<PlayerBehaviour>().regenAmount = 8;
                    break;
                }
        }

        switch (speed)
        {
            case 1:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 1.0f;
                    break;
                }

            case 2:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 1.2f;
                    break;
                }

            case 3:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 1.4f;
                    break;
                }

            case 4:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 1.6f;
                    break;
                }
            case 5:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 1.8f;
                    break;
                }
            case 6:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 2.0f;
                    break;
                }
            case 7:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 2.2f;
                    break;
                }
            case 8:
                {
                    player.GetComponent<PlayerBehaviour>().speedMulti = 2.4f;
                    break;
                }
        }

        switch (range)
        {
            case 1:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.0f;
                    break;
                }

            case 2:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.1f;
                    break;
                }

            case 3:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.2f;
                    break;
                }

            case 4:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.3f;
                    break;
                }
            case 5:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.4f;
                    break;
                }
            case 6:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.6f;
                    break;
                }
            case 7:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 1.8f;
                    break;
                }
            case 8:
                {
                    player.GetComponent<PlayerBehaviour>().collectionAreaMulti = 2.0f;
                    break;
                }
        }

        UIManager.instance.endgameRequired++;
        player.GetComponent<PlayerBehaviour>().ApplyUpgrades();
    }
}
