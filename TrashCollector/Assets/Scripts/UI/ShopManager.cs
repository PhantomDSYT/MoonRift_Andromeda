using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    public int[,] shopItems = new int[3,4];


    // Start is called before the first frame update
    void Start()
    {
        // ID's
        shopItems[0, 0] = 0; // Max Health
        shopItems[0, 1] = 1; // Health Regeneration
        shopItems[0, 2] = 2; // Speed
        shopItems[0, 3] = 3; // Collection Range


        // Price
        shopItems[1, 0] = 50;
        shopItems[1, 1] = 50;
        shopItems[1, 2] = 50;
        shopItems[1, 3] = 50;

        // Levels
        shopItems[2, 0] = 1;
        shopItems[2, 1] = 1;
        shopItems[2, 2] = 1;
        shopItems[2, 3] = 1;
    }

    // Update is called once per frame
    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;
        var currentItem = buttonRef.GetComponent<PlayerUpgrades>().itemId;

        if (UIManager.instance.trashPoints >= shopItems[1, currentItem])
        {
            UIManager.instance.SubtractTrash(shopItems[1, currentItem]);
            shopItems[2, currentItem]++;

            shopItems[1, currentItem] = (int)(shopItems[1, currentItem] * 1.2f);

            buttonRef.GetComponent<PlayerUpgrades>().SetLevel(shopItems[2, currentItem]);

            buttonRef.GetComponent<PlayerUpgrades>().NewPrice(shopItems[1, currentItem]);

            if (shopItems[2, currentItem] == 8)
                buttonRef.GetComponent<PlayerUpgrades>().MaxLevel();
                
            buttonRef.GetComponent<PlayerUpgrades>().Upgrade();
        }
    }
}
