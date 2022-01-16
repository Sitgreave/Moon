using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Carrier _carrier; 
    [SerializeField] private Inventory _inventory;

 

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Inventory>(out Inventory objInventory))
        {
            switch (objInventory.InventoryType)
            {
                case InventoryType.Fillable:
                    _carrier.TryTransfer(objInventory, _inventory);
                    break;
                case InventoryType.Transmitting:
                    _carrier.TryTransfer(_inventory, objInventory);
                    break;
            }
          
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Inventory>(out _))
        {
            _carrier.StopTransfering();
        }
    }



}
