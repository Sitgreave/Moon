using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Vision _vision;
    [SerializeField] private Carrier _carrier; 
    [SerializeField] private CharacterInventory _inventory;

   
    private void Start()
    {
        //_vision.ResourceDetected += VisionNotify;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.TryGetComponent<Inventory>(out Inventory objInventory))
        {
            switch (objInventory.InventoryType)
            {
                case InventoryType.Fillable:
                    _carrier.TryTransfer(objInventory, _inventory, false);
                    break;
                case InventoryType.Transmitting:
                    _carrier.TryTransfer(_inventory, objInventory, true);
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
