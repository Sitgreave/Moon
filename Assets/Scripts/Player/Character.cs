
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Vision _vision;
    [SerializeField] private CharacterInventory _inventory;
   
    
    private void Start()
    {
        _vision.ResourceDetected += VisionNotify;
    }

    private void VisionNotify()
    {
        _inventory.GetNewResource(_vision.NearResources.Dequeue());
    }

 
   
    
}
