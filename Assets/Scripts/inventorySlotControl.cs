using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventorySlotControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int slotID=0;
    private componentManager.inventorySlot slot;

    void Awake()
    {
        slot = new componentManager.inventorySlot(slotID, this.gameObject);
        inventoryManager.playerInventory.setSlot(slot);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
