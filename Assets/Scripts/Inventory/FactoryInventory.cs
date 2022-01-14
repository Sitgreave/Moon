using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryInventory : Inventory
{
    [SerializeField] private ResourceType []requiredTypes;
    
    public bool ThisTypeRequired(ResourceType type)
    {
        for (int i = 0; i < requiredTypes.Length; i++)
        {
            if (requiredTypes[i] == type) return true;
        }
        return false;
    }
}
