using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class inventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static componentManager.basicInventory playerInventory= new componentManager.basicInventory();
    private componentManager.itemBuilder itemRegister = new componentManager.itemBuilder();
    private static GameObject inventoryUI;

    
    void Start()
    {
        itemRegister.registerItems();
        inventoryUI = GameObject.Find("InventoryUI");
        activateInventory();
        disactivateInventory();
        addItemToPlayersInventory(itemRegister.registeredItems["cucumber"]);
        addItemToPlayersInventory(itemRegister.registeredItems["apple"]);

    }

    static void activateInventory()
    {
        Cursor.lockState = CursorLockMode.None;
        inventoryUI.SetActive(true);
        foreach (string toPrint in playerInventory.getInventoryItems())
        {
            Debug.Log(toPrint);
        }
    }

    static void addItemToPlayersInventory(componentManager.item itemToAdd)
    {
        inventoryUI.SetActive(true);
        playerInventory.addItem(itemToAdd);
        inventoryUI.SetActive(false);
    }

    static void disactivateInventory()
    {
        Cursor.lockState = CursorLockMode.Locked;
        inventoryUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("i")){
            
            activateInventory();
        }
        if (inventoryUI.active && Input.GetKey(KeyCode.Escape))
        {
            disactivateInventory();
        }
    }
}
