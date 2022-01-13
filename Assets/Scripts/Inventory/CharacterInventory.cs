using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory : Inventory
{
    private Stack<Resource> _resourcesToDrop = new Stack<Resource>();
    public Stack<Resource> ResourcesToDrop => _resourcesToDrop;

    public void GetToDropResorources(ResourceType type)
    {
        if (_resourcesToDrop.Count == 0)
        {
            for (int i = 0; i < Resources.Count; i++)
            {
                if (Resources[i].Type == type)
                {
                    _resourcesToDrop.Push(Resources[i]);
                    DropResource(Resources[i]);
                }
            }
        }
    }
}
